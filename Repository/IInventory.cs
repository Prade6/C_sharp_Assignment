using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface IInventory
    {
        public string GetProduct(int id);
        public int GetQuantityInStock(int id);
        public int AddToInventory(int id,int quantity);
        public int RemoveFromInventory(int id,int quantity);
        public int UpdateStockQuantity(int id,int newQuantity);
        public bool IsProductAvailable(int id,int quantitytocheck);
        //public int GetInventoryValue();
        public List<Inventory> ListLowStockProducts(int threshold);
        public List<Inventory> ListOutOfStockProducts();
        public List<Inventory> ListAllProducts();

        public int addinventory(int id,int q,string date);
    }
}
