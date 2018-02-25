using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class TransactionsRepo : BaseRepo<Transactions>
    {
        public TransactionsRepo(AppContext context) : base(context)
        {
        }
    }
}