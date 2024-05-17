using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exception;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class Orderrepository:IOrder
    {
        
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
        IInventory inventory;
        

    public Orderrepository()
            {
                sqlConnection = new SqlConnection(UconnectDb.Getconnectstring());
                sqlCommand = new SqlCommand();
            inventory=new Inventoryrepository();
            }
    public decimal CalculateTotalAmount(int id)
        {
            sqlCommand.CommandText = "Select * from Orders";
            sqlCommand.Connection=sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Orders orders = new Orders();
            while(reader.Read())
            {
                if(id == (int)reader["OrderID"])
                {
                    orders.TotalAmount = (decimal)reader["TotalAmount"];
                    break;
                }
            }
            sqlConnection.Close();
            return orders.TotalAmount;
        
        }


       public List<OrderDetails> GetOrderDetails(int id)
        {
            bool check=false;
            List<OrderDetails> orders = new List<OrderDetails>();
            sqlCommand.CommandText = "select o.OrderID,p.ProductName,od.Quantity from Orders o " +
                "join OrderDetails od on o.OrderID=od.OrderID " +
                "join Products p on p.ProductID=od.ProductID";
                
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            OrderDetails orderDetails = new OrderDetails();
            while (reader.Read())
            {
                if (id == (int)reader["OrderID"])
                { 
                    orderDetails.OrderID = (int)reader["OrderID"];
                    orderDetails.ProductName = (string)reader["ProductName"];
                    orderDetails.Quantity = (int)reader["Quantity"];
                    orders.Add(orderDetails);
                    check = true;
                    break;
                }
            }
            sqlConnection.Close();
            if(!check)
            {
                Console.WriteLine("Order id not available");
            }
            return orders;
        }

        public int UpdateOrderStatus(int id,string status) {

                sqlCommand.CommandText = "update Orders set status=@orderstatus where OrderID=@id";
                sqlCommand.Parameters.Add("@orderstatus", SqlDbType.VarChar).Value = status;
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                int check = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
         
            return check;
        }


        public int CancelOrder(int id)////
        {
            int update,pid=0,quantity=0,delete;
            sqlCommand.CommandText = "select * from orderdetails od join orders o on o.orderid=od.orderid where o.orderid=@id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (id == (int)sqlDataReader["OrderID"])
                {
                    quantity = (int)sqlDataReader["Quantity"];
                    pid = (int)sqlDataReader["Productid"];
                    break;
                }
              
            }

            sqlConnection.Close();
            update = inventory.AddToInventory(pid, quantity);
            delete = deleteorder(id);
            return delete;
        }

        public int deleteorder(int id)
        {
            sqlCommand.CommandText = "Delete from orders where orderID=@oid";
            sqlCommand.Parameters.Add("@oid", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int check = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "Delete from orderdetails where orderID=@oid";
            sqlConnection.Close();
            return check;
        }

        public string trackstatus(int id)
        {
            Orders orders = new Orders();
            sqlCommand.CommandText = "select * from orders";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (id == (int)reader["OrderID"])
                {
                    orders.Status = (string)reader["status"];
                    break;
                }
            }
            sqlConnection.Close ();
            return orders.Status;

        }

        public int placeorder(int id,string date,decimal price,string status,int quantity,int pid)
        {
            int check = 0;
            bool available=inventory.IsProductAvailable(pid, quantity);
            if(available)
            {
                sqlCommand.CommandText = "insert into orders values(@id,@date,@price,@status)";
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
                sqlCommand.Parameters.Add("@price", SqlDbType.Decimal).Value = price;
                sqlCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                sqlCommand.Parameters.Add("@pid", SqlDbType.Int).Value = id;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.CommandText = "insert into orderdetails values(@id,@pid,@quantity)";
                check = sqlCommand.ExecuteNonQuery();
                inventory.RemoveFromInventory(pid, quantity);
                sqlConnection.Close();
            }
 
            return check;
        }

        public List<OrderDetails> salesreport(string sdate, string edate)
        {
            OrderDetails orderDetails = new OrderDetails();
            List<OrderDetails> orderDetails1 = new List<OrderDetails>();
            sqlCommand.CommandText = "select * from Orders join orderdetails on orders.orderid=orderdetails.orderid where orderdate between @sdate and @edate";
            sqlCommand.Parameters.Add("@sdate",SqlDbType.VarChar).Value=sdate;
            sqlCommand.Parameters.Add("@edate",SqlDbType.VarChar).Value=edate;   
            sqlCommand.Connection= sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                orderDetails.OrderID = (int)reader["orderid"];
                orderDetails.ProductName = (string)reader["productname"];
                orderDetails.OrderDate = (DateTime)reader["orderdate"];
                orderDetails.Quantity = (int)reader["quantity"];
                orderDetails1.Add(orderDetails);
            }
            sqlConnection.Close();
            return orderDetails1;
        }

        public int payment(int id, string mode, decimal price)
        {
            int check = 0,pay=0;
            bool status = orderexists(id);
            if(status)
            {
                try
                {
                    sqlCommand.CommandText = "select*from orders where orderid=@id";
                    sqlCommand.CommandText = "insert into payment values(@id,@amount,@mode)";
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    sqlCommand.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode;
                    sqlCommand.Parameters.Add("@amount", SqlDbType.Decimal).Value = price;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while(reader.Read())
                    {
                        if (price == (int)reader["TotalAmount"])
                        {

                            pay = 1;
                        }
                    }
                    check = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (check == 0 && pay==1)
                    {
                        throw new PaymentFailedException("Payment Failed");
                    }
                }
                catch (PaymentFailedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Order not available");
            }

            return check;

        }

        public bool orderexists(int id)
        {
            bool status = false;
            sqlCommand.CommandText = "select * from orders";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["OrderID"] == id)
                {
                    status = true;
                    break;
                }

            }
            sqlConnection.Close();
            return status;
        }
    }
}
