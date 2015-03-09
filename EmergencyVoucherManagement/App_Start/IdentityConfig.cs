﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using EmergencyVoucherManagement.Models.Vouchers;
using EmergencyVoucherManagement.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Web;

namespace EmergencyVoucherManagement
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        private async Task<bool> AuthAgainstAD(string userName, string password)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(
                String.Format("username={0}&password={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(password)),
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await client.PostAsync("https://auth.rescue.org/SimpleAuthenticationRESTService.aspx", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                return result["result"].Value<bool>();
            }

            return false;
        }

        public override async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            var user = base.FindAsync(userName, password).Result;

            if (user == null)
            {
                if (await AuthAgainstAD(userName, password))
                {
                    var identity = await CreateAsync(new ApplicationUser
                    {
                        UserName = userName,
                        Email = String.Format("{0}@theirc.org", userName)
                    }, password);

                    return await base.FindAsync(userName, password);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return user;
            }
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
