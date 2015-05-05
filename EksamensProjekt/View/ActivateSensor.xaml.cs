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

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for ActivateSensor.xaml
    /// </summary>
    public partial class ActivateSensor : Window
    {
        
        public ActivateSensor()
        {
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TimeRightNow_Button_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo c = new CultureInfo("da-DK"); //Laver et CulturInfo object c hvor vi siger at dens cultur skal være dansk
            Date_TextBox.Text = c.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek); //Her bruger vi object c til at convert ugens dag til dansk sprog
            Time_TextBox.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }
    }
}
