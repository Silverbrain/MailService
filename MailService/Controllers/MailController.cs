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

        //this page show the mails list to the user _for now this is the default page_
        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(db.Mails.Include(x => x.Sender).Where(x => x.Reciever == user).OrderByDescending(x => x.SentDate).ToList());
        }

        //in this page user can see a single mail
        [Authorize]
        public async Task<IActionResult> ViewMail(string MailId)
        {
            return View(await db.Mails.Include(x => x.Sender).FirstOrDefaultAsync(x => x.id == MailId));
        }

        //this action opens the mail compose page
        [Authorize]
        public IActionResult NewMail()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> MailBox(string Choose)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            switch (Choose)
            {
                case "Inbox":
                    return View("MailBox", db.Mails.Include(x => x.Sender).Where(x => x.Reciever == user).OrderByDescending(x => x.SentDate).ToList());
                case "Sent":
                    return View("MailBox", db.Mails.Include(x => x.Sender).Where(x => x.Sender == user).OrderByDescending(x => x.SentDate).ToList());
                default:
                    return BadRequest();
            }
        }

        //this method creates a new mail and save it into the database
        [Authorize]
        public async Task<IActionResult> SendMail(NewMailViewModel model)
        {
            var reciever = await userManager.FindByEmailAsync(model.RecieverEmail);
            var sender = await userManager.FindByNameAsync(User.Identity.Name);

            if (reciever != null)
            {
                Mail mail = new Mail
                {
                    Body = model.Body,
                    Subject = model.Subject,
                    IsRead = false,
                    SentDate = DateTime.Now,
                    Sender = sender,
                    Reciever = reciever,
                    //this line initialize the list of folders that this mail will be inside theme.
                    Folders = new List<MailFolder>()
                };

                 //Body Summary without HTMLTags
                if (!string.IsNullOrEmpty(model.Body))
                {
                    model.Body = Regex.Replace(model.Body, "</p>", " ");
                    model.Body = Regex.Replace(model.Body, "<.*?>", string.Empty);
                    model.Body = Regex.Replace(model.Body, "&nbsp;", string.Empty);
                    try
                    {
                       mail.BodySummary = model.Body.Substring(0,200);

                    }
                    catch (Exception)
                    {

                       mail.BodySummary=model.Body;
                    }
                }
                //this line put this mail inside the senders sent folder
                mail.Folders.Add(new MailFolder() { Folder = sender.Folders[(int)Folder.DefaultFolder.Sent] });

                //this line put this mail inside the recievers inbox folder
                mail.Folders.Add(new MailFolder() { Folder = reciever.Folders[(int)Folder.DefaultFolder.Inbox] });

                db.Mails.Add(mail);
                await db.SaveChangesAsync();
                return RedirectToAction("Inbox", "Mail");
            }
            else
            {
                //error message
            }

            return View();
        }
    }
}