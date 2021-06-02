using System;
using System.Collections.Generic;
using System.Text;
using TriviaXamarinApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string nickname;
        public string Nickname
        {
            get
            {
                return this.nickname;
            }
            set
            {
                if (this.nickname != value)
                {
                    this.nickname = value;
                    OnPropertyChanged("Nickname");
                }
            }
        }
        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public Command RegisterButtonC => new Command(RegisterAsync);
        public async void RegisterAsync()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User uu = new User
            {
                NickName = this.nickname,
                Email = this.email,
                Password = this.password,
            };
            bool u = await proxy.RegisterUser(uu);
            if (!u)
            {
                Message = "Registration isn't completed successfully";
            }
            else
            {
                App app = (App)App.Current;
                app.CurrentUser = uu;
                Page p = new HomePage();

                app.MainPage = new NavigationPage(p);
            }
        }
    }
}
