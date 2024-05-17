using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exception;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class Customerrepository:ICustomer
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public Customerrepository()                                            //Creating object for connection
        {
            sqlConnection=new SqlConnection(UconnectDb.Getconnectstring());
            sqlCommand = new SqlCommand();
        }

        //Calculating order placed by specific customer
        public int CalculateTotalOrders(int id)                            
        {
            Customers customers = new Customers();
            try
            {
                sqlCommand.CommandText = "select * from Customers";
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                int i = 0;
                while (reader.Read())
                {
                    if (id == (int)reader["CustomerID"])
                    {
                        customers.Totalorder = (int)reader["Total_Orders"];
                        i = 1;
                        break;
                    }
                }
                if(i==0)
                {
                    throw new AuthenticationException("CustomerId not found");
                }
                sqlConnection.Close();
                
            }
            catch(AuthenticationException e)
            {
                Console.WriteLine(e.Message);
            }
            return customers.Totalorder;
        }

        //Customer registeration
        public int Customerregister(string fname, string lname, string pno, string email, string address,int order)
        {
            sqlCommand.CommandText = "insert into customers values(@FirstName,@LastName,@email,@Phone,@address,@order)";
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
            sqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar).Value = pno;
            sqlCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            sqlCommand.Parameters.Add("@address", SqlDbType.VarChar).Value = address;
            sqlCommand.Parameters.Add("@order", SqlDbType.VarChar).Value = order;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return status;
        }

        //To get customer details
        public List<Customers> GetCustomerDetails(int id)                            
        {
            int check = 0;

            List<Customers> customers = new List<Customers>();
            
             try
            {
                sqlCommand.CommandText = "Select CustomerID,concat(FirstName,' ',LastName) as Customer_name,Email,Phone,Address from Customers";
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (id == (int)reader["CustomerID"])
                    {
                        check = 1;
                        Customers customers1 = new Customers();
                        customers1.FirstName = (string)reader["Customer_name"];
                        customers1.Email = (string)reader["Email"];
                        customers1.Phone = (string)reader["Phone"];
                        customers1.Address = (string)reader["Address"];
                        customers.Add(customers1);
                        break;
                    }
                }
                sqlConnection.Close();
                if(check==0)
                {
                    throw new AuthenticationException("CustomerId not found");
                }
            }
            catch(AuthenticationException e)
            {
                Console.WriteLine(e.Message);
               
            }
          
            return customers;
        }

        //Updating customer detail
        public int UpdateCustomerInfo(int id,string e_mail,string add)                    
        {
            sqlCommand.CommandText = "update Customers set Email=@usermail,[Address]=@useradd where CustomerID=@userid";
            sqlCommand.Parameters.Add("@usermail", SqlDbType.VarChar).Value = e_mail;
            sqlCommand.Parameters.Add("@useradd", SqlDbType.VarChar).Value = add;
            sqlCommand.Parameters.Add("@userid", SqlDbType.Int);
            sqlCommand.Parameters["@userid"].Value = id;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status=sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return status;

         
        }
    }
}
