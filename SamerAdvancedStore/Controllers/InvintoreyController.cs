using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.Services;
using SamerAdvancedStore.ViewModel;

namespace SamerAdvancedStore.Controllers
{
    [System.Web.Http.RoutePrefix("api/Invintorey")]
    public class InvintoreyController : BaseController
    {
        private InvintoryService IService = null;
        public InvintoreyController()
        {
            IService = Service.InvintoryService;
        }

        // GET: api/Invintoreys

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetInvintories/")]
        public async Task<IHttpActionResult> GetInvintories()
        {

            InvintoriesViewModel model = await IService.GetInvintories();
            if (model != null)
                return Ok(model);
            return BadRequest("عفوا يوجد خطأ ...");
        }

        // GET: api/Invintoreys/5
        [HttpGet]
        [Route("GetInvintorey/{id}")]
        public async Task<IHttpActionResult> GetInvintorey(int id)
        {
            InvintoryViewModel model = await IService.GetInvintory(id);
            if (model != null)
            {
                return Ok(model);
            }

            return BadRequest("عفوا يوجد خطأ ...");
        }

        //// PUT: api/Invintoreys/5
        [HttpPut]
        [Route("PutInvintorey/{id}")]
        public async Task<IHttpActionResult> PutInvintorey(int id, InvintoryViewModel invintorey)
        {
            if (await IService.PutInvintorey(id, invintorey))
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }
        [Route("PutInvintorey/{id}")]
        public async Task<IHttpActionResult> CloseInvintorey(int id, InvintoryViewModel invintorey)
        {
            if (await IService.PutInvintorey(id, invintorey))
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }
        // POST: api/Invintoreys
        [HttpPost]
        [Route("PostInvintorey/")]
        public async Task<IHttpActionResult> PostInvintorey(InvintoryViewModel invintorey)
        {
            var model = await IService.PostInvintorey(invintorey);
            if (model != null)
            {
                return Ok(model.Id);
            }

            return BadRequest("عفوا يوجد خطأ ...");

        }

        //// DELETE: api/Invintoreys/5
        [HttpDelete]
        [Route("DeleteInvintorey/{id}")]
        public async Task<IHttpActionResult> DeleteInvintorey(int id)
        {

            if (await IService.DeleteInvintorey(id))
            {
                return Ok();
            }

            return BadRequest("عفوا يوجد خطأ ...");
        }

    }
}