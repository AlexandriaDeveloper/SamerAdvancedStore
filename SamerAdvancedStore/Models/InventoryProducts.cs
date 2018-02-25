using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class InventoryProducts
    {
        private decimal _CoastPerUnit;
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int? InvintoryId { get; set; }




        [Required]
        [Range(0, 500)]
        public int Quantity { get; set; }



        [Required]
        [Range(0, 500)]
        public int CurrentQuantity { get; set; }
        [Required]

        public decimal Coast { get; set; }

        [Required]
        public decimal CoastPerUnit { get; set; }

        private bool availibility = true;

        [DefaultValue(true)]
        public bool Availibility
        {
            get
            {
                return availibility;
            }
            set
            {
                availibility = value;
            }
        }
        //public Product Products { get; set; }
        //public Invintorey Invintories { get; set; }
    }
}