using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Views;
using Zapp.Data;
using System.Collections.Generic;
using Zapp.Models;
using System.Threading.Tasks;

namespace Zapp
{
    public partial class App : Application
    {

        static ZappDatabase database;

        public static ZappDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ZappDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zapp.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            var users = Database.GetUsers(); 
            if (users.Result.Count == 0)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new Homepage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
