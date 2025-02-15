using System;
using System.Collections.Generic;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReactiveUI;
using Avalonia.Controls;

namespace AvaloniaApplication1.ViewModels
{
	public class UserMainViewModel : ViewModelBase
    {
        User user;
        List<Gender> genders;
        string info;
        

        public User User { get => user; set {
                Info = "";
                user = value;
            } }
        public string Info { get => info; set => this.RaiseAndSetIfChanged(ref info, value); }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders, value); }
        public Gender selectedGender;
        public Gender SelectedGender { get => selectedGender; set
            {
                selectedGender = value;
                Info = "";
            }
        }

        public UserMainViewModel(int id)
        {

            user = myConnection.Users.Include(x => x.GenderNavigation).FirstOrDefault(x => x.IdUser == id);
            Genders = myConnection.Genders.ToList();
        }


        public DateTimeOffset DateTimeOffset
        {
            get => new DateTimeOffset(User.Dateofbirtdh.Date, TimeSpan.Zero);
            set {
                User.Dateofbirtdh = new DateTime(value.Year, value.Month, value.Day);
                Info = "";
            }
        }
        public void Save()
        {
            try
            {
                myConnection.SaveChanges();
                Info = "Данные успешно сохранены";
            }
            catch (Exception ex)
            {
                Info = $"Ошибка: {ex.Message}";
            }
        }
    }
}