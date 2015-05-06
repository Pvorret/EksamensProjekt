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
using System.Globalization;
using EksamensProjekt.Controller;

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for ActivateSensor.xaml
    /// </summary>
    public partial class ActivateSensor : Window
    {
        CitizenController _controller;

        public ActivateSensor()
        {
            _controller = new CitizenController();
            InitializeComponent();
            _controller.GetAllCitizen();
            for (int i = 0; i < _controller.Citizens.Count; i++)
            {
                Citizen_Dropdown.Items.Add(_controller.Citizens[i].Name);
            }
            
        }
        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TimeRightNow_Button_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo c = new CultureInfo("da-DK"); //Laver et CulturInfo object c hvor vi siger at dens cultur skal være dansk
            
            char[] a = c.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).ToCharArray(); //Her bruger vi object c til at convert ugens dag til dansk sprog
            a[0] = char.ToUpper(a[0]);
            Date_TextBox.Text = new string(a);
            Time_TextBox.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }

        private void Citizen_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < _controller.Citizens.Count; i++)
            {
                if (_controller.Citizens[i].Name == Citizen_Dropdown.SelectedItem.ToString())
                {
                    _controller.GetSensorTypeFromCitizen(_controller.Citizens[i].CprNr);
                    for (int j = 0; j < _controller.SensorTypes.Count; j++)
                    {
                        Sensor_DropDown.Items.Add(_controller.SensorTypes[j]);
                    }
                }
            }
            
        }
    }
}
