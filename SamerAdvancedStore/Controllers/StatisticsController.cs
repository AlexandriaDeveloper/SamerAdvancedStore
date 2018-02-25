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
    [System.Web.Http.RoutePrefix("api/Statistics")]
    public class StatisticsController : BaseController
    {
        private StatisticService SService;
        public StatisticsController()
        {
            SService = Service.StatisticService;
        }
        //used
        // GET: api/Invintory
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Invintory/")]
        public async Task<IHttpActionResult> Invintory()
        {
            var Invintory = await SService.GetInvintory();
            if (Invintory != null)
            {
                return Ok(Invintory);
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }
        //used

        // GET: api/Profits
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProfits/{id}")]
        public async Task<IHttpActionResult> GetProfits(int id)
        {
      
            return Ok(await SService.GetProfits(id));
        }


        //used

        // GET: api/GetOrders/
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrders/")]
        public async Task<IHttpActionResult> GetOrders()

        {
           
                return Ok(await SService.GetOrders());     
    
        }

   

    }
}
