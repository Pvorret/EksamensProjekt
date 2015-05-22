using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EksamensProjekt.Controller;

namespace UnitTest {
    [TestClass]
    public class UnitTestBehandleSensorInput {
        [TestMethod]
        public void TestMethod1() 
        {
            RuleSetController RSC = new RuleSetController();
            DateTime DT = new DateTime(2015,12,14,13,24,00,00);
            RSC.HandelSensorInput(23323, DT);//dfsdf
            
        }
    }
}
