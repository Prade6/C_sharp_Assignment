using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class Logging:Ilogging
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
 
        public Logging()
        {
            sqlConnection = new SqlConnection(UconnectDb.Getconnectstring());
            sqlCommand = new SqlCommand();
            
        }
        public bool logging(int id, string pass)
        { 
            string a = pass;

            bool digit = a.Any(char.IsDigit);
            bool upper = a.Any(char.IsUpper);
            bool spl = a.Any(char.IsSymbol);
            bool check = false;

            if (a.Length > 6)
            {
                if (digit == true)
                {
                    if (upper == true)
                    {
                        if (a.Contains('#') || a.Contains('@') || a.Contains('&') || a.Contains('*'))
                        {
                            Console.WriteLine("Logging in....");
                            check = true;
                        }
                        else
                        {
                            Console.WriteLine("Should contain special charcter");
                        }
                    }
                    else
                    {

                        Console.WriteLine("Should contain uppercase");

                    }
                }
                else
                {
                    Console.WriteLine("Should contain digit");
                }

            }
            else
            {
                Console.WriteLine("Length should be atleast 8");
            }
            return check;
        }
    }
}
