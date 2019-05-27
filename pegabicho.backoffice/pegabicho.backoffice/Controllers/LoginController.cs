using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pegabicho.backoffice.Controllers
{
    public class LoginController : CoreController
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string senha)
        {
            var userLoginCommand = new LoginDTO();
            userLoginCommand.Login = login;
            userLoginCommand.Senha = senha;
            var request = await Post<T>("/api/Login/backoffice", LoginDTO);
            return View();
        }
    }
}