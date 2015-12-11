using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AzureConsumptionTracker.Model;
using AzureConsumptionTracker.Helper;
using System.Collections.ObjectModel;

namespace AzureConsumptionTracker
{
	public partial class SubscriptionsView : ContentPage
	{
		bool IsFirstRunComplete { get; set; } = false;
		HttpClient client;
		TenantResponse tenantCollection;
		ObservableCollection<SubscriptionResponse.Subscription> SubscriptionCollection { get; set; }

		public SubscriptionsView ()
		{
			InitializeComponent ();
			SubscriptionCollection = new ObservableCollection<SubscriptionResponse.Subscription> ();
			client = new HttpClient ();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			if(!IsFirstRunComplete)
				await GetTenants ();
			SubscriptionsList.ItemsSource = SubscriptionCollection;
			SubscriptionsList.ItemTapped += SubscriptionsListItemTapped;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			SubscriptionsList.ItemsSource = null;
			this.SubscriptionsList.ItemTapped -= SubscriptionsListItemTapped;
		}

		void SubscriptionsListItemTapped (object sender, ItemTappedEventArgs e)
		{
			App.SelectedSubscription = (SubscriptionResponse.Subscription)e.Item;
			this.SubscriptionsList.SelectedItem = null;
			this.Navigation.PushAsync (new DetailsView ());
		}

		private async Task GetTenants()
		{
			var requestUrl = "https://management.azure.com/tenants?api-version=2015-01-01";
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthenticationResult.AccessToken);

			try {
				var tenantResponse = await client.GetStringAsync(requestUrl);
				tenantCollection = JsonConvert.DeserializeObject<TenantResponse> (tenantResponse);
			} catch (Exception ex) {
				//await DisplayAlert("Error!", ex.Message, "Dismiss");
			}
			foreach (var tenant in tenantCollection.TenantCollection) {
				await  GetSubscriptions (tenant.TenantId);
			}
		}

		private async Task GetSubscriptions(string tenantId)
		{
			var requestUrl = "https://management.azure.com/subscriptions?api-version=2015-01-01";
			try {
				var data = await DependencyService.Get<IAuthenticator> ().AuthenticateSilently (tenantId, App.ManagementResourceUri, App.ClientId);
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.AccessToken);
				var subsriptionResponse = await client.GetStringAsync (requestUrl);
				var subscriptions = JsonConvert.DeserializeObject<SubscriptionResponse>(subsriptionResponse);
				foreach (var subscription in subscriptions.SubscriptionCollection) {
					SubscriptionCollection.Add(subscription);
				}
				IsFirstRunComplete = true;
			} catch (Exception ex) {
				//await DisplayAlert("Error!", ex.Message, "Dismiss");
			}

		}
	}
}

