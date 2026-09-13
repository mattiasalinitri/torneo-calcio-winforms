using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class ClassificaForm : Form
{
    private ClassificaController controller = new ClassificaController();
    private Button btnAggiorna;
    private DataGridView grid;

    public ClassificaForm()
    {
       // InitializeComponent();

        this.Text = "Classifica del Torneo";
        this.Width = 650;
        this.Height = 450;
        this.StartPosition = FormStartPosition.CenterScreen;

        btnAggiorna = new Button();
        btnAggiorna.Text = "Aggiorna classifica";
        btnAggiorna.Location = new System.Drawing.Point(10, 10);
        btnAggiorna.Size = new System.Drawing.Size(150, 30);
        btnAggiorna.Click += new EventHandler(btnAggiorna_Click);
        this.Controls.Add(btnAggiorna);

        grid = new DataGridView();
        grid.Location = new System.Drawing.Point(10, 50);
        grid.Size = new System.Drawing.Size(610, 350);
        grid.ReadOnly = true;
        this.Controls.Add(grid);

        CaricaClassifica();
    }

    private void btnAggiorna_Click(object sender, EventArgs e)
    {
        CaricaClassifica();
    }

    private void CaricaClassifica()
    {
        grid.DataSource = controller.CalcolaClassifica();
    }
}