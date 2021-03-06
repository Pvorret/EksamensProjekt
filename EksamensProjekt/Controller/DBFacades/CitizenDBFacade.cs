﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using System.Windows;

namespace EksamensProjekt.Controller.DBFacades {
    class CitizenDBFacade {
        static SqlConnection dbconn;
        
        private static void ConnectDB() // lavet af Nicolaj
        {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }
        
        private static void CloseDB() // lavet af Nicolaj
        {
            dbconn.Close();
            dbconn.Dispose();
        }
        public static void CreateCitizen(Model.Citizen citizen) // lavet af Nicolaj
        {
            try {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_CreateCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@C_Name", citizen.Name));
                cmd.Parameters.Add(new SqlParameter("@C_CPRNR", citizen.CprNr));
                cmd.Parameters.Add(new SqlParameter("@C_Gender", citizen.Gender));
                cmd.Parameters.Add(new SqlParameter("@C_Age", citizen.Age));
                cmd.Parameters.Add(new SqlParameter("@C_PhoneNumber", citizen.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@C_Religion", citizen.Religion));
                cmd.Parameters.Add(new SqlParameter("@A_Address", citizen.Address));
                cmd.Parameters.Add(new SqlParameter("@A_City", citizen.City));
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e) 
            {
                throw new Exception("Error in creating Citizen" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static void AddSensorTypeToCitizen(string cprNr, Dictionary<string, int> sensorType) // lavet af Phillip
        {
            try {
                ConnectDB();
                foreach (KeyValuePair<string, int> sensortypeamount in sensorType) {

                    SqlCommand cmd = new SqlCommand("SP_AddSensorTypeFromCitizenCprNr", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@C_CPRNR", cprNr));
                    cmd.Parameters.Add(new SqlParameter("@ST_Type", sensortypeamount.Key));
                    cmd.Parameters.Add(new SqlParameter("@CST_AmountNeeded", sensortypeamount.Value));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e) 
            {
                throw new Exception("Error in adding a SensorTypes to a Citizen " + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static void AddTime(string cprNr, List<Model.Time> times) //lavet af Phillip
        {
            try
            {
                ConnectDB();
                foreach (Model.Time time in times)
                {
                    SqlCommand cmd = new SqlCommand("SP_AddTime", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CPRNR", cprNr));
                    cmd.Parameters.Add(new SqlParameter("@T_Day", time.Day));
                    cmd.Parameters.Add(new SqlParameter("@T_StartTime", time.StartTime));
                    cmd.Parameters.Add(new SqlParameter("@T_EndTime", time.EndTime));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke tilføje time" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static void AddIllnessToCitizen(Model.Citizen citizen) //lavet af Phillip
        {
            try
            {
                ConnectDB();
                foreach (string s in citizen.Illness)
                {
                    SqlCommand cmd3 = new SqlCommand("SP_AddIllness", dbconn);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.Add(new SqlParameter("@I_Name", s));
                    cmd3.Parameters.Add(new SqlParameter("@C_CPRNR", citizen.CprNr));
                    cmd3.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error in Add Illness" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static void AddRelative(Model.Citizen citizen) //lavet af Michael
        {
            try
            {
                ConnectDB();
                foreach (Model.Relative r in citizen.Relatives)
                {
                    SqlCommand cmd2 = new SqlCommand("SP_AddRelative", dbconn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@R_CPRNR", r.CprNr));
                    cmd2.Parameters.Add(new SqlParameter("@R_Name", r.Name));
                    cmd2.Parameters.Add(new SqlParameter("@R_Age", r.Age));
                    cmd2.Parameters.Add(new SqlParameter("@R_Gender", r.Gender));
                    cmd2.Parameters.Add(new SqlParameter("@R_PhoneNumber", r.PhoneNumber));
                    cmd2.Parameters.Add(new SqlParameter("@R_Email", r.Email));
                    cmd2.Parameters.Add(new SqlParameter("@A_Address", r.Address));
                    cmd2.Parameters.Add(new SqlParameter("@A_City", r.City));
                    cmd2.Parameters.Add(new SqlParameter("@C_CPRNR", citizen.CprNr));
                    cmd2.ExecuteNonQuery();

                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error in adding Relative" + e.Message);
            }
            finally
            {
                CloseDB();
            }
        }
        public static List<string> GetIllnessType() // lavet af Michael
        {
            List<string> IllnessList = new List<string>();

            try 
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetIllnessType", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read()) 
                {
                    string Illness = (string)rdr["I_Name"];
                    IllnessList.Add(Illness);
                }
            }
            catch (SqlException e) 
            {
                throw new Exception ("Error in getting Illness" + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return IllnessList;

        }
        public static List<Model.Citizen> GetAllCitizen() // lavet af Phillip
        {
            List<Model.Citizen> Citizens = new List<Model.Citizen>();

            try
            {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("SP_GetAllCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read())
                {
                    Citizens.Add(new Model.Citizen(Convert.ToString(rdr["C_Name"]), Convert.ToString(rdr["C_CPRNR"])));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error in getting Citizen" + e.Message);
            }
            finally
            {
                CloseDB();
            }
            return Citizens;
        }
        public static List<Relative> GetRelativeTime(string cprNr)//lavet af Stefan
        {
            List<Relative> RelativeTimeList = new List<Relative>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetRelativeTimeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Citizen_CPRNR", cprNr));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string R_CPRNR = reader["R_CPRNR"].ToString();
                    string R_Name = reader["R_Name"].ToString();
                    string R_Phone = reader["R_Phonenumber"].ToString();
                    string A_Address = reader["A_Address"].ToString();
                    string A_City = reader["A_City"].ToString();

                    DateTime T_StartTime = Convert.ToDateTime(reader["T_StartTime"]);
                    DateTime T_EndTime = Convert.ToDateTime(reader["T_EndTime"]);
                    string T_Day = reader["T_Day"].ToString();

                    Relative relative = new Relative(R_Name, R_CPRNR, R_Phone, A_Address, A_City);
                    List<Time> time = new List<Time>();
                    time.Add(new Time(T_StartTime, T_EndTime, T_Day));
                    relative.NotAvailableTimes = time;
                    RelativeTimeList.Add(relative);
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
            return RelativeTimeList;
        }
        public static List<Citizen> GetCitizenTime(int serialNumber)//lavet af Stefan
        {
            List<Citizen> citizenList = new List<Citizen>();
            Citizen citizen = new Citizen();
            try
            {
            
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetCitizenTimeFromSensor", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read() && reader.HasRows)
                {
                    string C_CPRNR = reader["C_CPRNR"].ToString();
                    string C_Name = reader["C_Name"].ToString();
                    string A_Address = reader["A_Address"].ToString();
                    string A_City = reader["A_City"].ToString();
                    DateTime T_StartTime = Convert.ToDateTime(reader["T_StartTime"]);
                    DateTime T_EndTime = Convert.ToDateTime(reader["T_EndTime"]);
                    string T_Day = reader["T_Day"].ToString();
                    citizen = new Citizen(C_CPRNR, C_Name, A_Address, A_City);
                    citizen.HomeCareTimes.Add(new Time(T_StartTime, T_EndTime, T_Day));
                    citizenList.Add(citizen);
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
            return citizenList;
        }
        public static List<Relative> GetRelativeFromCitizen(string cprNr) // lavet af Phillip
        {
            List<Relative> relatives = new List<Relative>();
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand("SP_GetRelativeTimeFromCitizen", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Citizen_CPRNR", cprNr));
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read() && rdr.HasRows)
                {
                    if (relatives.Count != 0)
                    {
                        for (int i = 0; i < relatives.Count; i++)
                        {
                            if (relatives[i].CprNr != Convert.ToString(rdr["R_CPRNR"]))
                            {
                                relatives.Add(new Relative(Convert.ToString(rdr["R_Name"]), Convert.ToString(rdr["R_CPRNR"])));
                            }
                        }
                    }
                    else
                    {
                        relatives.Add(new Relative(Convert.ToString(rdr["R_Name"]), Convert.ToString(rdr["R_CPRNR"])));
                    }   
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
    }
}
