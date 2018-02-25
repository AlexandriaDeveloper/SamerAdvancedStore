using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class OrderRepo:BaseRepo<Order>
    {
        public OrderRepo(AppContext context) : base(context)
        {
        }
    }
}