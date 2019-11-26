using System;
using System.Collections.Generic;
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
    }
}
