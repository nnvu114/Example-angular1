using System;
using Petronas.SampleApp.Model;
using Petronas.SampleApp.Repository.Classes;
using Petronas.SampleApp.Repository.Classes.Shared;
using Petronas.SampleApp.WebApi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Petronas.SampleApp.WebApi
{
    /// <summary>
    /// Autofac configuration
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// OAuthBearerAuthenticationOptions
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        /// <summary>
        /// Configuration IAppBuilder
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider(new AuthRepository(new SampleAppContext())),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(new AuthRepository(new SampleAppContext()))
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            ////Configure Google External Login
            //googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "xxxxxx",
            //    ClientSecret = "xxxxxx",
            //    Provider = new GoogleAuthProvider()
            //};
            //app.UseGoogleAuthentication(googleAuthOptions);

            ////Configure Facebook External Login
            //facebookAuthOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = "xxxxxx",
            //    AppSecret = "xxxxxx",
            //    Provider = new FacebookAuthProvider()
            //};
            //app.UseFacebookAuthentication(facebookAuthOptions);
        }
    }
}