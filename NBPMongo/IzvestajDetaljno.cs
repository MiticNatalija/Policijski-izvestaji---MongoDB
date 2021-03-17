using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace NBPMongo
{
    public partial class IzvestajDetaljno : Form
    {
        public Izvestaj iz;
        DataManager dm;
        Prestupnik p;
        public IzvestajDetaljno()
        {
            dm = new DataManager();
            InitializeComponent();
        }
        public IzvestajDetaljno(Izvestaj izvestaj)
        {
            iz = izvestaj;
            dm = new DataManager();
            InitializeComponent();
            lblDatum.Text = izvestaj.Datum.ToShortDateString();
            txtOpis.Text = izvestaj.Opis;
            lblInspektor.Text = iz.Inspektor;
            lblNadleznost.Text = iz.Tip;
            p = dm.getPrestupnik((ObjectId)iz.Osumnjiceni.Id);
            lblIme.Text = p.Ime + " " + p.Prezime;
            if (iz.Presuda != null)
            {
                Presuda p = (Presuda)dm.getPresuda((ObjectId)iz.Presuda.Id);
                btnDodajPresudu.Visible = false;
                txtNovcanaKazna.Text = p.NovcanaKazna;
                txtZatvorskaKazna.Text = p.ZatvorskaKazna;
                txtNovcanaKazna.ReadOnly = true;
                txtZatvorskaKazna.ReadOnly = true;
                dtpPresuda.Visible = false;
                Label noviDatum = new Label();
                groupBox1.Controls.Add(noviDatum);
                noviDatum.Text = p.DatumDonosenja.ToShortDateString();
                noviDatum.Location = new Point(110, 27);
                
                if(p.Napomena!="")
                {
                    lblNapomena.Text = p.Napomena;
                    lblNapomena.Visible = true;
                }
            }
        }

        private void btnDodajPresudu_Click(object sender, EventArgs e)
        {
            Presuda p = new Presuda();
            p.DatumDonosenja = dtpPresuda.Value;
            if(txtNovcanaKazna.Text!="")
                 p.NovcanaKazna = txtNovcanaKazna.Text;
            if (txtZatvorskaKazna.Text != "")
                p.ZatvorskaKazna = txtZatvorskaKazna.Text;
            Prestupnik pr = dm.getPrestupnik((ObjectId)iz.Osumnjiceni.Id);
            if (iz.Datum.Year - pr.DatumRodjenja.Year <19 && pr.roditelj!=null) //u momentu kreiranja izvestaja, prestupnik je maloletan
            {
                Roditelj roditelj = dm.getRoditelj((ObjectId)pr.roditelj.Id);
                p.Napomena = "Prestupnik je maloletan, pa se novcana i zatvorska kazna primenjuju na njegovog " +
                    "roditelja/staratelja: " + roditelj.Ime + " " + roditelj.Prezime;
            }
            dm.dodajPresuduIzvestaju(iz, p);
            MessageBox.Show("Presuda dodata!");
            txtNovcanaKazna.ReadOnly = true;
            txtZatvorskaKazna.ReadOnly = true;
            this.Close();
        }
    }
}