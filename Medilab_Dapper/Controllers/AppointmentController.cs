using Medilab_Dapper.Repositories.AppointmentRepository;
using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.Controllers
{
    public class AppointmentController(IAppointmentRepository appointmentRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var appointments = await appointmentRepository.GetAllAppointmentsAsync();
            return View(appointments);
        }

        public IActionResult DeleteAppointment(int id)
        {
            var appointment = appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
    }
}
