using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GridGenerator
{
    /// <summary>
    /// Interaction logic for GenerateResponse.xaml
    /// </summary>
    public partial class GenerateResponse : Window
    {
        string gridFile = @"MyGrid.json";

        public GenerateResponse()
        {
            InitializeComponent();
            
        }

        private void GetResponse_Click(object sender, RoutedEventArgs e)
        {
            string challenge = "";

            if (txtChallenge.Text.Equals(""))
            {
                MessageBox.Show("Challenge cannot be empty !");
            } else if (txtChallenge.Text.Length < 6)
            {
                MessageBox.Show("Challenge too short!");
            } else if (txtChallenge.Text.Contains(" "))
            {
                MessageBox.Show("Please remove spaces from the challenge!");
            } else
            {
                challenge = txtChallenge.Text;
                var partSize = 2;
                var partChallenge = Enumerable.Range(0, (challenge.Length + partSize - 1) / partSize)
                    .Select(i => challenge.Substring(i * partSize, Math.Min(challenge.Length - i * partSize, partSize))).ToList();

                var responseValues = CompareKeys(partChallenge);

                try
                {
                    var response = responseValues[0] + responseValues[1] + responseValues[2];
                    txtResponse.Text = response;
                }
                catch (Exception)
                {
                    MessageBox.Show("Challenge invalid?? ");
                }

                

                
                
            }

        
        }

        private List<string> CompareKeys(List<string> challengePart)
        {
            var index = 0;
            var challengeKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(gridFile));
            var keys = challengeKeys.Values.ToList();
            List<string> values = new List<string>();

            //foreach (string val in keys)
            //{

            //}

            foreach (KeyValuePair<string, string> key in challengeKeys)
            {

                foreach (string val in challengePart)
                {
                    if (key.Key == val)
                    {
                        values.Add(key.Value);

                    }
                }
                
                


            }


            return values;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
