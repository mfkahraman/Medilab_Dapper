using Humanizer;
using Medilab_Dapper.Dtos.DepartmentDtos;
using Medilab_Dapper.Repositories.DepartmentRepository;
using Medilab_Dapper.Repositories.ImageRepository;
using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.Controllers
{
    public class DepartmentController(IDepartmentRepository repository,
                                        IImageService imageService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var departments = await repository.GetAllDepartmentsAsync();
            return View(departments);
        }

        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDto)
        {
            if (createDto.ImageFile != null)
            {
                var imagePath = await imageService.SaveImageAsync(createDto.ImageFile, "departments");
                createDto.ImageUrl = imagePath;
                ModelState.Remove("ImageUrl");
            }

            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            await repository.CreateDepartmentAsync(createDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateDepartment(int id)
        {
            var department = await repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }
            await repository.UpdateDepartmentAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            await repository.DeleteDepartmentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
