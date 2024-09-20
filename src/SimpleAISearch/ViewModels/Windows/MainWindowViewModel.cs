using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace SimpleAISearch.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - SimpleAISearch";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "AI Chat",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Chat24 },
                TargetPageType = typeof(Views.Pages.AIChat)
            },           
               new NavigationViewItem()
            {
                Content = "Function Calling",
                Icon = new SymbolIcon { Symbol = SymbolRegular.WindowDevTools24 },
                TargetPageType = typeof(Views.Pages.FunctionCalling)
            },
                 new NavigationViewItem()
            {
                Content = "Simple AI Search",
                Icon = new SymbolIcon { Symbol = SymbolRegular.TextProofingTools24 },
                TargetPageType = typeof(Views.Pages.AISearch)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
