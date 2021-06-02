using System;
using System.Collections.Generic;
using System.Text;
using exercise.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace exercise.ViewModels
{
    class viewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> Notes { get; set; }

        private string theNote;
        public string TheNote
        {
            get => this.theNote;
            set
            {
                if (value != theNote)
                {
                    theNote = value;
                    OnPropertyChanged(nameof(TheNote));
                }
            }
        }
        public viewModel()
        {
            this.Delete = new Command(DeleteExecution);
            this.Save = new Command(SaveExecution);
            this.Notes = new ObservableCollection<string>();
        }
        public Command Delete
        {
            get;
        }

        public Command Save
        {
            get;
        }

        public void DeleteExecution()
        {
            this.Notes.Clear();
        }
        
        
        public void SaveExecution()
        {
            this.Notes.Add(TheNote);
        }
    }
}
