namespace StaticRustLauncher.ViewModels;

class ConfirmExitViewModel
{
    public ICommand ConfirmExitCommand { get; }
    public ICommand CancelExitCommand { get; }

    public ConfirmExitViewModel()
    {
        ConfirmExitCommand = new LambdaCommand(OnConfirmExit);
        CancelExitCommand = new LambdaCommand(OnCancelExit);
    }

    private void OnConfirmExit(object parameter)=>
        System.Windows.Application.Current.Shutdown();

    private void OnCancelExit(object parameter)
    {        
        if (parameter is Window window)        
            window.Close();        
    }
}
