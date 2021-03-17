using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPMongo
{
    public class Roditelj
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrLK { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRodjenja { get; set; }
    }
}
