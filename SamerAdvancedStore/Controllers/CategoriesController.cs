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
using SamerAdvancedStore.Repository;
using SamerAdvancedStore.Services;
using SamerAdvancedStore.ViewModel;
//42393723
namespace SamerAdvancedStore.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : BaseController
    {
       private CategoryService cs = null;
        
        public CategoriesController()
        {
            cs = Service.CategoryService;

        }

        // GET: api/Categories
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IHttpActionResult> GetCategories()
        {

            var categories = await cs.GetCategories();

            if (categories != null)
            {
                IndexCategoryViewMode model = new IndexCategoryViewMode();
                model.message = string.Format("تم العثور على {0} نتيجه", categories.Count());
                if (categories != null)
                    foreach (var category in categories)
                    {
                        model.Categories.Add(category);
                    }
                return Ok(model);
            }

            return BadRequest("عفوا يوجد خطأ ...");
        }

        // GET: api/Categories/5

        [HttpGet]
        [Route("GetCategory/{id}")]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            CategoryViewModel category = await cs.GetCategory(id);

            if (category != null)
            {
                return Ok(category);
            }
            return BadRequest("عفوا يوجد خطأ ...");
        }

        // PUT: api/Categories/5
        [HttpPut]
        [Route("PutCategory/{id}")]
        public async Task<IHttpActionResult> PutCategory(int id, CategoryViewModel category)
        {
            if (await cs.PutCategory(category))
            {
                return Ok();

            }
            return BadRequest("عفوا يوجد خطأ ...");

        }

        // POST: api/Categories
        [HttpPost]
        [Route("PostCategory/")]
        public async Task<IHttpActionResult> PostCategory(CategoryViewModel category)
        {
            if (await cs.PostCategory(category))
                return Ok("تم اضافة التصنيف");
            return BadRequest("عفوا يوجد خطأ ...");
        }

        // DELETE: api/Categories/5
        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {

            if (await cs.DeleteCategory(id))
                return Ok("تم الحذف بنجاح");
            return BadRequest("عفوا يوجد خطأ ...");
        }

    }
}