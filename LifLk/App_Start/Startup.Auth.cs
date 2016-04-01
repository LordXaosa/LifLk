using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Providers.OpenID;
using Owin.Security.Providers.Steam;


namespace LifLk
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            var options = new SteamAuthenticationOptions
            {
                ApplicationKey = "D1B966B943125B996AACF10C795382BB",
                Provider = new OpenIDAuthenticationProvider // Steam is based on OpenID
                {
                    OnAuthenticated = async context =>
                    {
                        // Retrieve the user's identity with info like username and steam id in Claims property
                        var identity = context.Identity;
                        string idstr = identity.Claims.FirstOrDefault().Value;
                        idstr = idstr.Split('/')[5];
                        long id = long.Parse(idstr);
                        HttpContext.Current.Session["userSteamId"] = id;
                    }
                }
            };
            app.UseSteamAuthentication(options);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}