using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1;

public partial class ProfileUser : UserControl
{
    public ProfileUser(int id)
    {
        InitializeComponent();
        DataContext = new UserMainViewModel(id);
    }
}