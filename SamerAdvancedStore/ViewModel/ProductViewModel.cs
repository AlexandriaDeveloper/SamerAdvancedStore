using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{
    public class ProductViewModel
    {

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "يجب إدخال أسم المنتج ")]
        [StringLength(90,ErrorMessage = "يجب الا يتعدى الاسم 90 حرف")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "من فضلك أدخل كود المنتج")]
        [StringLength(12)]
        [Index(IsUnique = true)]
        public string ProductCode { get; set; }

        public string Model { get; set; }

        public int? Size { get; set; }
        public string Color { get; set; }

        [Required(ErrorMessage = "يجب إدخال سعر البيع")]
        [Range(1,99999,ErrorMessage = "يجب إدخال السعر اكبر من 1 جنيه")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "من فضلك أختار تصنيف للمنتج ")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal? CoastPerUnit { get; set; }

    
        public bool Avilibility { get; set; }
        public int Quantity { get; set; }
    }

    public class IndexProductViewMode
    {
        public string message { get; set; }
        public List<ProductViewModel> Products{ get; set; }

        public IndexProductViewMode()
        {
            this.Products = new List<ProductViewModel>();
        }

    }


}