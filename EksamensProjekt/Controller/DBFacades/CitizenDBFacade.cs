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

                SqlParameter sqlName = new SqlParameter("@C_Name", citizen.Name);
                cmd.Parameters.Add(sqlName);

                SqlParameter sqlCPRNR = new SqlParameter("@C_CPRNR", citizen.CprNr);
                cmd.Parameters.Add(sqlCPRNR);

                SqlParameter sqlGender = new SqlParameter("@C_Gender", citizen.Gender);
                cmd.Parameters.Add(sqlGender);

                SqlParameter sqlAge = new SqlParameter("@C_Age", citizen.Age);
                cmd.Parameters.Add(sqlAge);

                SqlParameter sqlPhoneNumber = new SqlParameter("@C_PhoneNumber", citizen.PhoneNumber);
                cmd.Parameters.Add(sqlPhoneNumber);

                foreach (string s in citizen.Illness) {
                    SqlParameter sqlIllness = new SqlParameter("@I_Name", s);
                    cmd.Parameters.Add(sqlIllness);
                }

                SqlParameter sqlReligion = new SqlParameter("@C_Religion", citizen.Religion);
                cmd.Parameters.Add(sqlReligion);

                //HomeCare skal ligges ind i Time tabllen med CPRNR for den Citizen den er fra.

                foreach (KeyValuePair<string, string> homecare in citizen.HomeCare) {
                    SqlParameter sqlDay = new SqlParameter("@T_Day", homecare.Key);
                    cmd.Parameters.Add(sqlDay);

                    SqlParameter sqlTime = new SqlParameter("@T_TimePeriod", homecare.Value);
                    cmd.Parameters.Add(sqlTime);
                }

                SqlParameter sqlAddress = new SqlParameter("@A_Address", citizen.Address);
                cmd.Parameters.Add(sqlAddress);

                SqlParameter sqlCity = new SqlParameter("@A_City", citizen.City);
                cmd.Parameters.Add(sqlCity);

                //AddRelatives(citizen.Relatives);
                
                SqlCommand cmd2 = new SqlCommand("SP_AddRelative", dbconn);
                cmd2.CommandType = CommandType.StoredProcedure;
                
                foreach (Model.Relative r in citizen.Relatives) {

                    SqlParameter sqlRelativeName = new SqlParameter("@R_Name", r.Name);
                    cmd2.Parameters.Add(sqlRelativeName);

                    SqlParameter sqlRelativeCPRNR = new SqlParameter("@R_CPRNR", r.CprNr);
                    cmd2.Parameters.Add(sqlRelativeCPRNR);

                    SqlParameter sqlRelativeGender = new SqlParameter("@R_Gender", r.Gender);
                    cmd2.Parameters.Add(sqlRelativeGender);

                    SqlParameter sqlRelativeAge = new SqlParameter("@R_Age", r.Age);
                    cmd2.Parameters.Add(sqlRelativeAge);

                    SqlParameter sqlRelativePhoneNumber = new SqlParameter("@R_PhoneNumber", r.PhoneNumber);
                    cmd2.Parameters.Add(sqlRelativePhoneNumber);

                    foreach (KeyValuePair<string, string> notavailable in r.NotAvailable) {
                        SqlParameter sqlDay = new SqlParameter("@T_Day", notavailable.Key);
                        cmd.Parameters.Add(sqlDay);

                        SqlParameter sqlTime = new SqlParameter("@T_TimePeriod", notavailable.Value);
                        cmd.Parameters.Add(sqlTime);
                    }

                    SqlParameter sqlRelativeAddress = new SqlParameter("@A_Address", r.Address);
                    cmd2.Parameters.Add(sqlRelativeAddress);

                    SqlParameter sqlRelativeCity = new SqlParameter("@A_City", r.City);
                    cmd2.Parameters.Add(sqlRelativeCity);

                    SqlParameter sqlRelativeCitizenCPRNR = new SqlParameter("@C_CPRNR", citizen.CprNr);
                    cmd2.Parameters.Add(sqlRelativeCitizenCPRNR);
                }

                cmd.ExecuteNonQuery();

                CloseDB();
            }
            catch (SqlException e) {

                throw new Exception("Error in creating Citizen" + e.Message);
            }
        }

        public static void AddRelatives (List<Model.Relative> relatives) {

            try {
                ConnectDB();

                foreach (Model.Relative r in relatives) {

                    SqlCommand cmd = new SqlCommand("SP_AddRelative", dbconn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter sqlRelativeCitizenCPRNR = new SqlParameter("@C_CPRNR", r.CitizenCprNr);
                    cmd.Parameters.Add(sqlRelativeCitizenCPRNR);

                    SqlParameter sqlRelativeName = new SqlParameter("@R_Name", r.Name);
                    cmd.Parameters.Add(sqlRelativeName);

                    SqlParameter sqlRelativeCPRNR = new SqlParameter("@R_CPRNR", r.CprNr);
                    cmd.Parameters.Add(sqlRelativeCPRNR);

                    SqlParameter sqlRelativeGender = new SqlParameter("@R_Gender", r.Gender);
                    cmd.Parameters.Add(sqlRelativeGender);

                    SqlParameter sqlRelativeAge = new SqlParameter("@R_Age", r.Age);
                    cmd.Parameters.Add(sqlRelativeAge);

                    SqlParameter sqlRelativePhoneNumber = new SqlParameter("@R_PhoneNumber", r.PhoneNumber);
                    cmd.Parameters.Add(sqlRelativePhoneNumber);

                    foreach (KeyValuePair<string, string> notavailable in r.NotAvailable) {
                        SqlParameter sqlDay = new SqlParameter("@T_Day", notavailable.Key);
                        cmd.Parameters.Add(sqlDay);

                        SqlParameter sqlTime = new SqlParameter("@T_TimePeriod", notavailable.Value);
                        cmd.Parameters.Add(sqlTime);

                    }

                    SqlParameter sqlRelativeAddress = new SqlParameter("@A_Address", r.Address);
                    cmd.Parameters.Add(sqlRelativeAddress);

                    SqlParameter sqlRelativeCity = new SqlParameter("@A_City", r.City);
                    cmd.Parameters.Add(sqlRelativeCity);

                    cmd.ExecuteNonQuery();
                }

                CloseDB();
            }
            catch (SqlException e) {
                
                throw new Exception ("Error in adding Relative" + e.Message);
            }
        }

        public static List<string> GetIllnessType(int id) {
            List<string> IllnessList = new List<string>();

            try {
                ConnectDB();

                SqlCommand cmd = new SqlCommand("GetIllnessType", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlId = new SqlParameter("@I_ID", id);
                cmd.Parameters.Add(sqlId);

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.HasRows && rdr.Read()) {
                    string Illness = (string)rdr["I_Name"];
                    IllnessList.Add(Illness);
                }

                CloseDB();

                return IllnessList;

            }
            catch (SqlException e) {
                
                throw new Exception ("Error in getting Illness" + e.Message);
            }
        }

        private static void ConnectDB() {
            dbconn = new SqlConnection(DBHelper._connectionString);
            dbconn.Open();
        }

        private static void CloseDB() {
            dbconn.Close();
        }
    }
}
