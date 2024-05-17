using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface IOrderDetail
    {
        //public int CalculateSubtotal();
        public List<OrderDetails> GetOrderDetailInfo(int id);
        public int UpdateQuantity(int id,int quan);
        public int AddDiscount(int id,decimal dis);

    }
}
