using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StaticRustLauncher.ViewModels.Base;

/// <summary>
/// Базовая ViewModel, реализует интерфейсы INotifyPropertyChanged, IDisposable
/// </summary>
public class BaseViewModel : INotifyPropertyChanged, IDisposable
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null!) =>    
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

    /// <summary>
    /// Метод для установки свойст, с отслеживанием и уведомлением об изменении. Избегает зацикливания обновления свойств
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="PropertyName"></param>
    /// <returns></returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null!)
    {
        if (Equals(field, value)) return false;

        field = value;
        OnPropertyChanged(nameof(field));
        return true;
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private bool _Disposed;
    protected virtual void Dispose(bool disposing) 
    {
        if (!disposing || _Disposed) return;        
        _Disposed = true;
    }
}
