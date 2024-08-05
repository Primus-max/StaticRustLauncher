namespace StaticRustLauncher.Converters;

class ServerTypeToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return new SolidColorBrush(Color.FromRgb(24, 24, 27));

        if (value is ServerType serverType)        
            return GetBrushForType(serverType);
        
        else if (value is HostingType hostingType)
            return GetBrushForType(hostingType);        

        return new SolidColorBrush(Color.FromRgb(24, 24, 27));
    }

    private Brush GetBrushForType(object type)
    {
        if (type.Equals(ServerType.Premium) || type.Equals(HostingType.OurChoice))
        {
            return new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb(230, 0, 0, 0), 0.0),
                    new GradientStop(Color.FromArgb(230, 28, 111, 117), 1.0)
                })
            {
                StartPoint = new Point(0.0, 0.0),
                EndPoint = new Point(1.0, 1.0)
            };
        }
        else
        {
            return type.Equals(ServerType.Vip)
                ? new LinearGradientBrush(
                            new GradientStopCollection
                            {
                    new GradientStop(Color.FromRgb(0, 0, 0), 0.0),
                    new GradientStop(Color.FromRgb(15, 61, 20), 0.8125)
                            })
                {
                    StartPoint = new Point(0.0, 0.0),
                    EndPoint = new Point(1.0, 1.0)
                }
                : new SolidColorBrush(Color.FromRgb(24, 24, 27));
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
