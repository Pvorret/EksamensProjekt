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
            
            char[] a = c.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).ToCharArray(); //Her bruger vi object c til at convert ugens dage til dansk
            a[0] = char.ToUpper(a[0]);
            Date_TextBox.Text = DateTime.Now.Date.ToString();
            Hour_TextBox.Text = DateTime.Now.Hour.ToString();
            Minute_TextBox.Text = DateTime.Now.Minute.ToString();
        }

        private void Citizen_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sensor_DropDown.Items.Clear();
            for (int i = 0; i < _controller.Citizens.Count; i++)
            {
                if (_controller.Citizens[i].Name == Citizen_Dropdown.SelectedItem.ToString())
                {
                    _controller.GetSensorsFromCitizen(_controller.Citizens[i].CprNr);
                    {
                        for (int k = 0; k < _controller.Citizens[i].Sensors.Count; k++)
                        {
                                Sensor_DropDown.Items.Add(_controller.Citizens[i].Sensors[k].SerialNumber);
                        }
                    }
                }
            }
            
        }
        public void Date_TextBox_GotFocus(object sender, RoutedEventArgs e) 
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Date_TextBox_GotFocus;
        }
        public void Hour_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Hour_TextBox_GotFocus;
        }
        public void Minute_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Minute_TextBox_GotFocus;
        }
        private void Activate_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime activationTime = new DateTime();
            activationTime = Convert.ToDateTime(Date_TextBox.Text);
            TimeSpan actTime = new TimeSpan(Convert.ToInt32(Hour_TextBox.Text), Convert.ToInt32(Minute_TextBox.Text), DateTime.Now.Second);
            activationTime = activationTime + actTime;
            RuleSetController RSC = new RuleSetController();
            RSC.HandelSensorInput(Convert.ToInt32(Sensor_DropDown.SelectedItem), activationTime);
        }

    }
}
