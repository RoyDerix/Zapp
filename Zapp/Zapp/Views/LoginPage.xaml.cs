using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Models;
using Zapp.Services;

namespace Zapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async public void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                AuthUser authUser = new AuthUser()
                {
                    user = username.Text,
                    password = password.Text
                };
                DataService ds = new DataService();
                User user = ds.authUser(authUser);
                await App.Database.SaveUser(user);
                await Navigation.PopAsync();
                await Navigation.PushAsync(new Homepage());
            }
            catch
            {
                await DisplayAlert("Fout", "Gebruikersnaam of wachtwoord is niet correct", "OK");
            }
        }
    }
}