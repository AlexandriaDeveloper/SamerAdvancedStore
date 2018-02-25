using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{
    public class StatisticsViewModel
    {

    }
    public  class InvintoryStaticViewModel{
        public ICollection<ProfitViewModel> Stocks { get; set; }

        public InvintoryStaticViewModel()
        {
            this.Stocks = new List<ProfitViewModel>();
        }
    }

    public class ProfitViewModel
    {

        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal? TotalProfitLoss { get; set; }

        public int TotalBoughtQuantity { get; set; }
        public int TotalSoldQuantity { get; set; }
        public decimal TotalSold { get; set; }
        public decimal TotalBought { get; set; }
        public int? StockQuantity { get; set; }
        public decimal? StockCoast { get; set; }



        public ICollection<SoldOrdersViewModel> SoldOrders { get; set; }
        public ICollection<BoughtOrderViewModel> BoughtOrders { get; set; }

        public ProfitViewModel()
        {
            SoldOrders = new List<SoldOrdersViewModel>();
            BoughtOrders = new List<BoughtOrderViewModel>();
        }
    }


    public class SoldOrdersViewModel
    {
        public int Id { get; set; }
        public int SoldQuantity { get; set; }
        public decimal SoldPrice { get; set; }
        public int orderId { get; set; }


    }

    public class BoughtOrderViewModel
    {
        public int Id { get; set; }
        public int BoughtQuantity { get; set; }
        public decimal BoughtPrice { get; set; }
        public int? InvintoryId { get; set; }
        public bool Available { get; set; }
    }




    public class ProfitPerOrderViewModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalProfit { get; set; }
        public ICollection<OrderItemViewModel> OrderItemViewModels { get; set; }

        public ProfitPerOrderViewModel()
        {
            this.OrderItemViewModels= new List<OrderItemViewModel>();
        }
    }


    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal?  Discount { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Net { get; set; }

        public  decimal Coast { get; set; }
        public decimal Profit { get; set; }
    }



    public class OrdersStatisticViewModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public ICollection<OrderStatisticViewModel> OrderStatisticViewModels { get; set; }

        public OrdersStatisticViewModel()
        {
            this.OrderStatisticViewModels= new List<OrderStatisticViewModel>();
        }
    }

    public class OrderStatisticViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalOrderCoast { get; set; }
        public decimal TotalOrderProfit { get; set; }
        public decimal OrderNet { get; set; }
        public ICollection<ItemOrderStatisticViewModel> ItemOrderStatisticViewModels { get; set; }

        public OrderStatisticViewModel()
        {
            ItemOrderStatisticViewModels= new List<ItemOrderStatisticViewModel>();
        }
    }


    public class ItemOrderStatisticViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int  SoldQuantity { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalSold { get; set; }
        public decimal ProductCoastAvg { get; set; }
        public decimal OrderTotalCoast { get; set; }
        public decimal Net { get; set; }

    }
}