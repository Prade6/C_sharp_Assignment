using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class AuthenticationException:ApplicationException
    {
        public AuthenticationException(string message) : base(message) { }
    }
}
