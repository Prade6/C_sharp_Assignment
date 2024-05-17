using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Service
{
    internal interface Icustomerservice
    {
        void GetCustomerDetails();
        void Registercustomer();
        void updatecustomer();

        void gettotalorder();
    }
}
