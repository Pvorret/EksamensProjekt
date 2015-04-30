using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;

namespace EksamensProjekt.Controller {
    public class CitizenController {
        public List<Relative> Relatives = new List<Relative>();
        public List<string> SensorTypes = new List<string>();
        public List<string> IllnessList = new List<string>();

        public void CreateCitizen(string name, string cprnr, string gender, string age, string address, string city, string phonenumber, List<string> illness, string religion, List<string> sensorTypes, Dictionary<string, string> homeCare)
        {
            Citizen citizen = new Citizen(name, cprnr, gender, age, address, city, phonenumber, illness, religion, sensorTypes, homeCare);
            citizen.Relatives = Relatives;

            Controller.DBFacades.CitizenDBFacade.CreateCitizen(citizen);

        }

        //Brugt til tilføjelse af en pårørende i ændre borger
        public void AddRelative(string citizenId, string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<string> notAvailable)
        {
            Controller.DBFacades.CitizenDBFacade.AddRelatives(Relatives);
        }
        //Brugt til opret borger
        public void AddRelative(string name,string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, Dictionary<string, string> notAvailable)
        {
            Relatives.Add(new Relative(name, cprNr, gender, age, address, city, phoneNumber, email, notAvailable));
        }

        public void GetAllSensorType() {
            SensorTypes = Controller.DBFacades.SensorDBFacade.GetSensorType(0);
        }

        public void GetAllIllness() {
            IllnessList = Controller.DBFacades.CitizenDBFacade.GetIllnessType(0);
        }

    }
}
