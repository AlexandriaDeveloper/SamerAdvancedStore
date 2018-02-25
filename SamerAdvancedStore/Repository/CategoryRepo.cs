using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class CategoryRepo:BaseRepo<Category>
    {
        public CategoryRepo(AppContext context) : base(context)
        {
        }
    }
}