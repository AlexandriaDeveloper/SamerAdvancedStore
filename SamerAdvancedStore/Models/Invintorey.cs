using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Invintorey
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }


        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]

         public decimal TotalSum { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Open { get; set; }


        public ICollection<Transactions> Products { get; set; }

        public Invintorey()
        {
            this.Products = new Collection<Transactions>();
        }
    }
}