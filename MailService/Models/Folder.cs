using MailService.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Folder
    {
        public enum DefaultFolder 
        {
            Inbox = 1,
            Sent = 2,
            Drafts = 3,
            Favorites = 4,
            Trash = 5
        }


        public string id { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; }

        public ICollection<MailFolder> Mails { get; set; }

        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public ApplicationUser User { get; set; }
    }
}
