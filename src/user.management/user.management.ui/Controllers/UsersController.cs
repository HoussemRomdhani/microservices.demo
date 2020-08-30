using Microsoft.AspNetCore.Mvc;

namespace user.management.ui.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
