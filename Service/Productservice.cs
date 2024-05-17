using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class Productservice : IProductservice
    {
        readonly IProduct _product;
        readonly IInventory inventory;
        public Productservice() {

            _product = new Productrepository();
        }

        // Retrieves and displays detailed information about the product
        public void getproduct()
        {start:
            try
            {
                Console.WriteLine("Enter product id::");
                int prod_id = int.Parse(Console.ReadLine());
                if (prod_id < 101)
                {
                    throw new System.Exception("Product id is not valid");
                   
                }
                List<Products> productdetail = _product.GetProductDetails(prod_id);
                foreach (Products product in productdetail)
                {
                    Console.WriteLine(product);
                }

            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
 
        }


        public void deleteproduct()
        {start:
            try
            {
                Console.WriteLine("Enter product id::");
                int prod_id = int.Parse(Console.ReadLine());
                if (prod_id < 101)
                {
                    throw new System.Exception("Product id is not valid");
                    
                }
                int status = _product.deleteproduct(prod_id);
                if (status > 0)
                {
                    Console.WriteLine("Product deleted");
                }


            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }

        }


        public void alldetail()
        {
            List<Inventory> inventories = inventory.ListAllProducts();
            foreach (Inventory inventory1 in inventories)
            {
                Console.WriteLine(inventory1);
            }
        }

        //Allows updates to product details(e.g., price, description)
        public void updateproduct()
        {start:
            try
            {
                Console.WriteLine("Enter product id:");
                int update_id = int.Parse(Console.ReadLine());
                if(update_id<101)
                {
                    throw new System.Exception("Product id is not valid");
                
                }
                Console.WriteLine("Enter Product Description:");
                string prod_desc = Console.ReadLine();
                Console.WriteLine("Enter Price:");
                int prod_price = int.Parse(Console.ReadLine());
                int status = _product.UpdateProductInfo(update_id, prod_desc, prod_price);
                if (status > 0)
                {
                    Console.WriteLine("Product details get updated successfully");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
              
            }

        public void search()
        {
            Console.WriteLine("Enter category:");
            string category=Console.ReadLine();
            List<Products> products = _product.search(category);
            foreach(Products p in products)
            {
                Console.WriteLine(p);
            }
        }

        //Checks if the product is currently in stock.
        void Isproductavailable()
        {
            Console.WriteLine("Enter product id:");
            int check_id = int.Parse(Console.ReadLine());
            bool check_status = _product.IsProductInStock(check_id);
            if (check_status)
            {
                Console.WriteLine("Product is in stock");
            }
            else
            {
                Console.WriteLine("Not in STOCK!!!");
            }
        }

    }

}

