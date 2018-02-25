using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{


    public class OrdersViewModel
    {

        public string Message { get; set; }
        public ICollection<OrderViewModel> Orders { get; set; }
        public OrdersViewModel()
        {
            this.Orders = new List<OrderViewModel>();
        }
    }

    public class OrderViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "يجب أدخال اسم العميل")]
        [StringLength(90,ErrorMessage = "  اسم العميل يجب الا يزيد عن 90 حرف")]
        public string ClientName { get; set; }


        


        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public bool Open { get; set; }


        public ICollection<TransactionOutViewModel>  OrderDetails { get; set; }

        public OrderViewModel()
        {
            this.OrderDetails = new List<TransactionOutViewModel>();
        }


    }

   
}