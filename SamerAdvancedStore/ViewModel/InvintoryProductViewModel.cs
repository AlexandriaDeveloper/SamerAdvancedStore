using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{
    public class InvintoryProductsViewModel
    {
        public int Id { get; set; }
        public InvintoryViewModel Invintory { get; set; }
        public ICollection<TransactionInViewModel> Transaction { get; set; }

        public InvintoryProductsViewModel()
        {
            this.Transaction = new List<TransactionInViewModel>();
        }


    }

    public class ProductInInvintoryViewModel
    {
        [Key]
        public int Id { get; set; }
       
        public int? InvintoryId { get; set; }
        [Required]
        [StringLength(90)]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        [Index(IsUnique = true)]
        public string ProductCode { get; set; }

        public string Model { get; set; }

        public int Size { get; set; }
        public string Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string State { get; set; }
        public string CategoryName { get; set; }
        //[Required(ErrorMessage = "من فضلك أدخل الكمية ")]
        [Range(0, 99999, ErrorMessage = "يجب أدخال رقم بين 1 او أكبر ")]
        public int? CurrentQuantity { get; set; }

        [Required(ErrorMessage = "من فضلك أدخل الكمية ")]
        [Range(0,99999,ErrorMessage = "يجب أدخال رقم بين 1 او أكبر ")]
        public int? Quantity { get; set; }
        //[Required(ErrorMessage = "من فضلك أدخل قيمة امر الشراء")]
      //  [Range(1, 99999, ErrorMessage = "يجب أدخال رقم بين 1 او أكبر ")]
        public decimal? Coast { get; set; }

   
        public int ProductId { get; set; }
        public decimal CoastPerUnit { get; set; }
    }

}