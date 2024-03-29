﻿using MailService.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Mail
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public DateTime SentDate { get; set; }
        public DateTime ReadDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodySummary { get; set; }
        public bool IsRead { get; set; }
        public string Sender_id { get; set; }
        [ForeignKey("Sender_id")]
        public ApplicationUser Sender { get; set; }
        public string Reciever_id { get; set; }
        [ForeignKey("Reciever_id")]
        public virtual ApplicationUser Reciever { get; set; }
        public ICollection<MailFolder> MailFolders { get; set; }
        public string State_Id { get; set; }
        [ForeignKey("State_Id")]
        public State States { get; set; }
    }
}
