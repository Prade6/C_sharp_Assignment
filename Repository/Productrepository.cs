using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class Productrepository:IProduct
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public Productrepository()                                            //Creating object for connection
        {
            sqlConnection = new SqlConnection(UconnectDb.Getconnectstring());
            sqlCommand = new SqlCommand();
        }

        public int deleteproduct(int id)
        {
            int check = 0;
            try
            {
                sqlCommand.CommandText = "Delete from products where productID=@id";
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                check = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if(check==0)
                {
                    throw new System.Exception("Product is not available");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return check;
        }
        public List<Products> GetProductDetails(int id) {                   //Retrieving detail of product 
            List<Products> products = new List<Products>();
            sqlCommand.CommandText = "Select * from Products";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int i = 0;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (id == (int)reader["ProductID"])
                {
                    Products products1 = new Products();
                    products1.ProductID = (int)reader["ProductID"];
                    products1.ProductName = (string)reader["ProductName"];
                    products1.Description = (string)reader["Description"];
                    products1.Price = (decimal)reader["Price"];
                    products.Add(products1);
                    i = 1;
                    break;
                }
            }
            if (i == 0)
            {
                Console.WriteLine("Product is not available");
            }

            sqlConnection.Close();
            return products;

        }
       public int UpdateProductInfo(int id,string desp,decimal price)           //Updating a product
        {
            int status = 0;
            try
            {
                sqlCommand.CommandText = ("update Products set Description=@user_desp,Price=@prod_price where ProductID=@prod_id");
                sqlCommand.Parameters.Add("@user_desp", SqlDbType.VarChar).Value = desp;
                sqlCommand.Parameters.Add("@prod_price", SqlDbType.Decimal).Value = price;
                sqlCommand.Parameters.Add("@prod_id", SqlDbType.Int).Value = id;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                status = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (status==0)
                {
                    throw new System.Exception("Product id is not available");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            return status;

        }
      public bool IsProductInStock(int id)                                  //Check product is in stock
        {
            sqlCommand.CommandText = "Select*from Inventory";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            bool status = false;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                if (id == (int)sqlDataReader["ProductID"])
                {
                    if ((int)sqlDataReader["QuantityInStock"]>0)
                    {
                        status = true;
                        break;
                        
                    }
                }

            }
            sqlConnection.Close();
            return status;

        }

        public List<Products> search(string category)
        {
            List<Products> products = new List<Products>();
            Products products1 = new Products();
            int available = 0;
            try
            {
                sqlCommand.CommandText = "Select*from products";
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                bool status = false;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (category == (string)sqlDataReader["category"])
                    {
                        available = 1;
                        products1.ProductName = (string)sqlDataReader["productname"];
                        products1.Description = (string)sqlDataReader["description"];
                        products1.Price = (decimal)sqlDataReader["price"];
                        products.Add(products1);
                    }

                }
                sqlConnection.Close();
                if(available==0)
                {
                    throw new System.Exception("Product not available in this category");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return products;
        }

    }
}
