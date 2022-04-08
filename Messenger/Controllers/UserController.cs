using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
          public IActionResult Index()
          {
              return View();
          }
    }
}