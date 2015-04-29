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

                //HomeCare skal ligges ind i Time tabllen med CPRNR for den Citizen det er den er fra.

                SqlParameter sqlHomeCare = new SqlParameter("@C_HomeCare", citizen.HomeCare);
                cmd.Parameters.Add(sqlHomeCare);

                SqlParameter sqlAddress = new SqlParameter("@A_Address", citizen.Address);
                cmd.Parameters.Add(sqlAddress);

                SqlParameter sqlCity = new SqlParameter("@A_City", citizen.City);
                cmd.Parameters.Add(sqlCity);

                SqlCommand cmd2 = new SqlCommand("SP_AddRelatives", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;

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

                    //NotAvailable skal ligges ind i Time tabllen med CPRNR for den Relative det er den er fra.

                    SqlParameter sqlRelativeNotAvailable = new SqlParameter("@R_NotAvailable", r.NotAvailable);
                    cmd2.Parameters.Add(sqlRelativeNotAvailable);

                    SqlParameter sqlRelativeAddress = new SqlParameter("@A_Address", r.Address);
                    cmd2.Parameters.Add(sqlRelativeAddress);

                    SqlParameter sqlRelativeCity = new SqlParameter("@A_City", r.City);
                    cmd2.Parameters.Add(sqlRelativeCity);
                }

                cmd.ExecuteNonQuery();

                CloseDB();
            }
            catch (SqlException e) {

                throw new Exception("Error in creating Citizen" + e.Message);
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
