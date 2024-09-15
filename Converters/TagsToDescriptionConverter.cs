
namespace StaticRustLauncher.Converters;
/// <summary>
/// Парсинг тегов из строки
/// </summary>
public class TagsToDescriptionConverter : IValueConverter
{
    private readonly IReadOnlyDictionary<char, string> _tags;

    public TagsToDescriptionConverter() =>
        _tags = StaticCollections.СharToTag;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !(value is string tags))
            return string.Empty;

        string[] tagList = tags.Split(',');
        string frequencyTag = string.Empty;
        string vanillaTag = string.Empty;
        int daysSinceBorn = 0;

        foreach (var tag in tagList)
        {
            // Поиск временного тега (born...)
            if (tag.StartsWith("born"))
            {
                if (long.TryParse(tag.Substring(4), out long bornTimestamp))
                {
                    DateTime bornDate = DateTimeOffset.FromUnixTimeSeconds(bornTimestamp).DateTime;
                    daysSinceBorn = (DateTime.UtcNow - bornDate).Days;
                }
            }

            // Поиск частоты вайпа
            if (tag.StartsWith("^") && tag.Length > 1)
            {
                char tagChar = tag[1];
                if (_tags.TryGetValue(tagChar, out string tagValue))
                {
                    if (tagChar == 'v')
                        vanillaTag = tagValue;
                    else
                        frequencyTag = tagValue;
                }
            }
        }

        return $"{vanillaTag} {frequencyTag} Вайп {daysSinceBorn} дней";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
