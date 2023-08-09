using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Allatkereskedes {
    public class Kereskedes {
        private string nev;
        private List<Allat> allatok = new List<Allat>();
        private List<Partner> partnerek = new List<Partner>();
        private List<Szamla> szamlak = new List<Szamla>();

        public string getNev() => nev;
        public List<Allat> getAllatok() => allatok;
        public List<Szamla> getSzamlak() => szamlak;
        public List<Partner> getPartnerek() => partnerek;

        public Kereskedes(string input) {
            TextFileReader reader = new TextFileReader(input);
            this.nev = reader.ReadLine();
            int partnerekSzama = 0;
            int.TryParse(reader.ReadLine(), out partnerekSzama);

            for (int i = 0; i < partnerekSzama; i++) {
                string tmp = reader.ReadLine();
                string[] partnerDetails = {tmp.Substring(0, tmp.LastIndexOf(" ")),tmp.Substring(tmp.LastIndexOf(" "))};
                Partner tempPartner = new Partner(partnerDetails[0]);
                addPartner(tempPartner);
                int tranzakciokSzama = 0;
                int.TryParse(partnerDetails[1], out tranzakciokSzama);

                for (int j = 0; j < tranzakciokSzama; j++) {
                    string[] tranzakcioDetails = reader.ReadLine().Split(";");
                    string allatFajta = tranzakcioDetails[0];
                    string tempId = tranzakcioDetails[1];
                    string tempSzin = tranzakcioDetails[2];
                    double tempErtek = 0;
                    double.TryParse(tranzakcioDetails[3], out tempErtek);
                    bool tempFiatal = false;
                    if (tranzakcioDetails[4].Equals("fiatal")) {
                        tempFiatal = true;
                    }
                    string tempDatum = tranzakcioDetails[5];
                    bool tempBeszerzesi = tranzakcioDetails[6].Equals("beszerzési");
                    Allat? tempAllat = null;

                    switch (allatFajta) {
                        case "Hörcsög":
                            tempAllat = new Horcsog(tempId, tempSzin, tempErtek, tempFiatal);
                            break;
                        case "Pinty":
                            tempAllat = new Pinty(tempId, tempSzin, tempErtek, tempFiatal);
                            break;
                        case "Tarantulla":
                            tempAllat = new Tarantulla(tempId, tempSzin, tempErtek, tempFiatal);
                            break;
                    }
                    if (tempBeszerzesi) {
                        beszallitas(tempPartner, tempErtek, tempAllat, tempDatum);
                    }
                    else {
                        if (tempAllat.getFiatal()) {
                            eladas(tempPartner, tempAllat.aktualisAr(), tempAllat, tempDatum);
                        }
                        else {
                            eladas(tempPartner, tempAllat.aktualisAr(), tempAllat, tempDatum);
                        }
                    }
                }
            }
        }
        public void addPartner(Partner p) {
            try {
                if (!partnerek.Contains(p)) {
                    this.partnerek.Add(p);
                }
                else {
                    throw new ArgumentException("Ilyen partner már létezik,ezért nem került hozzáadásra");
                }
            }
            catch (ArgumentException exc) {
                Console.Error.WriteLine(exc.Message);
            }

        }
        public void beszallitas(Partner p, double ar, Allat a, string datum) {
            try {
                if (partnerek.Contains(p) && !allatok.Contains(a)) {
                    allatok.Add(a);
                    szamlak.Add(new Szamla(this, a, p, datum, ar, szamlaFajta.beszerzési));
                }
                else if (!partnerek.Contains(p)) {
                    throw new ArgumentException("Hiba \"" + p.getNev() + "\" tranzakciónál," + a.getAzonosito() + " azonosítójú állattal. Csak olyan partnerekkel szabad kereskedni akik a partnerlistában benne vannak.");
                }
                else if (allatok.Contains(a)) {
                    throw new ArgumentException("Hiba \"" + p.getNev() + "\" tranzakciónál," + a.getAzonosito() + " azonosítójú állattal. Ilyen állat már van, egyszerre csak egy lehet.");
                }
            }
            catch (ArgumentException exc) {
                Console.Error.WriteLine(exc.Message);
            }

        }
        public void eladas(Partner p, double ar, Allat a, string datum) {
            try {
                if (!partnerek.Contains(p)) {
                    throw new ArgumentException("Hiba \"" + p.getNev() + "\" tranzakciónál," + a.getAzonosito() + " azonosítójú állattal. Csak olyan partnerekkel szabad kereskedni akik a partnerlistában benne vannak.");
                }
                if (allatok.Contains(a) == false) {
                    throw new ArgumentException("Hiba \"" + p.getNev() + "\" tranzakciónál," + a.getAzonosito() + " azonosítójú állattal. Ilyen állat nincs a listában ezért nem lehet eladni.");
                }
                if (partnerek.Contains(p) && allatok.Contains(a)) {
                    allatok.Remove(a);
                    szamlak.Add(new Szamla(this, a, p, datum, a.aktualisAr(), szamlaFajta.eladási));
                }
            }
            catch (ArgumentException exc) {
                Console.Error.WriteLine(exc.Message);
            }
        }       

        public bool vanEAdottSzínűPinty(string szin) {
            bool van = false;
            foreach (Allat elem in allatok) {
                if (elem.isPinty() && elem.getSzin().Equals(szin)) {
                    van = true;
                    break;
                }
            }
            return van;
        }
        public int horcsogokSzama() {
            int horcsogokSzama = 0;
            foreach (Allat elem in allatok) {
                if (elem.isHorcsog()) {
                    horcsogokSzama++;
                }
            }
            return horcsogokSzama;
        }
        public dynamic legnagyobbErtekuTarantulla() {
            double maxErtek = 0;
            Allat? tarantulla = null;
            for (int i = 0; i < allatok.Count; i++) {
                if (allatok.ElementAt(i).isTarantulla()) {
                    if (allatok.ElementAt(i).aktualisAr() > maxErtek) {
                        maxErtek = allatok.ElementAt(i).aktualisAr();
                        tarantulla = allatok.ElementAt(i);
                    }
                }
            }
            if (tarantulla is null) {
                return "Nincs tarantulla az üzletben.";
            }
            else {
                return tarantulla;
            }
        }
        public int adottPartnerrelKotottSzerzodesekSzama(Partner p) {
            int szamlalo = 0;
            foreach (Szamla item in szamlak) {
                if (item.getPartner().getNev().Equals(p.getNev())) {
                    szamlalo++;
                }
            }
            return szamlalo;
        }
        public double nyereseg() {
            double bevetel = 0;
            double kiadas = 0;
            foreach (Szamla item in szamlak) {
                if (item.GetSzamlaFajta() == szamlaFajta.eladási) {
                    bevetel += item.getPenzosszeg();
                }
                else {
                    kiadas += item.getPenzosszeg();
                }

            }

            return bevetel - kiadas;
        }
    }
}
