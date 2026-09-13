using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;


public partial class GiocatoriForm : Form
    {
        private GiocatoreController controller = new GiocatoreController();
        private SquadraController squadraController = new SquadraController();
        private int idSelezionato = -1;

        private TextBox txtNome;
        private TextBox txtCognome;
        private TextBox txtRuolo;
        private NumericUpDown numMaglia;
        private ComboBox cmbSquadra;
        private Button btnNuovo;
        private Button btnSalva;
        private Button btnElimina;
        private DataGridView grid;

        public GiocatoriForm()
        {
           

            this.Text = "Gestione Giocatori";
            this.Width = 700;
            this.Height = 520;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblNome = new Label();
            lblNome.Text = "Nome:";
            lblNome.Location = new System.Drawing.Point(10, 15);
            lblNome.Size = new System.Drawing.Size(80, 20);
            this.Controls.Add(lblNome);

            txtNome = new TextBox();
            txtNome.Location = new System.Drawing.Point(100, 12);
            txtNome.Size = new System.Drawing.Size(180, 20);
            this.Controls.Add(txtNome);

            Label lblCognome = new Label();
            lblCognome.Text = "Cognome:";
            lblCognome.Location = new System.Drawing.Point(10, 45);
            lblCognome.Size = new System.Drawing.Size(80, 20);
            this.Controls.Add(lblCognome);

            txtCognome = new TextBox();
            txtCognome.Location = new System.Drawing.Point(100, 42);
            txtCognome.Size = new System.Drawing.Size(180, 20);
            this.Controls.Add(txtCognome);

            Label lblRuolo = new Label();
            lblRuolo.Text = "Ruolo:";
            lblRuolo.Location = new System.Drawing.Point(10, 75);
            lblRuolo.Size = new System.Drawing.Size(80, 20);
            this.Controls.Add(lblRuolo);

            txtRuolo = new TextBox();
            txtRuolo.Location = new System.Drawing.Point(100, 72);
            txtRuolo.Size = new System.Drawing.Size(180, 20);
            this.Controls.Add(txtRuolo);

            Label lblMaglia = new Label();
            lblMaglia.Text = "Numero maglia:";
            lblMaglia.Location = new System.Drawing.Point(10, 105);
            lblMaglia.Size = new System.Drawing.Size(100, 20);
            this.Controls.Add(lblMaglia);

            numMaglia = new NumericUpDown();
            numMaglia.Location = new System.Drawing.Point(120, 103);
            numMaglia.Size = new System.Drawing.Size(60, 20);
            numMaglia.Minimum = 1;
            numMaglia.Maximum = 99;
            this.Controls.Add(numMaglia);

            Label lblSquadra = new Label();
            lblSquadra.Text = "Squadra:";
            lblSquadra.Location = new System.Drawing.Point(300, 15);
            lblSquadra.Size = new System.Drawing.Size(80, 20);
            this.Controls.Add(lblSquadra);

            cmbSquadra = new ComboBox();
            cmbSquadra.Location = new System.Drawing.Point(380, 12);
            cmbSquadra.Size = new System.Drawing.Size(180, 20);
            cmbSquadra.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSquadra.DisplayMember = "Nome";
            this.Controls.Add(cmbSquadra);

            btnNuovo = new Button();
            btnNuovo.Text = "Nuovo";
            btnNuovo.Location = new System.Drawing.Point(10, 140);
            btnNuovo.Size = new System.Drawing.Size(90, 30);
            btnNuovo.Click += new EventHandler(btnNuovo_Click);
            this.Controls.Add(btnNuovo);

            btnSalva = new Button();
            btnSalva.Text = "Salva";
            btnSalva.Location = new System.Drawing.Point(110, 140);
            btnSalva.Size = new System.Drawing.Size(90, 30);
            btnSalva.Click += new EventHandler(btnSalva_Click);
            this.Controls.Add(btnSalva);

            btnElimina = new Button();
            btnElimina.Text = "Elimina";
            btnElimina.Location = new System.Drawing.Point(210, 140);
            btnElimina.Size = new System.Drawing.Size(90, 30);
            btnElimina.Click += new EventHandler(btnElimina_Click);
            this.Controls.Add(btnElimina);

            grid = new DataGridView();
            grid.Location = new System.Drawing.Point(10, 180);
            grid.Size = new System.Drawing.Size(660, 290);
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.CellClick += new DataGridViewCellEventHandler(grid_CellClick);
            this.Controls.Add(grid);

            CaricaSquadre();
            CaricaDati();
        }

        private void CaricaSquadre()
        {
            cmbSquadra.DataSource = squadraController.GetAll();
        }

        private void CaricaDati()
        {
            grid.DataSource = controller.GetAll();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            object item = grid.Rows[e.RowIndex].DataBoundItem;
            Giocatore g = item as Giocatore;
            if (g != null)
            {
                idSelezionato = g.IdGiocatore;
                txtNome.Text = g.Nome;
                txtCognome.Text = g.Cognome;
                txtRuolo.Text = g.Ruolo;
                if (g.NumeroMaglia == 0)
                    numMaglia.Value = 1;
                else
                    numMaglia.Value = g.NumeroMaglia;

                for (int i = 0; i < cmbSquadra.Items.Count; i++)
                {
                    Squadra s = cmbSquadra.Items[i] as Squadra;
                    if (s != null && s.IdSquadra == g.IdSquadra)
                    {
                        cmbSquadra.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnNuovo_Click(object sender, EventArgs e)
        {
            idSelezionato = -1;
            txtNome.Text = "";
            txtCognome.Text = "";
            txtRuolo.Text = "";
            numMaglia.Value = 1;
            if (cmbSquadra.Items.Count > 0)
                cmbSquadra.SelectedIndex = 0;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() == "" || txtCognome.Text.Trim() == "")
            {
                MessageBox.Show("Nome e cognome sono obbligatori.");
                return;
            }

            Squadra squadraSel = cmbSquadra.SelectedItem as Squadra;
            if (squadraSel == null)
            {
                MessageBox.Show("Seleziona una squadra.");
                return;
            }

            Giocatore g = new Giocatore();
            g.IdGiocatore = idSelezionato;
            g.Nome = txtNome.Text.Trim();
            g.Cognome = txtCognome.Text.Trim();
            g.Ruolo = txtRuolo.Text.Trim();
            g.NumeroMaglia = (int)numMaglia.Value;
            g.IdSquadra = squadraSel.IdSquadra;

            try
            {
                if (idSelezionato == -1)
                    controller.Insert(g);
                else
                    controller.Update(g);

                CaricaDati();
                btnNuovo_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (idSelezionato == -1)
            {
                MessageBox.Show("Seleziona prima un giocatore dalla tabella.");
                return;
            }

            DialogResult risposta = MessageBox.Show("Eliminare il giocatore selezionato?", "Conferma", MessageBoxButtons.YesNo);
            if (risposta != DialogResult.Yes) return;

            try
            {
                controller.Delete(idSelezionato);
                CaricaDati();
                btnNuovo_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
    }

