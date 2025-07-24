using Medilab_Dapper.Dtos.DoctorDtos;
using Medilab_Dapper.Repositories.DepartmentRepository;
using Medilab_Dapper.Repositories.DoctorRepository;
using Medilab_Dapper.Repositories.ImageRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Medilab_Dapper.Controllers
{
    public class DoctorController(IDoctorRepository repository,
                                  IDepartmentRepository departmentRepository,
                                  IImageService imageService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var doctors = await repository.GetDoctorsWithDepartmentAsync();
            return View(doctors);
        }

        public async Task<IActionResult> CreateDoctor()
        {
            await GetDepartmentsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto dto)
        {
            if (dto.ImageFile != null)
            {
                var imagePath = await imageService.SaveImageAsync(dto.ImageFile, "doctors");
                dto.ImageUrl = imagePath;
                ModelState.Remove("ImageUrl");
            }

            await GetDepartmentsAsync();


            if (ModelState.IsValid)
            {
                await repository.CreateDoctorAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public async Task<IActionResult> UpdateDoctor(int id)
        {
            await GetDepartmentsAsync();

            var doctor = await repository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var updateDoctorDto = new UpdateDoctorDto
            {
                DoctorId = doctor.DoctorId,
                NameSurname = doctor.NameSurname,
                Description = doctor.Description,
                DepartmentId = doctor.DepartmentId,
                ImageUrl = doctor.ImageUrl
            };

            return View(updateDoctorDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorDto dto)
        {
            if (dto.ImageFile != null)
            {
                //Delete old image if exists
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                    await imageService.DeleteImageAsync(dto.ImageUrl);

                var imagePath = await imageService.SaveImageAsync(dto.ImageFile, "doctors");
                dto.ImageUrl = imagePath;
                ModelState.Remove("ImageUrl");
            }


                await GetDepartmentsAsync();

            if (ModelState.IsValid)
            {
                await repository.UpdateDoctorAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await repository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            //Delete image if exists
            if (!string.IsNullOrEmpty(doctor.ImageUrl))
                await imageService.DeleteImageAsync(doctor.ImageUrl);

            await repository.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task GetDepartmentsAsync()
        {
            var deparments = await departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Departments = deparments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
        }
    }
}
