using System.Collections.Generic;

namespace AdminPanel.ViewModels
{
    public class ManagePostViewModel
    {
        public ManagePostViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }
        public PostViewModel PostViewModel { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
