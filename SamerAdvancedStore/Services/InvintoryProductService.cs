using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Services
{
    public class InvintoryProductService : BaseService
    {
        private InvintoryService IS;

        public InvintoryProductService()
        {
            IS = new InvintoryService();

        }
        public async Task<InvintoryProductsViewModel> GetInvintoryProducts(int id)
        {
            var inv = await IS.GetInvintory(id);

            if (inv != null)
            {
                InvintoryProductsViewModel model = new InvintoryProductsViewModel()
                {
                    Invintory = inv
                };
                var products = await uow.TransactionsRepo.GetAllAsync(x => x.InvintoryId == id, includeProperties: "Product");
                foreach (var product in products)
                {
                    model.Transaction.Add(BindModelToViewModel(product));
                }
                return model;
            }
            return null;

        }


        public async Task<TransactionInViewModel> GetInvintoryProduct(int id)
        {
            var invProdut = await uow.TransactionsRepo.GetSingleAsync(x => x.Id == id);

            var productData = await uow.ProductRepo.GetByIdAsync(invProdut.ProductId);


            invProdut.Product = productData;
            var cat = await uow.CategoryRepo.GetByIdAsync(productData.CategoryId);

            invProdut.Product.Category = cat;
            if (invProdut != null)
            {


                TransactionInViewModel model = BindModelToViewModel(invProdut);

                return model;
            }
            return null;

        }


        public async Task<bool> PostInvintoryProduct(TransactionInViewModel product)
        {
            uow.TransactionsRepo.Insert(BindViewModelToModel(product,new Transactions()));
            if (await uow.SaveAsync())
            {
                return true;
            }
            return false;

        }


        public async Task<bool> PutInvintoryProduct(int id, TransactionInViewModel product)
        {
            var currentinvprod = await uow.TransactionsRepo.GetByIdAsync(id);
            if (currentinvprod != null)
            {
                //currentinvprod.ProductId = product.ProductId;
                //currentinvprod.InvintoryId = product.InvintoryId;
                //currentinvprod.ProductIn = product.ProductIn;
                //currentinvprod.State = "IN";
                //currentinvprod.Coast = product.Coast;
                //currentinvprod.PricePerUnit = Math.Round(product.PricePerUnit, 2);


                BindViewModelToModel(product, currentinvprod);

                await uow.SaveAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteInvintoryProduct(int id)
        {
            var currentinvprod = await uow.TransactionsRepo.GetByIdAsync(id);
            if (currentinvprod != null)
            {
                uow.TransactionsRepo.Delete(currentinvprod);
                await uow.SaveAsync();
                return true;
            }
            return false;
        }
        private TransactionInViewModel BindModelToViewModel(Transactions model)
        {
            TransactionInViewModel viewModel = new TransactionInViewModel();


            viewModel.Id = model.Id;
            viewModel.ProductId = model.ProductId;
            viewModel.CategoryId = model.Product.CategoryId;
            viewModel.ProductName = model.Product.Name;
            viewModel.Color = model.Product.Color;
            viewModel.Model = model.Product.Model;
            viewModel.Price = model.Product.Price;
            viewModel.ProductCode = model.Product.ProductCode;
            viewModel.Size = model.Product.Size;
            viewModel.PricePerUnit = model.PricePerUnit;
            viewModel.Coast = model.Coast;
            viewModel.Discount = model.Discount.HasValue ? model.Discount.Value : 0;
            viewModel.State = model.State;
            viewModel.InvintoryId = model.InvintoryId;
            viewModel.ProductIn = model.ProductIn.HasValue ? model.ProductIn.Value : 0;

            return viewModel;

        }

        private Transactions BindViewModelToModel(TransactionInViewModel viewModel, Transactions model)
        {
            model.ProductId = viewModel.ProductId;
            model.InvintoryId = viewModel.InvintoryId;
            model.ProductIn = viewModel.ProductIn;
            model.State = "IN";
            model.Coast = viewModel.Coast.HasValue ? viewModel.Coast.Value : 0;
            model.Discount = viewModel.Discount;
            model.PricePerUnit = Math.Round(viewModel.PricePerUnit, 2);
            return model;
        }
    }
}