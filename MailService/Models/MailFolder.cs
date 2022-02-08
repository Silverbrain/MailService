using MailService.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailService.Models
{
    public class MailFolder
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string Mail_id { get; set; }
        public string Folder_id { get; set; }
        public string User_id { get; set; }

        [ForeignKey("Mail_id")]
        public Mail Mail { get; set; }
        [ForeignKey("Folder_id")]
        public Folder Folder { get; set; }
    }
}