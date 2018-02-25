using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SamerAdvancedStore.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IHttpActionResult> GetHome()
        {
            return Ok(DateTime.UtcNow);
        }
    }
}