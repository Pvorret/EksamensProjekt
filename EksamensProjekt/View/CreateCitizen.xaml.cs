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

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for CreateCitizen.xaml
    /// </summary>
    public partial class CreateCitizen : Window
    {
        public CreateCitizen()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRelative_Button_Click(object sender, RoutedEventArgs e)
        {
            AddRelative AR = new AddRelative();
            AR.Show();
        }

        private void AddNewIllness_Button_Click(object sender, RoutedEventArgs e)
        {
            AddIllness AI = new AddIllness();
            AI.Show();
        }
    }
}
