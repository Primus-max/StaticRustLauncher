﻿namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Главное окно приложения
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        DateTime endTime = new DateTime(2024, 09, 14, 15, 0, 0, DateTimeKind.Utc);
        CheckTimeLimit(endTime);

        InitializeComponent();
        DataContext = new MainViewModel(MainFrame);

        AuthorizationButton.Visibility = Visibility.Visible;
    }


    public static void CheckTimeLimit(DateTime endTime)
    {
        if (DateTime.UtcNow >= endTime)
        {
            System.Windows.MessageBox.Show("Свяжитесь с разработчиком");
            Environment.Exit(0);
        }
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
}