using Medilab_Dapper.Dtos.DoctorDtos;
using Medilab_Dapper.Repositories.DoctorRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medilab_Dapper.Controllers
{
    public class DoctorController(IDoctorRepository repository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var doctors = await repository.GetAllDoctorsAsync();
            return View(doctors);
        }

        public IActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto dto)
        {
            if (ModelState.IsValid)
            {
                await repository.CreateDoctorAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> UpdateDoctor(int id)
        {
            var doctor = await repository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorDto dto)
        {
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
            return View(doctor);
        }
    }
}
