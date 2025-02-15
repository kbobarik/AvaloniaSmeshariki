using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using AvaloniaApplication1.Models;
using ReactiveUI;
using Avalonia;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Avalonia.Threading;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.ViewModels
{
	public class AddSmeshViewModel : ViewModelBase
	{

        string name;
        string animal;
        List<Gender> genders;
        List<Property> property;
        List<Property> property2;
        List<Property> smeshProperty;
        Property selectedProperty ;
        Gender selectedGender;
		int age;
		Bitmap imageSmesh;
        byte[] image;
        public ObservableCollection<Property> Items { get; } = new ObservableCollection<Property>();
        public ICommand AddItemCommand { get; }
        public string Name { get=> name; set => this.RaiseAndSetIfChanged(ref name, value); }
        public string Animal { get => animal; set => this.RaiseAndSetIfChanged(ref animal, value); }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders, value); }
        public List<Property> Property { get => property; set => this.RaiseAndSetIfChanged(ref property, value); }
        public List<Property> Property2 { get => property2; set => this.RaiseAndSetIfChanged(ref property2, value); }
        public List<Property> SmeshProperty { get => smeshProperty; set => this.RaiseAndSetIfChanged(ref smeshProperty, value); }
        public Gender SelectedGender { get => selectedGender; set => this.RaiseAndSetIfChanged(ref selectedGender, value); }
        public Property SelectedProperty { get => selectedProperty; set  { selectedProperty = value; AddNewItem(); } }
        public int Age { get => age; set => this.RaiseAndSetIfChanged(ref age, value); }

        public Bitmap ImageSmesh { get => imageSmesh; set => this.RaiseAndSetIfChanged(ref imageSmesh, value); }
        public byte[] Image { get => image; set => this.RaiseAndSetIfChanged(ref image, value); }


        public AddSmeshViewModel()
		{
            property = myConnection.Properties.ToList();
            property2 = myConnection.Properties.ToList();
            selectedProperty = property.FirstOrDefault();
            genders = myConnection.Genders.ToList();
		}

        public async Task AddImage()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
               desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Фото смешарика",
                AllowMultiple = false
            });
            await using var readStream = await files[0].OpenReadAsync();
            byte[] buffer = new byte[10000000];
            var bytes = readStream.ReadAtLeast(buffer, 1);
            Array.Resize(ref buffer, bytes);
            Image = buffer;
            ImageSmesh = new Bitmap(new MemoryStream(buffer));
        }

        public void Back() => MainWindowViewModel.Self.Uc = new AdminPage();
        public void Save()
        {
            myConnection.Add(new Smeshariki()
            {
                Name = Name,
                Age = Age,
                Gender = SelectedGender.IdGender,
                Animal = Animal,
                Image = Image,
                Idproperties = Items
            });
            myConnection.SaveChanges();
            MainWindowViewModel.Self.Uc = new AdminPage();
        }

        private async void AddNewItem()
        {
            var newItem = SelectedProperty;
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                if(newItem != null)
                {
                    Items.Add(newItem);
                    Property = Property.Except(Items).ToList();
                }
            });
        }
        public async void DeleteItem(int id)
        {
            var newItem = await myConnection.Properties.FirstOrDefaultAsync(x=> x.IdProperty ==id);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (newItem != null)
                {
                    Items.Remove(newItem);
                    Property = Property2;
                    Property = Property.Except(Items).ToList();
                
            }
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}