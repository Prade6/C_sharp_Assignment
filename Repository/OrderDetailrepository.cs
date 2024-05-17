using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class OrderDetailrepository:IOrderDetail
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        public OrderDetailrepository()
        {
            sqlConnection=new SqlConnection(UconnectDb.Getconnectstring());
            sqlCommand = new SqlCommand();
        }

        //public int CalculateSubtotal() { return 0; }
        public List<OrderDetails> GetOrderDetailInfo(int id)            //To display details of order
        {
            List<OrderDetails> orderDetails=new List<OrderDetails>();  // List to store data from db
            sqlCommand.CommandText = "Select*from OrderDetails";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                if ((int)reader["OrderDetailID"]==id)
                {
                    OrderDetails orderDetails1 = new OrderDetails();                //Create obj for the cls to store the data
                    orderDetails1.Product = (Products)reader["ProductID"];
                    orderDetails1.Quantity = (int)reader["Quantity"];
                    orderDetails.Add(orderDetails1 );
                    break;
                }
            }
            sqlConnection.Close();  
            return orderDetails;
        }
        
        public int UpdateQuantity(int id,int quan)                                //To update quantity in order detail
        {
            sqlCommand.CommandText = "update OrderDetails set quantity=@quantity where OrderDetailID=@id";
            sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quan;
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return status;
        }

        public int AddDiscount(int id,decimal dis)///                              //To add discount to amount in order
        {
            sqlCommand.CommandText = "update Orders set TotalAmount=(TotalAmount*@dis)+TotalAmount " +
                "join OrderDetails on OrderDetails.orderID=Orders.orderID where OrderDetailID=@id";
            sqlCommand.Parameters.Add("@dis", SqlDbType.Decimal).Value = dis;
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return status;
         }

    }
}
