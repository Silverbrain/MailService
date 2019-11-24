using MailService.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class SentMail
    {
        public int id { get; set; }
        public int Mail_id { get; set; }
        public string Sender_id { get; set; }
        [ForeignKey("Mail_id")]
        public Mail Mail { get; set; }
        [ForeignKey("Sender_id")]
        public ApplicationUser Sender { get; set; }
    }
}
