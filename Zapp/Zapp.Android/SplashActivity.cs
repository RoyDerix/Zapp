using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapp.Services;

namespace Zapp.Droid
{
    [Activity(Label = "Zapp", Icon = "@drawable/zapp_logo", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected async override void OnResume()
        {
            base.OnResume();
            DataService ds = new DataService(); 
            await ds.SaveDbOpdrachten();
            await ds.SaveDbKlanten();
            await ds.SaveDbTaken();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

    }
}