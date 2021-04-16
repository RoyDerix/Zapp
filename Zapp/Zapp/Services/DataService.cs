using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zapp.Models;

namespace Zapp.Services
{
    public class DataService
    {
        private ApiService api;

        public DataService()
        {
            api = new ApiService();
        }
        
        public async Task<bool> SaveDbOpdrachten()
        {
            var opdrachtenlijst = api.getApiOpdrachten();
            await App.Database.ClearAllOpdrachten();
            await App.Database.SaveApiOpdrachten(opdrachtenlijst);
            return true;
        }

        public async Task<bool> SaveDbKlanten()
        {
            var klantenlijst = api.getApiKlanten();
            await App.Database.ClearAllKlanten();
            await App.Database.SaveApiKlanten(klantenlijst);
            return true;
        }

        public async Task<bool> SaveDbTaken()
        {
            var takenlijst = api.getApiTaken();
            await App.Database.ClearAllTaken();
            await App.Database.SaveApiTaken(takenlijst);
            return true;
        }

        public User authUser(AuthUser authUser)
        {
            return api.authUser(authUser);
        }

        public Opdracht createOpdracht(OpdrachtPost opdracht)
        {
            return api.createOpdracht(opdracht);
        }

        public Klant createKlant(KlantPost klant)
        {
            return api.createKlant(klant);
        }

        public Taak createTaak(TaakPost taak)
        {
            return api.createTaak(taak);
        }


    }
}
