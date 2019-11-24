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

namespace MailService.Controllers
{
    public class MailController : Controller
    {
        UserManager<ApplicationUser> userManager;
        MailServiceContext db;

        public MailController(UserManager<ApplicationUser> _userManager, MailServiceContext _db)
        {
            userManager = _userManager;
            db = _db;
        }

        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(db.Mails.Include(x => x.Sender).ThenInclude(x => x.Sender).Where(x => x.Reciever.Reciever == user).OrderByDescending(x => x.SentDate).ToList());
        }

        public async Task<IActionResult> ViewMail(int MailId)
        { 
            return View(await db.Mails.Include(x => x.Sender).ThenInclude(x=>x.Sender).FirstOrDefaultAsync(x=>x.id == MailId));
        }

        //this action opens the mail compose page
        public IActionResult NewMail()
        {
            return View();
        }

        //this method creates a new mail and save it into the database
        public async Task<IActionResult> SendMail(NewMailViewModel model)
        {
            var reciever = await userManager.FindByEmailAsync(model.RecieverEmail);
            if (reciever != null)
            {
                Mail mail = new Mail
                {
                    Body = model.Body,
                    Subject = model.Subject,
                    IsRead = false,
                    SentDate = DateTime.Now,
                    Sender = new SentMail { Sender = await userManager.FindByNameAsync(User.Identity.Name) },
                    Reciever = new RecievedMail { Reciever = reciever }
                };
                db.Mails.Add(mail);
                await db.SaveChangesAsync();
                return RedirectToAction("Inbox","Mail");
            }
            else
            {
                //error message
            }

            return View();
        }
    }
}