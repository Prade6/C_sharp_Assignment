using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface IOrder
    {
        public decimal CalculateTotalAmount(int id);
        public List<OrderDetails> GetOrderDetails(int id);
        public int UpdateOrderStatus(int id,string status);
        public int CancelOrder(int id);
        public int deleteorder(int id);
        public  string trackstatus(int id);

        public int placeorder(int id,string date,decimal price,string status,int quantity,int pid);

        public List<OrderDetails> salesreport(string sdate,string edate);
        public int payment(int id, string mode, decimal price);

        
    }
}
