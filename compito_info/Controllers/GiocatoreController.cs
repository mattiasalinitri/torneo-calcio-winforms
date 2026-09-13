using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GiocatoreController
{
    private ClsSQLServer _db = new ClsSQLServer();

    private Giocatore CreaDaRiga(DataRow row)
    {
        Giocatore g = new Giocatore();
        g.IdGiocatore = Convert.ToInt32(row["IdGiocatore"]);
        g.Nome = row["Nome"].ToString();
        g.Cognome = row["Cognome"].ToString();

        if (row["Ruolo"] == DBNull.Value)
            g.Ruolo = "";
        else
            g.Ruolo = row["Ruolo"].ToString();

        if (row["NumeroMaglia"] == DBNull.Value)
            g.NumeroMaglia = 0;
        else
            g.NumeroMaglia = Convert.ToInt32(row["NumeroMaglia"]);

        g.IdSquadra = Convert.ToInt32(row["IdSquadra"]);
        g.NomeSquadra = row["NomeSquadra"].ToString();
        return g;
    }

    public List<Giocatore> GetAll()
    {
        List<Giocatore> lista = new List<Giocatore>();
        string query = "SELECT g.IdGiocatore, g.Nome, g.Cognome, g.Ruolo, g.NumeroMaglia, g.IdSquadra, s.Nome AS NomeSquadra " +
                        "FROM Giocatori g JOIN Squadre s ON g.IdSquadra = s.IdSquadra ORDER BY s.Nome, g.Cognome;";
        DataTable dt = _db.ExecuteQuery(query);

        for (int i = 0; i < dt.Rows.Count; i++)
            lista.Add(CreaDaRiga(dt.Rows[i]));

        return lista;
    }

    public void Insert(Giocatore g)
    {
        string query = "INSERT INTO Giocatori (Nome, Cognome, Ruolo, NumeroMaglia, IdSquadra) " +
                        "VALUES (@nome, @cognome, @ruolo, @numero, @idSquadra);";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@nome", g.Nome);
        parametri.Add("@cognome", g.Cognome);
        parametri.Add("@ruolo", g.Ruolo);
        parametri.Add("@numero", g.NumeroMaglia);
        parametri.Add("@idSquadra", g.IdSquadra);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void Update(Giocatore g)
    {
        string query = "UPDATE Giocatori SET Nome=@nome, Cognome=@cognome, Ruolo=@ruolo, " +
                        "NumeroMaglia=@numero, IdSquadra=@idSquadra WHERE IdGiocatore=@id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@nome", g.Nome);
        parametri.Add("@cognome", g.Cognome);
        parametri.Add("@ruolo", g.Ruolo);
        parametri.Add("@numero", g.NumeroMaglia);
        parametri.Add("@idSquadra", g.IdSquadra);
        parametri.Add("@id", g.IdGiocatore);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void Delete(int idGiocatore)
    {
        string query = "DELETE FROM Giocatori WHERE IdGiocatore = @id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@id", idGiocatore);
        _db.ExecuteNonQuery(query, parametri);
    }

    public List<Giocatore> Cerca(string testo)
    {
        List<Giocatore> lista = new List<Giocatore>();
        string query = "SELECT g.IdGiocatore, g.Nome, g.Cognome, g.Ruolo, g.NumeroMaglia, g.IdSquadra, s.Nome AS NomeSquadra " +
                        "FROM Giocatori g JOIN Squadre s ON g.IdSquadra = s.IdSquadra " +
                        "WHERE g.Nome LIKE @t OR g.Cognome LIKE @t;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@t", "%" + testo + "%");
        DataTable dt = _db.ExecuteQuery(query, parametri);

        for (int i = 0; i < dt.Rows.Count; i++)
            lista.Add(CreaDaRiga(dt.Rows[i]));

        return lista;
    }
}
