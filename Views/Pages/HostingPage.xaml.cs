﻿using System.Collections.ObjectModel;

namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для HostingPage.xaml
/// </summary>
public partial class HostingPage : Page
{
    public ObservableCollection<Hosting> Hosts { get; set; } = [];
    public Hosting SelectedHosting { get; set; } = null!;

    public HostingPage()
    {
        InitializeComponent();
        //using StaticRustLauncher.Services;LoadHosts();
        DataContext = this;
    }


    //private void LoadHosts()
    //{
    //    Hosts = new ObservableCollection<Hosting>
    //    {
    //        new Hosting
    //        {
    //            Name = "HostOne",
    //            Description = "A reliable hosting service with excellent uptime.",
    //            Users = 1500,
    //            Projects = 350,
    //            HostingType = HostingType.Reliable
    //        },
    //        new Hosting
    //        {
    //            Name = "HostTwo",
    //            Description = "Our choice for budget-friendly hosting.",
    //            Users = 2300,
    //            Projects = 500,
    //            HostingType = HostingType.OurChoice
    //        },
    //        new Hosting
    //        {
    //            Name = "HostThree",
    //            Description = "Premium hosting with advanced features.",
    //            Users = 1200,
    //            Projects = 200,
    //            HostingType = HostingType.Reliable
    //        },
    //        new Hosting
    //        {
    //            Name = "HostFour",
    //            Description = "Affordable and dependable hosting for small businesses.",
    //            Users = 1800,
    //            Projects = 450,
    //            HostingType = HostingType.OurChoice
    //        }
    //    };
    //}


    private void OpenDescriptionHost_Click(object sender, SelectionChangedEventArgs e)
    {
        var hostDetailWindow = new HostsWindow(SelectedHosting);
        WindowHelper.OpenWindowWithBlur(hostDetailWindow);
    }
}
