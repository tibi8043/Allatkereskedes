using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allatkereskedes {
    public abstract class Allat {
        private string azonosito;
        private string szin;
        private double ertek;
        private bool fiatal;
        private double fiatalSzorzo;
        private double felnottSzorzo;


        protected Allat(string azonosito, string szin, double ertek, bool fiatal, double fiatalSzorzo, double felnottSzorzo) {
            this.azonosito = azonosito;
            this.szin = szin;
            this.ertek = ertek;
            this.fiatal = fiatal;
            this.fiatalSzorzo = fiatalSzorzo;
            this.felnottSzorzo = felnottSzorzo;

        }
        //getter setter

        public double getErtek() => ertek;
        public bool getFiatal() => fiatal;
        public string getSzin() => szin;
        public string getAzonosito() => azonosito;
        public double getFiatalSzorzo() => fiatalSzorzo;
        public double getFelnottSzorzo() => felnottSzorzo;

        //metodusok
        public double aktualisAr() {
            if (fiatal) {
                return ertek * fiatalSzorzo;
            }
            else {
                return ertek * felnottSzorzo;
            }
        }


        public override string ToString() {
            return "Azonosító:" + this.azonosito + " Szín: " + this.szin + " Érték: " + this.ertek + " Fiatal: " + this.fiatal;
        }
        public virtual bool isHorcsog() => false;
        public virtual bool isPinty() => false;
        public virtual bool isTarantulla() => false;

        public override bool Equals(object? obj) {
            return obj is Allat allat &&
                   azonosito == allat.azonosito &&
                   ertek == allat.ertek &&
                   szin == allat.szin &&
                   fiatal == allat.fiatal;
        }

        public override int GetHashCode() {
            return HashCode.Combine(azonosito, szin, ertek, fiatal);
        }
    }

    public class Horcsog : Allat {
        public Horcsog(string azonosito, string szin, double ertek, bool fiatal) : base(azonosito, szin, ertek, fiatal, 2.0, 1.0) { }

        public override bool isHorcsog() => true;
        public override string ToString() => "HÖRCSÖG: " + base.ToString();

    }
    public class Pinty : Allat {
        public Pinty(string azonosito, string szin, double ertek, bool fiatal) : base(azonosito, szin, ertek, fiatal, 1.0, 3.0) { }

        public override bool isPinty() => true;
        public override string ToString() => "PINTY: " + base.ToString();
    }
    public class Tarantulla : Allat {
        public Tarantulla(string azonosito, string szin, double ertek, bool fiatal) : base(azonosito, szin, ertek, fiatal, 3.0, 2.0) { }

        public override bool isTarantulla() => true;
        public override string ToString() => "TARANTULLA: " + base.ToString();
    }
}
