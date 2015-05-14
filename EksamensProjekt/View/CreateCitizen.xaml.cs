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
using System.Text.RegularExpressions;

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for CreateCitizen.xaml
    /// </summary>
    public partial class CreateCitizen : Window
    {
        public CitizenController _controller;
        AddReligion AR;
        List<string> Days = new List<string>();
        List<DateTime> StartTime = new List<DateTime>();
        List<DateTime> EndTime = new List<DateTime>();
        List<string> IllnessList = new List<string>();
        Dictionary<string, int> SensorTypesList = new Dictionary<string, int>();
        string timeHomeCare;
        string CitizenCPRNR;
        public CreateCitizen()
        {
            _controller = new CitizenController();
            InitializeComponent();
            _controller.GetAllIllness();
            _controller.GetAllSensorType();
            CitizenGender_Dropdown.Items.Add("Mand");
            CitizenGender_Dropdown.Items.Add("Kvinde");

            CitizenReligion_Dropdown.Items.Add("Kristendom");
            CitizenReligion_Dropdown.Items.Add("Jødedom");
            CitizenReligion_Dropdown.Items.Add("Islam");
            CitizenReligion_Dropdown.Items.Add("Buddhisme");
            CitizenReligion_Dropdown.Items.Add("Hinduisme");
            CitizenReligion_Dropdown.Items.Add("Sikhisme");
            CitizenReligion_Dropdown.Items.Add("Tilføj Anden");

            HomeCareDays_Dropdown.Items.Add("Mandag");
            HomeCareDays_Dropdown.Items.Add("Tirsdag");
            HomeCareDays_Dropdown.Items.Add("Onsdag");
            HomeCareDays_Dropdown.Items.Add("Torsdag");
            HomeCareDays_Dropdown.Items.Add("Fredag");
            HomeCareDays_Dropdown.Items.Add("Lørdag");
            HomeCareDays_Dropdown.Items.Add("Søndag");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddRelative_Button_Click(object sender, RoutedEventArgs e)
        {
            AddRelative AR = new AddRelative(_controller);
            AR.ShowDialog();
            //_controller = AR._controller;
            for (int i = 0; i < _controller.Relatives.Count; i++)
			{
                CitizenRelatives_ListBox.Items.Add(_controller.Relatives[i].Name);			 
			}
        }
        private void AddNewIllness_Button_Click(object sender, RoutedEventArgs e)
        {
            AddIllness AI = new AddIllness();
            AI.ShowDialog();
            CitizenIllness_ListBox.Items.Add(AI.NewIllness);
            IllnessList.Add(AI.NewIllness);
        }

        private void CitizenReligion_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CitizenReligion_Dropdown.SelectedItem.ToString() == "Tilføj Anden")
            {
                AR = new AddReligion();
                AR.ShowDialog();
                CitizenReligion_Dropdown.Items.Insert(CitizenReligion_Dropdown.Items.Count -1, AR.NewReligion);
                CitizenReligion_Dropdown.SelectedItem = AR.NewReligion;               
            }

        }

        private void AddHomeCareTime_Button_Click(object sender, RoutedEventArgs e)
        {
            timeHomeCare = HomeCareStartHour_TextBox.Text + ":" + HomeCareStartMinute_TextBox.Text + " - "  + HomeCareEndHour_TextBox.Text + ":" + HomeCareEndMinute_TextBox.Text;
            try
            {
                HomeCareTimes_ListBox.Items.Add(HomeCareDays_Dropdown.SelectedItem.ToString() + ": " + timeHomeCare);
                Days.Add(HomeCareDays_Dropdown.SelectedItem.ToString());
                StartTime.Add(Convert.ToDateTime(HomeCareStartHour_TextBox.Text + ":" + HomeCareStartMinute_TextBox.Text));
                EndTime.Add(Convert.ToDateTime(HomeCareEndHour_TextBox.Text + ":" + HomeCareEndMinute_TextBox.Text));
            }
            catch (Exception i)
            {
                MessageBox.Show("Man kan kun have et tidsrum per dag" + i.Message);
            }
            HomeCareStartHour_TextBox.Clear();
            HomeCareStartMinute_TextBox.Clear();
            HomeCareEndHour_TextBox.Clear();
            HomeCareEndMinute_TextBox.Clear();
        }
        private void CitizenSensorNeeds_Dropdown_DropDownOpened(object sender, EventArgs e) {
            if (CitizenSensorNeeds_Dropdown.Items.Count == 0) {
                foreach (string s in _controller.SensorTypes) {
                    CitizenSensorNeeds_Dropdown.Items.Add(s);
                }
            }
        }
        private void Illness_Dropdown_DropDownOpened(object sender, EventArgs e) {
            if (Illness_Dropdown.Items.Count == 0) {
                foreach (string s in _controller.IllnessList) {
                    Illness_Dropdown.Items.Add(s);
                }
            }
        }
        private void Illness_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            CitizenIllness_ListBox.Items.Add(Illness_Dropdown.SelectedItem.ToString());
            IllnessList.Add(Illness_Dropdown.SelectedItem.ToString());
        }
        private void AddSensorNeeds_Button_Click(object sender, RoutedEventArgs e)
        {
            CitizenSensorNeeds_ListBox.Items.Add(CitizenSensorNeeds_Dropdown.SelectedItem.ToString() + ": " + CitizenSensorNeedsAmount.Text);
            SensorTypesList.Add(CitizenSensorNeeds_Dropdown.SelectedItem.ToString(), int.Parse(CitizenSensorNeedsAmount.Text));
        }
        private void CreateCitizen_Button_Click(object sender, RoutedEventArgs e) {
            CitizenCPRNR = CitizenBirthdate_TextBox.Text + "-" + CitizenLast4CPR_TextBox.Text;
            _controller.CreateCitizen(CitizenName_TextBox.Text, CitizenCPRNR, CitizenGender_Dropdown.SelectedItem.ToString(), CitizenAge_TextBox.Text, CitizenAddress_TextBox.Text, CitizenCity_TextBox.Text, CitizenPhoneNumber_TextBox.Text, IllnessList, CitizenReligion_Dropdown.SelectedItem.ToString(), SensorTypesList, Days, StartTime, EndTime);
            this.Close();
        }

        private void CitizenBirthdate_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CitizenLast4CPR_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CitizenName_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zæøåA-ZÆØÅ]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CitizenAge_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CitizenPhoneNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HomeCareStartHour_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HomeCareStartMinute_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HomeCareEndHour_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HomeCareEndMinute_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
