using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Services
{
    public class OrderService : BaseService
    {
        public async Task<OrdersViewModel> GetOrders()
        {
            var orders = await uow.OrderRepo.GetAllAsync();
            OrdersViewModel model = new OrdersViewModel();
            model.Message = string.Format("تم العثور على {0} نتيجه", orders.Count());

            foreach (var order in orders)
            {
                model.Orders.Add(BindModelToViewModel(order));
            }

            return model;
        }

        public async Task<OrderViewModel> GetOrder(int id)
        {
            var order = await uow.OrderRepo.GetByIdAsync(id);
            if (order != null)
            {
                return BindModelToViewModel(order);
            }
            return null;
        }

        public async Task<Order> PostOrder(OrderViewModel order)
        {

            var orderData = (BindViewModelToMode(order, new Order()));
            uow.OrderRepo.Insert(orderData);
            if (await uow.SaveAsync())
            {
                return orderData;

            }

            return null;
        }

        public async Task<bool> PutOrder(int id, OrderViewModel order)
        {
            var orderModel = await uow.OrderRepo.GetByIdAsync(id);
            uow.OrderRepo.Update(BindViewModelToMode(order, orderModel));
            if (await uow.SaveAsync())
            {
                return true;

            }
            return false;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            Order order = await uow.OrderRepo.GetByIdAsync(id);
            if (order != null)
            {
                uow.OrderRepo.Delete(id);
                if (await uow.SaveAsync())
                {
                    return true;
                }
            }
            return false;
        }


        public OrderViewModel BindModelToViewModel(Order model)
        {

            OrderViewModel ViewModel = new OrderViewModel();
            ViewModel.Id = model.Id;
            ViewModel.TotalPrice = model.TotalPrice;
            ViewModel.OrderDate = DateTime.Now;
            ViewModel.Open = model.Open;
            ViewModel.ClientName = model.ClientName;
            return ViewModel;
        }

        public Order BindViewModelToMode(OrderViewModel ViewMode, Order Model)
        {
            if (ViewMode.Id == 0)
            {
                Model.OrderDate = DateTime.Now;
                Model.Open = true;
            }
            else
            {
                Model.Open = ViewMode.Open;
            }
            Model.ClientName = ViewMode.ClientName;
            return Model;
        }

    }
}