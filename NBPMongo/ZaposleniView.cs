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
    public partial class ZaposleniView : Form
    {
        DataManager dm;
        Zaposleni zap;
        bool tabSelecting = false;
        Prestupnik pres;
        public ZaposleniView()
        {
            dm = new DataManager();
            InitializeComponent();
            btnNoviIzvestaj.Visible = false;
            tabPage1.Controls.Add(labIme);
            tabPage1.Controls.Add(labPrezime);
        }
        public ZaposleniView(Zaposleni z)
        {
            dm = new DataManager();
            InitializeComponent();
            initDgvPrestupnici();
            this.zap = z;
            label12.Text = z.Ime + " " + z.Prezime;
            if (zap.Admin)
            {
                initDgvZaposleni();
                btnDodajZaposlenog.Visible = true;
                btnIzmeniZaposlenog.Visible = true;
                btnObrisiZaposlenog.Visible = true;
            }
            if (pres == null)
            {
                btnNoviIzvestaj.Visible = false;
            }
            btnBrisiIzvestaje.Visible = false;
            label6.Visible = false;
            tabPage1.Controls.Add(labIme);
            tabPage1.Controls.Add(labPrezime);

        }

        #region Init
        private void initDgvPrestupnici()
        {
            dgvPrestupnici.Rows.Clear();
            List<Prestupnik> lista = dm.getPrestupnici();
            foreach (Prestupnik p in lista)
            {
                dgvPrestupnici.Rows.Add(p.Ime, p.Prezime, p.BrLK, p.Visina, p.Adresa, p.Id);
            }
        }
        private void initDgvZaposleni()
        {
            dgvZaposleni.Rows.Clear();
            List<Zaposleni> lista = dm.vratiZaposlene();
            foreach (Zaposleni z in lista)
            {
                dgvZaposleni.Rows.Add(z.Ime, z.Prezime, z.Delatnost, z.Id, z.User, z.Pass);
            }

        }
        private void initDgvIzvestaji(String licna)
        {
            dgvIzvestaji.DataSource = null;
            List<Izvestaj> lista = dm.getIzvestajiPrestupnik(licna);
            dgvIzvestaji.DataSource = lista;
            dgvIzvestaji.Columns["Id"].Visible = false;
            dgvIzvestaji.Columns["Opis"].Visible = false;
            dgvIzvestaji.Columns["Osumnjiceni"].Visible = false;
            dgvIzvestaji.Columns["Inspektor"].Visible = false;
            dgvIzvestaji.Columns["Presuda"].Visible = false;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage3)
            {
                if (!tabSelecting)
                    e.Cancel = true;
            }
            if (e.TabPage == Zaposleni)
            {
                if (!zap.Admin)
                {
                    e.Cancel = true;
                }

            }
        }
        #endregion

        #region Clear
        private void clearDodajPrestupnika()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtLicna.Text = "";
            txtAdresa.Text = "";
            txtVisina.Text = "";
            dtpDatum.Value = DateTime.Now;
        }
        #endregion

        #region PregledPrestupnika

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (txtLk.Text == "")
            {
                MessageBox.Show("Unesite broj licne karte!");
                return;
            }
            pres = dm.getPrestupnik(txtLk.Text);
            if (pres == null)
            {
                btnNoviIzvestaj.Visible = false;
                btnBrisiIzvestaje.Visible = false;
                label6.Visible = false;
                dgvIzvestaji.DataSource = null;
                labIme.Text = "";
                labPrezime.Text = "";
                MessageBox.Show("Osoba ne postoji u bazi!");
                return;
            }
            btnNoviIzvestaj.Visible = true;
            btnBrisiIzvestaje.Visible = true;
            label6.Visible = true;
            labIme.Text = pres.Ime;
            labPrezime.Text = pres.Prezime;
            initDgvIzvestaji(txtLk.Text);

        }

        private void btnNoviIzvestaj_Click(object sender, EventArgs e)
        {
            tabSelecting = true;
            tabControl1.SelectTab(tabPage3);
            txtFilterIme.Text = pres.Ime;
            txtFilterPrezime.Text = pres.Prezime;
            txtLKIzvestaj.Text = pres.BrLK;
            tabSelecting = false;
        }

        private void dgvIzvestaji_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Izvestaj i = (Izvestaj)dgvIzvestaji.Rows[e.RowIndex].DataBoundItem;
            IzvestajDetaljno form = new IzvestajDetaljno(i);

            form.ShowDialog();
        }

        private void btnBrisiIzvestaje_Click(object sender, EventArgs e)
        {
            dm.brisiIzvestaje(txtLk.Text);
            initDgvIzvestaji(txtLk.Text);
        }
        #endregion

        #region DodajPrestupnika
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "" || txtPrezime.Text == "" || txtAdresa.Text == "" || txtLicna.Text == "" || txtVisina.Text == "")
            {
                MessageBox.Show("Morate popuniti sva polja!");
                return;
            }
            Prestupnik p = new Prestupnik();
            p.Ime = txtIme.Text;
            p.Prezime = txtPrezime.Text;
            p.BrLK = txtLicna.Text;
            p.Adresa = txtAdresa.Text;
            p.Visina = txtVisina.Text;
            if (dtpDatum.Value != DateTime.Now)
                p.DatumRodjenja = dtpDatum.Value;

            if (p.DatumRodjenja != null && DateTime.Now.Year - p.DatumRodjenja.Year < 18)
            { 
                MessageBox.Show("Prestupnik je maloletan i ne moze odgovarati za svoja dela. Unesite roditelja u bazu.");
                DodajRoditelja fp = new DodajRoditelja(p);
                fp.dm = this.dm;
                fp.ShowDialog();
            }
            else
                dm.addPrestupnik(p);
            MessageBox.Show("Dodavanje je uspesno obavljeno!");
            clearDodajPrestupnika();
            initDgvPrestupnici();
            tabSelecting = true;
            pres = p;
            tabControl1.SelectTab(tabPage3);
            txtFilterIme.Text = pres.Ime;
            txtFilterPrezime.Text = pres.Prezime;
            txtLKIzvestaj.Text = pres.BrLK;
            tabSelecting = false;
        }
        #endregion

        #region NapisiIzvestaj
   
        private void btnDodajIzvestaj_Click(object sender, EventArgs e)
        {
            if(txtOpisIzvestaj.Text=="" )
            {
                MessageBox.Show("Morate dodati tekst izvestaja!");
                return;
            }
            if (DateTime.Now < dtpDatumIzvestaja.Value)
            {
                MessageBox.Show("Izvestaj moze biti donet najkasnije s danasnjim datumom");
                return;
            }
            Izvestaj i = new Izvestaj();
            i.Opis = txtOpisIzvestaj.Text;
            i.Tip = zap.Delatnost;
            i.Datum = dtpDatumIzvestaja.Value;
            i.Inspektor = label12.Text;

            dm.addIzvestaj(i, pres);
            MessageBox.Show("Izvestaj je uspesno dodat!");
            tabControl1.SelectTab(tabPage1);
            initDgvIzvestaji(pres.BrLK);
            txtLk.Text = pres.BrLK;
            btnNoviIzvestaj.Visible = true;
            btnBrisiIzvestaje.Visible = true;
            labIme.Text = pres.Ime;
            labPrezime.Text = pres.Prezime;
            label6.Visible = true;
        }
        #endregion

        #region Zaposleni
        private void btnDodajZaposlenog_Click(object sender, EventArgs e)
        {
            bool b = false;
            if (txtImeZaposleni.Text == "" || txtPrezimeZaposleni.Text == "" ||cbxDelatnost.SelectedItem==null ||
                txtUsernameZaposleni.Text == "" || txtPassZaposleni.Text == "")
                MessageBox.Show("Morate popuniti sva polja!");
            else
            {
                if (dm.checkUserZaposleni(txtUsernameZaposleni.Text))
                {
                    MessageBox.Show("Korisnicko ime vec postoji!");
                    return;
                }
                if (chbAdmin.Checked == true)
                    b = true;
                Zaposleni z = new Zaposleni(txtImeZaposleni.Text, txtPrezimeZaposleni.Text, txtUsernameZaposleni.Text,
                    txtPassZaposleni.Text, cbxDelatnost.SelectedItem.ToString(), b);
                dm.addZaposleni(z);
                MessageBox.Show("Uspesno ste dodali zaposlenog!");
                initDgvZaposleni();
            }
        }

        private void btnObrisiZaposlenog_Click(object sender, EventArgs e)
        {
            String usrname = dgvZaposleni.SelectedRows[0].Cells["user"].Value.ToString();
            String password = dgvZaposleni.SelectedRows[0].Cells["pass"].Value.ToString();
            Zaposleni z = dm.getZaposleni(usrname, password);
            if (z != null && z.Id != zap.Id)
            {
                dm.obrisiZaposlenog(z.Id);
                MessageBox.Show("Zaposleni uspesno obrisan!");
                initDgvZaposleni();
            }
            else
             MessageBox.Show("Brisanje neuspesno."); //ne moze da obrise sam sebe dok je u aplikaciji
        }

        private void btnIzmeniZaposlenog_Click(object sender, EventArgs e)
        {
            if (cbxNovaDelatnost.SelectedIndex==-1)
            {
                MessageBox.Show("Morate odabrati delatnost!");
                return;
            }
            String usern = dgvZaposleni.SelectedRows[0].Cells["user"].Value.ToString();
            String passw = dgvZaposleni.SelectedRows[0].Cells["pass"].Value.ToString();
            Zaposleni z = dm.getZaposleni(usern, passw);
            dm.updateDelatnost(z.Id,cbxNovaDelatnost.SelectedItem.ToString() );
            MessageBox.Show("Uspesno!");
            initDgvZaposleni();
        }
        #endregion

        #region PromenaLozinke
        private void btnPromeniLozinku_Click(object sender, EventArgs e)
        {
            if (txtSaraLozinka.Text == "" || txtNovaLozinka.Text == "" || txtPotvrdaLozinke.Text == "")
            {
                MessageBox.Show("Popunite sva polja!");
                return;

            }
            if (!dm.checkPassZaposleni(zap.User, txtSaraLozinka.Text))
            {
                MessageBox.Show("Pogresna stara lozinka!");
                return;
            }
            if (!txtNovaLozinka.Text.Equals(txtPotvrdaLozinke.Text))
            {
                MessageBox.Show("Pogresna nova lozinka!");
                return;
            }
            dm.updateLozinka(zap.Id, txtNovaLozinka.Text);
            MessageBox.Show("Lozinka je promenjena!");

        }

        #endregion

       
    }
}
    
