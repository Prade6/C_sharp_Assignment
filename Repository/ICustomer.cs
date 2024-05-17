using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface ICustomer
    {
        int CalculateTotalOrders(int id);
        List<Customers> GetCustomerDetails(int id);
        int UpdateCustomerInfo(int id,string e_mail,string add);

        int Customerregister(string fname,string lname,string pno,string email,string address,int order);

    }
}
