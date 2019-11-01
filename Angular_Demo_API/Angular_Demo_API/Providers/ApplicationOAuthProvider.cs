using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Angular_Demo_API.Models;
using Angular_Demo_API.Models.DAL;
using System.IO;

namespace Angular_Demo_API.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private _IAllRepositry<RegisterUser> interfaceObj;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            this.interfaceObj = new AllRepositry<RegisterUser>();

            _publicClientId = publicClientId;
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

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);

            var AllUser = interfaceObj.GetModel();
            var regUserId = AllUser.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstOrDefault();


            RegisterUser RUserData = new RegisterUser();

            var Udata = interfaceObj.GetModelById(Convert.ToInt32(regUserId));
            properties.Dictionary.Add("Id",Udata.Id.ToString());
            properties.Dictionary.Add("FirstName", Udata.FirstName);
            properties.Dictionary.Add("MiddelName", Udata.MiddelName);
            properties.Dictionary.Add("LastName", Udata.LastName);
            properties.Dictionary.Add("Email", Udata.Email);
            properties.Dictionary.Add("Phone", Udata.Phone);
            properties.Dictionary.Add("Role", Udata.Role);

            string base64String = null;
            string path = System.Web.HttpContext.Current.Server.MapPath(Udata.Image);
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    
                }
            }
            properties.Dictionary.Add("Image", base64String);


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
        Uri expectedRootUri = new Uri(context.RedirectUri);

        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        {
            context.Validated();
        }
    }

    return Task.FromResult<object>(null);
}

public static AuthenticationProperties CreateProperties(string userName)
{
    IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
    return new AuthenticationProperties(data);
}
    }
}