using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class SensorLog
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public DateTime ActivationTime { get; set; }
        public DateTime ContactTime { get; set; }
        public string ContactPerson { get; set; }
        public string ContactMessage { get; set; }

        public SensorLog(int serialNumber, DateTime activationTime)
        {
            this.SerialNumber = serialNumber;
            this.ActivationTime = activationTime;
        }
        public SensorLog()
        {
        }
    }
}
