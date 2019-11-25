using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MailService.Models;
using Microsoft.AspNetCore.Identity;

namespace MailService.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; set; }
        public String Family { get; set; }

        //Unable to determine the relationship represented by navigation property 'ApplicationUser.Mails' (fixed!!!)
        [InverseProperty("Sender")]
        public ICollection<Mail> SentMails { get; set; }
        [InverseProperty("Reciever")]
        public ICollection<Mail> RecievedMails { get; set; }

        //list of default folders that every user should have
        public List<Folder> Folders { get; set; } = new List<Folder>() {
            new Folder() {Name = "Inbox"},
            new Folder() {Name = "Sent"},
            new Folder() {Name = "Drafts"},
            new Folder() {Name = "Favorites"},
            new Folder() {Name = "Trash"}
        };
    }
}
