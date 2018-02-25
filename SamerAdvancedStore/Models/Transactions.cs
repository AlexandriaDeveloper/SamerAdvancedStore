using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }

        public int? OrderId { get; set; }


        public int? InvintoryId { get; set; }


        [Required]
        public string State { get; set; }

        [Range(1, 1000)]
        public int? ProductIn { get; set; }
        [Range(1, 1000)]
        public int? ProductOut { get; set; }


        public decimal PricePerUnit { get; set; }
        public decimal? Discount { get; set; }

        public decimal? Coast { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public decimal? Sold { get; set; }

        public Product Product { get; set; }
        public Invintorey Invintory { get; set; }

        public Order Order { get; set; }

    }
}