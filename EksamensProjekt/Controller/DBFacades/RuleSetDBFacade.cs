﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EksamensProjekt.Model;
using SensorRuleSet;

namespace EksamensProjekt.Controller.DBFacades
{
    class RuleSetDBFacade
    {
        static SqlConnection dbconn;
        private static void ConnectDB()
        {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }
        private static void CloseDB()
        {
            dbconn.Close();
            dbconn.Dispose();
        }

        public static SensorLog CreateSensorLog(SensorLog sl)
        {
            SensorLog sensorLog = new SensorLog(sl.SensorSerialNumber, sl.ActivationTime);
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_CreateSensorLog", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", sl.SensorSerialNumber));
                cmd.Parameters.Add(new SqlParameter("@ActivationTime", sl.ActivationTime));
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while(rdr.HasRows && rdr.Read())
                {
                    sensorLog.ID = Convert.ToInt32(rdr["SL_ID"]);
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Sensor not added to database" + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensorLog;
        }
        public static void UpdateSensorLog(SensorLog sl)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_UpdateSensorLog", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", sl.ID));
                cmd.Parameters.Add(new SqlParameter("@ContactPerson", sl.ContactPerson));
                cmd.Parameters.Add(new SqlParameter("@ContactTime", sl.ContactTime));
                cmd.Parameters.Add(new SqlParameter("@ContactMessage", sl.ContactMessage));
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Sensor not added to database" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }

        public static List<SensorRule> GetSensorRuleFromSerialNumber(int serialNumber) {
            List<SensorRule> sensorruleList = new List<SensorRule>();

            try {
                ConnectDB();
                
                SqlCommand cmd = new SqlCommand("SP_GetSensorRuleFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@S_SerialNumber", serialNumber));

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read()) {
                    int SensorDependency = (int)rdr["SR_S_SensorDependecy"];
                    bool WaitOrLook = rdr.GetBoolean(int.Parse(rdr["SR_WaitOrLook"].ToString()));
                    int TimeToWait = (int)rdr["SR_TimeToWait"];
                    int TimeToLook = (int)rdr["SR_TimeToLook"];
                    SensorRule sensorrule = new SensorRule(SensorDependency, WaitOrLook, TimeToWait, TimeToLook);
                    sensorruleList.Add(sensorrule);
                }

                CloseDB();
            }
            catch (SqlException e) {
                
                throw new Exception("Error in getting SensorRules " + e.Message);
            }           

            return sensorruleList;
        }

        public static void AddSensorRuleManagement(string cprnr, Dictionary<string, int> RuleManagement) {
            try {
                ConnectDB();
                foreach (KeyValuePair<string, int> sensortypeamount in RuleManagement) {


                    SqlCommand cmd = new SqlCommand("SP_AddSensorRuleManagement", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@C_CPRNR", cprnr));
                    cmd.Parameters.Add(new SqlParameter("@ST_Type", sensortypeamount.Key));
                    cmd.Parameters.Add(new SqlParameter("CST_AmountNeeded", sensortypeamount.Value));

                    cmd.ExecuteNonQuery();
                }
                CloseDB();
            }
            catch (SqlException e) {

                throw new Exception("Error adding SensorRuleManagement" + e.Message);
            }
        }

        public static Dictionary<string, int> GetSensorRuleManagementFromSensorSerialNumber()
        {
            Dictionary<string, int> rulemanagement = new Dictionary<string, int>();
            return rulemanagement;
        }
        public static void GetTimeRangeRuleFromSensorSerialNumber(int serialNumber)
        {

        }
    }
}
