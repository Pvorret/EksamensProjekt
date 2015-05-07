﻿using System;
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
    static class SensorDBFacade
    {
        public static SqlConnection dbconn;
        static SqlCommand cmd;

        public static void CreateSensor(Sensor sensor)//Stefan
        {
            int activatedToBit;

            if (sensor.Activated == false)
            {
                activatedToBit = 0;
            }
            else
            {
                activatedToBit = 1;
            }
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_CreateSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", sensor.SerialNumber));
                cmd.Parameters.Add(new SqlParameter("@Type", sensor.Type));
                cmd.Parameters.Add(new SqlParameter("@activated", activatedToBit));
                cmd.Parameters.Add(new SqlParameter("@Location", ""));
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Error! Sensor not added to database");
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

        public static bool DeleteSensor(int serialNumber)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_DeleteSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public static List<Relative> GetRelativeTime(string cprNr)//Stefan
        {
            List<Relative> RelativeTimeList = new List<Relative>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetRelativeTimeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Citizen_CPRNR", cprNr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string R_CPRNR = reader["R_CPRNR"].ToString();
                    string R_Name = reader["R_Name"].ToString();
                    string R_Phone = reader["R_Phonenumber"].ToString();
                    string A_Address = reader["A_Address"].ToString();
                    string A_City = reader["A_City"].ToString();
                    
                    DateTime T_StartTime = DateTime.Parse(reader["StartTime"].ToString());
                    DateTime T_EndTime = DateTime.Parse(reader["EndTime"].ToString());
                    string T_Day = reader["T_Day"].ToString();

                    Relative relative = new Relative(R_CPRNR, R_Name, R_Phone, A_Address, A_City);
                    relative.NotAvailable.Add(new Time(T_StartTime, T_EndTime, T_Day));
                    RelativeTimeList.Add(relative);                 
                }
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return RelativeTimeList;
        }
        public static List<Citizen> GetCitizenTime(int serialNumber)//stefan
        {
            List<Citizen> CitizenTimeList = new List<Citizen>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetCitizenFromSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SerialNumber", serialNumber);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())//C_CPRNR, C_Name, A_Address, A_City, T_Day, T_TimePeriod
                {
                    string C_CPRNR = reader["C_CPRNR"].ToString();
                    string C_Name = reader["C_Name"].ToString();
                    string A_Address = reader["A_Address"].ToString();
                    string A_City = reader["A_City"].ToString();
                    DateTime T_StartTime = DateTime.Parse(reader["T_StartTime"].ToString());
                    DateTime T_EndTime = DateTime.Parse(reader["T_EndTime"].ToString());
                    string T_Day = reader["T_Day"].ToString();

                    Citizen citizen = new Citizen(C_CPRNR, C_Name, A_Address, A_City);
                    citizen.HomeCare.Add(new Time(T_StartTime, T_EndTime, T_Day));
                    CitizenTimeList.Add(citizen);

                }
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseDB();
            }
            return CitizenTimeList;
        }
        public static void ConnectDB()
        {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }

        public static void CloseDB()
        {
            dbconn.Close();
            dbconn.Dispose();
        }

        public static List<string> GetSensorTypeFromCitizen(string CprNr)
        {
            List<string> SensorTypes = new List<string>();

            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetSensorTypeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CprNr", CprNr));
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

        public static void ConnectSensorToCitizen(int SensorSerialNumber, string CPRNR, string SensorLocation)//NY
        {
            try
            {
                ConnectDB();
                cmd = new SqlCommand("SP_ConnectSensorToCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", SensorSerialNumber));
                cmd.Parameters.Add(new SqlParameter("@CPRNR", CPRNR));
                cmd.Parameters.Add(new SqlParameter("@Location", SensorLocation));
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
    }
}
