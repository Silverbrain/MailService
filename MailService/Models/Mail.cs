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
        public String Subject { get; set; }
        public String Body { get; set; }
        public string Sender_id { get; set; }
        [ForeignKey("Sender_id")]
        public ApplicationUser Sender { get; set; }
        public string Reciever_id { get; set; }
        [ForeignKey("Reciever_id")]
        public ApplicationUser Reciever { get; set; }
    }
}
