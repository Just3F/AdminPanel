using AdminPanel.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Components
{
    public class LeftSideMenu : ViewComponent
    {
        private IHomeService _service;
        public LeftSideMenu(IHomeService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke(int maxPrice)
        {
            var model =_service.GetMainCategories();
            return View(model);
        }
    }
}
