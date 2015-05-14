using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace EksamensProjekt.Controller.DBFacades
{
    public class SensorDBFacade
    {
        static int ruleSetManagementId = 0;
        static int sensorRuleId = 0;
        static int timeRangeRuleId = 0;
        static List<int> sensorDependency = new List<int>();
        public static SqlConnection dbconn;
        static SqlCommand cmd;
        public static void ConnectDB() {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }
        public static void CreateSensor(Sensor sensor)//Stefan
        {
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_CreateSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", sensor.SerialNumber));
                cmd.Parameters.Add(new SqlParameter("@Type", sensor.Type));
                cmd.Parameters.Add(new SqlParameter("@activated", Convert.ToInt32(sensor.Activated)));
                cmd.Parameters.Add(new SqlParameter("@Location", ""));
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static List<string> GetSensorType(int id)
        {
            List<string> sensortype = new List<string>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetSensorType", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ST_Id", id));
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    string sensor = reader["ST_Type"].ToString();
                    sensortype.Add(sensor);
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensortype;
        }
        public static List<Sensor> GetSensor(int serialNumber)
        {
            List<Sensor> sensors = new List<Sensor>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Sensor sensor = new Sensor();
                    sensor.SerialNumber = Convert.ToInt32(reader["S_SerialNumber"]);
                    sensor.Location = reader["S_Location"].ToString();
                    if (Convert.ToInt32(reader["S_Activated"]) == 0)
                    {
                        sensor.Activated = false;
                    }
                    if (Convert.ToInt32(reader["S_Activated"]) == 1)
                    {
                        sensor.Activated = true;
                    }
                    sensor.Type = reader["ST_Type"].ToString();
                    sensors.Add(sensor);
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensors;
        }
        public static void DeleteSensorRuleFromDependency()
        {
            foreach (int i in sensorDependency)
            {
                try
                {
                    ConnectDB();
                    cmd = new SqlCommand("SP_DeleteSensorRuleFromSensorDependency", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SR_ID", i));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    CloseDB();
                }
            }
        }
        public static bool DeleteSensor(int serialNumber)
        {
            GetRuleSetIDFromSerialNumber(serialNumber);
            GetSensorDependencyFromSerialNumber(serialNumber);
            DeleteSensorRuleFromDependency();
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_DeleteSensorV2", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SRMSR_SR_ID", sensorRuleId));
                cmd.Parameters.Add(new SqlParameter("@SRMTRR_TRR_ID", timeRangeRuleId));
                cmd.Parameters.Add(new SqlParameter("@SRM_ID", ruleSetManagementId));
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                CloseDB();
            }
        }
        public static List<Relative> GetRelativeFromCitizen(string cprNr)
        {
            List<Relative> relatives = new List<Relative>();
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_GetRelativeTimeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Citizen_CPRNR", cprNr));
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    relatives.Add(new Relative(Convert.ToString(rdr["R_CPRNR"]), Convert.ToString(rdr["R_Name"])));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke hente pårørende " + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return relatives;
        }
        public static List<string> GetSensorTypeFromCitizen(string cprNr)
        {
            List<string> SensorTypes = new List<string>();

            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetSensorTypeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CprNr", cprNr));
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    SensorTypes.Add(Convert.ToString(rdr["ST_Type"]));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error in getting SensorTypes" + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return SensorTypes;
        }
        public static void ConnectSensorToCitizen(int serialNumber, string cprNr, string sensorLocation)//NY
        {
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_ConnectSensorToCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                cmd.Parameters.Add(new SqlParameter("@CPRNR", cprNr));
                cmd.Parameters.Add(new SqlParameter("@Location", sensorLocation));
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Error! Sensor and Citizen not Connected");
            }
            finally
            {
                CloseDB();
            }

        }
        public static void CloseDB() {
            dbconn.Close();
            dbconn.Dispose();
        }
        public static List<Sensor> GetSensorFromCitizen(string citizenCprNr)
        {
            List<Sensor> sensors;
            try
            {
                ConnectDB();
                sensors = new List<Sensor>();
                cmd = new SqlCommand("SP_GetSensorFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CitizenCPRNR", citizenCprNr));
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    sensors.Add(new Sensor(Convert.ToInt32(rdr["S_SerialNumber"])));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke hente sensors " + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return sensors;
        }
        public static void GetRuleSetIDFromSerialNumber(int serialNumber)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetRuleSetIDFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                SqlDataReader reader = cmd.ExecuteReader();
                List<int> sensorDependency = new List<int>();

                while (reader.Read())
                {
                    ruleSetManagementId = Convert.ToInt32(reader["SRM_ID"]);
                    try
                    {
                        sensorRuleId = Convert.ToInt32(reader["SRMSR_SR_ID"]);
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                    }
                    try
                    {
                        timeRangeRuleId = Convert.ToInt32(reader["SRMTRR_TRR_ID"]);
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                    }
                    
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
        }
        public static void GetSensorDependencyFromSerialNumber(int serialNumber)
        {
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_GetSensorDependencyIDFromSerialNumber", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                        sensorDependency.Add(Convert.ToInt32(reader["SR_ID"]));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
    }
}
