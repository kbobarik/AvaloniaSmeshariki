using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1;

public partial class AdminPage : UserControl
{
    public AdminPage()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}