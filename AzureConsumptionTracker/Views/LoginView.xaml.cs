using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AzureConsumptionTracker.Helper;

namespace AzureConsumptionTracker
{
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();
		}

		private async void LoginButtonOnClicked(object sender, EventArgs e)
		{
			var data = await DependencyService.Get<IAuthenticator>()
				.Authenticate(App.LoginAuthority, App.ManagementResourceUri, App.ClientId, App.ReturnUri);
			App.AuthenticationResult = data;
			var userName = data.UserInfo.GivenName + " " + data.UserInfo.FamilyName;
			await DisplayAlert("Authenticated User:", userName, "Ok");
			this.Navigation.PushAsync (new SubscriptionsView ());
		}
	}
}

