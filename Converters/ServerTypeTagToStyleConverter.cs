
namespace StaticRustLauncher.Converters;

public class ServerTypeTagToStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not ServerType serverType) 
            return Application.Current.Resources[""];

        switch (serverType)
        {
            case ServerType.Premium:
                return Application.Current.Resources["TagPremiumStyle"];
            case ServerType.Vip:
                return Application.Current.Resources["TagVipStyle"];
            default:
                return Application.Current.Resources[""];
        }

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
