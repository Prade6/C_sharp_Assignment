using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exception;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class Inventoryrepository:IInventory
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public Inventoryrepository()
        {
            sqlConnection= new SqlConnection(UconnectDb.Getconnectstring());
            sqlCommand = new SqlCommand();
        }
        public string GetProduct(int id)                        //To retrieve product of inventory id
        {
            Products products = new Products();
            sqlCommand.CommandText = "Select * from products join Inventory on products.productID=Inventory.ProductID";
            sqlCommand.Connection= sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["InventoryID"] == id)
                {
                    products.ProductName = (string)reader["productname"];
                    break;
                }
            }
            sqlConnection.Close();
            return products.ProductName; 
        }

        public int GetQuantityInStock(int id) {                             //To retrieve stock quantity of product

            Inventory inventory = new Inventory();
            sqlCommand.CommandText = "select*from Inventory";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["InventoryID"] == id)
                {
                    inventory.QuantityInStock = (int)reader["QuantityInStock"];
                    break;
                }
            }
            
            sqlConnection.Close();
            return inventory.QuantityInStock; 
        }

        public int AddToInventory(int id,int quantity) {                        //to add quantity to stock

            int check = 0;
            sqlCommand.CommandText = "update inventory set quantityinstock=quantityinstock+@quantity where productid=@id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            check = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return check;
        }
        
        public int RemoveFromInventory(int id,int quantity)     ////////                       //to remove quantity from stock
        {
            int check = 0;
            try
            {
               bool status= IsProductAvailable(id,quantity);
                if(status)
                {
                    sqlCommand.CommandText = "update inventory set quantityinstock=quantityinstock-@quantity where productid=@id";
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    check = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
               
               
            }
            catch(ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
           return check;
        }
        public int UpdateStockQuantity(int id,int newQuantity) {                        //To update the stock

            sqlCommand.CommandText = "update Inventory set QuantityInStock=@quantity where ProductID=@id";
            sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = newQuantity;
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return status;
            }
        public bool IsProductAvailable(int id,int quantitytocheck) {
            bool status = false;                                                        //to check the product available
            try
            {
                sqlCommand.CommandText = "select * from inventory";
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                while (reader.Read())
                {
                    if ((int)reader["ProductID"] == id)
                    {
                        if (quantitytocheck < (int)reader["QuantityInStock"])
                        {
                            status = true;
                            break;
                        }

                    }

                }
                sqlConnection.Close();
                if(status==false) 
                {
                    throw new InsufficientStockException("Stock is not available");
                }
            }
 
            catch(InsufficientStockException e)
            {
                Console.WriteLine(e.Message);
            }

            return status;
        }
     

        //public int GetInventoryValue() { return 0; }
        public List<Inventory> ListLowStockProducts(int threshold)                  //to check the product below the limit
        {
            List<Inventory> inventory = new List<Inventory>();
            sqlCommand.CommandText = "select*from inventory join products on products.productid=inventory.productid";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Inventory inventory1 = new Inventory();
                if ((int)reader["QuantityInStock"]<threshold)
                {
                    inventory1.ProductName = (string)reader["Productname"];
                    inventory.Add(inventory1);
                }
               
            }
            sqlConnection.Close();
            return inventory ; }
        public List<Inventory> ListOutOfStockProducts() {                               //to list the out of stock product
            List<Inventory> inventory = new List<Inventory>();
            sqlCommand.CommandText = "select*from inventory join products on products.productid=inventory.productid";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Inventory inventory1 = new Inventory();
                if ((int)reader["QuantityInStock"] <1)
                {
                    inventory1.ProductName = (string)reader["Productname"];
                    inventory.Add(inventory1);
                }

            }
            sqlConnection.Close();
            return inventory;
        }
        
        public List<Inventory> ListAllProducts() {                      //to list all details of product
            List<Inventory> inventory = new List<Inventory>();
            sqlCommand.CommandText = "select*from inventory join products on products.productid=inventory.productid";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Inventory inventory1 = new Inventory();
                inventory1.ProductID= (int)reader["productid"];
                inventory1.ProductName = (string)reader["productname"];
                inventory1.QuantityInStock = (int)reader["quantityinstock"];
                inventory1.Category = (string)reader["category"];
                inventory.Add(inventory1);
            }
            sqlConnection.Close();
            return inventory;
             }

        public int addinventory(int id,int quantity,string date)
        {
            int status = 0;
            bool check=productexists(id);
            if(check)
            {
                try
                {
                    sqlCommand.CommandText = "insert into inventory values (@id,@quantity,@date)";
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    sqlCommand.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    status = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Product exists");
            }
    
          
            return status;
        }

        public bool productexists(int id)
        {
            bool status = true;
            sqlCommand.CommandText = "select * from inventory";
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["ProductID"] == id)
                {
                        status = false;
                        break;
                }

            }
            sqlConnection.Close();
            return status;
        }
    }
}
