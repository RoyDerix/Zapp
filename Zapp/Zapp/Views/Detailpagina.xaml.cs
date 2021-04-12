using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zapp.Views
{
    [QueryProperty(nameof(OpdrachtId), nameof(OpdrachtId))]
    public partial class Detailpagina : TabbedPage
    {

        public string OpdrachtId
        {
            set
            {
                LoadTaken(value);
                LoadKlant(value);
                this.OpdrachtId2 = value;
            }
        }

        public string OpdrachtId2;

        public Detailpagina(OpdrachtCompleet opdracht)
        {
            InitializeComponent();
            OpdrachtId = opdracht.id;

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

        async void LoadKlant(string id)
        {
            var opdracht = await App.Database.GetOpdracht(id);
            var klant = await App.Database.GetKlant(opdracht.klant);
            Details.BindingContext = klant;

        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            Taak taak = (Taak)checkbox.BindingContext;
            TaakPost postTaak = new TaakPost(taak);

            Main main = new Main();
            Taak newTaak = main.createTaak(postTaak);
            main.saveDbTaken();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            var opdracht = await App.Database.GetOpdracht(OpdrachtId2);
            if (opdracht.aangemeld == null)
            {
                bool answer = await DisplayAlert("Aanmelden", "Wil je je nu aanmelden?", "Aanmelden", "Cancel");
                if(answer == true)
                {
                    opdracht.aangemeld = "tijd van aanmelden";

                    OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                    Main main = new Main();
                    Opdracht newOpdracht = main.createOpdracht(postOpdracht);
                    main.saveDbOpdrachten();
                }
            }
            else if (opdracht.afgemeld == null)
            {
                bool answer = await DisplayAlert("Afmelden", "Wil je je nu afmelden?", "Afmelden", "Cancel");
                if (answer == true)
                {
                    opdracht.afgemeld = "tijd van afmelden";

                    OpdrachtPost postOpdracht = new OpdrachtPost(opdracht);
                    Main main = new Main();
                    Opdracht newOpdracht = main.createOpdracht(postOpdracht);
                    main.saveDbOpdrachten();
                }
            }

        }
    }
}