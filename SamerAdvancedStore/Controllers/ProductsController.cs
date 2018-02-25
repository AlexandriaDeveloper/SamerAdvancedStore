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
using System.Web.Mvc;
using SamerAdvancedStore.Models;
using SamerAdvancedStore.Repository;
using SamerAdvancedStore.Services;
using SamerAdvancedStore.ViewModel;
//42393723
namespace SamerAdvancedStore.Controllers
{
    [System.Web.Http.RoutePrefix("api/Products")]
    public class ProductsController : BaseController
    {
        private ProductService ps;
        public ProductsController()
        {
            ps = Service.ProductService;
        }

        // GET: api/Products
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProducts")]
        public async Task<IHttpActionResult> GetProducts()
        {
            var model = await ps.GetProducts();
            if (model != null)
                return Ok(model);

            return BadRequest("عفوا يوجد خطأ ...");
        }

        // GET: api/Products/5

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProduct/{id}")]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var Product = await ps.GetProduct(id);



            if (Product == null)
            {
                return BadRequest("عفوا يوجد خطأ ...");
            }

            return Ok(Product);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProductByCode/{code}")]
        public async Task<IHttpActionResult> GetProductByCode(string code)
        {
            var Product = await ps.GetProductByCode(code);
            if (Product == null)
            {
                return BadRequest("عفوا هذا الكود غير موجود");
            }
            return Ok(Product);
        }
        // PUT: api/Products/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("PutProduct/{id}")]
        public async Task<IHttpActionResult> PutProduct(int id, ProductViewModel Product)
        {

            if (await ps.PutProduct(id, Product))
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");

        }

        // POST: api/Products
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("PostProduct/")]
        public async Task<IHttpActionResult> PostProduct(ProductViewModel Product)
        {
            if (await ps.ProductCodeExists(Product.ProductCode))
            {
                ModelState.AddModelError("", "هذا الكود مسجل من قبل ");
            }
            if (!ModelState.IsValid)
            {

                return this.AddError(ModelState);

            }
            if (await ps.PostProduct(Product))
            {
                return Ok();
            }
            return BadRequest("عفوا يوجد خطأ ...");

        }

        // DELETE: api/Products/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("DeleteProduct/{id}")]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            if (await ps.DeleteProduct(id))
                return Ok("تم الحذف بنجاح");
            return BadRequest("عفوا يوجد خطأ ...");
        }










    }
}