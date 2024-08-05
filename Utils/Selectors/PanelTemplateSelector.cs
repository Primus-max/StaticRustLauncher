namespace StaticRustLauncher.Utils.Selectors;

public class PanelTemplateSelector : DataTemplateSelector
{
    public DataTemplate? AvailableNewVersionTemplate { get; set; }
    public DataTemplate? LoadingPanelTemplate { get; set; }
    public DataTemplate? InstallGameTemplate { get; set; }
    public DataTemplate? PlayNowTemplate { get; set; }
    public DataTemplate? StatisticsTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is AvailableNewVersionControl)
            return AvailableNewVersionTemplate;
        if (item is LoadingPanelControl)
            return LoadingPanelTemplate;
        if (item is InstallGameControl)
            return InstallGameTemplate;
        if (item is StatisticsControl)
            return StatisticsTemplate;
        return item is PlayNowControl ? PlayNowTemplate : base.SelectTemplate(item, container);
    }
}
