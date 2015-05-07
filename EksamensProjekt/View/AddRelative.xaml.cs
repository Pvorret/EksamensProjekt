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
        List<string> Days = new List<string>();
        List<DateTime> StartTime = new List<DateTime>();
        List<DateTime> EndTime = new List<DateTime>();
        string relativeCprNr;
        string timeNotAvailable;

        public AddRelative(CitizenController _controller)
        {
            InitializeComponent();
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
            _controller = new CitizenController();
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

        private void AddNotAvailableTime_Button_Click(object sender, RoutedEventArgs e)
        {
            timeNotAvailable = NotAvailableStartHour_TextBox.Text + ":" + NotAvailableStartMinute_TextBox + " - " + NotAvailableEndHour_TextBox.Text + ":" + NotAvailableEndMinute_TextBox.Text;
            NotAvailableTimes_ListBox.Items.Add(NotAvailableDays_Dropdown.SelectedItem.ToString() + ": " + timeNotAvailable);
            Days.Add(NotAvailableDays_Dropdown.SelectedItem.ToString());
            StartTime.Add(Convert.ToDateTime(NotAvailableStartHour_TextBox.Text + ":" + NotAvailableStartMinute_TextBox.Text));
            EndTime.Add(Convert.ToDateTime(NotAvailableEndHour_TextBox.Text + ":" + NotAvailableEndMinute_TextBox.Text));
            NotAvailableStartHour_TextBox.Clear();
            NotAvailableStartMinute_TextBox.Clear();
            NotAvailableEndHour_TextBox.Clear();
            NotAvailableEndMinute_TextBox.Clear();
        }
        private void AddRelative_Button_Click(object sender, RoutedEventArgs e)
        {
            relativeCprNr = RelativeBirthdate_TextBox.Text + "-" + RelativeLast4CPR_TextBox.Text;

            _controller.AddRelative(RelativeName_TextBox.Text, relativeCprNr, RelativeGender_Dropdown.SelectedItem.ToString(), RelativeAge_TextBox.Text, RelativeAddress_TextBox.Text, RelativeCity_TextBox.Text, RelativePhoneNumber_TextBox.Text, RelativeEmail_TextBox.Text, Days, StartTime, EndTime);
            this.Close();
        }
    }
}
