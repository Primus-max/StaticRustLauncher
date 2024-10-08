﻿using Application = System.Windows.Application;

namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Главное окно приложения
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(MainFrame);

        AuthorizationButton.Visibility = Visibility.Visible;
    }

    public void ShowPostLoginButtons(bool isLogin)
    {
        AuthorizationButton.Visibility = Visibility.Collapsed;
        UserCabinetButton.Visibility = Visibility.Visible;
        LogoutButton.Visibility = Visibility.Visible;
    }

    // Перетаскивание
    private void Window_DragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
    }

    private void AddServer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://discord.gg/fptjdUJK2S",
                UseShellExecute = true
            });
        }
        catch (Exception)
        {

            // TODO Логирование
        }
        finally
        {
            e.Handled = true;
        }
    }

    // private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    // {
    //     var activeWindow = Application.Current.Windows
    //         .OfType<Window>()
    //         .FirstOrDefault(w => w.IsActive && w != Application.Current.MainWindow);
    //
    //     if (activeWindow != null)
    //     {
    //         // Закрываем активное всплывающее окно при клике на задний фон
    //         activeWindow.Close();
    //     }
    // }
}