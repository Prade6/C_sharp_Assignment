using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class Orderdetailservice : Iorderdetailservice
    {
        readonly IOrderDetail _iorderdetailservice;

        public Orderdetailservice()
        {
            _iorderdetailservice = new OrderDetailrepository();
        }

        //Retrieves and displays information about this order detail.
        void getdetail()
        {
            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            List<OrderDetails> orderDetails = _iorderdetailservice.GetOrderDetailInfo(order_id);
            foreach (OrderDetails item in orderDetails)
            {
                Console.WriteLine(item);
            }
        }

        //Allows updating the quantity of the product in this order detail.
        void updatequantity()
        {

            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity:");
            int quan = int.Parse(Console.ReadLine());
            int status = _iorderdetailservice.UpdateQuantity(order_id, quan);
            if (status > 0)
            {
                Console.WriteLine("Quantity Updated");
            }
            else
            {
                Console.WriteLine("Not updated");
            }
        }

        //Applies a discount to this order detail.
        void adddiscount()
        {

            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter discount:");
            decimal dis = Convert.ToDecimal(Console.ReadLine());
            dis = dis / 100;
            int check = _iorderdetailservice.AddDiscount(order_id, dis);
            if (check > 0)
            {
                Console.WriteLine("Discount added");
            }
        }


    }
}
