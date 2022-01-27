using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.Extensions.Configuration;
using API.ViewModel;
using System.Net;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public class Hashing
        {
            private static string GetRandomSalt()
            {
                return BCrypt.Net.BCrypt.GenerateSalt(12);
            }
            public static string HashPassword(string Password)
            {
                return BCrypt.Net.BCrypt.HashPassword(Password, GetRandomSalt());
            }
            public static bool ValidatePassword(string Password, string correctHash)
            {
                return BCrypt.Net.BCrypt.Verify(Password, correctHash);
            }
        }

        public IEnumerable<object> GetRoles(string email)
        {
            var findEmail = myContext.Accounts.Where(a => a.Email == email).FirstOrDefault();
            var AccountRole = myContext.AccountRoles.Where(ar => ar.Account_ID == findEmail.Account_ID).Select(ar => ar.Role.Name).ToList();
            return AccountRole;

        }

        public HttpStatusCode Login(LoginVM loginVM)
        {
            var findEmail = myContext.Accounts.FirstOrDefault(a => a.Email == loginVM.Email);

            if (findEmail != null)
            {
                var findNIK = myContext.Accounts.FirstOrDefault(a => a.NIK == findEmail.NIK);
                bool verifiedPass = Hashing.ValidatePassword(loginVM.Password, findNIK.Password);
                if (verifiedPass == true)
                {
                    return HttpStatusCode.OK; //Login Success
                }
                else
                {
                    return HttpStatusCode.BadRequest; //Wrong Password
                }
            }
            else
            {
                return HttpStatusCode.BadRequest; //Email Not Found
            }
        }
    }
}
