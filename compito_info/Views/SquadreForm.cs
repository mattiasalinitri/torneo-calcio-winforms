using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

public partial class SquadreForm : Form
{
    private SquadraController controller = new SquadraController();
    private int idSelezionato = -1; // -1 = nessuna riga selezionata, sto creando una nuova squadra

    private Label lblNome;
    private TextBox txtNome;
    private Label lblCitta;
    private TextBox txtCitta;
    private Label lblAnno;
    private NumericUpDown numAnno;
    private Button btnNuovo;
    private Button btnSalva;
    private Button btnElimina;
    private DataGridView grid;

    public SquadreForm()
    {
        //InitializeComponent();

        this.Text = "Gestione Squadre";
        this.Width = 650;
        this.Height = 500;
        this.StartPosition = FormStartPosition.CenterScreen;

        lblNome = new Label();
        lblNome.Text = "Nome:";
        lblNome.Location = new System.Drawing.Point(10, 15);
        lblNome.Size = new System.Drawing.Size(80, 20);
        this.Controls.Add(lblNome);

        txtNome = new TextBox();
        txtNome.Location = new System.Drawing.Point(100, 12);
        txtNome.Size = new System.Drawing.Size(200, 20);
        this.Controls.Add(txtNome);

        lblCitta = new Label();
        lblCitta.Text = "Citta':";
        lblCitta.Location = new System.Drawing.Point(10, 45);
        lblCitta.Size = new System.Drawing.Size(80, 20);
        this.Controls.Add(lblCitta);

        txtCitta = new TextBox();
        txtCitta.Location = new System.Drawing.Point(100, 42);
        txtCitta.Size = new System.Drawing.Size(200, 20);
        this.Controls.Add(txtCitta);

        lblAnno = new Label();
        lblAnno.Text = "Anno fondazione:";
        lblAnno.Location = new System.Drawing.Point(10, 75);
        lblAnno.Size = new System.Drawing.Size(120, 20);
        this.Controls.Add(lblAnno);

        numAnno = new NumericUpDown();
        numAnno.Location = new System.Drawing.Point(140, 73);
        numAnno.Size = new System.Drawing.Size(100, 20);
        numAnno.Minimum = 1850;
        numAnno.Maximum = 2100;
        numAnno.Value = 2000;
        this.Controls.Add(numAnno);

        btnNuovo = new Button();
        btnNuovo.Text = "Nuovo";
        btnNuovo.Location = new System.Drawing.Point(10, 110);
        btnNuovo.Size = new System.Drawing.Size(90, 30);
        btnNuovo.Click += new EventHandler(btnNuovo_Click);
        this.Controls.Add(btnNuovo);

        btnSalva = new Button();
        btnSalva.Text = "Salva";
        btnSalva.Location = new System.Drawing.Point(110, 110);
        btnSalva.Size = new System.Drawing.Size(90, 30);
        btnSalva.Click += new EventHandler(btnSalva_Click);
        this.Controls.Add(btnSalva);

        btnElimina = new Button();
        btnElimina.Text = "Elimina";
        btnElimina.Location = new System.Drawing.Point(210, 110);
        btnElimina.Size = new System.Drawing.Size(90, 30);
        btnElimina.Click += new EventHandler(btnElimina_Click);
        this.Controls.Add(btnElimina);

        grid = new DataGridView();
        grid.Location = new System.Drawing.Point(10, 150);
        grid.Size = new System.Drawing.Size(610, 290);
        grid.ReadOnly = true;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.CellClick += new DataGridViewCellEventHandler(grid_CellClick);
        this.Controls.Add(grid);

        CaricaDati();
    }

    private void CaricaDati()
    {
        grid.DataSource = controller.GetAll();
    }

    private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        object item = grid.Rows[e.RowIndex].DataBoundItem;
        Squadra s = item as Squadra;
        if (s != null)
        {
            idSelezionato = s.IdSquadra;
            txtNome.Text = s.Nome;
            txtCitta.Text = s.Citta;
            if (s.AnnoFondazione == 0)
                numAnno.Value = 2000;
            else
                numAnno.Value = s.AnnoFondazione;
        }
    }

    private void btnNuovo_Click(object sender, EventArgs e)
    {
        idSelezionato = -1;
        txtNome.Text = "";
        txtCitta.Text = "";
        numAnno.Value = 2000;
    }

    private void btnSalva_Click(object sender, EventArgs e)
    {
        if (txtNome.Text.Trim() == "")
        {
            MessageBox.Show("Il nome della squadra e' obbligatorio.");
            return;
        }

        Squadra s = new Squadra();
        s.IdSquadra = idSelezionato;
        s.Nome = txtNome.Text.Trim();
        s.Citta = txtCitta.Text.Trim();
        s.AnnoFondazione = (int)numAnno.Value;

        try
        {
            if (idSelezionato == -1)
                controller.Insert(s);
            else
                controller.Update(s);

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
            MessageBox.Show("Seleziona prima una squadra dalla tabella.");
            return;
        }

        DialogResult risposta = MessageBox.Show("Eliminare la squadra selezionata?", "Conferma", MessageBoxButtons.YesNo);
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



    private void SquadreForm_Load(object sender, EventArgs e)
    {

    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // SquadreForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "SquadreForm";
            this.Load += new System.EventHandler(this.SquadreForm_Load_1);
            this.ResumeLayout(false);

    }

    private void SquadreForm_Load_1(object sender, EventArgs e)
    {

    }
}
