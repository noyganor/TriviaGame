﻿using System;
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
    class LoginViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public Command LoginC => new Command(LoginAsync);
        public async void LoginAsync()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);
            if (u == null)
            {
                Message = "Email or password incorrect";
            }
            else
            {
                App app = (App)App.Current;
                app.CurrentUser = u;
                Page p = new HomePage();
                
                app.MainPage = new NavigationPage(p);
            }
        }

        public Command RegisterPageC => new Command(RegisterUserPage);
        public async void RegisterUserPage()
        {
            App app = (App)App.Current;           
            app.MainPage = new Register();
        }



    }
}
