using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SquadraController
{
    private ClsSQLServer _db = new ClsSQLServer();

    public List<Squadra> GetAll()
    {
        List<Squadra> lista = new List<Squadra>();
        string query = "SELECT IdSquadra, Nome, Citta, AnnoFondazione FROM Squadre ORDER BY Nome;";
        DataTable dt = _db.ExecuteQuery(query);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            Squadra s = new Squadra();
            s.IdSquadra = Convert.ToInt32(row["IdSquadra"]);
            s.Nome = row["Nome"].ToString();

            if (row["Citta"] == DBNull.Value)
                s.Citta = "";
            else
                s.Citta = row["Citta"].ToString();

            if (row["AnnoFondazione"] == DBNull.Value)
                s.AnnoFondazione = 0;
            else
                s.AnnoFondazione = Convert.ToInt32(row["AnnoFondazione"]);

            lista.Add(s);
        }
        return lista;
    }

    public void Insert(Squadra s)
    {
        string query = "INSERT INTO Squadre (Nome, Citta, AnnoFondazione) VALUES (@nome, @citta, @anno);";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@nome", s.Nome);
        parametri.Add("@citta", s.Citta);
        parametri.Add("@anno", s.AnnoFondazione);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void Update(Squadra s)
    {
        string query = "UPDATE Squadre SET Nome = @nome, Citta = @citta, AnnoFondazione = @anno WHERE IdSquadra = @id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@nome", s.Nome);
        parametri.Add("@citta", s.Citta);
        parametri.Add("@anno", s.AnnoFondazione);
        parametri.Add("@id", s.IdSquadra);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void Delete(int idSquadra)
    {
        string query = "DELETE FROM Squadre WHERE IdSquadra = @id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@id", idSquadra);
        _db.ExecuteNonQuery(query, parametri);
    }

    public List<Squadra> Cerca(string testo)
    {
        List<Squadra> lista = new List<Squadra>();
        string query = "SELECT IdSquadra, Nome, Citta, AnnoFondazione FROM Squadre WHERE Nome LIKE @t OR Citta LIKE @t;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@t", "%" + testo + "%");
        DataTable dt = _db.ExecuteQuery(query, parametri);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            Squadra s = new Squadra();
            s.IdSquadra = Convert.ToInt32(row["IdSquadra"]);
            s.Nome = row["Nome"].ToString();
            if (row["Citta"] == DBNull.Value)
                s.Citta = "";
            else
                s.Citta = row["Citta"].ToString();
            if (row["AnnoFondazione"] == DBNull.Value)
                s.AnnoFondazione = 0;
            else
                s.AnnoFondazione = Convert.ToInt32(row["AnnoFondazione"]);
            lista.Add(s);
        }
        return lista;
    }
}

