using MailService.Areas.Identity.Data;
using MailService.Models;
using MailService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Services.Interfaces
{
    public interface IMailTransferService
    {
        Task<int> AddNewMailAsync(NewMailViewModel mailModel);
        Task<Mail> FindMailByIdAsync(string mailId);
        Task<List<Mail>> GetMailsAsync(string userId);
        Task<List<Mail>> GetMailsAsync(string userId, string folderId);
    }
}
