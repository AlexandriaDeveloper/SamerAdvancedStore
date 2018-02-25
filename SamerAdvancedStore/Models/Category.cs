using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(90)]
        [Required]
        public string Name { get; set; }



        public ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products= new Collection<Product>();
        }
    }
}