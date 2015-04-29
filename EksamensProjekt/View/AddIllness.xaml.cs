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
    /// Interaction logic for AddIllness.xaml
    /// </summary>
    
    public partial class AddIllness : Window
    {
        public string NewIllness { get; set; }

        public AddIllness()
        {
            InitializeComponent();
        }

        private void AddIllness_Button_Click(object sender, RoutedEventArgs e)
        {
            NewIllness = IllnessName_TextBox.Text;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

    }
}
