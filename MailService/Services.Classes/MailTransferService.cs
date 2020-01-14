using MailService.Areas.Identity.Data;
using MailService.Models;
using MailService.Services.Interfaces;
using MailService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailService.Services.Classes
{
    public class MailTransferService : IMailTransferService
    {
        private readonly MailServiceContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ControllerBase _controller;

        public enum Status
        {
            Succeeded = 0,
            Failed = 1
        }

        public MailTransferService(MailServiceContext db, UserManager<ApplicationUser> userManager, ControllerBase controller)
        {
            _db = db;
            _userManager = userManager;
            _controller = controller;
        }
        public async Task<Mail> FindMailByIdAsync(string MailId) =>
            await _db.Mails
            .Include(x => x.Sender)
            .FirstOrDefaultAsync(x => x.id == MailId);

        public async Task<List<Mail>> GetMailsAsync(string userId) =>
            await _db.Mails
            .Where(x => x.Reciever_id == userId)
            .OrderByDescending(x => x.SentDate)
            .ToListAsync();

        public async Task<List<Mail>> GetMailsAsync(string userId, string folderId) =>
            await _db.MailFolders.Include(mf => mf.Mail)
            .Where(mf => mf.Mail.Reciever_id == userId && mf.Folder_id == folderId)
            .Select(mf => mf.Mail)
            .OrderByDescending(m => m.SentDate)
            .ToListAsync();

        public async Task<int> AddNewMailAsync(NewMailViewModel mailModel)
        {
            var reciever = await _userManager.FindByEmailAsync(mailModel.RecieverEmail);
            var sender = await _userManager.FindByNameAsync(_controller.User.Identity.Name);

            if (reciever != null)
            {
                Mail mail = new Mail
                {
                    Body = mailModel.Body,
                    Subject = mailModel.Subject,
                    IsRead = false,
                    SentDate = DateTime.Now,
                    Sender = sender,
                    Reciever = reciever,
                    //this line initialize the list of folders that this mail will be inside theme.
                    MailFolders = new List<MailFolder>()
                };

                //Body Summary without HTMLTags
                if (!string.IsNullOrEmpty(mailModel.Body))
                {
                    mailModel.Body = Regex.Replace(mailModel.Body, "</p>", " ");
                    mailModel.Body = Regex.Replace(mailModel.Body, "<.*?>", string.Empty);
                    mailModel.Body = Regex.Replace(mailModel.Body, "&nbsp;", string.Empty);
                    try
                    {
                        mail.BodySummary = mailModel.Body.Substring(0, 200);
                    }
                    catch (Exception)
                    {
                        mail.BodySummary = mailModel.Body;
                    }
                }

                //this line put this mail inside the senders sent folder
                mail.MailFolders.Add(new MailFolder() { Folder = sender.Folders.ElementAt((int)Folder.DefaultFolder.Sent) });

                //this line put this mail inside the recievers inbox folder
                mail.MailFolders.Add(new MailFolder() { Folder = reciever.Folders.ElementAt((int)Folder.DefaultFolder.Inbox) });

                try
                {
                    _db.Mails.Add(mail);
                    await _db.SaveChangesAsync();
                    return (int)Status.Succeeded;
                }
                catch (Exception e)
                {
                    return (int)Status.Failed;
                }
            }
            return (int)Status.Failed;
        }

        public async Task<Mail> MoveToFolderAsync(Mail mail, Folder folder)
        {
            Task.Run(() => 
            {
                mail.MailFolders.Add(new MailFolder() { Folder = folder });
            }).Wait();
            
            return mail;
        }
    }
}
