using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Models;

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
            //AuthUser authUser = new AuthUser()
            //{
            //    user = username.Text,
            //    password = password.Text
            //};
            //Main main = new Main();
            //User user = main.authUser(authUser);
            //await App.Database.SaveUser(user);
            await Navigation.PushAsync(new Homepage());
        }
    }
}