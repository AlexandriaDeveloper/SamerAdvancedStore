using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Services
{
    public class OrderDetailsService : BaseService
    {
        private OrderService OService = null;
        public OrderDetailsService()
        {
            OService = new OrderService();
        }
        public async Task<TransactionsOrderViewModel> GetOrderDetalsies(int id)
        {
            var order = await uow.OrderRepo.GetByIdAsync(id);
            if (order != null)
            {


                var orderDetailsies = await uow.TransactionsRepo.GetAllAsync(x => x.OrderId == id);
                foreach (var orderDetails in orderDetailsies)
                {
                    var productData = await uow.ProductRepo.GetByIdAsync(orderDetails.ProductId);
                //    orderDetails.Product = productData;
                   // order.OrderDetailses.Add(orderDetails);
                }
                return BindModelToViewModel(order);
            }
            return null;

        }

        public async Task<bool> PostOrderDetails(TransactionOutViewModel orderDetails)
        {
            uow.TransactionsRepo.Insert(BindViewModelToModel(new Transactions(), orderDetails));
            if (await uow.SaveAsync())

            { return true; }
            return false;
        }
        public async Task<bool> PutOrderDetails(int id, TransactionOutViewModel orderDetails)
        {
            var oDetails = await uow.TransactionsRepo.GetByIdAsync(orderDetails.Id);

            if (oDetails != null)
            {

                BindViewModelToModel(oDetails, orderDetails);
                uow.TransactionsRepo.Update(oDetails);
                if (await uow.SaveAsync())

                { return true; }
            }
            return false;
        }
        public async Task<bool> DeleteOrderDetails(int id)
        {
            Transactions order = await uow.TransactionsRepo.GetByIdAsync(id);
            if (order != null)
            {
                uow.TransactionsRepo.Delete(id);
                if (await uow.SaveAsync())
                {
                    return true;
                }
            }
            return false;
        }
        private TransactionsOrderViewModel BindModelToViewModel(Order model)
        {
            TransactionsOrderViewModel viewModel = new TransactionsOrderViewModel();
            viewModel.Id = model.Id;
            viewModel.Order.Id = model.Id;
            viewModel.Order.Open = model.Open;
            viewModel.Order.ClientName = model.ClientName;
            viewModel.Order.OrderDate = model.OrderDate;
            viewModel.Order.TotalPrice = model.TotalPrice;

            foreach (var modelOrderDetailse in model.OrderDetailses)
            {
                var viewModelOrderDetailse = new TransactionOutViewModel();
                viewModelOrderDetailse.Id = modelOrderDetailse.Id;
                viewModelOrderDetailse.ProductId = modelOrderDetailse.Id;
                viewModelOrderDetailse.Color = modelOrderDetailse.Product.Color;
                viewModelOrderDetailse.Discount = modelOrderDetailse.Discount.HasValue
                    ? modelOrderDetailse.Discount.Value
                    : 0;
                viewModelOrderDetailse.Model = modelOrderDetailse.Product.Model;
                viewModelOrderDetailse.Size = modelOrderDetailse.Product.Size;
                viewModelOrderDetailse.ProductCode = modelOrderDetailse.Product.ProductCode;
                viewModelOrderDetailse.ProductName = modelOrderDetailse.Product.Name;
                viewModelOrderDetailse.Sold = modelOrderDetailse.Sold.HasValue ? modelOrderDetailse.Sold.Value : 0;
                viewModelOrderDetailse.PricePerUnit = modelOrderDetailse.PricePerUnit;
                viewModelOrderDetailse.ProductOut = modelOrderDetailse.ProductOut.HasValue
                    ? modelOrderDetailse.ProductOut.Value
                    : 0;
                viewModelOrderDetailse.Quantity = modelOrderDetailse.Product.Quantity;
                viewModel.Order.OrderDetails.Add(viewModelOrderDetailse);
            }

            return viewModel;

        }

        private Transactions BindViewModelToModel(Transactions model, TransactionOutViewModel viewModel)
        {

            model.Discount = viewModel.Discount.HasValue ? viewModel.Discount.Value : 0;
            model.OrderId = viewModel.OrderId;
            model.ProductId = viewModel.ProductId;
            model.ProductOut = viewModel.ProductOut;
            model.State = viewModel.State;
            model.PricePerUnit = Math.Round(viewModel.PricePerUnit);

            return model;
        }
    }
}