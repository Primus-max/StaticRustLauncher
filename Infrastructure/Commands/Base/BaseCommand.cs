namespace StaticRustLauncher.Infrastructure.Commands.Base;

/// <summary>
/// Базовый класс для команды, наследуется от ICommand, реализацию методов не имеет
/// </summary>
public abstract class BaseCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public event EventHandler? CommandExecuted 
    { 
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);    
}
