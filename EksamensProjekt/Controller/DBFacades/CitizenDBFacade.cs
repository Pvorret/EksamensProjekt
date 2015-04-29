using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EksamensProjekt.Controller.DBFacades {
    class CitizenDBFacade {
        static SqlConnection dbconn;
        public void CreateCitizen(Model.Citizen citizen) {
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

                SqlParameter sqlReligion = new SqlParameter("@C_Religion", citizen.Religion);
                cmd.Parameters.Add(sqlReligion);

                //HomeCare skal ligges ned i time med CPRNR for den Citizen

                SqlParameter sqlHomeCare = new SqlParameter("@C_HomeCare", citizen.HomeCare);
                cmd.Parameters.Add(sqlHomeCare);

                SqlParameter sqlAddress = new SqlParameter("@A_Address", citizen.Address);
                cmd.Parameters.Add(sqlAddress);

                SqlParameter sqlCity = new SqlParameter("@A_City", citizen.City);
                cmd.Parameters.Add(sqlCity);

                SqlCommand cmd2 = new SqlCommand("SP_AddRelatives", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.ExecuteNonQuery();

                CloseDB();
            }
            catch (Exception) {
                
                throw;
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
