using System;
using TextFile;
namespace Allatkereskedes {
    [TestClass]
    public class AlapMetodusTest {

        //alap metódusok letesztelése
        [TestMethod]
        public void KereskedesInicializalasTest() {
            // a testInput1.txt egy olyan állomány amely inicializál egy kereskedést,
            //a kereskedésben még semmi nem történik, minden listája üres

            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Assert.IsNotNull(kereskedes);
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 0);
            Assert.IsTrue(kereskedes.getPartnerek().Count() == 0);
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 0);

        }
        [TestMethod]
        public void AllatBeszerzesTest() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            kereskedes.addPartner(p);
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Allat pinty = new Pinty("pinty14", "kek", 2000, true);
            Allat tarantulla = new Tarantulla("tara24", "fekete", 5000, true);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.beszallitas(p, 2000, pinty, "2022.04.24.");
            kereskedes.beszallitas(p, 5000, tarantulla, "2022.04.24.");


            //3 állatot vásároltunk, a vásárlás mindenhol sikeres volt.
            //a számlák és az állatok listának is 3 hosszúságúnak kell lennie.
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsTrue(kereskedes.getAllatok().Contains(pinty));
            Assert.IsTrue(kereskedes.getAllatok().Contains(tarantulla));

            Assert.IsTrue(kereskedes.getSzamlak().Count == 3);
        }
        [TestMethod]
        public void AllatEladasTeszt() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            kereskedes.addPartner(p);
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Allat pinty = new Pinty("pinty14", "kek", 2000, true);
            Allat tarantulla = new Tarantulla("tara24", "fekete", 5000, true);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.beszallitas(p, 2000, pinty, "2022.04.24.");
            kereskedes.beszallitas(p, 5000, tarantulla, "2022.04.24.");

            kereskedes.eladas(p, 5000, tarantulla, "2022.04.24");

            //ilyen esetben az állatok listában 2 állatnak kell lennie,
            //a számla listában pedig 4 számlának ha minden tranzakció sikeres volt

            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsTrue(kereskedes.getAllatok().Contains(pinty));
            Assert.IsFalse(kereskedes.getAllatok().Contains(tarantulla));
            Assert.IsTrue(kereskedes.getSzamlak().Count == 4);

        }
        [TestMethod]
        public void PartnerHozzaadasTest() {

            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Partner v = new Partner("Horváth Alexandra");
            Partner b = new Partner("Rumos Virág");

            kereskedes.addPartner(p);
            kereskedes.addPartner(v);
            kereskedes.addPartner(b);

            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsTrue(kereskedes.getPartnerek().Contains(v));
            Assert.IsTrue(kereskedes.getPartnerek().Contains(b));
        }
        [TestMethod]
        public void AllatTranzakciokPArtnerekenKivulTest() {
            //Ha nem partner akkor nem történhet vele tranzakció
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Partner v = new Partner("Horváth Alexandra");

            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            kereskedes.addPartner(p);


            StringWriter s = new StringWriter();
            Console.SetError(s);
            kereskedes.beszallitas(v, 4000, horcsog, "2022.04.24.");
            Console.SetError(Console.Error);
            Assert.AreEqual("Hiba \"" + v.getNev() + "\" tranzakciónál," + horcsog.getAzonosito() + " azonosítójú állattal. Csak olyan partnerekkel szabad kereskedni akik a partnerlistában benne vannak.", s.ToString().Trim());

            kereskedes.beszallitas(p, 4000, horcsog, "2022.02.12");

            s = new StringWriter();
            Console.SetError(s);
            kereskedes.eladas(v, 4000, horcsog, "2022.04.24.");
            Console.SetError(Console.Error);
            Assert.AreEqual("Hiba \"" + v.getNev() + "\" tranzakciónál," + horcsog.getAzonosito() + " azonosítójú állattal. Csak olyan partnerekkel szabad kereskedni akik a partnerlistában benne vannak.", s.ToString().Trim());

            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsFalse(kereskedes.getPartnerek().Contains(v));
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 1);
        }

        [TestMethod]
        public void OlyanAllatVeteleAmilyenMarVanTest() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Allat horcsog2 = new Horcsog("horcsog12", "barna", 4000, true);

            StringWriter s = new StringWriter();
            Console.SetError(s);

            kereskedes.addPartner(p);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.beszallitas(p, 4000, horcsog2, "2022.04.25.");

            Console.SetError(Console.Error);
            Assert.AreEqual("Hiba \"" + p.getNev() + "\" tranzakciónál," + horcsog2.getAzonosito() + " azonosítójú állattal. Ilyen állat már van, egyszerre csak egy lehet.", s.ToString().Trim());
            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog2));
            Assert.IsTrue(kereskedes.getAllatok().Count() == 1);
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 1);

        }
        [TestMethod]
        public void OlyanAllatEladasaAmilyenNincsTest() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Allat horcsog2 = new Horcsog("horcsog125", "barna", 9000, false);

            StringWriter s = new StringWriter();
            Console.SetError(s);

            kereskedes.addPartner(p);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.eladas(p, 9000, horcsog2, "2022.02.25.");

            Console.SetError(Console.Error);
            Assert.AreEqual("Hiba \"" + p.getNev() + "\" tranzakciónál," + horcsog2.getAzonosito() + " azonosítójú állattal. Ilyen állat nincs a listában ezért nem lehet eladni.", s.ToString().Trim());

            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsFalse(kereskedes.getAllatok().Contains(horcsog2));
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 1);
        }
        public void PartnerHozzaadasaAmiMarLetezikTest() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Partner v = new Partner("Horváth Alexandra");
            Partner b = new Partner("Rumos Virág");

            kereskedes.addPartner(p);
            kereskedes.addPartner(v);
            kereskedes.addPartner(b);

            StringWriter s = new StringWriter();
            Console.SetError(s);
            kereskedes.addPartner(b);
            Console.SetError(Console.Error);
            Assert.AreEqual("Ilyen partner már létezik,ezért nem került hozzáadásra", s.ToString().Trim());

            //ebben az esetben a kereskedésnek 3 partnere van
            //hiszen a b-t nem adhatjuk hozzá 2szer

            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsTrue(kereskedes.getPartnerek().Contains(v));
            Assert.IsTrue(kereskedes.getPartnerek().Contains(b));
        }
    }
    [TestClass]
    public class FeladatMetodusTest {
        //feladat metüdusok letesztelése
        [TestMethod]
        public void aFeladat() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Pinty pinty = new Pinty("pinty244", "kék", 5000, false);

            kereskedes.addPartner(p);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.beszallitas(p, 5000, pinty, "2022.04.30");

            string adottSzin = "piros";


            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            Assert.IsTrue(kereskedes.getAllatok().Contains(horcsog));
            Assert.IsTrue(kereskedes.getAllatok().Contains(pinty));
            Assert.IsFalse(kereskedes.vanEAdottSzínûPinty(adottSzin));
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 2);

            adottSzin = "kék";
            Assert.IsTrue(kereskedes.vanEAdottSzínûPinty(adottSzin));
        }
        [TestMethod]
        public void bFeladat() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Allat horcsog = new Horcsog("horcsog12", "barna", 4000, true);
            Pinty pinty = new Pinty("pinty244", "kék", 5000, false);
            Allat horcsog2 = new Horcsog("horcsog1", "fekete", 6000, false);
            Allat horcsog3 = new Horcsog("horcsog12", "barna", 4000, true);
            Allat horcsog4 = new Horcsog("horcsog125", "ezüst", 7000, true);

            StringWriter s = new StringWriter();
            Console.SetError(s);
            kereskedes.addPartner(p);
            kereskedes.beszallitas(p, 4000, horcsog, "2022.04.24.");
            kereskedes.beszallitas(p, 5000, pinty, "2022.04.30");
            kereskedes.beszallitas(p, 6000, horcsog2, "2022.04.30");
            kereskedes.beszallitas(p, 4000, horcsog3, "2022.04.30");
            kereskedes.beszallitas(p, 7000, horcsog4, "2022.04.30");

            Console.SetError(Console.Error);
            Assert.AreEqual("Hiba \"" + p.getNev() + "\" tranzakciónál," + horcsog3.getAzonosito() + " azonosítójú állattal. Ilyen állat már van, egyszerre csak egy lehet.", s.ToString().Trim());

            Assert.IsTrue(kereskedes.getPartnerek().Contains(p));
            //3 mert az egyik nem adódott hozzá mert már van olyan hörcsög 
            Assert.IsTrue(kereskedes.horcsogokSzama() == 3);
            Assert.IsTrue(kereskedes.getSzamlak().Count() == 4);
        }
        [TestMethod]
        public void cFeladat() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            kereskedes.addPartner(p);
            Allat tarantulla = new Tarantulla("tar12", "barna", 4000, true);
            Allat tarantulla2 = new Tarantulla("tar123", "barna", 5000, true);
            Allat tarantulla3 = new Tarantulla("tar13", "fekete", 8000, false);
            Pinty pinty = new Pinty("pinty244", "kék", 5000, false);

            kereskedes.beszallitas(p, 4000, tarantulla, "2022.04.04");
            kereskedes.beszallitas(p, 5000, tarantulla2, "2022.04.04");
            kereskedes.beszallitas(p, 8000, tarantulla3, "2022.04.04");
            kereskedes.beszallitas(p, 5000, pinty, "2022.05.12");

            Assert.AreEqual(kereskedes.legnagyobbErtekuTarantulla(), tarantulla3);

            kereskedes.eladas(p, 4000, tarantulla, "2022.04.05.");
            kereskedes.eladas(p, 5000, tarantulla2, "2022.04.05.");
            kereskedes.eladas(p, 8000, tarantulla3, "2022.04.05.");

            Assert.AreEqual(kereskedes.legnagyobbErtekuTarantulla(), "Nincs tarantulla az üzletben.");
        }
        [TestMethod]
        public void dFeladat() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Partner k = new Partner("Tisza Levente");
            kereskedes.addPartner(p);
            kereskedes.addPartner(k);

            Allat tarantulla = new Tarantulla("tar12", "barna", 4000, true);
            Allat tarantulla2 = new Tarantulla("tar123", "barna", 5000, true);
            Allat tarantulla3 = new Tarantulla("tar13", "fekete", 8000, false);
            Pinty pinty = new Pinty("pinty244", "kék", 5000, false);

            kereskedes.beszallitas(p, 4000, tarantulla, "2022.04.04");
            kereskedes.beszallitas(k, 5000, tarantulla2, "2022.04.04");
            kereskedes.beszallitas(p, 8000, tarantulla3, "2022.04.04");
            kereskedes.beszallitas(k, 5000, pinty, "2022.05.12");

            kereskedes.eladas(k, 4000, tarantulla, "2022.04.05.");
            kereskedes.eladas(p, 5000, tarantulla2, "2022.04.05.");
            kereskedes.eladas(p, 8000, tarantulla3, "2022.04.05.");
            Assert.IsTrue(kereskedes.adottPartnerrelKotottSzerzodesekSzama(p) == 4);
            Assert.IsTrue(kereskedes.adottPartnerrelKotottSzerzodesekSzama(k) == 3);
        }
        [TestMethod]
        public void eFeladat() {
            Kereskedes kereskedes = new Kereskedes("testInput1.txt");
            Partner p = new Partner("Kozma Ferenc");
            Partner k = new Partner("Tisza Levente");
            kereskedes.addPartner(p);
            kereskedes.addPartner(k);
            Allat tarantulla = new Tarantulla("tar12", "barna", 4000, false);
            Allat pinty = new Pinty("pinty", "barna", 8000, true);

            kereskedes.beszallitas(p, 4000, tarantulla, "2022.04.05");
            kereskedes.beszallitas(p, 8000, pinty, "2022.04.05");
            //  összesen = 12000
            //  4000*2.0 = 8000;
            //  8000*1.0 = 8000;
            //           = 16000
            //  16000-12000 = 4000 haszon.
            kereskedes.eladas(k, 4000, tarantulla, "2022.10.04");
            kereskedes.eladas(k, 8000, pinty, "2022.10.04");
            Assert.IsTrue(kereskedes.nyereseg() == 4000.0);

        }
    }
}