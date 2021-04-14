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
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
            GetOpdrachten();
        }

        async private void GetOpdrachten()
        {
            var users = App.Database.GetUsers();
            var user_id = users.Result[0]._id;
            var opdrachten = await App.Database.GetOpdrachten(user_id);
            List<OpdrachtCompleet> data = new List<OpdrachtCompleet>();

            foreach (var opdracht in opdrachten)
            {
                string klant_id = opdracht.klant;
                var klant = await App.Database.GetKlant(klant_id);
                data.Add(new OpdrachtCompleet()
                {
                    datum = opdracht.datum,
                    aangemeld = opdracht.aangemeld,
                    afgemeld = opdracht.afgemeld,
                    opmerkingen = opdracht.opmerkingen,
                    naam = klant.voornaam + " " + klant.achternaam,
                    adres = klant.adres,
                    postcode = klant.postcode,
                    woonplaats = klant.woonplaats,
                    telefoonnummer = klant.telefoonnummer,
                    user = opdracht.user,
                    _id = opdracht._id,
                    id = opdracht.id
                });
            }
            data.Sort(delegate (OpdrachtCompleet x, OpdrachtCompleet y)
            {
                return y.datum.CompareTo(x.datum);
            });
            OpdrachtListView.ItemsSource = data;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                OpdrachtCompleet opdracht = (OpdrachtCompleet)e.Item;
                await Navigation.PushAsync(new Detailpagina(opdracht));
            }
        }

        async public void Logout(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Uitloggen", "Wil je uitloggen?", "Uitloggen", "Cancel");
            if (answer == true)
            {
                App.Database.Logout();
                await Navigation.PopAsync();
                await Navigation.PushAsync(new LoginPage());
            }

        }
    }
}