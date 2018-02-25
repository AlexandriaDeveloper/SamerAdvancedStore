using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using SamerAdvancedStore.ViewModel;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Services
{
    public class StatisticService : BaseService
    {
        #region API Methods
        public async Task<InvintoryStaticViewModel> GetInvintory()
        {
            var Stocks = await uow.ProductRepo.GetAllAsync();
            InvintoryStaticViewModel model = new InvintoryStaticViewModel();
            foreach (var stock in Stocks)
            {
                var trans = await GetTransactionItemsPerProduct(stock.Id);
                model.Stocks.Add(BindInvintoryStatisticModelToViewModel(trans, stock));
            }
            return (model);

        }

        public async Task<ProfitPerOrderViewModel> GetOrderProfits(int id)
        {
            var order = await uow.OrderRepo.GetByIdAsync(id);
            return BindProfitPerOrderModelModelToViewModel(order);

        }
        //used
        public async Task<OrdersStatisticViewModel> GetOrders()
        {
            var orders = await uow.OrderRepo.GetAllAsync(includeProperties: "OrderDetailses,OrderDetailses.Product");
            if (orders != null)
            {

                OrdersStatisticViewModel Model = new OrdersStatisticViewModel();
                foreach (var order in orders)
                {
                    Model.OrderStatisticViewModels.Add(await BindOrdersModelToViewModel(order));
                }

                return Model;
            }
            return null;
        }

        public async Task<ProfitViewModel> GetProfits(int id)
        {
            var productData = await uow.ProductRepo.GetByIdAsync(id);

            var Trans = await uow.TransactionsRepo.GetAllAsync(x => x.ProductId == id);

            return (await this.BindProfitViewMode(productData, Trans));


        }
        #endregion

        #region Calculation Methods
        private async Task<IEnumerable<Transactions>> GetTransactionItemsPerOrder(int OrderId)
        {

            return await uow.TransactionsRepo.GetAllAsync(x => x.OrderId == OrderId);

        }
        private async Task<IEnumerable<Transactions>> GetTransactionItemsPerProduct(int ProductId)
        {

            return await uow.TransactionsRepo.GetAllAsync(x => x.ProductId == ProductId);

        }


        private decimal GetProductCoasPerUnit(IEnumerable<Transactions> Trans)
        {
            var productIn = Trans.Sum(x => x.ProductIn.HasValue ? x.ProductIn.Value : 0);
            var coast = Trans.Sum(x => x.Coast.HasValue ? x.Coast.Value : 0);
            if (productIn > 0)
            {
                return Math.Round(coast / productIn, 2);
            }
            else
            {
                return 0;
            }


        }
        private async Task<decimal> GetProductCoasPerUnit(int ProductId)
        {
            var Trans = await GetTransactionItemsPerProduct(ProductId);
            return GetProductCoasPerUnit(Trans);


        }
        private decimal? ProfitByProduct(IEnumerable<Transactions> Trans)
        {
            return Trans.Sum(x => x.Sold) - (SoldCoast(Trans));

        }

        private decimal? SoldCoast(IEnumerable<Transactions> Trans)
        {
            return (GetProductCoasPerUnit(Trans) * Trans.Sum(x => x.ProductOut));
        }
        public async Task<decimal> GetCoastPerOrder(int orderId)
        {
            var Transactions =
                await GetTransactionItemsPerOrder(orderId);
            if (Transactions == null)
                return 0;
            decimal coast = 0;
            foreach (var transactionse in Transactions)
            {
                var avgPerUnit = await GetProductCoasPerUnit(transactionse.ProductId);

                coast += avgPerUnit * transactionse.ProductOut.Value;

            }


            return coast;
        }




        private async Task<decimal> GetOrderCoast(int orderId)
        {

            var result = await GetCoastPerOrder(orderId);

            return result;
        }

        #endregion

        #region Mapping Methods
        //fixing
        private async Task<OrderStatisticViewModel> BindOrdersModelToViewModel(Order order)
        {




            OrderStatisticViewModel viewModel = new OrderStatisticViewModel();

            viewModel.ClientName = order.ClientName;
            viewModel.OrderDate = order.OrderDate;


            foreach (var orderOrderDetailse in order.OrderDetailses)
            {
                var oDetails = await BindOrdersModelToViewModel(orderOrderDetailse);
                viewModel.ItemOrderStatisticViewModels.Add(oDetails);
            }

            //calculationg Totals
            viewModel.OrderNet = viewModel.ItemOrderStatisticViewModels.Sum(x => x.SoldPrice);

            viewModel.TotalOrderProfit = viewModel.ItemOrderStatisticViewModels.Sum(x => x.SoldPrice)
                - viewModel.ItemOrderStatisticViewModels.Sum(x => x.OrderTotalCoast);

            viewModel.TotalOrderCoast = viewModel.ItemOrderStatisticViewModels.Sum(x => x.OrderTotalCoast);

            return viewModel;

        }

        private async Task<ItemOrderStatisticViewModel> BindOrdersModelToViewModel(Transactions orderOrderDetailse)
        {
            var viewModelItem = new ItemOrderStatisticViewModel();

            var OrderCoast = await GetOrderCoast(orderOrderDetailse.OrderId.Value);
            var ProductCoastPerUnit = await GetProductCoasPerUnit(orderOrderDetailse.ProductId);
            var orderSold = orderOrderDetailse.Sold.HasValue ? orderOrderDetailse.Sold.Value : 0;




            var disCount = (orderOrderDetailse.Discount.HasValue
                ? orderOrderDetailse.Discount.Value : 0);
            var orderNet = orderSold - ProductCoastPerUnit - disCount;

            viewModelItem.ProductId = orderOrderDetailse.ProductId;
            viewModelItem.Discount = disCount;
            viewModelItem.ProductCoastAvg = ProductCoastPerUnit;
            viewModelItem.OrderTotalCoast = OrderCoast;
            viewModelItem.Id = orderOrderDetailse.Id;
            viewModelItem.ProductCode = orderOrderDetailse.Product.ProductCode;
            viewModelItem.ProductName = orderOrderDetailse.Product.Name;
            viewModelItem.SoldPrice = orderSold;
            viewModelItem.SoldQuantity = orderOrderDetailse.ProductOut.HasValue
                ? orderOrderDetailse.ProductOut.Value
                : 0;
            viewModelItem.Net = orderSold - OrderCoast;


            return viewModelItem;
        }

        private ProfitViewModel BindInvintoryStatisticModelToViewModel(IEnumerable<Transactions> Trans, Product stock)
        {
            ProfitViewModel profit = new ProfitViewModel();
            profit.ProductId = stock.Id;
            profit.ProductQuantity = stock.Quantity;
            profit.ProductName = stock.Name;
            profit.ProductCode = stock.ProductCode;
            profit.Color = stock.Color;
            profit.PricePerUnit = stock.Price;
            profit.Model = stock.Model;
            profit.Size = stock.Size;
            profit.TotalProfitLoss = ProfitByProduct(Trans);
            return profit;

        }

        private ProfitPerOrderViewModel BindProfitPerOrderModelModelToViewModel(Order order)
        {
            ProfitPerOrderViewModel viewModel = new ProfitPerOrderViewModel();
            viewModel.Id = order.Id;
            viewModel.ClientName = order.ClientName;
            viewModel.OrderDate = order.OrderDate;
            //  viewModel.TotalProfit=

            return viewModel;
        }




        private async Task<ProfitViewModel> BindProfitViewMode(Product productData, IEnumerable<Transactions> Trans)
        {

            ProfitViewModel model = new ProfitViewModel();
            model.ProductId = productData.Id;
            model.ProductCode = productData.ProductCode;
            model.ProductName = productData.Name;
            model.ProductQuantity = productData.Quantity;

            model.TotalSoldQuantity = Trans.Sum(x => x.ProductOut.HasValue ? x.ProductOut.Value : 0);
            model.TotalSold = Trans.Sum(x => x.Sold.HasValue ? x.Sold.Value : 0);

            model.TotalBought = Trans.Sum(x => x.Coast.HasValue ? x.Coast.Value : 0);
            model.TotalBoughtQuantity = Trans.Sum(x => x.ProductIn.HasValue ? x.ProductIn.Value : 0);
            model.StockQuantity = productData.Quantity;

            var stockAvg = await GetProductCoasPerUnit(productData.Id);

            foreach (var transactionse in Trans)
            {
                if (transactionse.State == "IN")
                {
                    model.BoughtOrders.Add(BindBoughtOrderModelToViewModel(transactionse));

                }
                else
                {
                    model.SoldOrders.Add(BindSoldOrdersModelToViewModel(transactionse));
                }
            }
            var currentStock = productData.Quantity;
            model.StockCoast = Math.Round(stockAvg * currentStock, 2);
            model.TotalProfitLoss = ProfitByProduct(Trans);
            return model;
        }



        private BoughtOrderViewModel BindBoughtOrderModelToViewModel(Transactions transactionse)
        {

            BoughtOrderViewModel viewModel = new BoughtOrderViewModel();

            viewModel.Id = transactionse.Id;
            viewModel.InvintoryId = transactionse.InvintoryId;
            viewModel.BoughtQuantity = transactionse.ProductIn.HasValue ? transactionse.ProductIn.Value : 0;
            viewModel.BoughtPrice = transactionse.Coast.HasValue ? transactionse.Coast.Value : 0;

            return viewModel;

        }

        private SoldOrdersViewModel BindSoldOrdersModelToViewModel(Transactions transactionse)
        {
            SoldOrdersViewModel viewModel = new SoldOrdersViewModel();
            viewModel.Id = transactionse.Id;
            viewModel.orderId = transactionse.OrderId.Value;
            viewModel.SoldQuantity = transactionse.ProductOut.HasValue ? transactionse.ProductOut.Value : 0;
            viewModel.SoldPrice = transactionse.Sold.HasValue ? transactionse.Sold.Value : 0;
            return viewModel;
        }

        #endregion
    }
}