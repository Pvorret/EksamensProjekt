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
    static class SensorDBFacade
    {
        public static SqlConnection dbconn;
        static SqlCommand cmd;
        static public void CreateSensor(Sensor sensor)
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
                cmd.Parameters.Add("@SerialNumber", sensor.SerialNumber);
                cmd.Parameters.Add("@Type", sensor.Type);
                cmd.Parameters.Add("@activated", activatedToBit);
                cmd.Parameters.Add("@Citizen", "");
                cmd.Parameters.Add("@Location", "");
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
    }
}
