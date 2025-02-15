using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1;

public partial class RedistrationPage : UserControl
{
    public RedistrationPage()
    {
        InitializeComponent();
        DataContext = new RegistrationPageViewModel();
    }
}