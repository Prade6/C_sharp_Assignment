using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Model
{
    //  Task 2: Class Creation:
    //• Create the classes(Customers, Products, Orders, OrderDetails and Inventory) with the specified
    //attributes.
    //• Implement the constructor for each class to initialize its attributes.

    internal class OrderDetails:Orders
    {
        int orderDetailID;
        Orders order;//Use composition to reference the Order to which this detail belongs.
        Products product; // Use composition to reference the Product included in the order detail.
        int quantity;

        public OrderDetails() { }

        public OrderDetails(int orderDetailID, Orders order, Products product, int quantity)
        {
            this.orderDetailID = orderDetailID;
            this.order = order;
            this.product = product;
            this.quantity = quantity;
        }

        // Task 3: Encapsulation:
        //• Implement encapsulation by making the attributes private and providing public properties
        //(getters and setters) for each attribute.
        //• Add data validation logic to setter methods (e.g., ensure that prices are non-negative, quantities
        //are positive integers).

        public int OrderDetailID { get {  return orderDetailID; } set {  orderDetailID = value; } }
        public Orders Order { get {  return order; } set { order = value; } }
        public Products Product { get { return product; } set { product = value; } }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value > 0)
                {
                    quantity = value;
                }
            }
        }

        //public override string ToString()
        //{
        //    return $"OrderDetailID:{OrderDetailID}\nProductID:{Product}\nQuantity:{Quantity}";
        //}

        public override string ToString()
        {
            return $"OrderID::{OrderID}\t\tProduct Name::{ProductName}\t\tQuantity::{Quantity}";
        }


    }
}
