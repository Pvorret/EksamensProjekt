using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class Citizen : Person
    {
        public List<string> Illness { get; set; }
        public string Religion { get; set; }
        public Dictionary<string, int> SensorTypesNeeded { get; set; }
        public List<Time> HomeCareTimes { get; set; }
        public List<Relative> Relatives { get; set; }
        public List<Sensor> Sensors { get; set; }
        public Citizen() { }
        public Citizen(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, string religion, List<string> illness, Dictionary<string, int> sensorTypes, List<Time> homeCareTimes)
        {
            this.Name = name;
            this.CprNr = cprNr;
            this.Gender = gender;
            this.Age = age;
            this.Address = address;
            this.City = city;
            this.PhoneNumber = phoneNumber;
            this.Illness = illness;
            this.Religion = religion;
            this.SensorTypesNeeded = sensorTypes;
            this.HomeCareTimes = homeCareTimes;
        }
        public Citizen(string name, string cprNr, string address, string city)
        {
            this.CprNr = cprNr;
            this.Name = name;
            this.Address = address;
            this.City = city;
        }
        public Citizen(string name, string cprNr)
        {
            this.Name = name;
            this.CprNr = cprNr;
            this.Relatives = new List<Relative>();
        }
    }
}
