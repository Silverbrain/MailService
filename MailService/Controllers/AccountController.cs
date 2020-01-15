using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Areas.Identity.Data;
using MailService.Models;
using MailService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager)
        {
            userManager = _UserManager;
            signInManager = _SignInManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
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
                    UserName = model.Email,
                    //list of default folders that every user should have
                    Folders = new List<Folder>() {
                        new Folder() {Name = "Inbox"},
                        new Folder() {Name = "Sent"},
                        new Folder() {Name = "Drafts"},
                        new Folder() {Name = "Favorites"},
                        new Folder() {Name = "Trash"}
                    }
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