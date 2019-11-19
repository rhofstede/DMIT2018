using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
using ChinookSystem.Data.DTOs;
#endregion

namespace ChinookSystem.BLL
{
    public class CustomerController
    {
        public Customer GetCustomer(int customerid)
        {
            using (var context = new ChinookContext())
            {
                return context.Customers.Find(customerid);
            }
        }
    }
}
