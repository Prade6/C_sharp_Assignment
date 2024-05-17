using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using TechShop.Model;
using TechShop.Repository;
using TechShop.TechShop_App;

namespace TechShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TechShopapplication tech=new TechShopapplication();
            tech.run();

// Task 2: Class Creation:
//• Create the classes(Customers, Products, Orders, OrderDetails and Inventory) with the specified
//attributes.
//• Implement the constructor for each class to initialize its attributes.
//• Implement methods as specified.


            Customers customers = new Customers(1, "Prade", "S", "prade@123", "5678906543", "karur");
            Products products = new Products(101, "ASUS Laptop", "Better battery", 45000);
            Orders orders = new Orders(1, customers, "2024-05-02", 45000);
            OrderDetails details = new OrderDetails(1, orders, products, 2);
            Inventory inventory = new Inventory(1, products, 25, "2024-05-01");




























        }
    }
}
