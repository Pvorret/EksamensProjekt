﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EksamensProjekt.Controller.DBFacades
{
    public static class DBHelper
    {
        static SqlConnection dbconn;
        public static string _connectionString = "Server=ealbd1.eal.local;" + "Database=EJL22_DB;" + "User Id=ejl22_usr;" + "Password=Baz1nga22;";
    }
}
