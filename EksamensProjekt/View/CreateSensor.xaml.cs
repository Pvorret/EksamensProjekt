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
    /// Interaction logic for CreateSensor.xaml
    /// </summary>
    public partial class CreateSensor : Window
    {
        public CreateSensor()
        {
            InitializeComponent();
            Type_ComboBox.Items.Add("Dør");
            Type_ComboBox.Items.Add("Fald");
            Type_ComboBox.Items.Add("Epilepsi");
            Type_ComboBox.Items.Add("Røg");
            Type_ComboBox.Items.Add("GPS");
            Type_ComboBox.Items.Add("Seng");
        }
        private void Type_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Close();
        }

        private void CreateSensor_Button_Click(object sender, RoutedEventArgs e)
        {
            SensorController con = new SensorController();
            con.CreateSensor(Convert.ToInt32(SerialNumber_TextBox), Type_ComboBox.SelectedItem.ToString());
        }
    }
}
