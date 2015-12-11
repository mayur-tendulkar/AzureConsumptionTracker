using System;

using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureConsumptionTracker
{
	public class App : Application
	{
		public static string ClientId = "<your-client-id>";
		public static string LoginAuthority = "https://login.microsoftonline.com/<your-tenant-id>";
		public static string ReturnUri = "<your-redirect-uri>";
		public static string GraphResourceUri = "https://graph.windows.net/";
		public static string ManagementResourceUri = "https://management.core.windows.net/";
		public static AuthenticationResult AuthenticationResult = null;
		public static AzureConsumptionTracker.Model.SubscriptionResponse.Subscription SelectedSubscription { get; set; }

		public App ()
		{
			
			MainPage = new NavigationPage(new LoginView ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

