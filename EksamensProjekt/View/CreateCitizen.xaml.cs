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
    /// Interaction logic for CreateCitizen.xaml
    /// </summary>
    public partial class CreateCitizen : Window
    {
        public CitizenController _controller;
        AddReligion AR;
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
            

        }
        private void AddNewIllness_Button_Click(object sender, RoutedEventArgs e)
        {
            AddIllness AI = new AddIllness();
            AI.Show();
        }

        private void CitizenReligion_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CitizenReligion_Dropdown.SelectedItem.ToString() == "Tilføj Anden")
            {
                AR = new AddReligion();
                AR.ShowDialog();
                CitizenReligion_Dropdown.Items.Add(AR.NewReligion);
                CitizenReligion_Dropdown.SelectedItem = AR.NewReligion;               
            }

        }
        
        
      
    }
}
