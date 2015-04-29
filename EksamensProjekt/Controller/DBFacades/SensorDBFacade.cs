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
        static SqlConnection SensorSQL = new SqlConnection(DBHelper._connectionString);
        static SqlCommand cmd;
        static public void CreateSensor(Sensor sensor)
        {
            cmd = new SqlCommand("SP_CreateSensor", SensorSQL);
            cmd.CommandType = CommandType.StoredProcedure;

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
                SensorSQL.Open();

                cmd.Parameters.Add("@S_SerialNumber", sensor.SerialNumber);
                cmd.Parameters.Add("@S_Type");
                cmd.Parameters.Add("@S_activated", activatedToBit);
                cmd.Parameters.Add("@S_C_Citizen", "");
                cmd.Parameters.Add("@S_Location", "");

                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error! Sensor not added to database");
            }
            finally
            {
                SensorSQL.Close();
            }
        }
    }
}
