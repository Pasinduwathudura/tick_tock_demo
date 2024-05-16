using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using ticktok_demo.Models;
using static ticktok_demo.Providers.ApplicationOAuthProvider;

namespace ticktok_demo.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly tickEntities db;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            _publicClientId = publicClientId;
            db = new tickEntities(); // Initialize tickEntities
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            // Retrieve employee data using user ID (employee no.)
            employee employee = await db.employees.FirstOrDefaultAsync(e => e.user_id == user.Id);

            if (employee == null)
            {
                context.SetError("invalid_employee", "Employee data not found.");
                return;
            }



            //user_role_reference roleRefDetails = await db.user_role_reference.FirstOrDefaultAsync(e => e.userId == user.Id);
            //user_roles roleDetails = await db.user_roles.FirstOrDefaultAsync(e => e.roleId == roleRefDetails.roleId);

            List<user_role_reference> roleRefDetailsList = await db.user_role_reference
    .Where(e => e.userId == user.Id)
    .ToListAsync();

            List<user_roles> roleDetailsList = new List<user_roles>();
            foreach (var roleRefDetails in roleRefDetailsList)
            {
                user_roles roleDetails = await db.user_roles
                    .FirstOrDefaultAsync(e => e.roleId == roleRefDetails.roleId);

                if (roleDetails != null)
                {
                    roleDetailsList.Add(roleDetails);
                }
                // If you expect all role references to have corresponding role details,
                // you may want to handle the case where roleDetails is null differently.
            } 

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user, employee, roleDetailsList);

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }


public static AuthenticationProperties CreateProperties(ApplicationUser user, employee employee, List<user_roles> roleDetailsList)
{
    var data = new Dictionary<string, string>
    {
        { "userName", user.UserName },
        { "userId", user.Id },
        { "employeeNo", employee.employee_no.ToString() },
        { "employeeFName", employee.emp_first_name },
        { "employeeLName", employee.emp_last_name },
        { "pic", employee.pic.ToString() },
        { "isActiveMobile", employee.is_active_mobile.ToString().ToLower() },
        { "isActiveWeb", employee.is_active_web.ToString().ToLower()}

         };


            // Create AuthenticationProperties with the string dictionary
            AuthenticationProperties properties = new AuthenticationProperties(data);
         
            return properties;
}

 



        internal static AuthenticationProperties CreateProperties(string userName)
        {
            throw new NotImplementedException();
        }
    }
}


