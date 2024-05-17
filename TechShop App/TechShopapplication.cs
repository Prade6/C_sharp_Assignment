using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Service;

namespace TechShop.TechShop_App
{
    internal class TechShopapplication
    {
        Icustomerservice customerservice;
        IProductservice productservice;
        IOrderservice orderservice;
        IInventoryservice inventoryservice;
        Iloggingservice loggingservice;

        public TechShopapplication()
        {
            customerservice = new Customerservice();
            productservice = new Productservice();
            orderservice = new Orderservice();
            inventoryservice = new Inventoryservice();
            loggingservice=new loggingservice();
        }

        public void run()
        {
            bool check = false;
            int i = 0;
            Console.WriteLine("*************Welcome to TECHSHOP***************");
        start:
            Console.WriteLine("1.Login\n2.Register");
            try
            {
                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    check = loggingservice.logging();
                    i++;
                }
                else if (option == 2)
                {
                    customerservice.Registercustomer();
                    check = true;
                }
                else
                {
                    Console.WriteLine("LOGIN OR REGISTER");

                }
                if (i == 3)
                {
                    Console.WriteLine("Retry after 1 minute!!");
                    goto start;
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
     

            if (check)
            {
            menu:

                Console.WriteLine("\n************\n1----Product management\n2----Order management\n" +
                    "3----Inventory Management\n4----Sales Reporting\n5----User account management" +
                    "\n6----Payment processing\n7----Product Search and Recommendations\n*******************************");
                try
                {


                    int opt = int.Parse(Console.ReadLine());

                    switch (opt)
                    {
                        case 1:

                            {
                                Console.WriteLine("1.Get specific product detail\n2.Update a product\n3.Inserting product\n4.Deleting product");
                                int op = int.Parse(Console.ReadLine());
                                switch (op)
                                {
                                    case 1:
                                        {
                                            productservice.getproduct();
                                            break;
                                        }
                                    case 2:
                                        {
                                            productservice.updateproduct();
                                            break;
                                        }
                                    case 3:
                                        {
                                            break;
                                        }
                                    case 4:
                                        {
                                            productservice.deleteproduct();
                                            break;
                                        }
                                }
                            }

                            break;
                        case 2:
                            {
                                Console.WriteLine("1.Get order details\n2.Place order\n3.Cancel order\n4.Track order");
                                int op = int.Parse(Console.ReadLine());
                                switch (op)
                                {
                                    case 1:
                                        orderservice.getorder();
                                        break;
                                    case 2:
                                        orderservice.placeorder();
                                        break;
                                    case 3:
                                        orderservice.cancelorder();
                                        break;
                                    case 4:
                                        orderservice.trackstatus();
                                        break;
                                }

                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("1.Add new product to inventory\n2.Update the product");
                                int op = int.Parse(Console.ReadLine());
                                switch (op)
                                {
                                    case 1:
                                        inventoryservice.addinventory();
                                        break;
                                    case 2:
                                        inventoryservice.updateinventory();
                                        break;
                                }

                            }
                            break;
                        case 4:
                            orderservice.salesreport();
                            break;
                        case 5:
                            {
                                Console.WriteLine("1.Get customer detail\n2.Update customer detail");
                                int op = int.Parse(Console.ReadLine());
                                switch (op)
                                {
                                    case 1:
                                        customerservice.GetCustomerDetails();
                                        break;
                                    case 2:
                                        customerservice.updatecustomer();
                                        break;
                                }
                            }
                            break;
                        case 6:
                            Console.WriteLine("Payment mode\n1.Gpay\n2.Cash\n3.Card");
                            orderservice.payment();
                            break;
                        case 7:
                            {
                                inventoryservice.allproduct();
                                productservice.search();

                                break;
                            }



                        default:
                            Console.WriteLine("Enter correct option ");
                            break;
                    }
                    goto menu;
                }

                catch(System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto menu;
                }
            }

            else
            {
                goto start;
            }

        }
      
    }
}
