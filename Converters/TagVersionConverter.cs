namespace StaticRustLauncher.Converters;

public class TagVersionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return string.Empty;
        if (value is string tags && !string.IsNullOrEmpty(tags))
        {
            var tagList = tags.Split(',');
            if(tagList.Length >= 5)
                return tagList[5];
            return string.Empty;
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
