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
                    case 1:
                        var getRoles = accountRepository.GetRoles(loginVM.Email);

                        var claims = new List<Claim>
                        {
                            new Claim("email", loginVM.Email),

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
                    case 2:
                        return Ok(new JWTtokenVM { status = HttpStatusCode.BadRequest, idtoken = null, message = "Wrong Password" });
                    case 3:
                        return Ok(new JWTtokenVM { status = HttpStatusCode.NotFound, idtoken = null, message = "Email Not Found" });
                    default:
                        return Ok(new JWTtokenVM { status = HttpStatusCode.BadRequest, idtoken = null, message = "Login Failed" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
