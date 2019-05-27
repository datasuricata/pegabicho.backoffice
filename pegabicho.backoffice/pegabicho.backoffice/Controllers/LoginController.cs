using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pegabicho.backoffice.Request.Base;
using pegabicho.shared.Endpoints;
using pegabicho.shared.Notifications;
using static pegabicho.backoffice.Models.Enums;

namespace pegabicho.backoffice.Controllers
{
    public class LoginController : CoreController
    {
        public LoginController(WebService WebService) : base(WebService)
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string senha)
        {
            try
            {
                DomainNotifyer.When(string.IsNullOrEmpty(login), "Login não pode ser vazio.");
                var request = await Post<dynamic>(eLogin.customer, new { email = login, password = senha });
                return View();
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error);
                return RedirectToAction("Login");
            }
        }
    }
}