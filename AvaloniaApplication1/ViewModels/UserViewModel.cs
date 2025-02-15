using System;
using System.Collections.Generic;
using Avalonia.Controls;
using AvaloniaApplication1.Models;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
        UserControl page = new UsersMainPage();
        public UserControl Page { get => page; set => this.RaiseAndSetIfChanged(ref page, value); }

     
        public void toProfile()
        {
            Page = new ProfileUser(MainWindowViewModel.Self.Id);
        }
        public void toSmesh() 
        {
            Page = new UsersMainPage();
        }
    }
}