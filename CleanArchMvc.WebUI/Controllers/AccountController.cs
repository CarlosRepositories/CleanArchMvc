using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate Authentication;
        public AccountController(IAuthenticate authentication)
        {
            Authentication = authentication;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/Home/Index";
            }

            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await Authentication.Authenticate(model.Email, model.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await Authentication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await Authentication.Logout();
            return Redirect("/Account/Login");
        }
    }
}
