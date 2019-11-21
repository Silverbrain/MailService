using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    public class MailController : Controller
    {
        [Authorize]
        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult ViewMail()
        {
            return View();
        }

        public IActionResult NewMail()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            return View();
        }
    }
}