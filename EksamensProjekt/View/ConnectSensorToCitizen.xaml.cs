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
using System.Text.RegularExpressions;
using EksamensProjekt.View;

namespace EksamensProjekt.View
{
    /// <summary>
    /// Interaction logic for ConnectSensorToCitizen.xaml
    /// </summary>
    public partial class ConnectSensorToCitizen : Window
    {
        SensorController _SensorController;
        CitizenController _CitizenController;
        RuleSetController _RuleSetController;
        public ConnectSensorToCitizen()
        {
            _SensorController = new SensorController();
            _CitizenController = new CitizenController();
            _RuleSetController = new RuleSetController();
            InitializeComponent();
            _SensorController.GetAllSensors();
            _CitizenController.GetAllCitizen();
            for (int i = 0; i < _SensorController.Sensors.Count; i++)
            {
                if (_SensorController.Sensors[i].Activated == false)
                {
                    Sensor_Dropdown.Items.Add(_SensorController.Sensors[i].SerialNumber);
                }
            }
            for (int i = 0; i < _CitizenController.Citizens.Count; i++)
            {
                Citizen_DropDown.Items.Add(_CitizenController.Citizens[i].Name);
            }
        }
        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Sensor_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < _SensorController.Sensors.Count; i++)
            {
                if (_SensorController.Sensors[i].SerialNumber == int.Parse(Sensor_Dropdown.SelectedItem.ToString()))
                {
                    SensorType_TextBox.Text = _SensorController.Sensors[i].Type;
                }
            }
        }
        private void Citizen_DropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            for (int i = 0; i < _CitizenController.Citizens.Count; i++)
            {
                if (_CitizenController.Citizens[i].Name == Citizen_DropDown.SelectedItem.ToString())
                {
                    CPRNR_TextBox.Text = _CitizenController.Citizens[i].CprNr;
                }
            }
        }
        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sensor_Dropdown.SelectedIndex != -1 && SensorLocation_TextBox.Text != "" && Citizen_DropDown.SelectedIndex != -1) 
                {
                    _SensorController.ConnectSensorToCitizen(int.Parse(Sensor_Dropdown.SelectedItem.ToString()), CPRNR_TextBox.Text, SensorLocation_TextBox.Text);
                    MessageBox.Show("Citizen og Sensor er forbundet!");
                    Close();  
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde nogle felter");
                }
            }
            catch (Exception d)
            {
                throw new Exception("Error! Du mangler at indputte nogle informationer " + d.Message);
            }
        }
        private void SensorLocation_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zæøåA-ZÆØÅ]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
