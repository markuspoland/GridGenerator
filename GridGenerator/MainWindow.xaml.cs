using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace GridGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string gridFile = "MyGrid.json";

        public MainWindow()
        {
            InitializeComponent();
            

            if (!File.Exists(gridFile))
            {
                theGrid.Visibility = Visibility.Hidden;

                MessageBoxResult gridMessage = MessageBox.Show("You don't have a GRID set up. Please fill out the grid and save it before generating your password chain.");

                if (gridMessage == MessageBoxResult.OK)
                {
                    theGrid.Visibility = Visibility.Visible;
                }

            } else
            {
                theGrid.Visibility = Visibility.Visible;
            }

                        
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<TextBox> collection = theGrid.Children.OfType<TextBox>();
            List<TextBox> txtb = collection.ToList();

            CreateGrid newGrid = new CreateGrid();

            var gridValues = newGrid.NewGrid(txtb);

            string jsonGrid = JsonConvert.SerializeObject(gridValues, Formatting.Indented);

            if (!File.Exists(gridFile))
            {
                using (StreamWriter file = File.CreateText(@"MyGrid.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, gridValues);
                }
            }
            

            
            
        }

        
    }
}
