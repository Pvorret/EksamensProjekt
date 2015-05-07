using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;
using EksamensProjekt.Controller.DBFacades;

namespace EksamensProjekt.Controller {
    public class CitizenController {

        public List<Relative> Relatives = new List<Relative>();
        public List<string> SensorTypes = new List<string>();
        public List<string> IllnessList = new List<string>();
        public List<Citizen> Citizens = new List<Citizen>();

        //Den har nico lavet
        public void CreateCitizen(string name, string cprnr, string gender, string age, string address, string city, string phonenumber, List<string> illness, string religion, Dictionary<string, int> sensorTypes, Dictionary<string, string> homeCare)
        {
            Citizen citizen = new Citizen(name, cprnr, gender, age, address, city, phonenumber, illness, religion, sensorTypes, homeCare);
            citizen.Relatives = Relatives;

            CitizenDBFacade.CreateCitizen(citizen);
            CitizenDBFacade.AddTime(citizen.CprNr, citizen.HomeCare);
            CitizenDBFacade.AddSensorType(citizen.CprNr, citizen.SensorTypesNeeded);
            CitizenDBFacade.AddRelative(citizen);
            foreach (Relative relative in citizen.Relatives)
            {
                CitizenDBFacade.AddTime(relative.CprNr, relative.NotAvailable);
            }
            CitizenDBFacade.AddIllnessToCitizen(citizen);

        }

        //Brugt til tilføjelse af en pårørende i ændre borger
        public void AddRelative(string citizenId, string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<string> notAvailable)
        {
            CitizenDBFacade.AddRelatives(Relatives);
        }
        //Brugt til opret borger
        public void AddRelative(string name,string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, Dictionary<string, string> notAvailable)
        {
            Relatives.Add(new Relative(name, cprNr, gender, age, address, city, phoneNumber, email, notAvailable));
        }

        public void GetAllSensorType() {
            SensorTypes = SensorDBFacade.GetSensorType(0);
        }

        public void GetSensorTypeFromCitizen(string CprNr)
        {
            SensorTypes = SensorDBFacade.GetSensorTypeFromCitizen(CprNr);
        }
        public void GetAllCitizen()
        {
            Citizens = CitizenDBFacade.GetAllCitizen();
        }

        public void GetAllIllness() {
            IllnessList = CitizenDBFacade.GetIllnessType();
        }

    }
}
