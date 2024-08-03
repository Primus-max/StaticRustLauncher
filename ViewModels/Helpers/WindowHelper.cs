namespace StaticRustLauncher.ViewModels.Helpers;

public static class WindowHelper
{
    public static void OpenWindowWithBlur(Window window)
    {
        var mainWindow = Application.Current.MainWindow;
        window.Owner = mainWindow;

        BlurEffectHelper.ApplyBlurEffect(mainWindow, 0, 10, 0.5);
        window.ShowDialog();
        BlurEffectHelper.RemoveBlurEffect(mainWindow, 10, 0, 0.5);
    }
}
