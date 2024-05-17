using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exception;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class loggingservice: Iloggingservice
    {
        readonly Ilogging _logging_Info;

        public loggingservice()
        {
            _logging_Info = new Logging();
        }
        public bool logging()
        {
            bool check = false;
            try
            {
                Console.WriteLine("Enter customer id:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                check = _logging_Info.logging(id, password);
                if (check)
                {
                    Console.WriteLine("Sucessfully logged");
                }
                else
                {
                    throw new LogininvalidException("Incorrect id or password");
                }
            }
            catch(LogininvalidException e)
            {
                Console.WriteLine(e.Message);
            } 
            return check;
        }

    }
}
