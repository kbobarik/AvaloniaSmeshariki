using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace AvaloniaApplication1.ViewModels
{
    partial class MainPageViewModel : ViewModelBase
    {
        List<Smeshariki> smesharikiMain;
        List<Smeshariki> smeshariki;
        string search;
        List<Gender> genders;
        Gender selectedGender;
        int id;
        List<Bitmap> images;
        public MainPageViewModel()
        {
            this.smesharikiMain = myConnection.Smesharikis.Include(x => x.GenderNavigation).Include(x=>x.Idproperties).ToList();
            this.smeshariki = this.smesharikiMain;
            genders =
            [
                new Gender() { Title = "Выберете пол", IdGender = 0 },
            .. smeshariki.Select(x => x.GenderNavigation).Distinct().OrderBy(x=>x.IdGender).ToList()
            ];
            selectedGender = genders.Where(x => x.IdGender == 0).First();

        }

     
       
        public string Search { get => search; set { search = value; AllFilters(); } }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders,value); }
        public Gender SelectedGender { get => selectedGender; set { selectedGender = value; AllFilters(); } }
        public List<Smeshariki> Smeshariki { get => smeshariki; set => this.RaiseAndSetIfChanged(ref smeshariki, value); }
        public List<Bitmap> Images { get => images; set => this.RaiseAndSetIfChanged(ref images, value); }
        public List<Smeshariki> SmesharikiMain { get => smesharikiMain; set => this.RaiseAndSetIfChanged(ref smesharikiMain, value); }
        public void ProfileUser()
        {
            MainWindowViewModel.Self.Uc = new ProfileUser(MainWindowViewModel.Self.Id);
        }


        public void OrderByAge(int id)
        {
            switch (id)
            {
                case 1:
                    Smeshariki = Smeshariki.OrderBy(x => x.Age).ToList();
                    break;
                case 2:
                    Smeshariki = Smeshariki.OrderByDescending(x => x.Age).ToList();
                    break;
            }
        }


        public void Drop(int id)
        {
            Smeshariki smesh =  myConnection.Smesharikis.FirstOrDefault(x => x.IdSmesharik == id);
            smesh.Idproperties = null;
            myConnection.Smesharikis.Remove(smesh);
            myConnection.SaveChanges();
            SmesharikiMain = myConnection.Smesharikis.ToList();
            Smeshariki = SmesharikiMain;
        }
        public void Update(int id)
        {
            MainWindowViewModel.Self.Uc = new UpdateSmesharik(id); 
        }
        private void AllFilters()
        {
            Smeshariki = SmesharikiMain;
            if(!string.IsNullOrEmpty(Search)) Smeshariki = Smeshariki.Where(x => x.Name.ToLower().Contains(Search.ToLower())).ToList();
            if (selectedGender != null && selectedGender.IdGender != 0) { Smeshariki = Smeshariki.Where(x => x.GenderNavigation == selectedGender).ToList(); }
        }






    }
}
