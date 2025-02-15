using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1;

public partial class UsersMainPage : UserControl
{
    public UsersMainPage()
    {
        InitializeComponent();
        DataContext = new MainPageViewModel();
    }
}