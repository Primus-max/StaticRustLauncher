
using System.Windows.Shapes;

namespace StaticRustLauncher.DependencyProperties;

public static class HighlightBehavior
{
    public static readonly DependencyProperty IsHighlightedProperty =
        DependencyProperty.RegisterAttached(
            "IsHighlighted",
            typeof(bool),
            typeof(HighlightBehavior),
            new PropertyMetadata(false, OnIsHighlightedChanged));

    public static bool GetIsHighlighted(DependencyObject obj) =>
        (bool)obj.GetValue(IsHighlightedProperty);

    public static void SetIsHighlighted(DependencyObject obj, bool value) =>
        obj.SetValue(IsHighlightedProperty, value);

    private static void OnIsHighlightedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Rectangle rectangle)
        {
            var isHighlighted = (bool)e.NewValue;
            rectangle.Fill = isHighlighted ? Brushes.LightBlue : Brushes.Transparent;
            rectangle.Visibility = isHighlighted ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
