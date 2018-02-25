using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.Services;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Controllers
{
    [System.Web.Http.RoutePrefix("api/InvintoryProducts")]
    public class InvintoryProductsController : BaseController
    {
        private InvintoryProductService IPService = null;
        public InvintoryProductsController()
        {
            IPService = Service.InvintoryProductService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetInvintoryProducts/{id}")]
        public async Task<IHttpActionResult> GetInvintoryProducts(int id)
        {

            var model = await IPService.GetInvintoryProducts(id);
            if (model!= null)
            {
                return Ok(model);
            }
            return BadRequest("عفوا يوجد خطأ ...");

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetInvintoryProduct/{id}")]
        public async Task<IHttpActionResult> GetInvintoryProduct(int id)
        {

            var invProdut =await IPService.GetInvintoryProduct(id);       

            if (invProdut != null)
            {
                TransactionInViewModel model = invProdut;
                return Ok(model);
            }
            return BadRequest("عفوا يوجد خطأ ...");

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("PostInvintoryProduct")]
        public async Task<IHttpActionResult> PostInvintoryProduct(TransactionInViewModel product)
        {
            if (product != null)
            {
             await   IPService.PostInvintoryProduct(product);
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("PutInvintoryProduct/{id}")]
        public async Task<IHttpActionResult> PutInvintoryProduct(int id, TransactionInViewModel product)
        {
           
            if (product != null)
            {
                await IPService.PutInvintoryProduct(id, product);
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }


        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("DeleteInvintoryProduct/{id}")]
        public async Task<IHttpActionResult> DeleteInvintoryProduct(int id)
        {
            if (await IPService.DeleteInvintoryProduct(id) )
            {
                return Ok(new { message = "تم حذف بنجاح " });
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }


        //private async Task<int> GetLastQuantity(int ProductId)
        //{
        //    var result = await uow.TransactionsRepo.GetAllAsync(p => p.ProductId == ProductId);
        //    if (result != null)
        //    {
        //        int quantityIn = result.Sum(x => x.ProductIn).HasValue ? result.Sum(x => x.ProductIn).Value : 0;
        //        int quantityOut = result.Sum(x => x.ProductOut).HasValue ? result.Sum(x => x.ProductOut).Value : 0;

        //        return quantityIn - quantityOut;
        //    }
        //    return 0;
        //}


    }


}
