using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _GalleryComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
