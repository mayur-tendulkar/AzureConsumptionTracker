using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AzureConsumptionTracker
{
	public partial class DetailsView : ContentPage
	{
		public DetailsView ()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			var client = new HttpClient ();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthenticationResult.AccessToken);
			var startTime = DateTime.Today.Subtract (new TimeSpan (1, 0, 0, 0)).ToString ("yyyy-MM-ddTHH:mm:ss");
			var endTime = DateTime.Today.ToString ("yyyy-MM-ddTHH:mm:ss");
			var requestUrl = String.Format("https://management.azure.com/subscriptions/{0}/providers/Microsoft.Commerce/UsageAggregates?api-version=2015-06-01-preview&reportedStartTime={1}&reportedEndTime={2}&aggregationGranularity=Daily&showDetails=true", App.SelectedSubscription.SubscriptionId, startTime, endTime);
			var usageData = await client.GetStringAsync (requestUrl);
			var data = JsonConvert.DeserializeObject<AzureConsumptionTracker.Model.ConsumptionResponse> (usageData);
			this.DetailsList.ItemsSource = data.ResponseData;
			int i = 100;
		}
	}
}

