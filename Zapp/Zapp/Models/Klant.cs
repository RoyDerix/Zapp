using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapp.Models
{
    public class Klant
    {
        public string id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string telefoonnummer { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        [PrimaryKey]
        public string _id { get; set; }
    }

    public class KlantenLijst
    {
        public List<Klant> entries { get; set; }
    }

    public class KlantPost
    {
        public KlantPost(Klant klant)
        {
            this.data = klant;
        }

        public Klant data { get; set; }
    }

}
