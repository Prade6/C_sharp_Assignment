using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechShop.Model
{
    //  Task 2: Class Creation:
    //• Create the classes(Customers, Products, Orders, OrderDetails and Inventory) with the specified
    //attributes.
    //• Implement the constructor for each class to initialize its attributes.

    internal class Inventory : Products
    {
        int inventoryID;
        Products product;
        int quantityInStock;
        DateTime lastStockUpdate;

        public Inventory() { }

        public Inventory(int inventoryID, Products product, int quantityInStock, string lastStockUpdate)
        {
            this.inventoryID = inventoryID;
            this.product = product;
            this.quantityInStock = quantityInStock;
            this.lastStockUpdate = DateTime.Parse(lastStockUpdate);
        }

        // Task 3: Encapsulation:
        //• Implement encapsulation by making the attributes private and providing public properties
        //(getters and setters) for each attribute.
        //• Add data validation logic to setter methods (e.g., ensure that prices are non-negative, quantities
        //are positive integers).

        public int InventoryID { get { return inventoryID; } set { inventoryID = value; } }
        public Products Product { get { return product; } set { product = value; } }
        public int QuantityInStock
        {
            get { return quantityInStock; }
            set
            {
                if (value > 0)
                {
                    quantityInStock = value;
                }
            }
        }
        public DateTime LastStockUpdate { get { return lastStockUpdate; } set { lastStockUpdate = value; } }

        public override string ToString()
        {
            return $"ProductID::{ProductID}\tProduct name::{ProductName}\tQuantity::{QuantityInStock}\tCategory::{Category}";
        }




    }
}
