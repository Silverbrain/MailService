using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class State
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string Flowchart_Id { get; set; }
        [ForeignKey("Flowchart_Id")]
        public Flowchart Flowchart { get; set; }
        public ICollection<Reader> Readers { get; set; }
        public ICollection<Mail> Mails { get; set; }

    }
}