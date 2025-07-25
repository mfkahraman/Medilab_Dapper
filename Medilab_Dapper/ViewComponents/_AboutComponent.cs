using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _AboutComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}