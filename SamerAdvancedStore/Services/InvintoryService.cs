using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Services
{
    public class InvintoryService : BaseService
    {
        public async Task<InvintoriesViewModel> GetInvintories()
        {
            var Invintories = await uow.InvintoryRepo.GetAllAsync();
            if (Invintories != null)
            {
                InvintoriesViewModel model = new InvintoriesViewModel();
                model.Message = string.Format("تم العثور على {0} نتيجه", Invintories.Count());
                foreach (var invintorey in Invintories)

                {
                    model.Invintory.Add(BindModelToViewModel(invintorey));
                }
                return model;
            }
            return null;
        }

        public async Task<InvintoryViewModel> GetInvintory(int Id)
        {
            var Invintory = await uow.InvintoryRepo.GetByIdAsync(Id);


            if (Invintory != null)
            {
                return BindModelToViewModel(Invintory);
            }

            return null;

        }

        public async Task<bool> PutInvintorey(int id, InvintoryViewModel ViewModel)
        {


            var model = await uow.InvintoryRepo.GetByIdAsync(id);
            if (model != null)
            {

                BindViewModelToModel(ViewModel, model);
                uow.InvintoryRepo.Update(model);
                if (await uow.SaveAsync())

                    return true;

            }
            return false;
        }

        public async Task<Invintorey> PostInvintorey(InvintoryViewModel invintorey)
        {
            if (invintorey != null)
            {
                var model = BindViewModelToModel(invintorey, new Invintorey());
                uow.InvintoryRepo.Insert(model);
                if (await uow.SaveAsync())
                {
                    return model;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteInvintorey(int id)
        {
            Invintorey invintorey = await uow.InvintoryRepo.GetByIdAsync(id);
            if (invintorey != null)
            {
                uow.InvintoryRepo.Delete(id);
                if (await uow.SaveAsync())
                {
                    return true;
                }
                return false;
            }


            return false;
        }

        private InvintoryViewModel BindModelToViewModel(Invintorey Model)
        {
            var ViewModel = new InvintoryViewModel();
            ViewModel.Id = Model.Id;
            ViewModel.CreatedDate = Model.CreatedDate;
            ViewModel.TotalSum = Model.TotalSum;
            ViewModel.Open = Model.Open;
            ViewModel.SupplierName = Model.SupplierName;
            ViewModel.SupplierPhone = Model.SupplierPhone;
            return ViewModel;
        }

        private Invintorey BindViewModelToModel(InvintoryViewModel ViewModel, Invintorey Model)
        {
            Model.Id = ViewModel.Id;
            if (Model.Id == 0)
            {
                Model.CreatedDate = DateTime.Now;
                Model.Open = true;
            }
            else
            {
                Model.Open = ViewModel.Open;
            }
            

            Model.TotalSum = ViewModel.TotalSum;

            Model.SupplierName = ViewModel.SupplierName;
            Model.SupplierPhone = ViewModel.SupplierPhone;
            return Model;
        }

    }



}