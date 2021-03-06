using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zapp.Services;
using Xamarin.Forms.Maps;

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

            Details.BindingContext = opdracht;

            LoadTaken();
            LoadKaart();
            LoadButton();
        }

        private void LoadButton()
        {
            if (opdrachtCompleet.aangemeld == null)
            {
                AanAfmelden.Text = "Aanmelden";
                AanAfmelden2.Text = "Aanmelden";
            }
            else if (opdrachtCompleet.afgemeld == null)
            {
                AanAfmelden.Text = "Afmelden";
                AanAfmelden2.Text = "Afmelden";
            }
            else
            {
                AanAfmelden.IsVisible = false;
                AanAfmelden2.IsVisible = false;
            }
        }

        private void LoadKaart()
        {
            double lat = Convert.ToDouble(opdrachtCompleet.lat);
            double lon = Convert.ToDouble(opdrachtCompleet.lon);
            Position position = new Position(lat, lon);
            Pin pinAdres = new Pin()
            {
                Type = PinType.Place,
                Label = "Woonadres",
                Position = position
            };
            Kaart.Pins.Add(pinAdres);
            Kaart.MoveToRegion(MapSpan.FromCenterAndRadius(pinAdres.Position, Distance.FromMeters(3000)));
        }

        async void LoadTaken()
        {
            var taken = await App.Database.GetTaken(opdrachtCompleet.id);
            TakenListView.ItemsSource = taken;
        }

        private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (opdrachtCompleet.aangemeld != null)
            {
                Taak taak = (Taak)checkbox.BindingContext;
                TaakPost postTaak = new TaakPost(taak);

                Taak newTaak = ds.createTaak(postTaak);
                await ds.SaveDbTaken();
            }
            else
            {
                checkbox.IsChecked = false;
                if (checkbox.IsChecked == true)
                {
                    await DisplayAlert("Fout", "Meld je aan, voordat je aan een taak begint", "OK");
                }
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var opdracht = await App.Database.GetOpdracht(opdrachtCompleet.id);

            if (opdracht.aangemeld == null)
            {
                Aanmelden(opdracht);
            }
            else if (opdracht.afgemeld == null)
            {
                Afmelden(opdracht);
            }
        }

        private async void Aanmelden(Opdracht opdracht)
        {
            string dateTime = DateTime.Now.ToString();
            bool answer = await DisplayAlert("Aanmelden", $"Tijd van aanmelden: {dateTime}", "Aanmelden", "Cancel");
            if (answer == true)
            {
                opdracht.aangemeld = dateTime;
                opdrachtCompleet.aangemeld = dateTime;

                OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                Opdracht newOpdracht = ds.createOpdracht(postOpdracht);
                await ds.SaveDbOpdrachten();

                AanAfmelden.Text = "Afmelden";
                AanAfmelden2.Text = "Afmelden";
            }
        }

        private async void Afmelden(Opdracht opdracht)
        {
            string dateTime = DateTime.Now.ToString();
            bool answer = await DisplayAlert("Afmelden", $"Tijd van afmelden: {dateTime}", "Afmelden", "Cancel");
            if (answer == true)
            {
                opdracht.afgemeld = dateTime;
                opdrachtCompleet.afgemeld = dateTime;

                OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                Opdracht newOpdracht = ds.createOpdracht(postOpdracht);
                await ds.SaveDbOpdrachten();

                opdrachtCompleet.afgemeld = dateTime;

                Application.Current.MainPage = new NavigationPage(new Homepage());
            }
        }
    }
}