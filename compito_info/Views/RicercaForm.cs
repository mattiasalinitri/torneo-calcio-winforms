using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class RicercaForm : Form
{
    private SquadraController squadraController = new SquadraController();
    private GiocatoreController giocatoreController = new GiocatoreController();
    private PartitaController partitaController = new PartitaController();

    private TextBox txtRicerca;
    private ComboBox cmbTipo;
    private Button btnCerca;
    private DataGridView grid;

    public RicercaForm()
    {
        //InitializeComponent();

        this.Text = "Ricerca";
        this.Width = 700;
        this.Height = 500;
        this.StartPosition = FormStartPosition.CenterScreen;

        Label lblCerca = new Label();
        lblCerca.Text = "Cerca:";
        lblCerca.Location = new System.Drawing.Point(10, 15);
        lblCerca.Size = new System.Drawing.Size(50, 20);
        this.Controls.Add(lblCerca);

        txtRicerca = new TextBox();
        txtRicerca.Location = new System.Drawing.Point(70, 12);
        txtRicerca.Size = new System.Drawing.Size(200, 20);
        this.Controls.Add(txtRicerca);

        cmbTipo = new ComboBox();
        cmbTipo.Location = new System.Drawing.Point(280, 12);
        cmbTipo.Size = new System.Drawing.Size(120, 20);
        cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTipo.Items.Add("Squadre");
        cmbTipo.Items.Add("Giocatori");
        cmbTipo.Items.Add("Partite");
        cmbTipo.SelectedIndex = 0;
        this.Controls.Add(cmbTipo);

        btnCerca = new Button();
        btnCerca.Text = "Cerca";
        btnCerca.Location = new System.Drawing.Point(410, 10);
        btnCerca.Size = new System.Drawing.Size(80, 25);
        btnCerca.Click += new EventHandler(btnCerca_Click);
        this.Controls.Add(btnCerca);

        grid = new DataGridView();
        grid.Location = new System.Drawing.Point(10, 50);
        grid.Size = new System.Drawing.Size(660, 400);
        grid.ReadOnly = true;
        this.Controls.Add(grid);
    }

    private void btnCerca_Click(object sender, EventArgs e)
    {
        string testo = txtRicerca.Text.Trim();
        string tipo = cmbTipo.SelectedItem.ToString();

        if (tipo == "Squadre")
            grid.DataSource = squadraController.Cerca(testo);
        else if (tipo == "Giocatori")
            grid.DataSource = giocatoreController.Cerca(testo);
        else if (tipo == "Partite")
            grid.DataSource = partitaController.Cerca(testo);
    }
}
