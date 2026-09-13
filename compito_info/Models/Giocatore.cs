using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Giocatore
{
   
        public int IdGiocatore;
        public string Nome { get; set; }  = "";
        public string Cognome { get; set; } = "";
        public string Ruolo { get; set; } = "";
        public int NumeroMaglia { get; set; }
        public int IdSquadra { get; set; }
        public string NomeSquadra { get; set; } = "";
    
}