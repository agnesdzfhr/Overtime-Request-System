using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Base;
using API.Context;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using API.ViewModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly MyContext myContext;
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, MyContext myContext, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var login = accountRepository.Login(loginVM);

                switch (login)
                {
                    case HttpStatusCode.OK:
                        var getRoles = accountRepository.GetRoles(loginVM.Email);
                        var findAcc = myContext.Accounts.FirstOrDefault(a => a.Email == loginVM.Email);
                        var findEmp = myContext.Employees.FirstOrDefault(e=>e.NIK == findAcc.NIK);

                        var claims = new List<Claim>
                        {
                            new Claim("email", loginVM.Email),
                            new Claim("nik", findEmp.NIK)

                        };
                        foreach (var roleData in getRoles)
                        {
                            claims.Add(new Claim("roles", roleData.ToString()));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(10),
                                signingCredentials: signIn
                            );
                        var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                        claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                        return Ok(new JWTtokenVM { status = HttpStatusCode.OK, idtoken = idToken, message = "Login Success" });
                    default:
                        return Ok(new JWTtokenVM { status = HttpStatusCode.BadRequest, idtoken = null, message = "Email or Password Wrong"});
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var forgotPassword = accountRepository.ForgotPassword(forgotPasswordVM);
            return forgotPassword switch
            {
                HttpStatusCode.OK => Ok(new { status = HttpStatusCode.OK, forgotPassword, message = "OTP already sent, please check your email" }),
                HttpStatusCode.NotFound => BadRequest(new { status = HttpStatusCode.BadRequest, forgotPassword, message = "Email not registered" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, forgotPassword, message = "Your email is empty" })
            };
        }
        [HttpPut("ChangePassword")]
        public ActionResult ChengePassword(ChangePasswordVM changePasswordVM)
        {
            var changePassword = accountRepository.ChangePassword(changePasswordVM);
            return changePassword switch
            {
                HttpStatusCode.OK => Ok(new { status = HttpStatusCode.OK, changePassword, message = "Change Password Success" }),
                HttpStatusCode.BadRequest => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP expired" }),
                HttpStatusCode.Forbidden => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP already used" }),
                HttpStatusCode.NotAcceptable => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP incorrect" }),
                HttpStatusCode.NotFound => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "Email not registered" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "Change Password Failed, your email is empty" }),
            };
        }
    }
}
