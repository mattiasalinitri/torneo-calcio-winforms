using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PartitaController
{
    private ClsSQLServer _db = new ClsSQLServer();

    private Partita CreaDaRiga(DataRow row)
    {
        Partita p = new Partita();
        p.IdPartita = Convert.ToInt32(row["IdPartita"]);
        p.IdSquadraCasa = Convert.ToInt32(row["IdSquadraCasa"]);
        p.IdSquadraOspite = Convert.ToInt32(row["IdSquadraOspite"]);
        p.NomeSquadraCasa = row["NomeCasa"].ToString();
        p.NomeSquadraOspite = row["NomeOspite"].ToString();
        p.DataPartita = Convert.ToDateTime(row["DataPartita"]);

        if (row["GolCasa"] == DBNull.Value)
            p.GolCasa = -1;
        else
            p.GolCasa = Convert.ToInt32(row["GolCasa"]);

        if (row["GolOspite"] == DBNull.Value)
            p.GolOspite = -1;
        else
            p.GolOspite = Convert.ToInt32(row["GolOspite"]);

        p.Giocata = Convert.ToBoolean(row["Giocata"]);
        return p;
    }

    public List<Partita> GetAll()
    {
        List<Partita> lista = new List<Partita>();
        string query = "SELECT p.IdPartita, p.IdSquadraCasa, p.IdSquadraOspite, " +
                        "casa.Nome AS NomeCasa, ospite.Nome AS NomeOspite, " +
                        "p.DataPartita, p.GolCasa, p.GolOspite, p.Giocata " +
                        "FROM Partite p " +
                        "JOIN Squadre casa ON p.IdSquadraCasa = casa.IdSquadra " +
                        "JOIN Squadre ospite ON p.IdSquadraOspite = ospite.IdSquadra " +
                        "ORDER BY p.DataPartita;";
        DataTable dt = _db.ExecuteQuery(query);

        for (int i = 0; i < dt.Rows.Count; i++)
            lista.Add(CreaDaRiga(dt.Rows[i]));

        return lista;
    }

    public void Insert(Partita p)
    {
        string query = "INSERT INTO Partite (IdSquadraCasa, IdSquadraOspite, DataPartita, Giocata) " +
                        "VALUES (@casa, @ospite, @data, 0);";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@casa", p.IdSquadraCasa);
        parametri.Add("@ospite", p.IdSquadraOspite);
        parametri.Add("@data", p.DataPartita);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void RegistraRisultato(int idPartita, int golCasa, int golOspite)
    {
        string query = "UPDATE Partite SET GolCasa=@golCasa, GolOspite=@golOspite, Giocata=1 WHERE IdPartita=@id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@golCasa", golCasa);
        parametri.Add("@golOspite", golOspite);
        parametri.Add("@id", idPartita);
        _db.ExecuteNonQuery(query, parametri);
    }

    public void Delete(int idPartita)
    {
        string query = "DELETE FROM Partite WHERE IdPartita = @id;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@id", idPartita);
        _db.ExecuteNonQuery(query, parametri);
    }

    public List<Partita> Cerca(string testo)
    {
        List<Partita> lista = new List<Partita>();
        string query = "SELECT p.IdPartita, p.IdSquadraCasa, p.IdSquadraOspite, " +
                        "casa.Nome AS NomeCasa, ospite.Nome AS NomeOspite, " +
                        "p.DataPartita, p.GolCasa, p.GolOspite, p.Giocata " +
                        "FROM Partite p " +
                        "JOIN Squadre casa ON p.IdSquadraCasa = casa.IdSquadra " +
                        "JOIN Squadre ospite ON p.IdSquadraOspite = ospite.IdSquadra " +
                        "WHERE casa.Nome LIKE @t OR ospite.Nome LIKE @t ORDER BY p.DataPartita;";
        Dictionary<string, object> parametri = new Dictionary<string, object>();
        parametri.Add("@t", "%" + testo + "%");
        DataTable dt = _db.ExecuteQuery(query, parametri);

        for (int i = 0; i < dt.Rows.Count; i++)
            lista.Add(CreaDaRiga(dt.Rows[i]));

        return lista;
    }

    public List<Partita> GetPartiteGiocateDiSquadra(int idSquadra)
    {
        List<Partita> tutte = GetAll();
        List<Partita> risultato = new List<Partita>();

        for (int i = 0; i < tutte.Count; i++)
        {
            Partita p = tutte[i];
            if (p.Giocata == true)
            {
                if (p.IdSquadraCasa == idSquadra || p.IdSquadraOspite == idSquadra)
                    risultato.Add(p);
            }
        }
        return risultato;
    }
}
