using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class InvintoryRepo :BaseRepo<Invintorey>
    {
        public InvintoryRepo(AppContext context) : base(context)
        {
        }
    }
}