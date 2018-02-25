using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Invintory
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone  { get; set; }
        public DateTime CreatedDate { get; set; }
   

        public ICollection<Transactions> InvintoryDetails { get; set; }

        public Invintory()
        {
            this.InvintoryDetails = new Collection<Transactions>();
        }
    }
}