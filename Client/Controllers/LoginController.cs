using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.ViewModel;

namespace Client.Controllers
{
    public class LoginController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public LoginController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("Login/Auth")]
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.idtoken;

            if (token == null)
            {
                //return RedirectToAction("index");
                TempData["message"] = jwtToken.message;
                return RedirectToAction("index", "Login"); //untuk return ke halaman Employees/Login.cshtml

            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "Home");
        }
    }
}
