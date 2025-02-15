using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1;

public partial class UpdateSmesharik : UserControl
{
    public UpdateSmesharik(int id)
    {
        InitializeComponent();
        DataContext = new UpdateSmesharikViewModel(id);
    }
}