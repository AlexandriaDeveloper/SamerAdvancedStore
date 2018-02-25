using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{


    public class InvintoriesViewModel
    {

        public string Message { get; set; }
        public ICollection<InvintoryViewModel> Invintory { get; set; }
        public InvintoriesViewModel()
        {
            this.Invintory = new List<InvintoryViewModel>();
        }
    }

    public class InvintoryViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "يجب أدخال الأسم ")]
        [StringLength(90,ErrorMessage = "يجب الأ يزيد الأسم عن 90 حرف")]
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }




        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]

        public decimal TotalSum { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Open { get; set; }


        public ICollection<InvintoryProductViewModel> InvintoryProduct { get; set; }

        public InvintoryViewModel()
        {
            this.InvintoryProduct = new List<InvintoryProductViewModel>();
        }


    }

    public class InvintoryProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public int InvintoryId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 500)]
        public int Quantity { get; set; }
        [Required]

        public decimal Coast { get; set; }


        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public decimal CoastPerUnit { get; set; }

        public InvintoryViewModel InvintoryViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
    }


}