using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Model
{

    //  Task 2: Class Creation:
    //• Create the classes(Customers, Products, Orders, OrderDetails and Inventory) with the specified
    //attributes.
    //• Implement the constructor for each class to initialize its attributes.
    internal class Products
    {
        int productID;
        string productName;
        string description;
        decimal price;
        string category;

        public Products() { }

        public Products(int productID, string productName, string description, decimal price)
        {
            this.productID = productID;
            this.productName = productName;
            this.description = description;
            this.price = price;
        }

        // Task 3: Encapsulation:
        //• Implement encapsulation by making the attributes private and providing public properties
        //(getters and setters) for each attribute.
        //• Add data validation logic to setter methods (e.g., ensure that prices are non-negative, quantities
        //are positive integers).

        public int ProductID { get {  return productID; } set {  productID = value; } }   
        public string ProductName {  get { return productName; } set { productName = value; } }
        public string Description { get { return description; } set { description = value; } }
        public decimal Price
        {
            get { return price; }

            set
            {
                if (value > 0)
                {
                    price = value;
                }
            }
        }

        public string Category { get { return category; }set { category = value; } }
        public override string ToString()
        {
            return $"Product name:{ProductName}\nDescription:{Description}\nPrice:{Price}";
        }

    }
}
