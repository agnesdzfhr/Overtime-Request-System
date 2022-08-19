using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.Extensions.Configuration;
using API.ViewModel;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

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
            var AccountRole = myContext.AccountRoles.Where(ar => ar.AccountID == findEmail.AccountID).Select(ar => ar.Role.Name).ToList();
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

        public HttpStatusCode ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (forgotPasswordVM.Email != "")
            {
                var checkAcc = myContext.Accounts.SingleOrDefault(a => a.Email == forgotPasswordVM.Email);
                if (checkAcc != null)
                {
                    Random random = new Random();
                    checkAcc.TokenOTP = random.Next(100000, 999999);
                    checkAcc.ExpiredToken = DateTime.Now.AddMinutes(5);

                    var fromAddress = new MailAddress("mccreg61net@gmail.com");
                    var passwordFrom = "61mccregnet";
                    var toAddress = new MailAddress(forgotPasswordVM.Email);
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, passwordFrom)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "Forgot Password",
                        Body = "Hello, " + checkAcc.Employee.FirstName + " here's your current OTP : " + checkAcc.TokenOTP + ".Immediately do Change Password",
                        IsBodyHtml = true,
                    }) smtp.Send(message);
                    myContext.Entry(checkAcc).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
            }
            return HttpStatusCode.BadRequest;
        }
        public HttpStatusCode ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (changePasswordVM.Email != "")
            {
                var checkAcc = myContext.Accounts.SingleOrDefault(e => e.Email == changePasswordVM.Email);
                if (checkAcc != null)
                {
                    var checkOTP = myContext.Accounts.SingleOrDefault(a => a.NIK == checkAcc.NIK);
                    if (checkOTP.TokenOTP == changePasswordVM.TokenOTP)
                    {
                        if (checkOTP.IsUsed == false)
                        {
                            if (checkOTP.ExpiredToken > DateTime.Now)
                            {
                                checkAcc.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword);
                                checkOTP.IsUsed = true;
                                myContext.Entry(checkOTP).State = EntityState.Modified;
                                myContext.SaveChanges();
                                return HttpStatusCode.OK;
                            }
                            else
                            {
                                return HttpStatusCode.BadRequest;
                            }
                        }
                        else
                        {
                            return HttpStatusCode.Forbidden;
                        }
                    }
                    else
                    {
                        return HttpStatusCode.NotAcceptable;
                    }
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
            }
            return HttpStatusCode.BadRequest;
        }

    }
}
