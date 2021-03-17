using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace NBPMongo
{
    public class DataManager
    {
        
        MongoClient client;
        MongoDatabase db;

        public DataManager()
        {
             client = new MongoClient(new MongoUrl("mongodb://localhost/?safe=true"));
            var server = client.GetServer();
            db = server.GetDatabase("policija");
        }


        #region Zaposleni
        public void addZaposleni(Zaposleni z)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            collection.Insert(z);
        }

        public void saveZaposleni(Zaposleni z)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            collection.Save(z);
        }
        public List<Zaposleni> vratiZaposlene()
        {
            List<Zaposleni> lista = new List<Zaposleni>();
            var collection = db.GetCollection<Zaposleni>("zaposleni");

            var r = from zap in collection.AsQueryable<Zaposleni>()
                    orderby zap.Prezime ascending
                    select zap;
            foreach (Zaposleni z in r)
                lista.Add(z);

            return lista;
        }

        public Zaposleni getZaposleni(string user, string pass)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            Zaposleni z;

            z = (from zap in collection.AsQueryable<Zaposleni>()
                 where zap.User == user && zap.Pass == pass
                 select zap).FirstOrDefault();

            return z;
        }
        public Zaposleni getZaposleni(ObjectId id)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            Zaposleni z;
            var query = Query.EQ("_id", id);
         MongoCursor<Zaposleni> za=   collection.Find(query);
            z = za.ToArray<Zaposleni>().First();
            return z;
        }

        public void obrisiZaposlenog(ObjectId id)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            var query = Query.EQ("_id", id);

            collection.Remove(query);
        }
        public bool checkUserZaposleni(string user)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            Zaposleni z = (from zap in collection.AsQueryable<Zaposleni>()
                           where zap.User.Equals(user)
                           select zap).FirstOrDefault();
            if (z == null)
                return false;
            return true;
        }
        public bool checkPassZaposleni(string user,string pass)
        {
            Zaposleni z = getZaposleni(user, pass);
            if (z == null)
                return false;
            return true;
        }
        public void updateLozinka(ObjectId id,string lozinka)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            var query = Query.EQ("_id", id);

            var update = MongoDB.Driver.Builders.Update.Set("Pass", BsonValue.Create(lozinka));
            collection.Update(query, update);
        }

        public void updateDelatnost(ObjectId id, String d)
        {
            var collection = db.GetCollection<Zaposleni>("zaposleni");
            var query = Query.EQ("_id", id);

            var update = MongoDB.Driver.Builders.Update.Set("Delatnost", BsonValue.Create(d));
            collection.Update(query, update);
        }
        #endregion

        #region Prestupnici
        public Prestupnik getPrestupnik(string lk)
        {
            var collection = db.GetCollection<Prestupnik>("prestupnik");
            Prestupnik p;
            p = (from pre in collection.AsQueryable<Prestupnik>()
                 where pre.BrLK.Equals(lk)
                 select pre).FirstOrDefault();

            return p;
        }

        public List<Prestupnik> getPrestupnici()
        {
            List<Prestupnik> lista = new List<Prestupnik>();
            var collection = db.GetCollection<Prestupnik>("prestupnik");
            var r = from pres in collection.AsQueryable<Prestupnik>()
                    orderby pres.Prezime ascending
                    select pres;
            foreach (Prestupnik z in r)
                lista.Add(z);

            return lista;
        }
        public void addPrestupnik(Prestupnik p)
        {
            var collection = db.GetCollection<Prestupnik>("prestupnik");
            collection.Insert(p);
        }
        public Prestupnik getPrestupnik(ObjectId id)
        {
            var collection = db.GetCollection<Prestupnik>("prestupnik");
            Prestupnik p;
            var query = Query.EQ("_id", id);
            MongoCursor<Prestupnik> za = collection.Find(query);
            p = za.ToArray<Prestupnik>().First();
            return p;
        }
        public void savePrestupnik(Prestupnik p)
        {
            var collection = db.GetCollection<Prestupnik>("prestupnik");
            collection.Save(p);
        }

        public void dodajRoditeljaPrestupniku(Prestupnik dete, Roditelj r)
        {
            addRoditelj(r);
            dete.roditelj = new MongoDB.Driver.MongoDBRef("roditelj", r.Id);
            addPrestupnik(dete);
        }
        #endregion

        #region Izvestaji

        public List<Izvestaj> getIzvestajiPrestupnik(string lk)
        {
            List<Izvestaj> lista = new List<Izvestaj>();
            var collection = db.GetCollection<Izvestaj>("izvestaji");
            Prestupnik p = getPrestupnik(lk);
            foreach (MongoDBRef iz in p.Izvestaji)
            {
                Izvestaj izvestaj = db.FetchDBRefAs<Izvestaj>(iz);
                lista.Add(izvestaj);
            }
            return lista;
        }

        public void addIzvestaj(Izvestaj i,Prestupnik p)
        {
            var collectionI = db.GetCollection<Izvestaj>("izvestaji");
            var collectionP = db.GetCollection<Prestupnik>("prestupnik");
            i.Osumnjiceni = new MongoDBRef("prestupnik", p.Id);

            collectionI.Insert(i);
            p.Izvestaji.Add(new MongoDBRef("izvestaji",i.Id));
            collectionP.Save(p);
        }

        public void obrisiIzvestaj(Izvestaj i)
        {
            var collectionI = db.GetCollection<Izvestaj>("izvestaji");
            var query = Query.EQ("_id", i.Id);
            collectionI.Remove(query);
        }

        public void brisiIzvestaje(String lk)
        {
            var collectionP = db.GetCollection<Prestupnik>("prestupnik");
            List<Izvestaj> liz = getIzvestajiPrestupnik(lk); //svi izvestaji za prestupnika
            List<Izvestaj> pom = new List<Izvestaj>();
            Prestupnik p = getPrestupnik(lk);
            foreach(Izvestaj i in liz)
            {
                if(i.Presuda==null && i.Datum.Year<DateTime.Now.Year-2)
                {
                    obrisiIzvestaj(i);
                    MongoDBRef m = new MongoDBRef("izvestaji", i.Id);
                    if (p.Izvestaji.Contains(m))
                    {
                        p.Izvestaji.Remove(m);
                    }
                }
            }
            collectionP.Save(p);

        }
        
        #endregion

        #region Presuda
        public Presuda getPresuda(ObjectId id)
        {
            var collection = db.GetCollection<Presuda>("presuda");
            Presuda p;
            var query = Query.EQ("_id", id);
            MongoCursor<Presuda> za = collection.Find(query);
            p = za.ToArray<Presuda>().First();
            return p;
        }

        public void dodajPresuduIzvestaju(Izvestaj i, Presuda p)
        {
            var collectionP = db.GetCollection<Presuda>("presuda");
            collectionP.Insert(p);
            var collectionI = db.GetCollection<Izvestaj>("izvestaji");
            i.Presuda = new MongoDBRef("presuda", p.Id);
            collectionI.Save(i);
        }
        #endregion

        #region Roditelj
        public void addRoditelj(Roditelj r)
        {
            var collection = db.GetCollection<Roditelj>("roditelj");
            collection.Insert(r);
        }

        public Roditelj getRoditelj(ObjectId id)
        {
            var collection = db.GetCollection<Roditelj>("roditelj");
            Roditelj r;
            var query = Query.EQ("_id", id);
            MongoCursor<Roditelj> za = collection.Find(query);
            r = za.ToArray<Roditelj>().First();
            return r;
        }
        #endregion
    }
}
