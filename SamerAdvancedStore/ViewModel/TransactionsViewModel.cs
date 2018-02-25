using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{
    public class TransactionsOrderViewModel
    {
        public int Id { get; set; }
        public OrderViewModel Order { get; set; }
       // public ICollection<TransactionOutViewModel> OrderDetails { get; set; }

        //public TransactionsOrderViewModel()
        //{
        //    this.OrderDetails = new List<TransactionOutViewModel>();
        //}

        public TransactionsOrderViewModel()
        {
            if(Order==null)
            { Order=new OrderViewModel();}
        }
    }

    public class TransactionsInvintoryViewModel
    {
        public int Id { get; set; }
        public InvintoryViewModel Invintory { get; set; }
        public ICollection<TransactionInViewModel> invintoryDetails { get; set; }

        public TransactionsInvintoryViewModel()
        {
            this.invintoryDetails = new List<TransactionInViewModel>();
        }


    }

    public class TransactionInViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? InvintoryId { get; set; }
        [Required]
        public string State
        {
            get { return "IN"; }
            set { value = "IN"; }
        }
        [Required]
        [Range(0, 1000000)]
        public int Quantity { get; set; }

        [Required (ErrorMessage = "يجب أدخال الكميه ")]
        [Range(1, 9999999, ErrorMessage = "يجب أدخال الكميه بين 1 او أكبر ")]
        public int? ProductIn { get; set; }       
        public decimal? Discount { get; set; }
        [Required(ErrorMessage = "أدخل السعر من فضلك")]
        [Range(1, 9999999, ErrorMessage = "يجب أدخال السعر  بين 1 او أكبر ")]
        public decimal? Coast { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
    
        public decimal Price { get; set; }
        [Required(ErrorMessage = "يجب أختيار تصنيف")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal PricePerUnit  {  get; set;  }

    }
    public class TransactionOutViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int? OrderId { get; set; }


        [Required]
        public string State
        {
            get { return "OUT"; }
            set { value = "OUT"; }
        }
        [Required]
        [Range(0, 10000000)]
        public int Quantity { get; set; }
          [Required (ErrorMessage = "يجب أدخال الكميه ")]
        [Range(1, 9999999, ErrorMessage = "يجب أدخال الكميه بين 1 او أكبر ")]
        public int ProductOut { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal? Discount { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }

        public int Size { get; set; }
        public string Color { get; set; }

        [Required(ErrorMessage = "أدخل السعر من فضلك")]
        [Range(1, 9999999, ErrorMessage = "يجب أدخال السعر  بين 1 او أكبر ")]
        public decimal Price { get; set; }

        public decimal? Sold { get; set; }
        [Required (ErrorMessage = "يجب أختيار تصنيف")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


    }

}