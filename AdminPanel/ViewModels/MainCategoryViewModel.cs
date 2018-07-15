using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class MainCategoryViewModel
    {
        public MainCategoryViewModel()
        {
            CategoryViewModels = new List<CategoryViewModel>();
        }
        public long PKID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<CategoryViewModel> CategoryViewModels { get; set; }
    }
}
