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
            getOpdrachten();
        }

        async private void getOpdrachten()
        {
            var users = App.Database.GetUsers();
            var opdrachten = await App.Database.GetAllOpdrachten();
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
                    voornaam = klant.voornaam,
                    achternaam = klant.achternaam,
                    adres = klant.adres,
                    postcode = klant.postcode,
                    woonplaats = klant.woonplaats,
                    telefoonnummer = klant.telefoonnummer,
                    user = opdracht.user,
                    _id = opdracht._id,
                    id = opdracht.id
                });
            }
            KlantenCollectionView.ItemsSource = data;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                OpdrachtCompleet opdracht = (OpdrachtCompleet)e.Item;
                await Shell.Current.GoToAsync($"{nameof(Detailpagina)}?{nameof(Detailpagina.OpdrachtId)}={opdracht.id}");
            }
        }
    }
}