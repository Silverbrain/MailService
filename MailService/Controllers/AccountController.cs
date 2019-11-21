using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Areas.Identity.Data;
using MailService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager)
        {
            userManager = _UserManager;
            signInManager = _SignInManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var status = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                if (status.Succeeded)
                {
                    return RedirectToAction("Inbox", "Mail");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                    //show error modal
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
                //show error modal
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            var _user = await userManager.FindByEmailAsync(model.Email);
            if (_user == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Name = model.Name,
                    Family = model.Family,
                    Email = model.Email,
                    UserName = model.Email
                };
                var status = await userManager.CreateAsync(user, model.Password);
                if (status.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return RedirectToAction("Register", "Account");

                    //show error modal
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
                //show error modal
            }
        }
    }
}