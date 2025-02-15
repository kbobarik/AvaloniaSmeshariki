using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        UserControl page = new MainPage();
        bool flag = false;
        List<string> pages = ["Смешарики", "Пользователи", "Добавить смешарика"];
        string selectedItem;
        public UserControl Page { get => page; set => this.RaiseAndSetIfChanged(ref page, value); }
        public List<string> Pages { get => pages; set => this.RaiseAndSetIfChanged(ref pages, value); }
        public string SelectedItem { get => selectedItem; set { selectedItem = value; navigation(); } }
        public bool Flag { get => flag; set => this.RaiseAndSetIfChanged(ref flag, value); }
        public void navigation()
        {
            if (selectedItem == "Смешарики")
            {
                Page = new MainPage();
            }
            else if (selectedItem == "Пользователи")
            {
                Page = new AdminUsers();
            }
            else if (selectedItem == "Добавить смешарика")
            {
                Page = new AddSmesh();
            }
        }

        public void Press()
        {
            Flag = !Flag;
        }

    }
}