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
        CitizenController _controller = new CitizenController();
        SensorController con = new SensorController();
        public DeleteSensor()
        {
            InitializeComponent();
            con.GetSensor(0);
            foreach (int i in con.GetSerialNumberList(con.Sensors))
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
            for (int i = 0; i < con.Sensors.Count; i++)
            {
                if (Convert.ToInt32(Select_ComboBox.SelectedItem) == con.Sensors[i].SerialNumber)
                {
                    Type_Label.Content = con.Sensors[i].Type;

                    if (con.Sensors[i].Activated == false)
                    {
                        Activ_Label.Content = "Ikke i brug";
                    }
                    if (con.Sensors[i].Activated == true)
                    {
                        Activ_Label.Content = "I brug";
                    }
                }
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < con.Sensors.Count; i++)
            {
                if (Convert.ToInt32(Select_ComboBox.SelectedItem) == con.Sensors[i].SerialNumber)
                {
                    if (con.DeleteSensor(con.Sensors[i].SerialNumber) == true)
                    {
                        Select_ComboBox.Items.Remove(Select_ComboBox.SelectedItem);
                    }
                }
            }
            
        }
    }
}
