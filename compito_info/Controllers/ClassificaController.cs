using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ClassificaController
{
    private ClsSQLServer _db = new ClsSQLServer();

    public List<ClassificaRiga> CalcolaClassifica()
    {
        Dictionary<int, ClassificaRiga> righe = new Dictionary<int, ClassificaRiga>();

        // 1) creo una riga vuota per ogni squadra
        DataTable squadre = _db.ExecuteQuery("SELECT IdSquadra, Nome FROM Squadre;");
        for (int i = 0; i < squadre.Rows.Count; i++)
        {
            int id = Convert.ToInt32(squadre.Rows[i]["IdSquadra"]);
            ClassificaRiga riga = new ClassificaRiga();
            riga.IdSquadra = id;
            riga.Nome = squadre.Rows[i]["Nome"].ToString();
            righe.Add(id, riga);
        }

        // 2) statistiche per le partite giocate IN CASA (GROUP BY + funzioni di aggregazione)
        string queryCasa = "SELECT IdSquadraCasa, COUNT(*) AS Giocate, " +
            "SUM(CASE WHEN GolCasa > GolOspite THEN 1 ELSE 0 END) AS Vittorie, " +
            "SUM(CASE WHEN GolCasa = GolOspite THEN 1 ELSE 0 END) AS Pareggi, " +
            "SUM(CASE WHEN GolCasa < GolOspite THEN 1 ELSE 0 END) AS Sconfitte, " +
            "SUM(GolCasa) AS GolFatti, SUM(GolOspite) AS GolSubiti " +
            "FROM Partite WHERE Giocata = 1 GROUP BY IdSquadraCasa;";
        DataTable statCasa = _db.ExecuteQuery(queryCasa);
        AggiungiStatistiche(righe, statCasa, "IdSquadraCasa");

        // 3) statistiche per le partite giocate IN TRASFERTA
        string queryOspite = "SELECT IdSquadraOspite, COUNT(*) AS Giocate, " +
            "SUM(CASE WHEN GolOspite > GolCasa THEN 1 ELSE 0 END) AS Vittorie, " +
            "SUM(CASE WHEN GolOspite = GolCasa THEN 1 ELSE 0 END) AS Pareggi, " +
            "SUM(CASE WHEN GolOspite < GolCasa THEN 1 ELSE 0 END) AS Sconfitte, " +
            "SUM(GolOspite) AS GolFatti, SUM(GolCasa) AS GolSubiti " +
            "FROM Partite WHERE Giocata = 1 GROUP BY IdSquadraOspite;";
        DataTable statOspite = _db.ExecuteQuery(queryOspite);
        AggiungiStatistiche(righe, statOspite, "IdSquadraOspite");

        // 4) calcolo punti e differenza reti per ogni riga
        List<ClassificaRiga> lista = new List<ClassificaRiga>(righe.Values);
        for (int i = 0; i < lista.Count; i++)
        {
            lista[i].DifferenzaReti = lista[i].GolFatti - lista[i].GolSubiti;
            lista[i].Punti = lista[i].Vittorie * 3 + lista[i].Pareggi;
        }

        // 5) ordino la lista per punti decrescenti (ordinamento a bolle, semplice)
        for (int i = 0; i < lista.Count - 1; i++)
        {
            for (int j = 0; j < lista.Count - 1 - i; j++)
            {
                if (lista[j].Punti < lista[j + 1].Punti)
                {
                    ClassificaRiga temp = lista[j];
                    lista[j] = lista[j + 1];
                    lista[j + 1] = temp;
                }
            }
        }

        return lista;
    }

    private void AggiungiStatistiche(Dictionary<int, ClassificaRiga> righe, DataTable dt, string colonnaId)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            int id = Convert.ToInt32(row[colonnaId]);

            if (righe.ContainsKey(id))
            {
                ClassificaRiga r = righe[id];
                r.PartiteGiocate = r.PartiteGiocate + Convert.ToInt32(row["Giocate"]);
                r.Vittorie = r.Vittorie + Convert.ToInt32(row["Vittorie"]);
                r.Pareggi = r.Pareggi + Convert.ToInt32(row["Pareggi"]);
                r.Sconfitte = r.Sconfitte + Convert.ToInt32(row["Sconfitte"]);
                r.GolFatti = r.GolFatti + Convert.ToInt32(row["GolFatti"]);
                r.GolSubiti = r.GolSubiti + Convert.ToInt32(row["GolSubiti"]);
            }
        }
    }
}

