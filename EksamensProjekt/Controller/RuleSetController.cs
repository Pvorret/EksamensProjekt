﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using EksamensProjekt.Helper;
using System.Text.RegularExpressions;
using System.Windows;

namespace EksamensProjekt.Controller
{
    public class RuleSetController
    {
        public SensorLog SensorLg;
        public List<SensorRule> SensorRules;
        public List<TimeRangeRule> TimeRangeRules;
        public List<Citizen> CitizenList = new List<Citizen>();
        public List<string> ContactList = new List<string>();
        public List<string> SensorRuleManagement = new List<string>();
        public bool Contact = true;
        public int SensorRuleId { get; set; }
        public int SensorRuleManagementId { get; set; }
        
        public void HandelSensorInput(int serialNumber, DateTime activationTime) //af Thomas
        {
            RuleSetController RSC = new RuleSetController();
            RSC.SensorRules = new List<SensorRule>();
            RSC.TimeRangeRules = new List<TimeRangeRule>();
            GetSensorInputInformation(serialNumber, activationTime, RSC);
            bool ruleExecuted = false;
            foreach (string s in RSC.SensorRuleManagement)
            {
                if (s == "TRR" && ruleExecuted == false)
                {
                    TimeRangeRule TRR = new TimeRangeRule();
                    RSC.TimeRangeRules = RuleSetDBFacade.GetTimeRangeRuleFromSerialNumber(serialNumber);
                    foreach (TimeRangeRule TR in TimeRangeRules)
                    {
                        bool ActiveRule = RSC.CheckTime(TR.Time,activationTime);
                        if (ActiveRule == true)
                        {
                            string ActingRule = TRR.TimeRangeRuleActivated(RSC, TR.Id);
                            if (ActingRule != "")
                            {
                                RSC.CheckActingRule(ActingRule, RSC);
                            }
                            ruleExecuted = true;
                        }
                    }
                }
                else
                    if (RSC.SensorRules.Count == 1)
                    {
                        SensorRule SR = new SensorRule();
                        SR.SensorRuleActivated(RSC, RSC.SensorRules[0], activationTime);
                    }
                    else
                        if (s == "SR" && ruleExecuted == false)
                        {
                            RSC.SensorRules = GetSensorRuleFromSerialNumber(serialNumber);
                            if (RSC.SensorRules.Count != 0)
                            {
                                SensorRule SR = new SensorRule();
                                SR.SensorRuleActivated(RSC, RSC.SensorRules[0], activationTime);
                            }
                        }
                
            }
            if (ContactList.Count == 0)
            {
                foreach (Citizen C in RSC.CitizenList)
                {
                    foreach (Relative R in C.Relatives)
                    {
                        foreach (Time T in R.NotAvailableTimes)
                        {
                            if (CheckTime(T, activationTime) == true)
                            {
                                RSC.ContactList.Add(R.CprNr);
                            }
                        }
                    }
                    foreach (Time t in C.HomeCareTimes)
                    {
                        if (RSC.ContactList.Count == 0)
                        {
                            RSC.ContactList.Add("Helper");
                        }
                    }
                }
            }
            if (Contact == true)
            {
                RSC.SendMessage(RSC.ContactList);
            }
        }
        public void GetSensorInputInformation(int serialNumber, DateTime activationTime, RuleSetController rSC) // Thomas og Stefan
        { 
            SensorLog SL = new SensorLog(serialNumber, activationTime);
            RuleSetDBFacade.CreateSensorLog(SL);
            rSC.CitizenList = CitizenDBFacade.GetCitizenTime(serialNumber);
            foreach (Citizen citizen in rSC.CitizenList)
            {
                citizen.Relatives = CitizenDBFacade.GetRelativeTime(citizen.CprNr);
            }
            rSC.SensorRuleManagement = RuleSetDBFacade.GetSensorRuleManagementFromSerialNumber(serialNumber);            
        }
        public void CreateSensorLog(int serialNumber, string activationTime)
        {
            SensorLg = new SensorLog(serialNumber, Convert.ToDateTime(activationTime));
            SensorLg = RuleSetDBFacade.CreateSensorLog(SensorLg);
        }
        public void UpdateSensorLog(string contactTime, string contactPerson, string contactMessage)
        {
            SensorLg = new SensorLog();
            SensorLg.ContactTime = Convert.ToDateTime(contactTime);
            SensorLg.ContactPerson = contactPerson;
            SensorLg.ContactMessage = contactMessage;
            RuleSetDBFacade.UpdateSensorLog(SensorLg);
        }
        public void AddSensorRuleManagement(string ruleSet, int serialNumber) {
            SensorRuleManagementId = RuleSetDBFacade.AddSensorRuleManagement(ruleSet, serialNumber);
        }
        public List<SensorRule> GetSensorRuleFromSerialNumber(int serialNumber) {
            return RuleSetDBFacade.GetSensorRuleFromSerialNumber(serialNumber);
        }
        public void AddSensorRuleFromSerialNumber(int serialNumber, int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook, bool whenToSend, int sensorRuleManagementId)//Stefan
        {
            SensorRule sensorRule = new SensorRule(sensorDependency, waitOrLook, timeToWait, timeToLook, whenToSend);
            SensorRuleId = RuleSetDBFacade.AddSensorRuleFromSerialNumber(serialNumber, sensorRule, sensorRuleManagementId);
        }
        public void AddTimeRangeRuleFromSerialNumber(int serialNumber, string day, DateTime startTime, DateTime endTime, string relativeCprNr, string actingRule, bool contactHelper) {
            TimeRangeRule timerange = new TimeRangeRule(relativeCprNr, actingRule, contactHelper, new Time(startTime, endTime, day));
            RuleSetDBFacade.AddTimeRangeRuleFromSerialNumber(serialNumber, timerange);
        }
        public List<string> GetSensorRuleManagementFromSerialNumber(int serialNumber)
        {
            return DBFacades.RuleSetDBFacade.GetSensorRuleManagementFromSerialNumber(serialNumber);
        }
        public void GetTimeRangeRuleFromSerialNumber(int serialNumber)
        {
            TimeRangeRules = new List<TimeRangeRule>();
            TimeRangeRules = DBFacades.RuleSetDBFacade.GetTimeRangeRuleFromSerialNumber(serialNumber);
        }
        public bool CheckTime(Time timeRange, DateTime activationTime)//Stefan
        {
            if (timeRange.Day == activationTime.DayOfWeek.ToString())
            {
                if (timeRange.StartTime <= activationTime)
                {
                    if (timeRange.EndTime >= activationTime)
                    {
                        return true;
                    }
                }
            }
            return false;          
        }
        public void CheckActingRule(string actingRuleString, RuleSetController rSC)
        {
            string[] rule = Regex.Split(actingRuleString, @"\W+");
            int id = int.Parse(rule[1]);
            if (rule[0] == "SR")
            {
                rSC.SensorRules.Add(RuleSetDBFacade.GetSensorRuleFromID(id));
            }
        }
        public void SendMessage(List<string> contactPersons)//Stefan
        {
            string message = "";
            SensorLg = new SensorLog();
            foreach (string CP in contactPersons)
            {
                
                if (CP == "Helper")
                {
                    SensorLg.ContactPerson = CP;
                    message = message + ", " + MessageBox.Show("Send Besked til Hjemmehjælper").ToString();
                }
                else
                {
                    SensorLg.ContactPerson = CP;
                    message = message + ", " + MessageBox.Show("Besked send til: " + CP).ToString();
                }
                
            }
            DateTime messageSendTime = DateTime.Now;
            SensorLg.ContactMessage = message;
            SensorLg.ContactTime = messageSendTime;

            RuleSetDBFacade.UpdateSensorLog(SensorLg);
        }
        public List<SensorLog> GetSensorLogFromDateTime(DateTime checkTime)
        {
            return RuleSetDBFacade.GetSensorLogFromDateTime(checkTime);
        }
    }
}
