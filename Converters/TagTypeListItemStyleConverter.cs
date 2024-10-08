﻿
namespace StaticRustLauncher.Converters;

public class ServerTypeTagToStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return System.Windows.Application.Current.Resources[""];

        if (value is string serverType)        
            return GetStyleForType(serverType);        
        else if (value is HostingType hostingType)        
            return GetStyleForType(hostingType);        

        return System.Windows.Application.Current.Resources[""];
    }

    private object GetStyleForType(object type)
    {
        if (type.ToString().Equals("premium", StringComparison.OrdinalIgnoreCase) 
            || type.Equals(HostingType.OurChoice) || type.Equals(HostingType.Reliable))        
            return System.Windows.Application.Current.Resources["TagPremiumStyle"];        
        else if (type.ToString().Equals("vip", StringComparison.OrdinalIgnoreCase))        
            return System.Windows.Application.Current.Resources["TagVipStyle"];        
        else        
            return System.Windows.Application.Current.Resources[""];        
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
