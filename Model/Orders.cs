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

    internal class Orders:Products
    {
        int orderID;
        Customers cus_id; //Use composition to reference the Customer who placed the order.
        DateTime orderDate;
        decimal totalAmount;
        string status;
        string paymentmode;

        public Orders() { }

        public Orders(int orderID, Customers customer, string orderDate, decimal totalAmount)
        {
            this.orderID = orderID;
            this.cus_id = customer;
            this.orderDate = DateTime.Parse(orderDate);
            this.totalAmount = totalAmount;

        }

        // Task 3: Encapsulation:
        //• Implement encapsulation by making the attributes private and providing public properties
        //(getters and setters) for each attribute.
        //• Add data validation logic to setter methods (e.g., ensure that prices are non-negative, quantities
        //are positive integers).

        public int OrderID { get { return orderID; } set { orderID = value; } }
        public Customers Customer { get { return cus_id; } set { cus_id = value; } }
        public DateTime OrderDate { get {  return orderDate; } set { orderDate = value; } }
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                if (value > 0)
                {
                    totalAmount = value;
                }
            }
        }

        public string Status {  get { return status; } set {  status = value; } }

        public string Paymentmode { get { return paymentmode; } set { paymentmode = value; } }

   




    }
}
