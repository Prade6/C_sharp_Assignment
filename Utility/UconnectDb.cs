using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Utility
{
    internal static class UconnectDb
    {
        private static IConfiguration _iconfiguration ;  //packages need to be installed for including json file--IConfiguration-building interface
        static UconnectDb()
        {
            Getappsettingfile();
        }
        private static void Getappsettingfile()                 //Adding explicitly a json file
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _iconfiguration= builder.Build();
        }
        public static string Getconnectstring()
        {
            return _iconfiguration.GetConnectionString("Database_connectionString");
        }

    }
}
