using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPMongo
{
    public class Izvestaj
    {
        public ObjectId Id { get; set; }
        public string Opis { get; set; }
      
        public string Tip { get; set; } //saobracajni,kriminalisticki itd...
        public DateTime Datum { get; set; }
        public MongoDBRef Osumnjiceni { get; set; }
        public String Inspektor { get; set; }  //pamti se ime i prezime inspektora
        public MongoDBRef Presuda { get; set; }
    }
}
