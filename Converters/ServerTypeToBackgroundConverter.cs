namespace StaticRustLauncher.Converters;

class ServerTypeToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not ServerType serverType) return new SolidColorBrush(Color.FromRgb(24, 24, 27));

        //double angleRadians = 52 * Math.PI / 180.0;
        //double x = Math.Cos(angleRadians);
        //double y = Math.Sin(angleRadians);

        switch (serverType)
        {
            case ServerType.Premium:
                return new LinearGradientBrush(
                    new GradientStopCollection
                    {
                        new GradientStop(Color.FromArgb(230, 0, 0, 0), 0.0), // rgba(0, 0, 0, 0.9) at 0%
                        new GradientStop(Color.FromArgb(230, 28, 111, 117), 1.0) // rgba(28, 111, 117, 0.9) at 100%
                    })
                {
                    StartPoint = new Point(0.0, 0.0),  // Начало градиента (левый верхний угол)
                    EndPoint = new Point(1.0, 1.0)        // Конец градиента (правый нижний угол)
                };
            case ServerType.Vip:
                return new LinearGradientBrush(
                    new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0, 0, 0), 0.0),      // #000000 at 0%
                        new GradientStop(Color.FromRgb(15, 61, 20), 0.8125)  // #0F3D14 at 81.25%
                    })
                {
                    StartPoint = new Point(0.0, 0.0),  // Начало градиента (левый верхний угол)
                    EndPoint = new Point(1.0, 1.0)     // Конец градиента (правый нижний угол)
                };
            default:
                return new SolidColorBrush(Color.FromRgb(24, 24, 27));
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
