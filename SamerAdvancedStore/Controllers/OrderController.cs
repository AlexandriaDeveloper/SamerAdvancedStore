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
    [System.Web.Http.RoutePrefix("api/Order")]
    public class OrderController : BaseController
    {
        private OrderService OService = null;
        public OrderController()
        {
            OService = Service.OrderService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrders/")]
        public async Task<IHttpActionResult> GetOrders()
        {
            var model = await OService.GetOrders();
            if (model != null)
            {
                return Ok(model);

            }

            return BadRequest("عفوا يوجد خطأ ...");
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrder/{id}")]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            var model = await OService.GetOrder(id);
            if (model != null)
            {
                return Ok(model);
            }

            return BadRequest("عفوا يوجد خطأ ...");

        }
        // POST: api/Order
        [HttpPost]
        [Route("PostOrder/")]
        public async Task<IHttpActionResult> PostOrder(OrderViewModel order)
        {
            if (order != null)
            {
                var model = await OService.PostOrder(order);
                if (model != null) 
                return Ok(model.Id);
            }

            return BadRequest("عفوا يوجد خطأ ...");

        }
        // Put: api/Order
        [HttpPut]
        [Route("PutOrder/{id}")]
        public async Task<IHttpActionResult> PutOrder(int id, OrderViewModel order)
        {
            if (order!=null)

            {
                if (await OService.PutOrder(id, order))
                {
                    return Ok();
                }
            }
            return BadRequest("عفوا يوجد خطأ ...");

        }
        //// DELETE: api/Order/5
        [HttpDelete]
        [Route("DeleteOrder/{id}")]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
           
            if (await OService.DeleteOrder(id) )
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }
    }
}
