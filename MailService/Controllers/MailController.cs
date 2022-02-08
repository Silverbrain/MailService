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
    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        IMailTransferService _transferService;

        public MailController(IMailTransferService transferService)
        {
            _transferService = transferService;
        }

        //this page show the mails list to the user _for now this is the default page_
        public async Task<IActionResult> Inbox()
        {
            var mails = await _transferService.GetMailsAsync();
            return View(mails);
        }

        //in this page user can see a single mail
        public async Task<IActionResult> ViewMail(string MailId) =>
            View(await _transferService.FindMailByIdAsync(MailId));

        //this action opens the mail compose page
        public IActionResult NewMail()
        {
            return View();
        }

        //this method creates a new mail and save it into the database
        public async Task<IActionResult> SendMail(NewMailViewModel model)
        {
            try
            {
                var status = await _transferService.AddNewMailAsync(model);
                
                if (status == (int)Status.Succeeded)
                    return RedirectToAction("Inbox", "Mail");
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return RedirectToAction("Inbox", "Mail");
            }
        }
    }
}