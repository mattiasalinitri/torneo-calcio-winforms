using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




    public class Squadra
    {
        public int IdSquadra { get; set; }
        public string Nome { get; set; } = "";
        public string Citta { get; set; }  = "";
        public int AnnoFondazione { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }


