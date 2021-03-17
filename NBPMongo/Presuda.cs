using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPMongo
{
    public class Presuda
    {
        public ObjectId Id { get; set; }
        public DateTime DatumDonosenja { get; set; }
        public String NovcanaKazna { get; set; }
        public String ZatvorskaKazna { get; set; }
        public String Napomena { get; set; } //ako je prestupnik maloletan u momentu kreiranja izvestaja, ovde ce 
        //pisati da se kazna odnosi na njegovog roditelja
    }
}
