using System;
using System.Collections.Generic;
using System.Text;
using TriviaXamarinApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using TriviaXamarinApp.Services;

namespace TriviaXamarinApp.ViewModels
{
    class PrivateQuestionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<string> questions;
        public ObservableCollection<string> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                if (this.questions != value)
                {
                    this.questions = value;
                    OnPropertyChanged("Questions");
                }
            }
        }

        //private string questionText;
        //public string QuestionText
        //{
        //    get
        //    {
        //        return this.questionText;
        //    }
        //    set
        //    {
        //        if (this.questionText != value)
        //        {
        //            this.questionText = value;
        //            OnPropertyChanged("QuestionText");
        //        }
        //    }
        //}

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }

        public PrivateQuestionsViewModel()
        {
            this.Questions = new ObservableCollection<string>();
            this.isRefreshing = false;
            CreateQuestionsCollection();
        }

        public void CreateQuestionsCollection()
        {
            App app = (App)App.Current;
            User u = app.CurrentUser;
            List<AmericanQuestion> myQuestions = u.Questions;
            foreach (AmericanQuestion q in myQuestions)
            {
                this.Questions.Add(q.GetQuestionText());
            }
        }

        //Delete question
        public Command DeleteCommand => new Command<AmericanQuestion>(DeleteQuestion);
        public async void DeleteQuestion(AmericanQuestion m)
        {

            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool succeeded = await proxy.DeleteQuestion(m);
            if(succeeded)
            {
                this.Questions.Remove(m.GetQuestionText());
            }

        }

        //Refresh collection
        public Command RefreshCommand => new Command(RefreshPage);
        public void RefreshPage()
        {
            App app = (App)App.Current;
            User u = app.CurrentUser;
            List<AmericanQuestion> myQuestions = u.Questions;
            this.Questions.Clear();
            foreach (AmericanQuestion q in myQuestions)
            {
                this.Questions.Add(q.GetQuestionText());
            }
            this.IsRefreshing = false;
        }
    }


}





