using CheKhabar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheKhabar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            if(passwordEntry.Text == confirmPasswordEntry.Text)
            {
                //Users user = new Users()
                //{
                //    Email = emailEntry.Text,
                //    Password = passwordEntry.Text
                //};
            }
            else
            {
                DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }
    }
}