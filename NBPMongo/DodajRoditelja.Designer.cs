namespace NBPMongo
{
    partial class DodajRoditelja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblp = new System.Windows.Forms.Label();
            this.lblMaloletnik = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.txtLicna = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtAdresa
            // 
            this.txtAdresa.Location = new System.Drawing.Point(119, 215);
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(183, 20);
            this.txtAdresa.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Prezime:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Adresa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Datum rodjenja:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Ime:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.Location = new System.Drawing.Point(119, 162);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(183, 20);
            this.dtpDatum.TabIndex = 69;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(129, 341);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(91, 23);
            this.btnDodaj.TabIndex = 77;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "Broj licne karte:";
            // 
            // lblp
            // 
            this.lblp.AutoSize = true;
            this.lblp.Location = new System.Drawing.Point(22, 273);
            this.lblp.Name = "lblp";
            this.lblp.Size = new System.Drawing.Size(158, 13);
            this.lblp.TabIndex = 78;
            this.lblp.Text = "Maloletnik koji je izvrsio prestup:";
            // 
            // lblMaloletnik
            // 
            this.lblMaloletnik.AutoSize = true;
            this.lblMaloletnik.Location = new System.Drawing.Point(233, 273);
            this.lblMaloletnik.Name = "lblMaloletnik";
            this.lblMaloletnik.Size = new System.Drawing.Size(51, 13);
            this.lblMaloletnik.TabIndex = 79;
            this.lblMaloletnik.Text = "whatever";
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(119, 40);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(183, 20);
            this.txtIme.TabIndex = 80;
            // 
            // txtPrezime
            // 
            this.txtPrezime.Location = new System.Drawing.Point(119, 81);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(183, 20);
            this.txtPrezime.TabIndex = 81;
            // 
            // txtLicna
            // 
            this.txtLicna.Location = new System.Drawing.Point(119, 118);
            this.txtLicna.Name = "txtLicna";
            this.txtLicna.Size = new System.Drawing.Size(183, 20);
            this.txtLicna.TabIndex = 82;
            // 
            // FormNoviPrestupnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 376);
            this.Controls.Add(this.txtLicna);
            this.Controls.Add(this.txtPrezime);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.lblMaloletnik);
            this.Controls.Add(this.lblp);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAdresa);
            this.Name = "FormNoviPrestupnik";
            this.Text = "Dodajte roditelja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblp;
        private System.Windows.Forms.Label lblMaloletnik;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.TextBox txtLicna;
    }
}

