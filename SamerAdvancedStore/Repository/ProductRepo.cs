using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class ProductRepo:BaseRepo<Product>
    {
  
        public ProductRepo(AppContext context) : base(context)
        {
       
        }

      
    }
}