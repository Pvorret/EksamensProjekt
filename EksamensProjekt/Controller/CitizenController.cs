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
        public List<Time> HomeCareTimes = new List<Time>();
        public List<Time> NotAvailableTimes = new List<Time>();

        //Den har nico lavet
        public void CreateCitizen(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, List<string> illness, string religion, Dictionary<string, int> sensorTypes, List<string> days, List<DateTime> startTime, List<DateTime> endTime)
        {
            for (int i = 0; i < days.Count; i++)
            {
                HomeCareTimes.Add(new Time(startTime[i], endTime[i], days[i]));
            }

            Citizen citizen = new Citizen(name, cprNr, gender, age, address, city, phoneNumber, illness, religion, sensorTypes, HomeCareTimes);
            citizen.Relatives = Relatives;

            CitizenDBFacade.CreateCitizen(citizen);
            CitizenDBFacade.AddTime(citizen.CprNr, citizen.HomeCareTimes);
            CitizenDBFacade.AddSensorTypeToCitizen(citizen.CprNr, citizen.SensorTypesNeeded);
            CitizenDBFacade.AddRelative(citizen);
            foreach (Relative relative in citizen.Relatives)
            {
                CitizenDBFacade.AddTime(relative.CprNr, relative.NotAvailableTimes);
            }
            CitizenDBFacade.AddIllnessToCitizen(citizen);

        }

        //Brugt til tilføjelse af en pårørende i ændre borger
        //public void AddRelative(string citizenId, string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<string> notAvailable)
        //{
        //    CitizenDBFacade.AddRelatives(Relatives);
        //}
        //Brugt til opret borger
        public void AddRelative(string name,string cprNr, string gender, string age, string address, string city, string phoneNumber, string email, List<string> days, List<DateTime> startTime, List<DateTime> endTime)
        {
            for (int i = 0; i < days.Count; i++)
            {
                NotAvailableTimes.Add(new Time(startTime[i], endTime[i], days[i]));
            }
            Relatives.Add(new Relative(name, cprNr, gender, age, address, city, phoneNumber, email, NotAvailableTimes));
        }
        public void GetSensorsFromCitizen(string cprNr)
        {
            for (int i = 0; i < Citizens.Count; i++)
            {
                if (Citizens[i].CprNr == cprNr)
                {
                    Citizens[i].Sensors = SensorDBFacade.GetSensorFromCitizen(cprNr);
                }
            }

        }
        public void GetAllSensorType() {
            SensorTypes = SensorDBFacade.GetSensorType(0);
        }
        public void GetSensorTypeFromCitizen(string cprNr)
        {
            SensorTypes = SensorDBFacade.GetSensorTypeFromCitizen(cprNr);
        }
        public void GetAllCitizen()
        {
            Citizens = CitizenDBFacade.GetAllCitizen();
        }
        public void GetRelativeFromCitizen(string cprNr)
        {
            for (int i = 0; i < Citizens.Count; i++)
            {
                if (Citizens[i].CprNr == cprNr)
                {
                    Citizens[i].Relatives = SensorDBFacade.GetRelativeFromCitizen(cprNr);
                }
            }
        }
        public void GetAllIllness() {
            IllnessList = CitizenDBFacade.GetIllnessType();
        }

    }
}
