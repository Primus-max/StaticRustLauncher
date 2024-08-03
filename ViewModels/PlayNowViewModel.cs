namespace StaticRustLauncher.ViewModels;

class PlayNowViewModel
{
    public ICommand PlayNowCommand { get; set; } = null!;

    public PlayNowViewModel()
    {
        PlayNowCommand = new LambdaCommand(OnPlayNowExecuted);
    }

    private void OnPlayNowExecuted(object parameter)
    {
        PlayNowWindow playNowWindow = new();
        var mainWindow = Application.Current.MainWindow;
        playNowWindow.Owner = mainWindow;

        BlurEffectHelper.ApplyBlurEffect(mainWindow, 0, 10, 0.5);
        playNowWindow.ShowDialog();
        BlurEffectHelper.RemoveBlurEffect(mainWindow, 10, 0, 0.5);
    }
}
