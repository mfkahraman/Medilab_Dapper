using Medilab_Dapper.Repositories.AppointmentRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medilab_Dapper.Controllers
{
    public class AppointmentController(IAppointmentRepository appointmentRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var appointments = await appointmentRepository.GetAllAppointmentsAsync();
            return View(appointments);
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            await appointmentRepository.DeleteAppointmentAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmAppointment(int id)
        {
            await appointmentRepository.ConfirmAppointmentAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CancelAppointment(int id)
        {
            await appointmentRepository.CancelAppointmentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
