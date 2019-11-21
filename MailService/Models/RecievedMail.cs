using MailService.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class RecievedMail
    {
        public int id { get; set; }
        public int Mail_id { get; set; }
        public string Reciever_id { get; set; }
        [ForeignKey("Mail_id")]
        public Mail Mail { get; set; }
        [ForeignKey("Reciever_id")]
        public ApplicationUser Reciever { get; set; }
    }
}
