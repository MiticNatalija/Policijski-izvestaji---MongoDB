using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBPMongo
{
    public partial class DodajRoditelja : Form
    {
        public DataManager dm;
        private Prestupnik dete;
        public DodajRoditelja()
        {
            InitializeComponent();
        }

        public DodajRoditelja(Prestupnik p)
        {
            InitializeComponent();
            dete = p;
            lblMaloletnik.Text = p.Ime + " " + p.Prezime;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "" || txtPrezime.Text == "" || txtAdresa.Text == "" || txtLicna.Text == "")
            {
                MessageBox.Show("Morate popuniti sva polja!");
                return;
            }
            Roditelj r = new Roditelj();
            r.Ime = txtIme.Text;
            r.Prezime = txtPrezime.Text;
            r.BrLK = txtLicna.Text;
            r.Adresa = txtAdresa.Text;
            if (dtpDatum.Value != DateTime.Now)
                r.DatumRodjenja = dtpDatum.Value;
            dm.dodajRoditeljaPrestupniku(dete, r);
            this.Close();
        }
    }
}
