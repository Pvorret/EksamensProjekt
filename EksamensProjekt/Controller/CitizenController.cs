﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;

namespace EksamensProjekt.Controller {
    class CitizenController {
        private List<Relative> Relatives = new List<Relative>();

        //Mangler Parameter
        public void CreateCitizen(string name, string cprnr, string gender, string age, string address, string city, string phonenumber, List<string> illness, string religion, List<string > sensors, Dictionary<string, string> homeCare, List<string> relatives)
        {

        }

        //Brugt til tilføjelse af en pårørende i ændre borger
        public void AddRelative(string citizenId, string name, string gender, string age, string address, string city, string phoneNumber, string email, List<string> notAvailable)
        {

        }
        //Brugt til opret borger
        public void AddRelative(string name,string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, Dictionary<string, string> notAvailable)
        {
            Relatives.Add(new Relative(name, cprNr, gender, age, address, city, phoneNumber, email, notAvailable));

        }

    }
}
