using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskMaster_Backend.Configurations
{
    public class Database
    {              
            public static string getConnectionString()
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["taskmaster"].ConnectionString;
            }                
    }
}