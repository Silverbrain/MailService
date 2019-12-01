using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Reader
    {       
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string State_Id { get; set; }
        [ForeignKey("State_Id")]
        public States States { get; set; }
    }
}
