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
    public class AnswersObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AnswersObject(Color c, string answer)
        {
            this.Color = c;
            this.Answer = answer;
        }
        private string answer;
        public string Answer
        {
            get
            {
                return this.answer;
            }
            set
            {
                if (this.answer != value)
                {
                    this.answer = value;
                    OnPropertyChanged("Answer");
                }
            }
        }

        private Color color;
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    OnPropertyChanged("Color");
                }
            }
        }      
    }

    class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        const int FOUR = 4;
        private ObservableCollection<string> answers;
        public ObservableCollection<string> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                if (this.answers != value)
                {
                    this.answers = value;
                    OnPropertyChanged("Answers");
                }
            }
        }

        private ObservableCollection<AnswersObject> answersColors;
        public ObservableCollection<AnswersObject> AnswersColors
        {
            get
            {
                return this.answersColors;
            }
            set
            {
                if (this.answersColors != value)
                {
                    this.answersColors = value;
                    OnPropertyChanged("AnswerColor");
                }
            }
        }

        private int correctAnswerIndex; //between 0 to 3

        private string question;
        public string Question
        {
            get
            {
                return this.question;
            }
            set
            {
                if (this.question != value)
                {
                    this.question = value;
                    OnPropertyChanged("Question");
                }
            }
        }

        private int counter;
        public int Counter
        {
            get
            {
                return this.counter;
            }
            set
            {
                if (this.counter != value)
                {
                    this.counter = value;
                    OnPropertyChanged("Counter");
                }
            }
        }

        private int FindAnswer(string answer)
        {
            for (int i = 0; i < FOUR; i++)
            {
                if (this.AnswersColors[i].Answer == answer)
                    return i;
            }
            return -1;
        }

        public Command AnswerButtonClick => new Command<string>(AnswerButton);
        public void AnswerButton(string answer)
        {
            int index = FindAnswer(answer);
            if (index == correctAnswerIndex)
            {
                Counter++;
                AnswersColors[index].Color = Color.Green;
            }

            else
            {
                AnswersColors[index].Color = Color.Red;
                AnswersColors[correctAnswerIndex].Color = Color.Green;
            }
        }

        public Command LoginPageGame => new Command(MoveToLoginPage);
        public void MoveToLoginPage()
        {
            App app = (App)App.Current;
            app.MainPage.Navigation.PushAsync(new Login());
        }

        public Command PostQuestionPageGame => new Command(MoveToPostQuestionPage);
        public void MoveToPostQuestionPage()
        {
            App app = (App)App.Current;
            User u = app.CurrentUser;
            if(u != null)
            {               
                app.MainPage.Navigation.PushAsync(new PostQuestions());
            }
            
            else
            {
                app.MainPage.Navigation.PushAsync(new Login());
            }
        }

        public Command MyQuestionsPageGame => new Command(MoveToMyQuestionsPage);
        public void MoveToMyQuestionsPage()
        {
            App app = (App)App.Current;
            User u = app.CurrentUser;
            if (u != null)
            {
                app.MainPage.Navigation.PushAsync(new MyQuestions());
            }

            else
            {
                app.MainPage.Navigation.PushAsync(new Login());
            }
        }

        public GameViewModel()
        {
            this.Answers = new ObservableCollection<string>();
            this.AnswersColors = new ObservableCollection<AnswersObject>();
            Counter = 0;
            GetQuestion();
        }

        private static Random r = new Random();

        public Command NextQButton => new Command(NextQuestion);
        public void NextQuestion()
        {
            GetQuestion();
        }

        private async void GetQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion q = await proxy.GetRandomQuestion();
            this.Question = q.QText;
            this.correctAnswerIndex = r.Next(0, 4);
            this.Answers.Clear();
            this.AnswersColors.Clear();
            int j = 0;
            for (int i = 0; i < FOUR; i++)
            {
                Color color = Color.Wheat;
                string answer;
                if (i != correctAnswerIndex)
                    answer = q.OtherAnswers[j++];
                else
                    answer = q.CorrectAnswer;
                this.AnswersColors.Add(new AnswersObject(color, answer));
            }
        }




    }
}
