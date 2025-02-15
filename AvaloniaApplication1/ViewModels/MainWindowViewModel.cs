using Avalonia.Controls;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Linq;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string login;
        string password;
        string info;
        UserControl uc ;
        int id;
        User user = new User();
        public static MainWindowViewModel Self;


        public MainWindowViewModel()
        {
            Self = this;
            Uc = new UserControl1();
        }        
        public string Login { get => login; set => this.RaiseAndSetIfChanged(ref login, value); }
        public int Id { get => id; set => this.RaiseAndSetIfChanged(ref id, value); }
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        public string Info { get => info; set => this.RaiseAndSetIfChanged(ref info, value); }
        public UserControl Uc { get => uc; set => this.RaiseAndSetIfChanged(ref uc, value); }

        public void Enter() => Uc = new Enter();
        public void Registrainon() => Uc = new RedistrationPage();

        public void SignIn()
        {
            user = myConnection.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                if(user.Role == 1)
                {
                    Id = user.IdUser;
                    Uc = new AdminPage();
                    
                }
                else
                {
                    Id = user.IdUser;
                    Uc = new UserPage();
                    
                }
            }
            else
            {
                Info = "Неверный логин или пароль";
                Password = "";
            }
        }



        

    }
}
