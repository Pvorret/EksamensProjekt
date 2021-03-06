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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EksamensProjekt.View;

namespace EksamensProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateSensor_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateSensor CS = new CreateSensor();
            CS.Show();
        }

        private void CreateCitizen_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCitizen CC = new CreateCitizen();
            CC.Show();
        }

        private void DeleteSensor_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteSensor DS = new DeleteSensor();
            DS.Show();
        }

        private void ActiveSensor_Button_Click(object sender, RoutedEventArgs e)
        {
            ActivateSensor AS = new ActivateSensor();
            AS.Show();
        }

        private void ConnectSensorAndCitizen_Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectSensorToCitizen CS = new ConnectSensorToCitizen();
            CS.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSensorRuleOrTimeRangeRule ASR = new AddSensorRuleOrTimeRangeRule();
            ASR.Show();
        }

    }
}
