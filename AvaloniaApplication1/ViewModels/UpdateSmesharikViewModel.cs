using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
	public class UpdateSmesharikViewModel : ViewModelBase
    {
		Smeshariki smesh;
		List<Gender> genders;
        Bitmap imageSmesh;
        List<Property> property;
        List<Property> property2;
        Property selectedProperty;
        public ICommand DeleteItemCommand { get; }
        public ObservableCollection<Property> Properties { get; }
        public Smeshariki Smesh { get => smesh; set => this.RaiseAndSetIfChanged(ref smesh, value); }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders, value); }
        public Gender selectedGender;
        public Gender SelectedGender { get => selectedGender; set => this.RaiseAndSetIfChanged(ref selectedGender, value); }
        public Bitmap ImageSmesh { get => imageSmesh; set => this.RaiseAndSetIfChanged(ref imageSmesh, value); }
        public List<Property> Property { get => property; set => this.RaiseAndSetIfChanged(ref property, value); }
        public List<Property> Property2 { get => property2; set => this.RaiseAndSetIfChanged(ref property2, value); }
        public Property SelectedProperty { get => selectedProperty; set { selectedProperty = value; AddNewItem(); } }
        public UpdateSmesharikViewModel(int id)
		{
            Genders = myConnection.Genders.ToList();
            smesh = myConnection.Smesharikis.Include(x => x.GenderNavigation).Include(x=> x.Idproperties).FirstOrDefault(x=> x.IdSmesharik ==id);
			imageSmesh = Smesh.Image != null ? new Bitmap(new MemoryStream(smesh.Image)) : new Bitmap("заглушка.png");
            Properties = new ObservableCollection<Property>(smesh.Idproperties);
            property2 = myConnection.Properties.ToList();
            property = property2.Except(Properties).ToList();
        }

		public void Save() 
		{

            smesh.Idproperties = Properties.ToList();

			myConnection.SaveChanges();
			MainWindowViewModel.Self.Uc = new AdminPage();
		} 


		public async Task Image()
		{
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
               desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Фотор смешарика",
                AllowMultiple = false
            });
            await using var readStream = await files[0].OpenReadAsync();
            byte[] buffer = new byte[10000000];
            var bytes = readStream.ReadAtLeast(buffer, 1);
            Array.Resize(ref buffer, bytes);
            smesh.Image = buffer;
			ImageSmesh = new Bitmap(new MemoryStream(smesh.Image));
			myConnection.SaveChanges();
        }

        public void Back() => MainWindowViewModel.Self.Uc = new AdminPage();

        public async void AddNewItem()
        {
            var newItem = SelectedProperty;
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (newItem != null)
                {
                    Properties.Add(newItem);
                    Property = Property.Except(Properties).ToList();
                }
            });
        }


        public async void DeleteProperty(int id)
        {
            var newItem = await myConnection.Properties.FirstOrDefaultAsync(x=> x.IdProperty == id);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (newItem != null)
                {
                    Properties.Remove(newItem);
                    Property = Property2;
                    Property = Property.Except(Properties).ToList();
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