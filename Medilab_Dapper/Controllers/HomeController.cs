using Medilab_Dapper.Dtos.AppointmentDtos;
using Medilab_Dapper.Models;
using Medilab_Dapper.Repositories.AppointmentRepository;
using Medilab_Dapper.Repositories.DepartmentRepository;
using Medilab_Dapper.Repositories.DoctorRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Medilab_Dapper.Controllers
{
    public class HomeController(IAppointmentRepository repository,
                                IDoctorRepository doctor,
                                IDepartmentRepository departmentRepository) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Homepage()
        {
            await GetDoctorsAndDepartmentsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = string.Join(" | ", errors) });
            }

            var result = await repository.CreateAppointmentAsync(dto);

            if (result)
            {
                return Json(new { success = true, message = "Randevu başarıyla oluşturuldu." });
            }
            else
            {
                return Json(new { success = false, message = "Randevu oluşturulurken bir hata oluştu." });
            }
        }


        private async Task GetDoctorsAndDepartmentsAsync()
        {
            var deparments = await departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Departments = deparments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();

            var doctors = await doctor.GetAllDoctorsAsync();
            ViewBag.Doctors = doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.NameSurname
            }).ToList();
        }

    }
}
