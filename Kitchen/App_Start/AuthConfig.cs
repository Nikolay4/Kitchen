using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Kitchen.Models;
using MvcApplication.Code;

namespace Kitchen
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // следует обновить сайт. Дополнительные сведения: http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

            OAuthWebSecurity.RegisterClient(
                   client: new VKontakteAuthenticationClient(
                          "4885594", "RrqQEsLdQM8aN3GUMkYn"),
                   displayName: "ВК", // надпись на кнопке
                   extraData: null);
                    }

    }
}
