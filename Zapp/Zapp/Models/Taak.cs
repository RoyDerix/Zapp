using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapp.Models
{
    public class Taak
    {
        public string id { get; set; }
        public string titel { get; set; }
        public bool uitgevoerd { get; set; }
        public string opdracht { get; set; }
        [PrimaryKey]
        public string _id { get; set; }
    }

    public class TakenLijst
    {
        public List<Taak> entries { get; set; }
    }

}

