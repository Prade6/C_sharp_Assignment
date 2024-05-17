using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class Inventoryservice:IInventoryservice
    {
        readonly IInventory _inventory;

        public Inventoryservice()
        {
            _inventory=new Inventoryrepository();
        }

       // A method to update the stock quantity to a new value
        public void updateinventory()
        {
            Console.WriteLine("Enter product id:");
            int id = int.Parse(Console.ReadLine());
            if(id<101)
            {
                Console.WriteLine("Product id should be more than 100");
            }
            Console.WriteLine("Enter quantity:");
            int quan = int.Parse(Console.ReadLine());
            int status = _inventory.UpdateStockQuantity(id, quan);
            if (status > 0)
            {
                Console.WriteLine("Quantity Updated");
            }
            else
            {
                Console.WriteLine("Productid is not valid\nNot updated");
            }
        }

        public void addinventory()
        {
            Console.WriteLine("Enter product id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity:");
            int quan = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter laststockupdate:");
            string date = Console.ReadLine();
            int check = _inventory.addinventory(id,quan,date);
            if (check > 0)
            {
                Console.WriteLine("Quantity Updated");
            }
            else
            {
                Console.WriteLine("Not updated");
            }

        }

        //A method to list all products in the inventory, along with their quantities.
        public void allproduct()
        {
            List<Inventory> inventories = _inventory.ListAllProducts();
            foreach (Inventory inventory1 in inventories)
            {
                Console.WriteLine(inventory1);
            }
        }

        //A method to retrieve the product associated with this inventory item

        void getinventory()
        {
            Console.WriteLine("Enter inventory item id:");
            int id = int.Parse(Console.ReadLine());
            string name = _inventory.GetProduct(id);
            Console.WriteLine($"Product in the provided inventory item :{name}");
        }

        //A method to get the current quantity of the product in stock.


        void getstock()
        {
            Console.WriteLine("enter inventory id:");
            int id = int.Parse(Console.ReadLine());
            int quan = _inventory.GetQuantityInStock(id);
            Console.WriteLine($"quantity in stock:{quan}");
        }



        //A method to check if a specified quantity of the product is available in the inventory.

        void checkstock()
        {
            Console.WriteLine("Enter product id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new quantity:");
            int quantity = int.Parse(Console.ReadLine());
            bool check = _inventory.IsProductAvailable(id, quantity);
            if (check)
            {
                Console.WriteLine("Product available");
            }
        }

    //A method to list products with quantities below a specified threshold, indicating low stock.

        void lowstock()
        {
            Console.WriteLine("Enter low stock limit:");
            int low = int.Parse(Console.ReadLine());
            List<Inventory> inventories = _inventory.ListLowStockProducts(low);
            foreach (Inventory item in inventories)
            {
                Console.WriteLine(item);
            }
        }



    //A method to list products that are out of stock.

        void outofstock()
        {
            List<Inventory> inventories = _inventory.ListOutOfStockProducts();
            foreach (Inventory item in inventories)
            {
                Console.WriteLine(item);
            }
        }

    //A method to add a specified quantity of the product to the inventory.

        void increasequantity()
        {
            Console.WriteLine("Enter product id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity to add:");
            int quantity = int.Parse(Console.ReadLine());
            int status = _inventory.AddToInventory(id, quantity);
            if (status > 0)
            {
                Console.WriteLine("Quantity Updated");
            }
            else
            {
                Console.WriteLine("Not updated");
            }
        }

    //: A method to remove a specified quantity of the product from the inventory.

        void reducequantity()
        {
            Console.WriteLine("Enter product id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity to remove:");
            int quantity = int.Parse(Console.ReadLine());
            int status = _inventory.RemoveFromInventory(id, quantity);
            if (status > 0)
            {
                Console.WriteLine("Quantity Updated");
            }
            else
            {
                Console.WriteLine("Not updated");
            }

        }


    }
}
