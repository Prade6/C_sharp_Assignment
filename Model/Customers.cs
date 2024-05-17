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
    internal class Customers
    {
        int customerID;
        string firstName;
        string lastName;
        string email;
        string phone;
        string address;
        int totalorder;

       public Customers() { }

        public Customers(int id,string fname,string lName,string e_mail,string p_no,string add) {
        
            customerID = id;
            firstName = fname;
            lastName = lName;
            email = e_mail;
            phone = p_no;
            address = add;
        }

// Task 3: Encapsulation:
//• Implement encapsulation by making the attributes private and providing public properties
//(getters and setters) for each attribute.
//• Add data validation logic to setter methods (e.g., ensure that prices are non-negative, quantities
//are positive integers).

        public int CustomerID { get { return customerID; } set { customerID = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Address { get { return address; } set { address = value; } }
        public int Totalorder { get { return totalorder; }set { totalorder = value; } }


        public override string ToString()
        {
            return $"Customer_name::{FirstName}\tEmail::{Email}\tPhone_Number::{Phone}\tAddress::{address}";
        }

    }
}
