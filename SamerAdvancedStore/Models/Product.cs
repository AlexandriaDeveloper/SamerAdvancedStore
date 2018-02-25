using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(90)]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        [Index(IsUnique = true)]
        public string ProductCode { get; set; }

        public string Model  { get; set; }

        public int Size { get; set; }
        public string Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        
        public int Quantity { get; set; }
        public bool Avilibility { get; set; }
        public Category Category { get; set; }
        public ICollection<Transactions> OrderDetailses { get; set; }
        //public ICollection<InventoryProducts> Invintory { get; set; }

        public Product()
        {
            this.OrderDetailses= new Collection<Transactions>();
            //this.Invintory = new Collection<InventoryProducts>();

        }


    }
}