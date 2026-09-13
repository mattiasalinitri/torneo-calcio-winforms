using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public partial class PrevisioneForm : Form
{
        private SquadraController squadraController = new SquadraController();
        private PrevisioneController previsioneController = new PrevisioneController();

        private ComboBox cmbCasa;
        private ComboBox cmbOspite;
        private Button btnCalcola;
        private Label lblRisultato;

        public PrevisioneForm()
        {
            //InitializeComponent();

            this.Text = "Previsione AI - Possibile vincitore";
            this.Width = 480;
            this.Height = 350;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblCasa = new Label();
            lblCasa.Text = "Squadra di casa:";
            lblCasa.Location = new System.Drawing.Point(10, 15);
            lblCasa.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(lblCasa);

            cmbCasa = new ComboBox();
            cmbCasa.Location = new System.Drawing.Point(140, 12);
            cmbCasa.Size = new System.Drawing.Size(200, 20);
            cmbCasa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCasa.DisplayMember = "Nome";
            this.Controls.Add(cmbCasa);

            Label lblOspite = new Label();
            lblOspite.Text = "Squadra ospite:";
            lblOspite.Location = new System.Drawing.Point(10, 50);
            lblOspite.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(lblOspite);

            cmbOspite = new ComboBox();
            cmbOspite.Location = new System.Drawing.Point(140, 47);
            cmbOspite.Size = new System.Drawing.Size(200, 20);
            cmbOspite.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOspite.DisplayMember = "Nome";
            this.Controls.Add(cmbOspite);

            btnCalcola = new Button();
            btnCalcola.Text = "Calcola previsione";
            btnCalcola.Location = new System.Drawing.Point(10, 90);
            btnCalcola.Size = new System.Drawing.Size(180, 30);
            btnCalcola.Click += new EventHandler(btnCalcola_Click);
            this.Controls.Add(btnCalcola);

            lblRisultato = new Label();
            lblRisultato.Location = new System.Drawing.Point(10, 140);
            lblRisultato.Size = new System.Drawing.Size(440, 160);
            lblRisultato.Text = "Seleziona due squadre e premi \"Calcola previsione\".";
            this.Controls.Add(lblRisultato);

            CaricaSquadre();
        }

        private void CaricaSquadre()
        {
            cmbCasa.DataSource = squadraController.GetAll();
            cmbOspite.DataSource = squadraController.GetAll();
            if (cmbOspite.Items.Count > 1)
                cmbOspite.SelectedIndex = 1;
        }

        private void btnCalcola_Click(object sender, EventArgs e)
        {
            Squadra casa = cmbCasa.SelectedItem as Squadra;
            Squadra ospite = cmbOspite.SelectedItem as Squadra;

            if (casa == null || ospite == null) return;

            if (casa.IdSquadra == ospite.IdSquadra)
            {
                MessageBox.Show("Seleziona due squadre diverse.");
                return;
            }

            RisultatoPrevisione esito = previsioneController.Prevedi(casa, ospite);

            string testo = casa.Nome + " vs " + ospite.Nome + "\r\n\r\n";
            testo = testo + "Vittoria " + casa.Nome + ": " + esito.ProbabilitaCasa.ToString("F1") + "%\r\n";
            testo = testo + "Pareggio: " + esito.ProbabilitaPareggio.ToString("F1") + "%\r\n";
            testo = testo + "Vittoria " + ospite.Nome + ": " + esito.ProbabilitaOspite.ToString("F1") + "%\r\n\r\n";
            testo = testo + esito.Commento;

            lblRisultato.Text = testo;
        }
    }


