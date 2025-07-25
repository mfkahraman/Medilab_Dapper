using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _ContactComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
