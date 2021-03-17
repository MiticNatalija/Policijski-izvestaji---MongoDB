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
    public partial class Login : Form
    {
        DataManager dm;
        public Login()
        {
            dm = new DataManager();
            
            InitializeComponent();
            txtPass.PasswordChar = '*';
            //Zaposleni z = new Zaposleni()
            //{
            //    Admin = true,
            //    Delatnost="SP",
            //    Ime="Miki",
            //    Pass="m",
            //    User="m",
            //    Prezime="F"
            
            //};
            //dm.addZaposleni(z);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

            if (txtPass.Text == "" || txtUser.Text == "")
            {
                MessageBox.Show("Morate popuniti sva polja!");
                return;
            }
            Zaposleni z = dm.getZaposleni(txtUser.Text, txtPass.Text);
            if (z == null)
            {
                MessageBox.Show("Pogresno korisnicko ime ili lozinka!");
                return;
            }
            ZaposleniView nova = new ZaposleniView(z);
            this.Hide();
            nova.ShowDialog();
            this.Close();
        }
    }
}
