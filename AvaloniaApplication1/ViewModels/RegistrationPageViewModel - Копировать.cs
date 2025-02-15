using Avalonia.Controls;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace AvaloniaApplication1.ViewModels
{
    partial class RegistrationPageViewModel : ViewModelBase
    {

        string surname = "";
        string name = "";
        string patronymic= "";
        DateTime dateOfBirth = DateTime.UtcNow;
        List<Gender> genderList;
        List<User> users;
        Gender selectedGender;
        string login = "";
        string info;
        string password = "";
        string repeatedPassword = "";

        public string Login { get => login; set => this.RaiseAndSetIfChanged(ref login, value); }
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        public string Info { get => info; set => this.RaiseAndSetIfChanged(ref info, value); }
        public string RepeatedPassword { get => repeatedPassword; set => this.RaiseAndSetIfChanged(ref repeatedPassword, value); }
        public string Name { get => name; set => this.RaiseAndSetIfChanged(ref name, value); }
        public string Surname { get => surname; set => this.RaiseAndSetIfChanged(ref surname, value); }
        public string Patronymic { get => patronymic; set => this.RaiseAndSetIfChanged(ref patronymic, value); }
        public DateTime DateOfBirth { get => dateOfBirth; set => this.RaiseAndSetIfChanged(ref dateOfBirth, value); }
        public List<Gender> GenderList { get => genderList; set => this.RaiseAndSetIfChanged(ref genderList, value); }
        public List<User> Users { get => users; set => this.RaiseAndSetIfChanged(ref users, value); }
        public Gender SelectedGender { get => selectedGender; set => this.RaiseAndSetIfChanged(ref selectedGender, value); }

        public RegistrationPageViewModel()
        {
           genderList = myConnection.Genders.ToList();
           genderList.Add(new Gender() { IdGender = 0, Title = "Выберете пол"});
           selectedGender = genderList.FirstOrDefault(x => x.IdGender == 0);
        }
        public DateTimeOffset DateTimeOffset
        {
            get => new DateTimeOffset(dateOfBirth, TimeSpan.Zero);
            set => dateOfBirth = new DateTime(value.Year, value.Month, value.Day);
        }


        public void Registration()
        {
            if (Password != RepeatedPassword)
            {
                Info = "Пароли отличаются";
            }
            else if(Password == "" || RepeatedPassword == "" || Login ==  "" || Name == "" || Surname =="" || SelectedGender == genderList.FirstOrDefault(x => x.IdGender == 0))
            {
                Info = "Заполните все поля";
            }
            else {
                try { 
                
                    Users = myConnection.Users.Where(x => x.Login == Login).ToList();
                    if (Users.Count > 0)
                    {
                        Info = "Логин занят";
                    }
                    else
                    {
                        User user = new User() { Firstname = Name, Lastname = Surname, Patronymic = Patronymic, Dateofbirtdh = DateOfBirth, Gender = SelectedGender.IdGender, Role = 1, Login = Login, Password = Password };
                        myConnection.Users.Add(user);
                        myConnection.SaveChanges();
                        MainWindowViewModel.Self.Uc = new UserPage();
                    }


            }
                catch (Exception ex)
                {
                Info = ex.Message;
            }
        }
            
            


        }




    }
}
