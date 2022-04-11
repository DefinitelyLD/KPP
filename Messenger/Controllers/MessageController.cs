using Microsoft.AspNetCore.Mvc;

namespace Messenger.WEB.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
