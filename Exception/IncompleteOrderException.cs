using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class IncompleteOrderException:ApplicationException
    {
        public IncompleteOrderException(string message) : base(message) { }

    }
}
