﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller;
using EksamensProjekt.Model;

namespace EksamensProjekt.Controller
{
    class SensorController
    {
        public void CreateSensor(int serialNumber, string type)
        {
            Sensor s = new Sensor(serialNumber, type);
            DBFacades.SensorDBFacade.CreateSensor(s);
        }
    }
}