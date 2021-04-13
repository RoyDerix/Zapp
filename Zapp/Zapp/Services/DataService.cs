using System;
using System.Collections.Generic;
using System.Text;
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

        public void SaveDbOpdrachten()
        {
            var opdrachtenlijst = api.getApiOpdrachten();
            App.Database.SaveApiOpdrachten(opdrachtenlijst);
        }

        public void SaveDbKlanten()
        {
            var klantenlijst = api.getApiKlanten();
            App.Database.SaveApiKlanten(klantenlijst);
        }

        public void SaveDbTaken()
        {
            var takenlijst = api.getApiTaken();
            App.Database.SaveApiTaken(takenlijst);
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
