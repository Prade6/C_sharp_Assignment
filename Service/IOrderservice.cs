using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Service
{
    internal interface IOrderservice
    {
        void trackstatus();
        void getorder();
        void placeorder();
        void salesreport();
        void cancelorder();

        void payment();
    
    }

}
