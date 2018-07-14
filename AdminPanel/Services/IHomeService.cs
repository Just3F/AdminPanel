using System.Collections.Generic;
using AdminPanel.ViewModels;

namespace AdminPanel.Services
{
    public interface IHomeService
    {
        List<CategoryViewModel> GetCategories();
    }
}