using Medilab_Dapper.Repositories.DepartmentRepository;
using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.ViewComponents
{
    public class _DepartmentsComponent(IDepartmentRepository repository) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departments = await repository.GetAllDepartmentsAsync();
            if (departments == null || !departments.Any())
            {
                return Content("No departments available.");
            }
            List<int> ids = new() { 1, 4, 6, 7, 8 };
            var filteredDepartments = departments.Where(d => ids.Contains(d.DepartmentId)).ToList();

            if (!filteredDepartments.Any())
            {
                return Content("No matching departments available.");
            }

            return View(filteredDepartments);
        }
    }
}
