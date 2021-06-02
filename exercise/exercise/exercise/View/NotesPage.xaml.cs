using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using exercise.ViewModels;

namespace exercise.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            this.BindingContext = new viewModel();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

            
    }
}