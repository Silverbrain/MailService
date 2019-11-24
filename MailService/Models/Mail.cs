using MailService.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Mail
    {
        public int id { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ReadDate { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public bool IsRead { get; set; }
        public SentMail Sender { get; set; }
        public RecievedMail Reciever { get; set; }
    }
}
