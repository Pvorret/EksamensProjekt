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
    /// Interaction logic for AddRelative.xaml
    /// </summary>
    public partial class AddRelative : Window
    {
        public CitizenController _controller { get; set; }
        Dictionary<string, string> notAvailableDic = new Dictionary<string, string>();
        string relativeCprNr;
        string timeNotAvailable;

        public AddRelative(CitizenController _controller)
        {
            InitializeComponent();
            _controller = new CitizenController();
            this._controller = _controller;
            RelativeGender_Dropdown.Items.Add("Mand");
            RelativeGender_Dropdown.Items.Add("Kvinde");

            NotAvailableDays_Dropdown.Items.Add("Mandag");
            NotAvailableDays_Dropdown.Items.Add("Tirsdag");
            NotAvailableDays_Dropdown.Items.Add("Onsdag");
            NotAvailableDays_Dropdown.Items.Add("Torsdag");
            NotAvailableDays_Dropdown.Items.Add("Fredag");
            NotAvailableDays_Dropdown.Items.Add("Lørdag");
            NotAvailableDays_Dropdown.Items.Add("Søndag");
        }
        public AddRelative()
        {
            InitializeComponent();
            RelativeGender_Dropdown.Items.Add("Mand");
            RelativeGender_Dropdown.Items.Add("Kvinde");

            NotAvailableDays_Dropdown.Items.Add("Mandag");
            NotAvailableDays_Dropdown.Items.Add("Tirsdag");
            NotAvailableDays_Dropdown.Items.Add("Onsdag");
            NotAvailableDays_Dropdown.Items.Add("Torsdag");
            NotAvailableDays_Dropdown.Items.Add("Fredag");
            NotAvailableDays_Dropdown.Items.Add("Lørdag");
            NotAvailableDays_Dropdown.Items.Add("Søndag");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRelative_Button_Click(object sender, RoutedEventArgs e)
        {
            relativeCprNr = RelativeBirthdate_TextBox.Text + "-" + RelativeLast4CPR_TextBox.Text;

            _controller.AddRelative(RelativeName_TextBox.Text, relativeCprNr, RelativeGender_Dropdown.SelectedItem.ToString(), RelativeAge_TextBox.Text, RelativeAddress_TextBox.Text, RelativeCity_TextBox.Text, RelativePhoneNumber_TextBox.Text, RelativeEmail_TextBox.Text, notAvailableDic);
        }

        private void AddNotAvailableTime_Button_Click(object sender, RoutedEventArgs e)
        {
            timeNotAvailable = NotAvailableStart_TextBox.Text + " - " + NotAvailableEnd_TextBox.Text;
            NotAvailableTimes_ListBox.Items.Add(NotAvailableDays_Dropdown.SelectedItem.ToString() + ": " + timeNotAvailable);
            notAvailableDic.Add(NotAvailableDays_Dropdown.SelectedItem.ToString(), timeNotAvailable);

            NotAvailableStart_TextBox.Clear();
            NotAvailableEnd_TextBox.Clear();
        }
    }
}
