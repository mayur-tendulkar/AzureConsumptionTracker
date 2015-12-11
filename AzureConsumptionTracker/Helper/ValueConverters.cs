using System;
using Xamarin.Forms;

namespace AzureConsumptionTracker.Helper
{
	public class UnitConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null) {
				var unit = value as Model.ConsumptionResponse.Value;
				if (unit.Properties.Unit.ToLower () == "apps")
					return unit.Properties.Quantity + " units";
				return unit.Properties.Quantity + " as " + unit.Properties.Unit;
			} else
				return string.Empty;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return string.Empty;//throw new NotImplementedException ();
		}

		#endregion
	}

	public class ProjectConverter : IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null) {
				var unit = value as Model.ConsumptionResponse.Value;
				if (unit.Properties.InfoFields.Project != null || unit.Properties.InfoFields.MeteredService != null)
					return string.Format("{0} ({1})", unit.Properties.InfoFields.Project, unit.Properties.InfoFields.MeteredService);
				return (value as Model.ConsumptionResponse.Value).Properties.MeterName;
			} else
				return string.Empty;
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return string.Empty;
			//throw new NotImplementedException ();
		}
		#endregion
		
	}
}

