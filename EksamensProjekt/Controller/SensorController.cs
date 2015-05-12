using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;

namespace EksamensProjekt.Controller
{
    public class SensorController
    {
        public List<Sensor> Sensors = new List<Sensor>();
        public void CreateSensor(int serialNumber, string type)
        {
            Sensor s = new Sensor(serialNumber, type);
            SensorDBFacade.CreateSensor(s);
        }
        public void GetSensor(int serialNumber)
        {
            Sensors = SensorDBFacade.GetSensor(serialNumber);
        }

        public void GetAllSensors()
        {
            Sensors = SensorDBFacade.GetSensor(0);
        }

        public List<int> GetSerialNumberList(List<Sensor> sensorList)
        {
            List<int> serialnumbers = new List<int>();
            foreach (Sensor s in sensorList)
            {
                serialnumbers.Add(s.SerialNumber);
            }
            return serialnumbers;
        }

        public bool DeleteSensor(int serialNumber)
        {
            return SensorDBFacade.DeleteSensor(serialNumber);
        }
        public void ConnectSensorToCitizen(int sensorSerialNumber, string cprNr, string sensorLocation)//NY
        {
            SensorDBFacade.ConnectSensorToCitizen(sensorSerialNumber, cprNr, sensorLocation);
        }
    }
}
