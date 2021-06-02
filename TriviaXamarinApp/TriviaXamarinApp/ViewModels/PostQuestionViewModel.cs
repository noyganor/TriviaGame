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
    class PostQuestionViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string questionText;
        public string QuestionText
        {
            get
            {
                return this.questionText;
            }
            set
            {
                if (this.questionText != value)
                {
                    this.questionText = value;
                    OnPropertyChanged("QuestionText");
                }
            }
        }

        private string correctAnswer;
        public string CorrectAnswer
        {
            get
            {
                return this.correctAnswer;
            }
            set
            {
                if (this.correctAnswer != value)
                {
                    this.correctAnswer = value;
                    OnPropertyChanged("CorrectAnswer");
                }
            }
        }

        private string wrongAnswer1;
        public string WrongAnswer1
        {
            get
            {
                return this.wrongAnswer1;
            }
            set
            {
                if (this.wrongAnswer1 != value)
                {
                    this.wrongAnswer1 = value;
                    OnPropertyChanged("WrongAnswer1");
                }
            }
        }

        private string wrongAnswer2;
        public string WrongAnswer2
        {
            get
            {
                return this.wrongAnswer2;
            }
            set
            {
                if (this.wrongAnswer2 != value)
                {
                    this.wrongAnswer2 = value;
                    OnPropertyChanged("WrongAnswer2");
                }
            }
        }

        private string wrongAnswer3;
        public string WrongAnswer3
        {
            get
            {
                return this.wrongAnswer3;
            }
            set
            {
                if (this.wrongAnswer3 != value)
                {
                    this.wrongAnswer3 = value;
                    OnPropertyChanged("WrongAnswer3");
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

        public Command AddButton => new Command(AddQuestion);
        public async void AddQuestion()
        {

            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            App app = (App)App.Current;
            User u = app.CurrentUser;

            AmericanQuestion q = new AmericanQuestion
            {
                QText = this.questionText,
                CorrectAnswer = this.correctAnswer,
                OtherAnswers = new string[]
                {
                    wrongAnswer1, wrongAnswer2, wrongAnswer3
                },
                CreatorNickName = u.NickName
            };
            bool succeeded = await proxy.PostNewQuestion(q);
            u.Questions.Add(q);

            if (!succeeded)
            {
                Message = "Question was not added";
            }
            else
            {
                Message = "Question was added successfully!";
                //await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
