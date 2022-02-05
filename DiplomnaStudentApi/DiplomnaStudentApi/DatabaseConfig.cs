using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi
{
    public class DatabaseConfig
    {
        public static string DbConnectionString = @"mongodb://root:bogdanpassword@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false";
        public static string DbName = "hostelDb";
    }
}
