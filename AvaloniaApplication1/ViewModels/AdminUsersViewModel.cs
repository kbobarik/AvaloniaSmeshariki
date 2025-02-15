using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
	public class AdminUsersViewModel : ViewModelBase
    {
        List<User> usersMain;
        List<User> users;
        string search;
        List<Gender> genders;
        Gender selectedGender;
        int id;
        public AdminUsersViewModel()
        {
            this.usersMain = myConnection.Users.Include(x => x.GenderNavigation).ToList();
            this.users = this.usersMain;
            genders =
            [
                new Gender() { Title = "Выберете пол", IdGender = 0 },
            .. users.Select(x => x.GenderNavigation).Distinct().OrderBy(x=>x.IdGender).ToList()
            ];
            selectedGender = genders.Where(x => x.IdGender == 0).First();

        }



        public string Search { get => search; set { search = value; AllFilters(); } }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders, value); }
        public Gender SelectedGender { get => selectedGender; set { selectedGender = value; AllFilters(); } }
        public List<User> Users { get => users; set => this.RaiseAndSetIfChanged(ref users, value); }
        public List<User> UsersMain { get => usersMain; set => this.RaiseAndSetIfChanged(ref usersMain, value); }


        public void OrderByAge(int id)
        {
            switch (id)
            {
                case 1:
                    Users = Users.OrderBy(x => x.Dateofbirtdh).ToList();
                    break;
                case 2:
                    Users = Users.OrderByDescending(x => x.Dateofbirtdh).ToList();
                    break;
            }
        }


        public void Drop(int id)
        {
            myConnection.Users.Remove(UsersMain.Where(x => x.IdUser == id).First());
            myConnection.SaveChanges();
            UsersMain = myConnection.Users.ToList();
            Users = UsersMain;
        }
        public void Update(int id)
        {
            MainWindowViewModel.Self.Uc = new UpdateUsers(id);
        }
        private void AllFilters()
        {
            Users = UsersMain;
            if (!string.IsNullOrEmpty(Search)) Users = Users.Where(x => x.Lastname.ToLower().Contains(Search.ToLower())).ToList();
            if (selectedGender != null && selectedGender.IdGender != 0) { Users = Users.Where(x => x.GenderNavigation == selectedGender).ToList(); }
        }
    }
}