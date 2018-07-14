using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class CategoriesModel
    {
        public CategoriesModel()
        {
            Categories = new List<CategoryViewModel>();
        }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
