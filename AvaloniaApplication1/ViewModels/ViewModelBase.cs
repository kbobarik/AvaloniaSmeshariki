using AvaloniaApplication1.Models;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public static AvaloniaProjectContext myConnection = new AvaloniaProjectContext();
    }
}
