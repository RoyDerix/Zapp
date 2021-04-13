using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapp.Models
{

    //public class Taken
    //{
    //    public OpdrachtTaak value { get; set; }
    //}

    //public class OpdrachtTaak
    //{
    //    public string titel { get; set; }
    //    public bool taskCompleted { get; set; }
    //}
    public class OpdrachtCompleet
    {
        public string datum { get; set; }
        public string aangemeld { get; set; }
        public string afgemeld { get; set; }
        public string opmerkingen { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string telefoonnummer { get; set; }
        public string user { get; set; }
        public string _id { get; set; }
        public string id { get; set; }
    }
    public class Opdracht
    {
        public string datum { get; set; }
        public string aangemeld { get; set; }
        public string afgemeld { get; set; }
        public string opmerkingen { get; set; }
        public string klant { get; set; }
        public string user { get; set; }
        //public List<Taken> taken { get; set; }
        [PrimaryKey]
        public string _id { get; set; }
        public string id { get; set; }
    }

    public class OpdrachtenLijst
    {
        public List<Opdracht> entries { get; set; }
    }

    public class OpdrachtPost
    {
        public OpdrachtPost(Opdracht opdracht)
        {
            this.data = opdracht;
        }

        public Opdracht data { get; set; }
    }



}
