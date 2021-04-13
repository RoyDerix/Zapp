using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Services;

namespace Zapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detailpagina : TabbedPage
    {
        public DataService ds;
        public OpdrachtCompleet opdrachtCompleet;

        public Detailpagina(OpdrachtCompleet opdracht)
        {
            opdrachtCompleet = opdracht;
            ds = new DataService();

            InitializeComponent();
            LoadTaken(opdracht.id);
            Details.BindingContext = opdracht;
        }

        async void LoadTaken(string id)
        {
            try
            {
                var taken = await App.Database.GetTaken(id);
                TakenListView.ItemsSource = taken;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load opdracht.");
            }
        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            Taak taak = (Taak)checkbox.BindingContext;
            TaakPost postTaak = new TaakPost(taak);

            Taak newTaak = ds.createTaak(postTaak);
            ds.SaveDbTaken();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString();
            var opdracht = await App.Database.GetOpdracht(opdrachtCompleet.id);
            if (opdracht.aangemeld == null)
            {
                bool answer = await DisplayAlert("Aanmelden", $"Tijd van aanmelden: {dateTime}", "Aanmelden", "Cancel");
                if(answer == true)
                {
                    opdracht.aangemeld = dateTime;

                    OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                    Opdracht newOpdracht = ds.createOpdracht(postOpdracht);
                    ds.SaveDbOpdrachten();
                }
            }
            else if (opdracht.afgemeld == null)
            {
                bool answer = await DisplayAlert("Afmelden", $"Tijd van afmelden: {dateTime}", "Afmelden", "Cancel");
                if (answer == true)
                {
                    opdracht.afgemeld = dateTime;

                    OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                    Opdracht newOpdracht = ds.createOpdracht(postOpdracht);
                    ds.SaveDbOpdrachten();
                }
            }

        }
    }
}