using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [StringLength(90, ErrorMessage = "أسم التصنيف لا يزيد عن 90 حرف")]
        [Required(ErrorMessage = "يجب أدخال أسم التصنيف")]
        public string Name { get; set; }
       
    }

    public class IndexCategoryViewMode
    {
        public string message { get; set; }
        public List<CategoryViewModel> Categories{ get; set; }

        public IndexCategoryViewMode()
        {
            this.Categories= new List<CategoryViewModel>();
        }

    }


}