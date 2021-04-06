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
            var opdrachten = await App.Database.GetOpdrachten();
            List<OpdrachtCompleet> data = new List<OpdrachtCompleet>();

            foreach (var opdracht in opdrachten)
            {
                string klant_id = opdracht.klant;
                var klant = await App.Database.GetKlant(klant_id);
                data.Add(new OpdrachtCompleet()
                {
                    datum = opdracht.datum,
                    aangemeld = opdracht.aangemeld,
                    opmerkingen = opdracht.opmerkingen,
                    voornaam = klant.voornaam,
                    achternaam = klant.achternaam,
                    adres = klant.adres,
                    postcode = klant.postcode,
                    woonplaats = klant.woonplaats,
                    telefoonnummer = klant.telefoonnummer,
                    _id = opdracht._id,
                    id = opdracht.id
                });
            }
            KlantenCollectionView.ItemsSource = data;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                OpdrachtCompleet opdracht = (OpdrachtCompleet)e.SelectedItem;
                await Shell.Current.GoToAsync($"{nameof(Detailpagina)}?{nameof(Detailpagina.ItemId)}={opdracht.id}");
            }
        }
    }
}