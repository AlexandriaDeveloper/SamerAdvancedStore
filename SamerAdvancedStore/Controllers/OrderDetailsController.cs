using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.Services;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Controllers
{
    [RoutePrefix("api/OrderDetails")]

    public class OrderDetailsController : BaseController
    {
        private OrderDetailsService ODService = null;

        public OrderDetailsController()
        {
            ODService = Service.OrderDetailsService;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrderDetalsies/{id}")]
        public async Task<IHttpActionResult> GetOrderDetalsies(int id)
        {
          

            var model = await ODService.GetOrderDetalsies(id);
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("PostOrderDetails")]
        public async Task<IHttpActionResult> PostOrderDetails(TransactionOutViewModel orderDetails)
        {
            if (orderDetails != null)
            {
                await ODService.PostOrderDetails(orderDetails);
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }




        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("PutOrderDetails/{id}")]
        public async Task<IHttpActionResult> PutOrderDetails(int id, TransactionOutViewModel orderDetails)
        {
            if (orderDetails != null)
            {
                await ODService.PutOrderDetails(id,orderDetails);
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("DeleteOrderDetails/{id}")]
        public async Task<IHttpActionResult> DeleteOrderDetails(int id)
        {          
            if (await ODService.DeleteOrderDetails(id))
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }
    }
}
