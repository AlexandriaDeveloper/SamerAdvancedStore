using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SamerAdvancedStore.Repository;

namespace SamerAdvancedStore.Services
{
    public class BaseService : IBaseService
    {
        protected UOW uow = null;
        public BaseService()
        {
            this.uow = new UOW();
        }




        public CategoryService CategoryService
        {
            get
            {
                return new CategoryService();
            }
        }
        public ProductService ProductService
        {
            get
            {
                return new ProductService();
            }
        }

        public OrderService OrderService
        {
            get
            {
                return new OrderService();
            }
        }
        public InvintoryService InvintoryService
        {
            get
            {
                return new InvintoryService();
            }
        }

        public InvintoryProductService InvintoryProductService {
            get
            {
                return new InvintoryProductService();
            }
        }
        public OrderDetailsService OrderDetailsService
        {
            get
            {
                return new OrderDetailsService();
            }
        }
        public StatisticService StatisticService
        {
            get
            {
                return new StatisticService();
            }
        }

    }
}