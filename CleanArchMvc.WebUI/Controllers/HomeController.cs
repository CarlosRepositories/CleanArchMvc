using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeedUserRoleInitial SeedUserRoleInitial;
        public HomeController(ISeedUserRoleInitial seedUserRoleInitial1) 
        {
            SeedUserRoleInitial = seedUserRoleInitial1;
        }
        public IActionResult Index()
        {
            SeedUserRoleInitial.SeedRoles();
            SeedUserRoleInitial.SeedUsers();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
