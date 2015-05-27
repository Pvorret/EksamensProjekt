using System;
using System.Collections.Generic;
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
using EksamensProjekt.Controller;
using System.Text.RegularExpressions;

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for CreateSensor.xaml
    /// </summary>
    public partial class CreateSensor : Window
    {
        CitizenController _controller;
        SensorController con;
        public CreateSensor()
        {
            InitializeComponent();
            _controller = new CitizenController();
            con = new SensorController();
            _controller.GetAllSensorType();
            foreach (string type in _controller.SensorTypes)
            {
                Type_ComboBox.Items.Add(type);
            }
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CreateSensor_Button_Click(object sender, RoutedEventArgs e)
        {
            con.CreateSensor(Convert.ToInt32(SerialNumber_TextBox.Text), Type_ComboBox.SelectedItem.ToString());
            SerialNumber_TextBox.Clear();
            MessageBox.Show("Sensor er oprettet i databasen");
        }
        private void Type_ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e) {

        }
        private void SerialNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        

    }
}
