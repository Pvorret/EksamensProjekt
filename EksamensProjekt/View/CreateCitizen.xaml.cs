﻿using System;
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
    /// Interaction logic for CreateCitizen.xaml
    /// </summary>
    public partial class CreateCitizen : Window
    {
        public CitizenController _controller;
        AddReligion AR;
        Dictionary<string, string> notAvailableDic = new Dictionary<string, string>();
        string timeNotAvailable;
        public CreateCitizen()
        {
            _controller = new CitizenController();
            InitializeComponent();
            CitizenGender_Dropdown.Items.Add("Mand");
            CitizenGender_Dropdown.Items.Add("Kvinde");

            CitizenReligion_Dropdown.Items.Add("Kristendom");
            CitizenReligion_Dropdown.Items.Add("Jødedom");
            CitizenReligion_Dropdown.Items.Add("Islam");
            CitizenReligion_Dropdown.Items.Add("Buddhisme");
            CitizenReligion_Dropdown.Items.Add("Hinduisme");
            CitizenReligion_Dropdown.Items.Add("Sikhisme");
            CitizenReligion_Dropdown.Items.Add("Tilføj Anden");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddRelative_Button_Click(object sender, RoutedEventArgs e)
        {
            AddRelative AR = new AddRelative(_controller);
            AR.ShowDialog();
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
            timeNotAvailable = HomeCareStart_TextBox.Text + " - " + HomeCareEnd_TextBox.Text;
            HomeCareTimes_ListBox.Items.Add(HomeCareDays_Dropdown.SelectedItem.ToString() + ": " + timeNotAvailable);
            notAvailableDic.Add(HomeCareDays_Dropdown.SelectedItem.ToString(), timeNotAvailable);

            HomeCareStart_TextBox.Clear();
            HomeCareEnd_TextBox.Clear();
        }

        private void CitizenSensorNeeds_Dropdown_DropDownOpened(object sender, EventArgs e) {
            if (CitizenSensorNeeds_Dropdown.Items.Count == 0) {
                foreach (string s in _controller.GetAllSensorType()) {
                    CitizenSensorNeeds_Dropdown.Items.Add(s);
                }
            }
        }

        private void Illness_Dropdown_DropDownOpened(object sender, EventArgs e) {
            if (Illness_Dropdown.Items.Count == 0) {
                foreach (string s in _controller.GetAllIllness()) {
                    Illness_Dropdown.Items.Add();
                }
            }
        }

        private void Illness_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            CitizenIllness_ListBox.Items.Add(Illness_Dropdown.SelectedItem);
        }
        
        
      
    }
}
