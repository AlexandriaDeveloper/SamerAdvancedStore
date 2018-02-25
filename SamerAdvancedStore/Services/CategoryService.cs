using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;
using WebGrease.Css.Extensions;

namespace SamerAdvancedStore.Services
{
    public class CategoryService : BaseService
    {
        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var categories = await uow.CategoryRepo.GetAllAsync();
            if (categories != null)
            {
                List<CategoryViewModel> model = new List<CategoryViewModel>();

                categories.ForEach(x => model.Add(BindModelToViewModel(x, new CategoryViewModel())));

                return model;
            }
            return null;

        }


        public async Task<CategoryViewModel> GetCategory(int id)
        {
            var category = await uow.CategoryRepo.GetByIdAsync(id);
            if (category != null)
                return BindModelToViewModel(category, new CategoryViewModel());
            return null;

        }

       
        public async Task<bool> PutCategory(CategoryViewModel category)
        {

            var originalCat = await uow.CategoryRepo.GetByIdAsync(category.Id);
            if (originalCat == null)
                return false;

            await BindViewModelToModel(category, originalCat);

            uow.CategoryRepo.Update(originalCat);
            if (await uow.SaveAsync())
            {
                return true;
            }
            return false;




        }


        public async Task<bool> PostCategory(CategoryViewModel category)
        {

            if (category != null)
            {

               
                    uow.CategoryRepo.Insert(await BindViewModelToModel(category, new Category()));
                    if (await uow.SaveAsync())
                    {
                        return true;
                    }
                    return false;
              
            }
            return false;
        }

        public async Task<bool> DeleteCategory(int Id)
        {
            Category categoryModel = await uow.CategoryRepo.GetByIdAsync(Id);
            if (categoryModel != null)
            {
                uow.CategoryRepo.Delete(categoryModel);
                await uow.SaveAsync();
                return true;
            }
            return false;

        }


        private CategoryViewModel BindModelToViewModel(Category category, CategoryViewModel categoryViewModel)
        {
            if (category.Id > 0)
            {
                categoryViewModel.Id = category.Id;
            }
            categoryViewModel.Name = category.Name;
            return categoryViewModel;

        }

        private async Task<Category> BindViewModelToModel(CategoryViewModel categoryViewModel, Category category)
        {

            category.Name = categoryViewModel.Name;

            return category;

        }


    }
}