using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.ViewModels
{
    public class LoginViewModel
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
