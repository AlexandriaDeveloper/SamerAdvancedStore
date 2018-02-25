using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SamerAdvancedStore.Models;

namespace SamerAdvancedStore.Repository
{
    public class UOW:IDisposable
    {
        private bool disposed = false;

        private  AppContext context= new AppContext();


        private CategoryRepo _categoryRepo;
        private ProductRepo _productRepo;

        private InvintoryRepo _invintoryRepo;

        private InvintoryProductRepo _invintoryProductRepo;


        private OrderRepo _orderRepo;
        private TransactionsRepo _transactionsRepo;
        public TransactionsRepo TransactionsRepo
        {
            get
            {

                if (this._transactionsRepo == null)
                {
                    this._transactionsRepo = new TransactionsRepo(context);
                }
                return _transactionsRepo;
            }
        }
        public OrderRepo OrderRepo
        {
            get
            {

                if (this._orderRepo == null)
                {
                    this._orderRepo = new OrderRepo(context);
                }
                return _orderRepo;
            }
        }
        public InvintoryProductRepo InvintoryProductRepo
        {
            get
            {

                if (this._invintoryProductRepo == null)
                {
                    this._invintoryProductRepo = new InvintoryProductRepo(context);
                }
                return _invintoryProductRepo;
            }
        }
        public InvintoryRepo InvintoryRepo
        {
            get
            {

                if (this._invintoryRepo == null)
                {
                    this._invintoryRepo = new InvintoryRepo(context);
                }
                return _invintoryRepo;
            }
        }
        public CategoryRepo CategoryRepo
        {
            get
            {

                if (this._categoryRepo == null)
                {
                    this._categoryRepo = new CategoryRepo(context);
                }
                return _categoryRepo;
            }
        }
        public ProductRepo ProductRepo
        {
            get
            {

                if (this._productRepo == null)
                {
                    this._productRepo = new ProductRepo(context);
                }
                return _productRepo;
            }
        }
        public bool Save()
        {
            var error = "";
            try
            {
                 context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                error += e.Message + " inner exception :" + e.InnerException;
                return false;
            }
            catch (DbException e)
            {
                error += e.Message + " inner exception :" + e.InnerException; ;
                return false;
            }
            catch (Exception e)
            {

                error += (e.Message) + " inner exception :" + e.InnerException; ;
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            var error = "";
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
               error+= e.Message +" inner exception :"+e.InnerException;
                return false;
            }
            catch (DbException e)
            {
                error += e.Message + " inner exception :" + e.InnerException; ;
                return false;
            }
            catch (Exception e)
            {

                error += (e.Message) + " inner exception :" + e.InnerException; ;
                return false;
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}