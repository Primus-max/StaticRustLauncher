namespace StaticRustLauncher.Converters;

public class HostingTypeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is HostingType hostingType
            ? hostingType switch
            {
                HostingType.OurChoice => "Наш выбор",
                HostingType.Reliable => "Надёжный",
                _ => "",
            }
            : (object)"";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
