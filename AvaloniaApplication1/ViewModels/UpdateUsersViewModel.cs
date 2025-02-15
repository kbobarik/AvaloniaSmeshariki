using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
	public class UpdateUsersViewModel : ViewModelBase
	{
		User user;
        List<Gender> genderList;
        Gender selectedGender;
        public User User { get => user; set => this.RaiseAndSetIfChanged(ref user, value); }
        public List<Gender> GenderList { get => genderList; set => this.RaiseAndSetIfChanged(ref genderList, value); }
        public Gender SelectedGender { get => selectedGender; set => this.RaiseAndSetIfChanged(ref selectedGender, value); }

        public UpdateUsersViewModel(int id)
		{
			user = myConnection.Users.FirstOrDefault(x => x.IdUser == id);
			genderList = myConnection.Genders.ToList();
            selectedGender = genderList.FirstOrDefault(x => x.IdGender == user.Gender);
        }

        public DateTimeOffset DateTimeOffset
        {
            get => new DateTimeOffset(user.Dateofbirtdh, TimeSpan.Zero);
            set => user.Dateofbirtdh = new DateTime(value.Year, value.Month, value.Day);
        }

        public void Back() => MainWindowViewModel.Self.Uc = new AdminPage();
    }
}