using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EksamensProjekt.Controller.DBFacades;

namespace EksamensProjekt.Controller.DBFacades {
    class CitizenDBFacade {

        static SqlConnection dbconn;
        public static void CreateCitizen(Model.Citizen citizen) {
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

                foreach (KeyValuePair<string, int> sensortypeamount in citizen.SensorTypes) {
                    SqlCommand cmd2 = new SqlCommand("SP_AddSensorTypeFromCitizenCPRNR", dbconn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    
                    cmd2.Parameters.Add(new SqlParameter("@C_CPRNR", citizen.CprNr));
                    cmd2.Parameters.Add(new SqlParameter("@ST_Type", sensortypeamount.Key));
                    cmd2.Parameters.Add(new SqlParameter("@CST_AmountNeeded", sensortypeamount.Value));
                }

                cmd.Parameters.Add(new SqlParameter("@A_Address", citizen.Address));

                cmd.Parameters.Add(new SqlParameter("@A_City", citizen.City));

                cmd.ExecuteNonQuery();
                

                CloseDB();
            }
            catch (SqlException e) {

                throw new Exception("Error in creating Citizen" + e.Message);
            }
        }
        public static void AddTime(string cprnr, Dictionary<string, string> times)
        {
            try
            {
                ConnectDB();
                foreach (KeyValuePair<string, string> time in times)
                {
                    SqlCommand cmd = new SqlCommand("SP_AddTime", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CPRNR", cprnr));
                    cmd.Parameters.Add(new SqlParameter("@T_Day", time.Key));
                    cmd.Parameters.Add(new SqlParameter("@T_TimePeriod", time.Value));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error! Kunne ikke tilføje time" + e.Message);
            }
        }

        public static void AddIllnessToCitizen(Model.Citizen citizen)
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
        }

        public static void AddRelative(Model.Citizen c)
        {
            try
            {
                ConnectDB();

                foreach (Model.Relative r in c.Relatives)
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

                    cmd2.Parameters.Add(new SqlParameter("@C_CPRNR", c.CprNr));
                    cmd2.ExecuteNonQuery();

                }
                
            }
            catch (SqlException e)
            {
                throw new Exception("Error in adding Relative" + e.Message);
            }
        }
        public static void AddRelatives (List<Model.Relative> relatives) {

            try {
                ConnectDB();

                foreach (Model.Relative r in relatives) {

                    SqlCommand cmd = new SqlCommand("SP_AddRelative", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@C_CPRNR", r.CitizenCprNr));

                    cmd.Parameters.Add(new SqlParameter("@R_Name", r.Name));

                    cmd.Parameters.Add( new SqlParameter("@R_CPRNR", r.CprNr));

                    cmd.Parameters.Add(new SqlParameter("@R_Gender", r.Gender));

                    cmd.Parameters.Add(new SqlParameter("@R_Age", r.Age));

                    cmd.Parameters.Add(new SqlParameter("@R_PhoneNumber", r.PhoneNumber));

                    foreach (KeyValuePair<string, string> notavailable in r.NotAvailable) {
                        cmd.Parameters.Add(new SqlParameter("@T_Day", notavailable.Key));

                        cmd.Parameters.Add(new SqlParameter("@T_TimePeriod", notavailable.Value));

                    }
                    cmd.Parameters.Add(new SqlParameter("@A_Address", r.Address));

                    cmd.Parameters.Add(new SqlParameter("@A_City", r.City));

                    cmd.ExecuteNonQuery();
                }

                CloseDB();
            }
            catch (SqlException e) {
                
                throw new Exception ("Error in adding Relative" + e.Message);
            }
        }

        public static List<string> GetIllnessType() {
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

        private static void ConnectDB() {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }

        private static void CloseDB() {
            dbconn.Close();
            dbconn.Dispose();
        }

        public static List<Model.Citizen> GetAllCitizen()
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
    }
}
