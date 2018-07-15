using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class CategoryPageViewModel
    {
        public CategoryPageViewModel()
        {
            MainCategoryViewModels = new List<MainCategoryViewModel>();
        }
        public CategoryViewModel CategoryViewModel { get; set; }
        public List<MainCategoryViewModel> MainCategoryViewModels { get; set; }
    }
}
