using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _DoctorsComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
