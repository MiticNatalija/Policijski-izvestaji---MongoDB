using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPMongo
{
    public class Zaposleni
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Delatnost { get; set; } /* SP- saobracajna policija
                                                 GP - granicna policja
                                                 KP - kriminalisticka policija
                                                 ON - odeljenje za narkotike
                                                 SAJ - specijalna antiteroristicka jedinica*/
        public string User { get; set; }
        public string Pass { get; set; }
        public bool Admin { get; set; }

        public Zaposleni() { }
        public Zaposleni(String i, String p, String us, String pass,String delatnost, bool ad)
        {
            Ime = i;
            Prezime = p;
            User = us;
            Pass = pass;
            Delatnost = delatnost;
            Admin = ad;
        }

    }
}
