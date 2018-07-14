using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class PostsModel
    {
        public PostsModel()
        {
            PostViewModels = new List<PostViewModel>();
        }
        public List<PostViewModel> PostViewModels { get; set; }
    }
}
