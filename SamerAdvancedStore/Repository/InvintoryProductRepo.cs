using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class InvintoryProductRepo :BaseRepo<InventoryProducts>
    {
        public InvintoryProductRepo(AppContext context) : base(context)
        {
        }
    }
}