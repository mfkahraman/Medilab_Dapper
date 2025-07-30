using Medilab_Dapper.Repositories.ContactMessageRepository;
using Microsoft.AspNetCore.Mvc;

namespace Medilab_Dapper.Controllers
{
    public class ContactMessageController(IContactMessageRepository repository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var messages = await repository.GetAllAsync();
            return View(messages);
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await repository.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MakeMessageRead(int id)
        {
            await repository.MakeReadAsync(id);
            return RedirectToAction("Index");
        }
    }
}
