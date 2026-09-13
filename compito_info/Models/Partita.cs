using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Partita
{
    public int IdPartita { get; set; }
    public int IdSquadraCasa { get; set; }
    public int IdSquadraOspite { get; set; }
    public string NomeSquadraCasa { get; set; } = "";
    public string NomeSquadraOspite { get; set; } = "";
    public DateTime DataPartita { get; set; }
    public int GolCasa { get; set; } = -1;
    public int GolOspite { get; set; } = -1;
    public bool Giocata { get; set; }
}