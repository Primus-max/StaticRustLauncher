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
        WindowHelper.OpenWindowWithBlur(playNowWindow);
    }
}
