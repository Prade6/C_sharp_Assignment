using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{ 
    internal class Customerservice:Icustomerservice
    {
        readonly ICustomer _customerrepository;

        public Customerservice()
        {
            _customerrepository = new Customerrepository();
        }

        //Registering new customer
        public void Registercustomer()
        {start:
            try
            {
                Console.WriteLine("Enter firstname::");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter lastname::");
                string lname = Console.ReadLine();
                Console.WriteLine("Enter address::");
                string address = Console.ReadLine();
                Console.WriteLine("Enter email::");
                string email = Console.ReadLine();
                Console.WriteLine("Enter phone number::");
                string pno = Console.ReadLine();
                Console.WriteLine("Enter total order:");
                int order=int.Parse(Console.ReadLine());
                if (pno.Length<10)
                {
                    throw new InvalidDataException("Phone number must be of length 10");
                }
                if (email.Contains('@'))
                {
                    if (email.Contains("gmail.com"))
                    {
                        if (!email.Contains(" "))
                        {

                        }
                        else
                        {
                            throw new InvalidDataException("email should not contain space");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("email is not valid");
                    }

                }
                else
                {
                    throw new InvalidDataException("email is not valid");
                }
                int status = _customerrepository.Customerregister(fname, lname, pno, email, address,order);
                if (status > 0)
                {
                    Console.WriteLine("Customer details get updated successfully");
                }
                else
                {
                    Console.WriteLine("Not updated");
                }
            }
            catch (InvalidDataException e)
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

        // Retrieves and displays detailed information about the customer.
        public void GetCustomerDetails()
        {
            eg: try
            {
                Console.WriteLine("Enter customer id::");
                int detail_id = int.Parse(Console.ReadLine());
                if (detail_id < 0)
                {
                    throw new InvalidDataException("Customer id can't be negative");
                }
                List<Customers> customer_info = _customerrepository.GetCustomerDetails(detail_id);
                foreach (Customers customer in customer_info)
                {
                    Console.WriteLine(customer);
                }
                if(customer_info.Count ==0)
                {
                    Console.WriteLine("Retry!!");
                    goto eg;
                }
            }
            catch(InvalidDataException e)
            {
                Console.WriteLine(e.Message);
                goto eg;
            }
    

        }

        //Allows the customer to update their information (e.g., email, phone, or address)
        public void updatecustomer()
        {
            try
            {
                Console.WriteLine("Enter customer id::");
                int update_cus_id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter address::");
                string address = Console.ReadLine();
                Console.WriteLine("Enter email::");
                string email = Console.ReadLine();
                if (email.Contains('@'))
                {
                    if (email.Contains("gmail.com"))
                    {
                        if (!email.Contains(" "))
                        {

                        }
                        else
                        {
                            throw new InvalidDataException("email should not contain space");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("email is not valid");
                    }

                }
                else
                {
                    throw new InvalidDataException("email is not valid");
                }
                int status = _customerrepository.UpdateCustomerInfo(update_cus_id, email, address);
                if (status > 0)
                {
                    Console.WriteLine("Customer details get updated successfully");
                }
                else
                {
                    Console.WriteLine("Invalid customer id");
                }
            }

            catch (InvalidDataException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Calculates the total number of orders placed by this customer
       public void gettotalorder()
        {
            Console.WriteLine("Enter customer id::");
            int cus_id = int.Parse(Console.ReadLine());
            int Total_order = _customerrepository.CalculateTotalOrders(cus_id);
            Console.WriteLine($"\n\n\nTotal Order placed by the customer{cus_id} is ={Total_order}");
        }
       

    }
}
