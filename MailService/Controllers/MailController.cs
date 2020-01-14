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
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using MailService.Services.Interfaces;

namespace MailService.Controllers
{
    public class MailController : Controller
    {
        UserManager<ApplicationUser> userManager;
        MailServiceContext db;
        IMailTransferService _transferService;

        public MailController(UserManager<ApplicationUser> _userManager, MailServiceContext _db, IMailTransferService transferService)
        {
            userManager = _userManager;
            db = _db;
            _transferService = transferService;
        }

        //this page show the mails list to the user _for now this is the default page_
        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var mails = await _transferService.GetMailsAsync(user.Id);
            return View(mails);
        }

        //in this page user can see a single mail
        [Authorize]
        public async Task<IActionResult> ViewMail(string MailId) =>
            View(await _transferService.FindMailByIdAsync(MailId));

        //this action opens the mail compose page
        [Authorize]
        public IActionResult NewMail()
        {
            return View();
        }

        //this method creates a new mail and save it into the database
        [Authorize]
        public async Task<IActionResult> SendMail(NewMailViewModel model)
        {
            var sender = await userManager.FindByNameAsync(User.Identity.Name);
            return RedirectToAction("Inbox", "Mail");
        }
    }
}