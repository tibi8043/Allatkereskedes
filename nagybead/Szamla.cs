using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allatkereskedes {
    public enum szamlaFajta { beszerzési, eladási }

    public class Szamla {
        private Kereskedes kereskedes;
        private Allat allat;
        private Partner partner;
        private string datum;
        private double penzosszeg;
        private szamlaFajta szamlaFajta;

        public Szamla(Kereskedes kereskedes, Allat allat, Partner partner, string datum, double penzosszeg, szamlaFajta szamlaFajta) {
            this.kereskedes = kereskedes;
            this.allat = allat;
            this.partner = partner;
            this.datum = datum;
            this.penzosszeg = penzosszeg;
            this.szamlaFajta = szamlaFajta;
        }
        //getter/setter
        public Kereskedes getKereskedes() => kereskedes;
        public Allat getAllat => allat;
        public Partner getPartner() => partner;
        public string getDatum() => datum;
        public double getPenzosszeg() => penzosszeg;
        public szamlaFajta GetSzamlaFajta() => szamlaFajta;
        public override string ToString() {
            return "Kereskedés neve: " + kereskedes.getNev() + ";\nÁllat: " + allat.ToString() + "; \nPartnerNeve: " + partner.getNev() + "; \nDátum: " + datum + "; \nPenzosszeg: " + penzosszeg + "; \n" + "Szamlafajta/Típus: " + szamlaFajta + ";" + "\n---------------------------------";

        }
    }
}
