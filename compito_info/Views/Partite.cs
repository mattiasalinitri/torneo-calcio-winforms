using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class PartiteForm : Form
{
    private PartitaController controller = new PartitaController();
    private SquadraController squadraController = new SquadraController();
    private int idSelezionato = -1;

    private ComboBox cmbCasa;
    private ComboBox cmbOspite;
    private DateTimePicker datePicker;
    private NumericUpDown numGolCasa;
    private NumericUpDown numGolOspite;
    private Button btnNuovo;
    private Button btnCrea;
    private Button btnRegistra;
    private Button btnElimina;
    private DataGridView grid;

    public PartiteForm()
    {
       // InitializeComponent();

        this.Text = "Gestione Partite e Risultati";
        this.Width = 750;
        this.Height = 550;
        this.StartPosition = FormStartPosition.CenterScreen;

        Label lblCasa = new Label();
        lblCasa.Text = "Squadra casa:";
        lblCasa.Location = new System.Drawing.Point(10, 15);
        lblCasa.Size = new System.Drawing.Size(90, 20);
        this.Controls.Add(lblCasa);

        cmbCasa = new ComboBox();
        cmbCasa.Location = new System.Drawing.Point(110, 12);
        cmbCasa.Size = new System.Drawing.Size(150, 20);
        cmbCasa.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCasa.DisplayMember = "Nome";
        this.Controls.Add(cmbCasa);

        Label lblOspite = new Label();
        lblOspite.Text = "Squadra ospite:";
        lblOspite.Location = new System.Drawing.Point(280, 15);
        lblOspite.Size = new System.Drawing.Size(90, 20);
        this.Controls.Add(lblOspite);

        cmbOspite = new ComboBox();
        cmbOspite.Location = new System.Drawing.Point(380, 12);
        cmbOspite.Size = new System.Drawing.Size(150, 20);
        cmbOspite.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOspite.DisplayMember = "Nome";
        this.Controls.Add(cmbOspite);

        Label lblData = new Label();
        lblData.Text = "Data:";
        lblData.Location = new System.Drawing.Point(10, 50);
        lblData.Size = new System.Drawing.Size(90, 20);
        this.Controls.Add(lblData);

        datePicker = new DateTimePicker();
        datePicker.Location = new System.Drawing.Point(110, 47);
        datePicker.Size = new System.Drawing.Size(150, 20);
        this.Controls.Add(datePicker);

        Label lblGolCasa = new Label();
        lblGolCasa.Text = "Gol casa:";
        lblGolCasa.Location = new System.Drawing.Point(10, 85);
        lblGolCasa.Size = new System.Drawing.Size(90, 20);
        this.Controls.Add(lblGolCasa);

        numGolCasa = new NumericUpDown();
        numGolCasa.Location = new System.Drawing.Point(110, 83);
        numGolCasa.Size = new System.Drawing.Size(60, 20);
        numGolCasa.Maximum = 30;
        this.Controls.Add(numGolCasa);

        Label lblGolOspite = new Label();
        lblGolOspite.Text = "Gol ospite:";
        lblGolOspite.Location = new System.Drawing.Point(280, 85);
        lblGolOspite.Size = new System.Drawing.Size(90, 20);
        this.Controls.Add(lblGolOspite);

        numGolOspite = new NumericUpDown();
        numGolOspite.Location = new System.Drawing.Point(380, 83);
        numGolOspite.Size = new System.Drawing.Size(60, 20);
        numGolOspite.Maximum = 30;
        this.Controls.Add(numGolOspite);

        btnNuovo = new Button();
        btnNuovo.Text = "Nuova partita";
        btnNuovo.Location = new System.Drawing.Point(10, 120);
        btnNuovo.Size = new System.Drawing.Size(110, 30);
        btnNuovo.Click += new EventHandler(btnNuovo_Click);
        this.Controls.Add(btnNuovo);

        btnCrea = new Button();
        btnCrea.Text = "Crea partita";
        btnCrea.Location = new System.Drawing.Point(130, 120);
        btnCrea.Size = new System.Drawing.Size(110, 30);
        btnCrea.Click += new EventHandler(btnCrea_Click);
        this.Controls.Add(btnCrea);

        btnRegistra = new Button();
        btnRegistra.Text = "Registra risultato";
        btnRegistra.Location = new System.Drawing.Point(250, 120);
        btnRegistra.Size = new System.Drawing.Size(130, 30);
        btnRegistra.Click += new EventHandler(btnRegistra_Click);
        this.Controls.Add(btnRegistra);

        btnElimina = new Button();
        btnElimina.Text = "Elimina";
        btnElimina.Location = new System.Drawing.Point(390, 120);
        btnElimina.Size = new System.Drawing.Size(90, 30);
        btnElimina.Click += new EventHandler(btnElimina_Click);
        this.Controls.Add(btnElimina);

        grid = new DataGridView();
        grid.Location = new System.Drawing.Point(10, 160);
        grid.Size = new System.Drawing.Size(710, 340);
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
        cmbCasa.DataSource = squadraController.GetAll();
        cmbOspite.DataSource = squadraController.GetAll();
    }

    private void CaricaDati()
    {
        grid.DataSource = controller.GetAll();
    }

    private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        object item = grid.Rows[e.RowIndex].DataBoundItem;
        Partita p = item as Partita;
        if (p != null)
        {
            idSelezionato = p.IdPartita;
            datePicker.Value = p.DataPartita;

            if (p.GolCasa == -1)
                numGolCasa.Value = 0;
            else
                numGolCasa.Value = p.GolCasa;

            if (p.GolOspite == -1)
                numGolOspite.Value = 0;
            else
                numGolOspite.Value = p.GolOspite;

            for (int i = 0; i < cmbCasa.Items.Count; i++)
            {
                Squadra s = cmbCasa.Items[i] as Squadra;
                if (s != null && s.IdSquadra == p.IdSquadraCasa)
                {
                    cmbCasa.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cmbOspite.Items.Count; i++)
            {
                Squadra s = cmbOspite.Items[i] as Squadra;
                if (s != null && s.IdSquadra == p.IdSquadraOspite)
                {
                    cmbOspite.SelectedIndex = i;
                    break;
                }
            }
        }
    }

    private void btnNuovo_Click(object sender, EventArgs e)
    {
        idSelezionato = -1;
        numGolCasa.Value = 0;
        numGolOspite.Value = 0;
        datePicker.Value = DateTime.Today;
    }

    private void btnCrea_Click(object sender, EventArgs e)
    {
        Squadra casa = cmbCasa.SelectedItem as Squadra;
        Squadra ospite = cmbOspite.SelectedItem as Squadra;

        if (casa == null || ospite == null)
        {
            MessageBox.Show("Seleziona entrambe le squadre.");
            return;
        }
        if (casa.IdSquadra == ospite.IdSquadra)
        {
            MessageBox.Show("Le due squadre devono essere diverse.");
            return;
        }

        Partita p = new Partita();
        p.IdSquadraCasa = casa.IdSquadra;
        p.IdSquadraOspite = ospite.IdSquadra;
        p.DataPartita = datePicker.Value;

        try
        {
            controller.Insert(p);
            CaricaDati();
            btnNuovo_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Errore: " + ex.Message);
        }
    }

    private void btnRegistra_Click(object sender, EventArgs e)
    {
        if (idSelezionato == -1)
        {
            MessageBox.Show("Seleziona prima una partita dalla tabella.");
            return;
        }

        try
        {
            controller.RegistraRisultato(idSelezionato, (int)numGolCasa.Value, (int)numGolOspite.Value);
            CaricaDati();
            btnNuovo_Click(sender, e);
            MessageBox.Show("Risultato registrato.");
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
            MessageBox.Show("Seleziona prima una partita dalla tabella.");
            return;
        }

        DialogResult risposta = MessageBox.Show("Eliminare la partita selezionata?", "Conferma", MessageBoxButtons.YesNo);
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
