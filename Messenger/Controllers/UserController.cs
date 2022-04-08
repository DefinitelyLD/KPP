using AutoMapper;
using Messenger.BLL.Models;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /* [HttpGet]
         public UserModel Get()
         {
             User userEn = new()
             {
                 Id = "1",
                 Name = "Yohan",
                 Password = "12345",
                 Email = "Libert",
             };
             return _mapper.Map<UserModel>(userEn);
         }

          public IActionResult Index()
          {
              return View();
          }*/
    }
}