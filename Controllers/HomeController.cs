using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return Content(User.Identity!.Name);
        }
    }
}