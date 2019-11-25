using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Folder
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; }

        public ICollection<MailFolder> Mails { get; set; }
    }
}
