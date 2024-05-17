using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exception;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class Orderservice:IOrderservice
    {
        readonly IOrder _order;
        public Orderservice()
        {
            _order = new Orderrepository();
        }

        public void payment()
        {start:
            try
            {
                Console.WriteLine("Enter orderid:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter payment mode:");
                string mode = Console.ReadLine();
                Console.WriteLine("Enter amount:");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                int check = _order.payment(id, mode, price);
                if (check > 0)
                {
                    Console.WriteLine("Payment successful");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
      
        }

        public void trackstatus()
        {start:
            try
            {
                Console.WriteLine("Enter order id to track status::");
                int id = int.Parse(Console.ReadLine());
                string status = _order.trackstatus(id);
                Console.WriteLine(status);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
           
        }

        public void placeorder()
        {
            start:
            try
            {
                Console.WriteLine("Enter customer id:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter productid");
                int pid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter order date:");
                string date = Console.ReadLine();
                Console.WriteLine("Enter quantity");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter amount:");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter status of order:");
                string status = Console.ReadLine();
                if (id == null || date == null || price == null || status == null || pid == null)
                {
                    throw new IncompleteOrderException("Fill all the details");
                }
                int check = _order.placeorder(id, date, price, status, quantity, pid);
                if (check > 0)
                {
                    Console.WriteLine("Order is placed");
                }
                else
                {
                    Console.WriteLine("Order is not placed");
                }
            }
            catch (IncompleteOrderException e)
            {
                Console.WriteLine(e.Message);
                goto start;

            } 
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
        }

        //Cancels the order and adjusts stock levels for products.
        public void cancelorder()
        {start:
            try
            {
                Console.WriteLine("Enter order id:");
                int order_id = int.Parse(Console.ReadLine());
                if(order_id <0)
                {
                    throw new System.Exception("Order cannot be negative");
                }
                int cancel = _order.CancelOrder(order_id);
                
                if (cancel > 0)
                {
                    Console.WriteLine("Order cancelled and stock updated");
                }
                
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

          
        }

            public void salesreport()
        {start:
            try
            {
                Console.WriteLine("Enter start date:");
                string sdate = Console.ReadLine();
                Console.WriteLine("Enter end date:");
                string edate = Console.ReadLine();
                List<OrderDetails> orderDetails = _order.salesreport(sdate, edate);
                foreach (OrderDetails item in orderDetails)
                {
                    Console.WriteLine(item);
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
     
                }

        //Retrieves and displays the details of the order(e.g., product list and quantities)
        public void getorder()
        {
            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            List<OrderDetails> orders = _order.GetOrderDetails(order_id);
            foreach (OrderDetails order in orders)
            {
                Console.WriteLine(order);
            }
        }


        //Calculate the total amount of the order
        void totalamount()
        {
            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            Console.WriteLine($"Total amount of the order::{_order.CalculateTotalAmount(order_id)}");
        }

        //Allows updating the status of the order(e.g., processing, shipped).
        void orderstatus()
        {
        
            Console.WriteLine("Enter order id:");
            int order_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter status of the order:");
            string status = Console.ReadLine();
            int check_status = _order.UpdateOrderStatus(order_id, status);
            if (check_status > 0)
            {
                Console.WriteLine("Status Updated");
            }
            else
            {
                Console.WriteLine("Not updated");
            }
        }

    }
}
