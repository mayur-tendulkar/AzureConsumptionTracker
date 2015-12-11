using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;
using AzureConsumptionTracker.Helper;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AzureConsumptionTracker.iOS.Helper.Authenticator))]
namespace AzureConsumptionTracker.iOS.Helper
{
	class Authenticator : IAuthenticator
    {
		public async Task<AuthenticationResult> AuthenticateSilently (string tenantId, string resource, string clientId)
		{
			var loginAuthnority = "https://login.microsoftonline.com/" + tenantId;
			var authContext = new AuthenticationContext (loginAuthnority);
			var authResult = await authContext.AcquireTokenSilentAsync(resource, clientId, new UserIdentifier(App.AuthenticationResult.UserInfo.UniqueId, UserIdentifierType.UniqueId)); 
			return authResult;
		}

        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
		
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri),
                new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
            return authResult;
        }

    }
}
