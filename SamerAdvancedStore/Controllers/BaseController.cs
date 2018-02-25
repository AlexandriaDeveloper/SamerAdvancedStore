using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.Repository;
using SamerAdvancedStore.Services;

namespace SamerAdvancedStore.Controllers
{
    public class BaseController : ApiController
    {
 
        protected UOW uow = null;        protected  BaseService Service=null;
        public BaseController()
        {
 
            this.Service= new BaseService();
        }


      
        public IHttpActionResult AddError(ModelStateDictionary modelState)
        {
           

            string message = "";

            foreach (var error in modelState)
            {

                foreach (var singleerror in error.Value.Errors)
                {
                    message += singleerror.ErrorMessage + " , ";
                }
               
            }

            return BadRequest( message);
        }
     
    }
}
