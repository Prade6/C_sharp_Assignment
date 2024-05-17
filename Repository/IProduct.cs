using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface IProduct
    {
        public List<Products> GetProductDetails(int id);
        public int UpdateProductInfo(int id,string desp,decimal price);
        public bool IsProductInStock(int id);
        public int deleteproduct(int id);
        public List<Products> search(string category);
    }
}
