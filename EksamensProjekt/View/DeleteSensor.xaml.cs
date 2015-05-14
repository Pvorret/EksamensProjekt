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

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for DeleteSensor.xaml
    /// </summary>
    public partial class DeleteSensor : Window
    {
        CitizenController _CitizenController = new CitizenController();
        SensorController _SensorController = new SensorController();
        public DeleteSensor()
        {
            InitializeComponent();
            _SensorController.GetSensor(0);
            foreach (int i in _SensorController.GetSerialNumberList(_SensorController.Sensors))
            {
                Select_ComboBox.Items.Add(i);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Select_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < _SensorController.Sensors.Count; i++)
            {
                if (Convert.ToInt32(Select_ComboBox.SelectedItem) == _SensorController.Sensors[i].SerialNumber)
                {
                    Type_Label.Content = _SensorController.Sensors[i].Type;

                    if (_SensorController.Sensors[i].Activated == false)
                    {
                        Activ_Label.Content = "Ikke i brug";
                    }
                    if (_SensorController.Sensors[i].Activated == true)
                    {
                        Activ_Label.Content = "I brug";
                    }
                }
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _SensorController.Sensors.Count; i++)
            {
                if (Convert.ToInt32(Select_ComboBox.SelectedItem) == _SensorController.Sensors[i].SerialNumber)
                {
                    if (_SensorController.DeleteSensor(_SensorController.Sensors[i].SerialNumber) == true)
                    {
                        Select_ComboBox.Items.Remove(Select_ComboBox.SelectedItem);
                    }
                }
            }
            Type_Label.Content = "";
            Activ_Label.Content = "";
        }
    }
}
