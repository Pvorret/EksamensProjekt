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
        public List<Sensor> Sensors { get; set; }
        public Dictionary<string, string> HomeCare { get; set; }
        public List<Relative> Relatives { get; set; }

        public Citizen(string name, string cprNr, string gender, string age, string address, string city, string phoneNumber, List<string> illness, string religion, List<Sensor> sensors, Dictionary<string, string> homeCare, List<Relative> relatives)
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
            this.HomeCare = homeCare;
            this.Relatives = relatives;
           
        }
    }
}
