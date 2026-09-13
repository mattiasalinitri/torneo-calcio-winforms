using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compito_info
{
   public partial class Form1 : Form
    {
        private Button btnSquadre;
        private Button btnGiocatori;
        private Button btnPartite;
        private Button btnClassifica;
        private Button btnRicerca;
        private Button btnPrevisione;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Gestione Torneo di Calcio";
            this.Width = 400;
            this.Height = 420;
            this.StartPosition = FormStartPosition.CenterScreen;

            btnSquadre = new Button();
            btnSquadre.Text = "Squadre";
            btnSquadre.Location = new System.Drawing.Point(90, 30);
            btnSquadre.Size = new System.Drawing.Size(200, 40);
            btnSquadre.Click += new EventHandler(btnSquadre_Click);
            this.Controls.Add(btnSquadre);

            btnGiocatori = new Button();
            btnGiocatori.Text = "Giocatori";
            btnGiocatori.Location = new System.Drawing.Point(90, 80);
            btnGiocatori.Size = new System.Drawing.Size(200, 40);
            btnGiocatori.Click += new EventHandler(btnGiocatori_Click);
            this.Controls.Add(btnGiocatori);

            btnPartite = new Button();
            btnPartite.Text = "Partite e risultati";
            btnPartite.Location = new System.Drawing.Point(90, 130);
            btnPartite.Size = new System.Drawing.Size(200, 40);
            btnPartite.Click += new EventHandler(btnPartite_Click);
            this.Controls.Add(btnPartite);

            btnClassifica = new Button();
            btnClassifica.Text = "Classifica";
            btnClassifica.Location = new System.Drawing.Point(90, 180);
            btnClassifica.Size = new System.Drawing.Size(200, 40);
            btnClassifica.Click += new EventHandler(btnClassifica_Click);
            this.Controls.Add(btnClassifica);

            btnRicerca = new Button();
            btnRicerca.Text = "Ricerca";
            btnRicerca.Location = new System.Drawing.Point(90, 230);
            btnRicerca.Size = new System.Drawing.Size(200, 40);
            btnRicerca.Click += new EventHandler(btnRicerca_Click);
            this.Controls.Add(btnRicerca);

            btnPrevisione = new Button();
            btnPrevisione.Text = "Previsione AI";
            btnPrevisione.Location = new System.Drawing.Point(90, 280);
            btnPrevisione.Size = new System.Drawing.Size(200, 40);
            btnPrevisione.Click += new EventHandler(btnPrevisione_Click);
            this.Controls.Add(btnPrevisione);
        }


        private void btnSquadre_Click(object sender, EventArgs e)
        {
            SquadreForm f = new SquadreForm();
            f.ShowDialog();
        }

        private void btnGiocatori_Click(object sender, EventArgs e)
        {
            GiocatoriForm f = new GiocatoriForm();
            f.ShowDialog();
        }

        private void btnPartite_Click(object sender, EventArgs e)
        {
            PartiteForm f = new PartiteForm();
            f.ShowDialog();
        }

        private void btnClassifica_Click(object sender, EventArgs e)
        {
            ClassificaForm f = new ClassificaForm();
            f.ShowDialog();
        }

        private void btnRicerca_Click(object sender, EventArgs e)
        {
            RicercaForm f = new RicercaForm();
            f.ShowDialog();
        }

        private void btnPrevisione_Click(object sender, EventArgs e)
        {
            PrevisioneForm f = new PrevisioneForm();
            f.ShowDialog();
        }
    }
}
