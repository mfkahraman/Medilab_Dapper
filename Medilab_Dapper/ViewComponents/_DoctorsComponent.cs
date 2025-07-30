using Medilab_Dapper.Repositories.DoctorRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medilab_Dapper.ViewComponents
{
    public class _DoctorsComponent(IDoctorRepository repository) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var doctors = (await repository.GetDoctorsWithDepartmentAsync()).Take(4).ToList();
            if (doctors == null || !doctors.Any())
            {
                return Content("No doctors available.");
            }
            return View(doctors);
        }
    }
}
