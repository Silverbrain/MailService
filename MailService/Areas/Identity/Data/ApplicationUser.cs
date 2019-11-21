using System;
using System.Collections.Generic;
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

        //Unable to determine the relationship represented by navigation property 'ApplicationUser.Mails'
        public ICollection<SentMail> SentMails { get; set; }
        public ICollection<RecievedMail> RecievedMails { get; set; }
    }
}
