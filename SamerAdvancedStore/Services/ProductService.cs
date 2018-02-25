using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using SamerAdvancedStore.Repository;
using System.Threading.Tasks;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Services
{
    public class ProductService : BaseService
    {



        public async Task<IndexProductViewMode> GetProducts()
        {
            var Products = await uow.ProductRepo.GetAllAsync();




            if (Products != null)
            {
                IndexProductViewMode model = new IndexProductViewMode();
                model.message = string.Format("تم العثور على {0} نتيجه", Products.Count());
                foreach (var Product in Products)
                {
                    model.Products.Add(BindModelToViewMode(Product));
                }
                return (model);
            }
            return null;
        }

        public async Task<ProductViewModel> GetProduct(int ProductId)
        {

            var Product = await uow.ProductRepo.GetByIdAsync(ProductId);



            if (Product == null)
            {
                return null;
            }
            return BindModelToViewMode(Product);

        }
        public async Task<ProductViewModel> GetProductByCode(string Code)
        {

            var Product = await uow.ProductRepo.GetSingleAsync(x => x.ProductCode == Code); ;



            if (Product == null)
            {
                return null;
            }
            return BindModelToViewMode(Product);

        }

        public async Task<bool> PutProduct(int id, ProductViewModel Product)
        {
            if (id != Product.ProductId)
            {
                return false;
            }

            var model = await uow.ProductRepo.GetByIdAsync(id);


            uow.ProductRepo.Update(BindViewModeToModel(Product, model));

            if (await uow.SaveAsync())
            {
                return true;
            }
            return false;

       
        }


        public async Task<bool> PostProduct(ProductViewModel Product)
        {
            if (Product != null)
            {
                uow.ProductRepo.Insert(BindViewModeToModel(Product, new Product()));
                if (await uow.SaveAsync())
                {
                    return true;
                }
                return false;

            }

            return false;
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            var Product = await uow.ProductRepo.GetByIdAsync(Id);
            if (Product != null)
            {
                uow.ProductRepo.Delete(Product);
                if (await uow.SaveAsync())
                {
                    return true;
                }
                return false;

            }
            return false;

        }

   

        public async Task<bool> ProductCodeExists(string ProductCode)
        {

            var found = uow.ProductRepo.GetAll(x => x.ProductCode == ProductCode);
            return found.Count() > 0 ? true : false;
        }
       




        private ProductViewModel BindModelToViewMode(Product Product)
        {

            ProductViewModel Model = new ProductViewModel();
            Model.ProductId = Product.Id;
            Model.ProductName = Product.Name;
            Model.Price = Product.Price;
            Model.CategoryName = uow.CategoryRepo.GetByID(Product.CategoryId).Name;
            Model.CategoryId = Product.CategoryId;
            Model.ProductCode = Product.ProductCode;
            Model.Avilibility = Product.Avilibility;
            Model.Model = Product.Model;
            Model.Color = Product.Color;
            Model.Size = Product.Size;
            Model.Quantity = Product.Quantity;


            return Model;

        }


        private Product BindViewModeToModel(ProductViewModel ProductVieModel, Product Model)
        {
            Model.Id = ProductVieModel.ProductId;
            Model.CategoryId = ProductVieModel.CategoryId;
            Model.Model = ProductVieModel.Model;
            Model.Color = ProductVieModel.Color;
            Model.ProductCode = ProductVieModel.ProductCode;
            Model.Size = ProductVieModel.Size.HasValue ? ProductVieModel.Size.Value : 0;
            Model.Avilibility = ProductVieModel.Avilibility;
            Model.Price = ProductVieModel.Price;
            Model.Name = ProductVieModel.ProductName;

            return Model;
        }

    }
}