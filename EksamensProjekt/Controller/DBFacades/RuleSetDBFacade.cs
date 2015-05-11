using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EksamensProjekt.Model;
using EksamensProjekt.Helper;

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
                    bool WaitOrLook = Convert.ToBoolean(int.Parse(rdr["SR_WaitOrLook"].ToString()));
                    int TimeToWait = (int)rdr["SR_TimeToWait"];
                    int TimeToLook = (int)rdr["SR_TimeToLook"];
                    SensorRule sensorrule = new SensorRule(SensorDependency, WaitOrLook, TimeToWait, TimeToLook);
                    sensorruleList.Add(sensorrule);
                }

                CloseDB();
            }
            catch (SqlException) {
                
                throw new Exception("Error in getting SensorRules");
            }           

            return sensorruleList;
        }
        public static Dictionary<string, int> GetSensorRuleManagementFromSensorSerialNumber(int serialNumber)
        {
            Dictionary<string, int> ruleManagement = new Dictionary<string, int>();
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
                    ruleManagement.Add(rdr["SRM_RuleSet"].ToString(), Convert.ToInt32(rdr["SRM_ID"]));
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
        public static List<TimeRangeRule> GetTimeRangeRuleFromSensorSerialNumber(int serialNumber)
        {
            List<TimeRangeRule> timerangerules = new List<TimeRangeRule>();
            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetTimeRangeRuleManagementFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                
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

                    TimeRangeRule TRR = new TimeRangeRule(actingRule, cprNr, new Time(id, startTime, endTime, day));
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
        public static void AddSensorRuleFromSerialNumber(int serialNumber, SensorRule sensorrule) {
            try {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_AddSensorRuleFromSensorSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SRM_S_SerialNumber", serialNumber));

                cmd.Parameters.Add(new SqlParameter("@SR_R_SensorDependency", sensorrule.SensorDependency));
                cmd.Parameters.Add(new SqlParameter("@SR_WaitOrLook", sensorrule.WaitOrLook));
                cmd.Parameters.Add(new SqlParameter("@SR_TimeToWait", sensorrule.TimeToWait));
                cmd.Parameters.Add(new SqlParameter("@SR_TimeToLook", sensorrule.TimeToLook));
                
                cmd.ExecuteNonQuery();

                CloseDB();
            }
            catch (SqlException e) {

                throw new Exception("Error in storing SensorRule i DB " + e.Message);
            }
        }
        public static void AddSensorRuleManagement(string ruleSet, int serialNumber)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_AddSensorRuleManagement", dbconn);

                cmd.Parameters.Add(new SqlParameter("@SRM_RuleSet", ruleSet));
                cmd.Parameters.Add(new SqlParameter("@SRM_S_SerialNumber", serialNumber));
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke tilføje det til Databasen " + e.Message);
            }
        }
        public static void AddTimeRangeRuleFromSensorSerialNumber(int serialNumber, TimeRangeRule timeRange)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_AddTimeRangeRuleFromSensorSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@S_SerialNumber", serialNumber));
                cmd.Parameters.Add(new SqlParameter("@T_Day", timeRange.Time));
                cmd.Parameters.Add(new SqlParameter("@T_StartTime", timeRange.Time.StartTime));
                cmd.Parameters.Add(new SqlParameter("@T_Endtime", timeRange.Time.EndTime));
                cmd.Parameters.Add(new SqlParameter("@TRR_R_CPRNR", timeRange.CPRNR));
                cmd.Parameters.Add(new SqlParameter("@TRR_ActingRule", timeRange.ActingRule));
                cmd.Parameters.Add(new SqlParameter("@TRR_ContactHelper", Convert.ToInt32(timeRange.ContactHelper)));
                cmd.ExecuteNonQuery();

                CloseDB();

            }
            catch (SqlException e)
            {
                throw new Exception("Error! TimeRangeRule kunne ikke tilføjes" + e.Message);
            }
        }
    }
}
