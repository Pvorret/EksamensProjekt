using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class Sensor
    {
        public int SerialNumber {get; set;}
        public string Type { get; set; }
        public string Location { get; set; }
        public bool Activated { get; set; }
        Citizen Citizen { get; set; }

        public Sensor(int serialnumber, string type)
        {
            this.SerialNumber = serialnumber;
            this.Type = type;
            this.Location = "";
            this.Activated = false;
        }
        public Sensor() { }
    }
}
