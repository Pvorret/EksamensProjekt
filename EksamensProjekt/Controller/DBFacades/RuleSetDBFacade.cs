using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EksamensProjekt.Model;
using EksamensProjekt.Helper;
using System.Windows;

namespace EksamensProjekt.Controller.DBFacades
{
    public class RuleSetDBFacade
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
        public static int CreateSensorLog(SensorLog sl)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_CreateSensorLog", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", sl.SerialNumber));
                cmd.Parameters.Add(new SqlParameter("@ActivationTime", sl.ActivationTime));
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while(rdr.HasRows && rdr.Read())
                {
                    int id = Convert.ToInt32(rdr["SL_ID"]);
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
            return id;
        }
        public static void UpdateSensorLog(SensorLog sl)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_UpdateSensorLog", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", sl.Id));
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
                    int Id = (int)rdr["SR_ID"];
                    int SensorDependency = (int)rdr["SR_S_SensorDependency"];
                    bool WaitOrLook = Convert.ToBoolean(rdr["SR_WaitOrLook"]);
                    int TimeToWait = (int)rdr["SR_TimeToWait"];
                    int TimeToLook = (int)rdr["SR_TimeToLook"];
                    SensorRule sensorrule = new SensorRule(Id, SensorDependency, WaitOrLook, TimeToWait, TimeToLook);
                    sensorruleList.Add(sensorrule);
                }
            }
            catch (SqlException) {
                
                throw new Exception("Error in getting SensorRules");
            }
            finally
            {
                CloseDB();
            }

            return sensorruleList;
        }
        public static List<string> GetSensorRuleManagementFromSerialNumber(int serialNumber)
        {
            List<string> ruleManagement = new List<string>();
            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetSensorRuleManagementFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    ruleManagement.Add(rdr["SRM_RuleSet"].ToString());
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return ruleManagement;
        }
        public static List<TimeRangeRule> GetTimeRangeRuleFromSerialNumber(int serialNumber)
        {
            List<TimeRangeRule> timerangerules = new List<TimeRangeRule>();
            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetTimeRangeRuleFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@S_SerialNumber", serialNumber));
                
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    string cprNr = rdr["TRR_R_CPRNR"].ToString();
                    string actingRule = rdr["TRR_ActingRule"].ToString();

                    DateTime startTime = Convert.ToDateTime(rdr["T_StartTime"]);
                    DateTime endTime = Convert.ToDateTime(rdr["T_EndTime"]);
                    string day = rdr["T_Day"].ToString();
                    int id = Convert.ToInt32(rdr["TRR_ID"]);
                    bool contactHelper = Convert.ToBoolean(rdr["TRR_ContactHelper"]);

                    TimeRangeRule TRR = new TimeRangeRule(id, actingRule, cprNr, contactHelper, new Time(id, startTime, endTime, day));
                    timerangerules.Add(TRR);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return timerangerules;
        }
        public static int AddSensorRuleFromSerialNumber(int serialNumber, SensorRule sensorRule, int sensorRuleManagementId) {
            int id = 0;
            try {
                ConnectDB();
               
                SqlCommand cmd = new SqlCommand("SP_AddSensorRuleFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SRM_S_SerialNumber", serialNumber));
                cmd.Parameters.Add(new SqlParameter("@SR_S_SensorDependency", sensorRule.SensorDependency));
                cmd.Parameters.Add(new SqlParameter("@SR_WaitOrLook", sensorRule.WaitOrLook));
                cmd.Parameters.Add(new SqlParameter("@SR_TimeToWait", sensorRule.TimeToWait));
                cmd.Parameters.Add(new SqlParameter("@SR_TimeToLook", sensorRule.TimeToLook));
                cmd.Parameters.Add(new SqlParameter("@SR_WhenToSend", Convert.ToInt32(sensorRule.WhenToSend)));
                cmd.Parameters.Add(new SqlParameter("@SRM_ID", sensorRuleManagementId));

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read() && rdr.HasRows) 
                {
                    id = (int)rdr["SR_ID"];
                }
                
            }
            catch (SqlException e) {

                throw new Exception("Error in storing SensorRule i DB " + e.Message);
            }
            finally
            {
                CloseDB();
             }
            return id;
        }
        public static int AddSensorRuleManagement(string ruleSet, int serialNumber)
        {
            int id = 0;
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_AddSensorRuleManagement", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SRM_RuleSet", ruleSet));
                cmd.Parameters.Add(new SqlParameter("@SRM_S_SerialNumber", serialNumber));

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.HasRows && rdr.Read())
                {
                    id = Convert.ToInt32(rdr["SRM_ID"]);
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke tilføje det til Databasen " + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return id;
        }
        public static void AddTimeRangeRuleFromSerialNumber(int serialNumber, TimeRangeRule timeRange)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_AddTimeRangeRuleFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@S_SerialNumber", serialNumber));
                cmd.Parameters.Add(new SqlParameter("@T_Day", timeRange.Time.Day));
                cmd.Parameters.Add(new SqlParameter("@T_StartTime", timeRange.Time.StartTime));
                cmd.Parameters.Add(new SqlParameter("@T_Endtime", timeRange.Time.EndTime));
                cmd.Parameters.Add(new SqlParameter("@TRR_R_CPRNR", timeRange.RelativeCPRNR));
                cmd.Parameters.Add(new SqlParameter("@TRR_ActingRule", timeRange.ActingRule));
                cmd.Parameters.Add(new SqlParameter("@TRR_ContactHelper", Convert.ToInt32(timeRange.ContactHelper)));
                
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error! TimeRangeRule kunne ikke tilføjes" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static SensorRule GetSensorRuleFromID(int id)
        {
            SensorRule sensorRule = new SensorRule();
            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetSensorRuleFromID", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SR_ID", id));
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    sensorRule.SensorDependency = Convert.ToInt32(rdr["SR_S_Dependency"]);
                    sensorRule.WaitOrLook = Convert.ToBoolean(rdr["SR_WaitOrLook"]);
                    sensorRule.TimeToWait = Convert.ToInt32(rdr["SR_TimeToWait"]);
                    sensorRule.TimeToLook = Convert.ToInt32(rdr["SR_TimeToLook"]);
                    sensorRule.WhenToSend = Convert.ToBoolean(rdr["SR_WhenToSend"]);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensorRule;
        }
        public static List<SensorLog> GetSensorLogFromDateTime(DateTime checkTime)
        {
            List<SensorLog> sensorLogList = new List<SensorLog>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetSensorLogFromTime", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Time", checkTime));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SensorLog sensorLog = new SensorLog();
                    sensorLog.SerialNumber = Convert.ToInt32(reader["SL_S_SerialNumber"]);
                    sensorLogList.Add(sensorLog);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensorLogList;
        }
    }
}
