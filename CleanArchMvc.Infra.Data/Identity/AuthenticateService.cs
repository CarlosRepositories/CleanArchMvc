using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class AuthenticateService : IAuthenticate
{
    private readonly UserManager<ApplicationUser> UserManager;
    private readonly SignInManager<ApplicationUser> SignInManager;

    public AuthenticateService(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        SignInManager = signInManager;
        UserManager = userManager;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await SignInManager.PasswordSignInAsync(email,
            password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }
    public async Task<bool> RegisterUser(string email, string password)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
        };


        var result = await UserManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {
            await SignInManager.SignInAsync(applicationUser, isPersistent: false);
        }

        // o retorno não deve ser apenas um bool
        // quando há um problema com a senha do usuário o método CreateAsync retorna os erros
        // esses erros devem ser repassados para quem chamou o método RegisterUser e apresentados ao usuário
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await SignInManager.SignOutAsync();
    }
}
