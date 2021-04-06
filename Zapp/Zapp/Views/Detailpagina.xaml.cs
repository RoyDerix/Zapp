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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class Detailpagina : ContentPage
    {

        public string ItemId
        {
            set
            {
                LoadOpdracht(value);
            }
        }

        public Detailpagina()
        {
            InitializeComponent();
        }

        async void LoadOpdracht(string id)
        {
            try
            {
                List<Taak> data = new List<Taak>();
                var taken = await App.Database.GetTaken();

                foreach(var taak in taken)
                {
                    if(taak.opdracht == id)
                    {
                        data.Add(new Taak()
                        {
                            id = taak.id,
                            titel = taak.titel,
                            uitgevoerd = taak.uitgevoerd,
                            opdracht = taak.opdracht,
                            _id = taak._id
                        });
                    }
                }
                TakenListView.ItemsSource = data;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load opdracht.");
            }
        }
    }
}