using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




    public class RisultatoPrevisione
    {
        public double ProbabilitaCasa;
        public double ProbabilitaPareggio;
        public double ProbabilitaOspite;
        public string Commento = "";
    }

    public class PrevisioneController
    {
        private PartitaController _partitaController = new PartitaController();

        public RisultatoPrevisione Prevedi(Squadra casa, Squadra ospite)
        {
            double forzaCasa = CalcolaForza(casa.IdSquadra) + 0.3;
            double forzaOspite = CalcolaForza(ospite.IdSquadra);

            double totale = forzaCasa + forzaOspite + 1.0;
            double probCasa = forzaCasa / totale;
            double probOspite = forzaOspite / totale;
            double probPareggio = 1.0 / totale;

            string commento;
            if (probCasa > probOspite && probCasa > probPareggio)
                commento = casa.Nome + " e' la squadra favorita in base allo storico dei risultati.";
            else if (probOspite > probCasa && probOspite > probPareggio)
                commento = ospite.Nome + " parte favorita, nonostante giochi in trasferta.";
            else
                commento = "Le due squadre sono equilibrate: partita molto incerta.";

            RisultatoPrevisione esito = new RisultatoPrevisione();
            esito.ProbabilitaCasa = probCasa * 100;
            esito.ProbabilitaPareggio = probPareggio * 100;
            esito.ProbabilitaOspite = probOspite * 100;
            esito.Commento = commento;
            return esito;
        }

        private double CalcolaForza(int idSquadra)
        {
            List<Partita> storico = _partitaController.GetPartiteGiocateDiSquadra(idSquadra);
            if (storico.Count == 0)
                return 1.0;

            double punti = 0;
            double differenzaReti = 0;

            for (int i = 0; i < storico.Count; i++)
            {
                Partita p = storico[i];
                bool giocaInCasa = (p.IdSquadraCasa == idSquadra);
                int fatti;
                int subiti;

                if (giocaInCasa)
                {
                    fatti = p.GolCasa;
                    subiti = p.GolOspite;
                }
                else
                {
                    fatti = p.GolOspite;
                    subiti = p.GolCasa;
                }

                if (fatti > subiti)
                    punti = punti + 3;
                else if (fatti == subiti)
                    punti = punti + 1;

                differenzaReti = differenzaReti + (fatti - subiti);
            }

            double puntiMedi = punti / storico.Count;
            double differenzaMedia = differenzaReti / storico.Count;
            double forza = puntiMedi + differenzaMedia * 0.2;

            if (forza < 0.2)
                forza = 0.2;

            return forza;
        }
    }


