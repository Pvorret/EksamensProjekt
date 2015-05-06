using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    class SensorLog
    {
        public int ID { get; set; }
        public int SensorSerialNumber { get; set; }
        public DateTime ActivationTime { get; set; }
        public DateTime ContactTime { get; set; }
        public string ContactPerson { get; set; }
        public string ContactMessage { get; set; }

        public SensorLog(int id, int sensorSerialNumber, DateTime activationTime, DateTime contactTime, string contactPerson, string contactMessage)
        {
            this.ID = id;
            this.SensorSerialNumber = sensorSerialNumber;
            this.ActivationTime = activationTime;
            this.ContactTime = contactTime;
            this.ContactPerson = contactPerson;
            this.ContactMessage = contactMessage;
        }
        public SensorLog(int id, int sensorSerialNumber, DateTime activationTime)
        {
            this.ID = id;
            this.SensorSerialNumber = sensorSerialNumber;
            this.ActivationTime = activationTime;
        }
    }
}
