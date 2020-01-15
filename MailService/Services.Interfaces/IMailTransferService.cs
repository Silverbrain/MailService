using MailService.Areas.Identity.Data;
using MailService.Models;
using MailService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Services.Interfaces
{
    public interface IMailTransferService
    {
        Exception FailedToAddNewMailException { get; set; }
        Exception UserNotFoundException { get; set; }

        Task<int> AddNewMailAsync(NewMailViewModel mailModel);
        Task<Mail> FindMailByIdAsync(string mailId);
        Task<List<Mail>> GetMailsAsync();
        Task<List<Mail>> GetMailsAsync(string folderId);
    }
}
