using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;
using EksamensProjekt.Controller;
using System.Text.RegularExpressions;

namespace EksamensProjekt.Helper {
    public class TimeRangeRule 
    {
        public int Id { get; set; }
        public string CPRNR { get; set; } //Ændre navn på property (eks. ContactPerson)
        public string ActingRule { get; set; }
        public bool ContactHelper { get; set; }
        public Time Time { get; set; }

        public TimeRangeRule(string relativeCprNr, string actingRule, bool contactHelper, Time time) 
        {
            this.ActingRule = actingRule;
            this.CPRNR = relativeCprNr;
            this.ContactHelper = contactHelper;
            this.Time = time;
        }
        public TimeRangeRule(int id, string relativeCprNr, string actingRule, bool contactHelper, Time time)
        {
            this.Id = id;
            this.ActingRule = actingRule;
            this.CPRNR = relativeCprNr;
            this.ContactHelper = contactHelper;
            this.Time = time;
        }
        public TimeRangeRule() { }
        public string TimeRangeRuleActivated(RuleSetController ruleSetController ,int id)//Stefan
        {
            ruleSetController.ContactList.Add(ruleSetController.TimeRangeRules[id].CPRNR);
            if (ruleSetController.TimeRangeRules[id].ContactHelper == true)
            {
                ruleSetController.ContactList.Add("Helper");
            }
            return ruleSetController.TimeRangeRules[id].ActingRule;
        }
    }
}
