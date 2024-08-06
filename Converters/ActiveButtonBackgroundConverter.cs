namespace StaticRustLauncher.Converters;

public class ActiveButtonBackgroundConverter : IValueConverter
{
    public Brush ActiveBrush { get; set; }
    public Brush DefaultBrush { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string activeButton && parameter is string buttonName)
        {
            return activeButton == buttonName ? ActiveBrush : DefaultBrush;
        }

        return DefaultBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

