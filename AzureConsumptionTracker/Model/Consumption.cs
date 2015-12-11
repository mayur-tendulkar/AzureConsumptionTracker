using System;
using Newtonsoft.Json;

namespace AzureConsumptionTracker.Model 
{
	public partial class ConsumptionResponse
	{
		public class InfoFields
		{

			[JsonProperty("meteredRegion")]
			public string MeteredRegion { get; set; }

			[JsonProperty("meteredService")]
			public string MeteredService { get; set; }

			[JsonProperty("meteredServiceType")]
			public string MeteredServiceType { get; set; }

			[JsonProperty("project")]
			public string Project { get; set; }
		}
	}

	public partial class ConsumptionResponse
	{
		public class Properties
		{

			[JsonProperty("subscriptionId")]
			public string SubscriptionId { get; set; }

			[JsonProperty("usageStartTime")]
			public string UsageStartTime { get; set; }

			[JsonProperty("usageEndTime")]
			public string UsageEndTime { get; set; }

			[JsonProperty("meterName")]
			public string MeterName { get; set; }

			[JsonProperty("meterCategory")]
			public string MeterCategory { get; set; }

			[JsonProperty("unit")]
			public string Unit { get; set; }

			[JsonProperty("meterId")]
			public string MeterId { get; set; }

			[JsonProperty("infoFields")]
			public InfoFields InfoFields { get; set; }

			[JsonProperty("quantity")]
			public double Quantity { get; set; }

			[JsonProperty("meterSubCategory")]
			public string MeterSubCategory { get; set; }

			[JsonProperty("instanceData")]
			public string InstanceData { get; set; }

			public override string ToString ()
			{
				return MeterName + " (" + InfoFields.Project + ")";
			}
		}
	}

	public partial class ConsumptionResponse
	{
		public class Value
		{

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("type")]
			public string Type { get; set; }

			[JsonProperty("properties")]
			public Properties Properties { get; set; }
		}
	}

	public partial class ConsumptionResponse
	{

		[JsonProperty("value")]
		public Value[] ResponseData { get; set; }
	}
}

