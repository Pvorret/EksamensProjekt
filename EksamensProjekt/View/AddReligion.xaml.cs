using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddReligion.xaml
    /// </summary>
    public partial class AddReligion : Window
    {
        public string NewReligion { get; set; }

        public AddReligion()
        {
            InitializeComponent();
        }

        private void AddReligion_Button_Click(object sender, RoutedEventArgs e)
        {
            NewReligion = ReligionName_TextBox.Text;
            this.Close();

        }

        private void ReligionName_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zæøåA-ZÆØÅ]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
