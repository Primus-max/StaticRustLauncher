using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace StaticRustLauncher.ViewModels.Helpers;

public static class BlurEffectHelper
{
    public static void ApplyBlurEffect(Window window, double from, double to, double durationInSeconds)
    {
        var blurEffect = new BlurEffect();
        window.Effect = blurEffect;

        var blurAnimation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds))
        };
        blurEffect.BeginAnimation(BlurEffect.RadiusProperty, blurAnimation);
    }

    public static void RemoveBlurEffect(Window window, double from, double to, double durationInSeconds)
    {
        if (window.Effect is BlurEffect blurEffect)
        {
            var unblurAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds))
            };

            unblurAnimation.Completed += (s, e) => window.Effect = null;
            blurEffect.BeginAnimation(BlurEffect.RadiusProperty, unblurAnimation);
        }
    }
}

