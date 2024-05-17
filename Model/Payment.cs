using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Model
{
    internal class Payment
    {
        int payid;
        Orders order;
        decimal amount;
        string mode;

        public Payment() { }

        public int Payid { get { return payid; } set { payid = value; } }
        public Orders Order { get {  return order; } set { order = value; } }
        public decimal Amount { get { return amount; } set { amount = value; } }
        public string Mode { get { return mode; } set { mode = value; } }

    }
}
