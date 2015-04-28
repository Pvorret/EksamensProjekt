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
        public TimeSpan HomeCare { get; set; }
        public List<Relative> Relatives { get; set; }
    }
}
