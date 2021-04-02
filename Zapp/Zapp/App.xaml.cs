using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Views;
using Zapp.Data;

namespace Zapp
{
    public partial class App : Application
    {

        static ZappDatabase database;

        // Create the database connection as a singleton.
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

            MainPage = new AppShell();
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
