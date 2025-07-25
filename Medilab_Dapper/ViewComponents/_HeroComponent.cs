using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _HeroComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
