using System;
using AzureConsumptionTracker.Helper;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using Android.App;
using Xamarin.Forms;
[assembly: Dependency(typeof(AzureConsumptionTracker.Droid.Helper.Authenticator))]
namespace AzureConsumptionTracker.Droid.Helper
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
			var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
			return authResult;
		}
	}
}

