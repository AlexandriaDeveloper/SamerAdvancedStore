using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [StringLength(90)]
        public string ClientName { get; set; }
        public DateTime  OrderDate { get; set; }
        public bool Open { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; set; }
        public ICollection<Transactions> OrderDetailses { get; set; }
        public Order()
        {
            this.OrderDetailses= new Collection<Transactions>();
        }
    }
}