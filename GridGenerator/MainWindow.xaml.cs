﻿using System;
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
        string gridFile = @"MyGrid.json";
        Dictionary<string, string> loadedGrid;

        public MainWindow()
        {
            InitializeComponent();
            

            if (!File.Exists(gridFile))
            {
                theGrid.Visibility = Visibility.Hidden;

                MessageBoxResult gridMessage = MessageBox.Show("You don't have a GRID set up. Please fill out the grid and save it before generating response.");

                if (gridMessage == MessageBoxResult.OK)
                {
                    theGrid.Visibility = Visibility.Visible;
                }

            } else
            {
                theGrid.Visibility = Visibility.Visible;
                loadedGrid = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(gridFile));
                FillGrid();
            }

                        
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<TextBox> collection = theGrid.Children.OfType<TextBox>();
            List<TextBox> txtb = collection.ToList();

            CreateGrid newGrid = new CreateGrid();

            var gridValues = newGrid.NewGrid(txtb);

            string json = JsonConvert.SerializeObject(gridValues, Formatting.Indented);
                        
            if (!File.Exists(gridFile))
            {
                File.WriteAllText(gridFile, json);

                MessageBox.Show("GRID saved successfully!");


            }
            

            
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void FillGrid()
        {
            IEnumerable<TextBox> collection = theGrid.Children.OfType<TextBox>();
            List<TextBox> txtb = collection.ToList();
            IEnumerable<string> gridValues = loadedGrid.Values;
            List<string> values = gridValues.ToList();

            foreach (TextBox box in txtb)
            {
                box.Text = values.First();
                values.RemoveAt(0);
            }

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult delete = MessageBox.Show("By clearing the GRID you will delete the content entirely. You will need to create a new one next time you run the application.", "Warning", MessageBoxButton.OKCancel);

            if (delete == MessageBoxResult.OK)
            {
                IEnumerable<TextBox> collection = theGrid.Children.OfType<TextBox>();
                List<TextBox> txtb = collection.ToList();

                foreach (TextBox box in txtb)
                {
                    box.Text = "";
                }

                File.Delete(gridFile);
            }
        }

        private void Response_Click(object sender, RoutedEventArgs e)
        {
            GenerateResponse responseWindow = new GenerateResponse();
            responseWindow.ShowDialog();
        }
    }
}
