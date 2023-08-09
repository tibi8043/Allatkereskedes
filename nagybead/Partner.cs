using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Allatkereskedes {
    public class Partner {
        
        private string nev { get; set; }
        
        public Partner(string nev) {
            this.nev = nev;
        }

        public string getNev() => this.nev;
        public override bool Equals(object? obj) {
            return obj is Partner partner &&
                   nev == partner.nev;
        }

        public override int GetHashCode() {
            return HashCode.Combine(nev);
        }



    }

}
