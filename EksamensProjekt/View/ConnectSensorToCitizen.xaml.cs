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
            RuleSet_DropDown.Items.Add("Sensor Regel");
            RuleSet_DropDown.Items.Add("Tidsrums Regel");
            Day_Dropdown.Items.Add("Mandag");
            Day_Dropdown.Items.Add("Tirsdag");
            Day_Dropdown.Items.Add("Onsdag");
            Day_Dropdown.Items.Add("Torsdag");
            Day_Dropdown.Items.Add("Fredag");
            Day_Dropdown.Items.Add("Lørdag");
            Day_Dropdown.Items.Add("Søndag");
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
                    _CitizenController.GetSensorsFromCitizen(_CitizenController.Citizens[i].CprNr);
                    for (int j = 0; j < _CitizenController.Citizens[i].Sensors.Count; j++)
                    {
                        SensorDependency_Dropdown.Items.Add(_CitizenController.Citizens[i].Sensors[j].SerialNumber);
                    }
                    for (int k = 0; k < _CitizenController.Citizens[i].Relatives.Count; k++)
                    {
                        Relative_Dropdown.Items.Add(_CitizenController.Citizens[i].Relatives[k].Name);
                    }
                }
            }
        }
        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Sensor_Dropdown.SelectedIndex != -1 && SensorLocation_TextBox.Text != "" && Citizen_DropDown.SelectedIndex != -1)
                {
                    _SensorController.ConnectSensorToCitizen(int.Parse(Sensor_Dropdown.SelectedItem.ToString()), CPRNR_TextBox.Text, SensorLocation_TextBox.Text);
                    if (RuleSet_DropDown.SelectedItem.ToString() == "Sensor Regel")
                    {
                        bool waitorLook;
                        int timeToWait;
                        int timeToLook;
                        if (Wait_RadioButton.IsChecked == true)
                        {
                            waitorLook = true;
                            timeToWait = Convert.ToInt32(WaitOrLookLength_TextBox.Text);
                            timeToLook = 0;
                        }
                        else
                        {
                            waitorLook = false;
                            timeToLook = Convert.ToInt32(WaitOrLookLength_TextBox.Text);
                            timeToWait = 0;
                        }
                        _RuleSetController.AddSensorRuleManagement("SR", Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()));
                        _RuleSetController.AddSensorRuleFromSerialNumber(Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()), Convert.ToInt32(SensorDependency_Dropdown.SelectedItem.ToString()), waitorLook, timeToWait, timeToLook);
                    }
                    else if (RuleSet_DropDown.SelectedItem.ToString() == "Tidsrum Regel" && AddActingRule_CheckBox.IsChecked != true)
                    {
                        bool contactHelper;
                        string relativeCprNr = "" ;
                        if(ContactHelper_CheckBox.IsChecked == true){
                            contactHelper = true;
                        }
                        else
                        {
                            contactHelper = false;
                        }
                        for (int i = 0; i < _CitizenController.Citizens.Count; i++)
                        {
                            for (int j = 0; j < _CitizenController.Citizens[i].Relatives.Count; j++)
                            {
                                if (_CitizenController.Citizens[i].Relatives[j].Name == Relative_Dropdown.SelectedItem.ToString())
                                {
                                    relativeCprNr = _CitizenController.Citizens[i].Relatives[j].CprNr;
                                }
                            }
                        }
                        _RuleSetController.AddSensorRuleManagement("TRR", Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()));
                        _RuleSetController.AddTimeRangeRuleFromSerialNumber(Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()), Day_Dropdown.SelectedItem.ToString(), Convert.ToDateTime(StartHour_TextBox.Text + ":" + StartMinute_TextBox.Text), Convert.ToDateTime(EndHour_TextBox.Text + ":" + EndMinute_TextBox.Text), relativeCprNr , "", contactHelper); //Ikke done.
                    }
                    else if (RuleSet_DropDown.SelectedItem.ToString() == "Tidsrum Regel" && AddActingRule_CheckBox.IsChecked == true)
                    {
                        bool contactHelper;
                        string relativeCprNr = "";
                        bool waitorLook;
                        int timeToWait;
                        int timeToLook;
                        if (Wait_RadioButton.IsChecked == true)
                        {
                            waitorLook = true;
                            timeToWait = Convert.ToInt32(WaitOrLookLength_TextBox.Text);
                            timeToLook = 0;
                        }
                        else
                        {
                            waitorLook = false;
                            timeToLook = Convert.ToInt32(WaitOrLookLength_TextBox.Text);
                            timeToWait = 0;
                        }
                        if (ContactHelper_CheckBox.IsChecked == true)
                        {
                            contactHelper = true;
                        }
                        else
                        {
                            contactHelper = false;
                        }
                        for (int i = 0; i < _CitizenController.Citizens.Count; i++)
                        {
                            for (int j = 0; j < _CitizenController.Citizens[i].Relatives.Count; j++)
                            {
                                if (_CitizenController.Citizens[i].Relatives[j].Name == Relative_Dropdown.SelectedItem.ToString())
                                {
                                    relativeCprNr = _CitizenController.Citizens[i].Relatives[j].CprNr;
                                }
                            }
                        }
                        _RuleSetController.AddSensorRuleManagement("TRR", Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()));
                        _RuleSetController.AddSensorRuleFromSerialNumber(Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()), Convert.ToInt32(SensorDependency_Dropdown.SelectedItem.ToString()), waitorLook, timeToWait, timeToLook);
                        _RuleSetController.AddTimeRangeRuleFromSerialNumber(Convert.ToInt32(Sensor_Dropdown.SelectedItem.ToString()), Day_Dropdown.SelectedItem.ToString(), Convert.ToDateTime(StartHour_TextBox.Text + ":" + StartMinute_TextBox.Text), Convert.ToDateTime(EndHour_TextBox.Text + ":" + EndMinute_TextBox.Text), relativeCprNr, "SR " + _RuleSetController.SensorRuleId, contactHelper); //Ikke done.    
                    }
                    MessageBox.Show("Citizen og Sensor er forbundet!");
                    
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde nogle felter");
                }
            }
            catch (Exception d)
            {
                throw new Exception("Error! Du mangler at indputte nogle informationer" + d.Message);
            }
        }
        private void RuleSet_DropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RuleSet_DropDown.SelectedItem.ToString() == "Sensor Regel")
            {
                Day_Dropdown.IsEnabled = false;
                StartHour_TextBox.IsEnabled = false;
                StartMinute_TextBox.IsEnabled = false;
                EndHour_TextBox.IsEnabled = false;
                EndMinute_TextBox.IsEnabled = false;
                Relative_Dropdown.IsEnabled = false;
                ContactHelper_CheckBox.IsEnabled = false;
                AddActingRule_CheckBox.IsEnabled = false;
                
                Wait_RadioButton.IsEnabled = true;
                Look_RadioButton.IsEnabled = true;
                SensorDependency_Dropdown.IsEnabled = true;
                WaitOrLookLength_TextBox.IsEnabled = true;
                Yes_RadioButton.IsEnabled = true;
                No_RadioButton.IsEnabled = true;
            }
            if (RuleSet_DropDown.SelectedItem.ToString() == "Tidsrums Regel")
            {
                Wait_RadioButton.IsEnabled = false;
                Look_RadioButton.IsEnabled = false;
                SensorDependency_Dropdown.IsEnabled = false;
                WaitOrLookLength_TextBox.IsEnabled = false;
                Yes_RadioButton.IsEnabled = false;
                No_RadioButton.IsEnabled = false;

                Day_Dropdown.IsEnabled = true;
                StartHour_TextBox.IsEnabled = true;
                StartMinute_TextBox.IsEnabled = true;
                EndHour_TextBox.IsEnabled = true;
                EndMinute_TextBox.IsEnabled = true;
                Relative_Dropdown.IsEnabled = true;
                ContactHelper_CheckBox.IsEnabled = true;
                AddActingRule_CheckBox.IsEnabled = true;
            }
        }
        private void AddActingRule_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Wait_RadioButton.IsEnabled = true;
            Look_RadioButton.IsEnabled = true;
            SensorDependency_Dropdown.IsEnabled = true;
            WaitOrLookLength_TextBox.IsEnabled = true;
            Yes_RadioButton.IsEnabled = true;
            No_RadioButton.IsEnabled = true;
        }
    }
}
