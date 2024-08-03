namespace StaticRustLauncher.ViewModels;

public class GameModeViewModel
{
    public ICommand PlayCommand { get; }
    public ICommand PlayWardenWiseCommand { get; }
    public ICommand PlayActivityACCommand { get; }

    public GameModeViewModel()
    {
        PlayCommand = new LambdaCommand(ExecutePlayCommand);
        PlayWardenWiseCommand = new LambdaCommand(ExecuteWardenWiseCommand);
        PlayActivityACCommand = new LambdaCommand(ExecuteActivityACCommand);
    }

    private void ExecutePlayCommand(object parameter) =>
        CloseWindow(parameter);


    private void ExecuteWardenWiseCommand(object parameter)=>    
        CloseWindow(parameter);
    

    private void ExecuteActivityACCommand(object parameter)=>  
        CloseWindow(parameter);
    

    private void CloseWindow(object parameter)
    {
        if (parameter is Window window) window.Close();
    }    
        
            
}

