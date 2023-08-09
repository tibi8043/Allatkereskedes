using System.Security.Cryptography.X509Certificates;
using TextFile;

namespace Allatkereskedes {
    internal class Program {
        static void Main(string[] args) {

            //kereskedés felpopulálása
            Kereskedes allatKer = new Kereskedes("input1.txt");

            Console.WriteLine(allatKer.getPartnerek().Count);
            //a kereskedésben lévő állatok
            Console.WriteLine("------------------------Az állatok listája-----------------------");
            foreach (Allat item in allatKer.getAllatok()) {
                Console.WriteLine(item);
            }


            //a kereskedésben llévő tranzakciók/számlák amelyek sikeresek voltak.
            Console.WriteLine();
            Console.WriteLine("------------------------A számlák listája------------------------");            
            foreach (Szamla item in allatKer.getSzamlak()) {
                Console.WriteLine(item);
            }                        
            

            //Van-e egy kereskedésben fehér színű pinty?
            Console.WriteLine("\n");
            string szin = "fehér";
            Console.WriteLine("a. Van-e egy kereskedésben "+ szin +" színű pinty? ");                        
            if (allatKer.vanEAdottSzínűPinty(szin)) {
                Console.WriteLine("Van ilyen színű pinty.");
            }
            else {
                Console.WriteLine("Nincs ilyen színű pinty.");
            }
            Console.WriteLine();            
            //hány hörcsöge van egy kereskedésnek?
            Console.WriteLine("\n");
            Console.WriteLine("b. Hány hörcsöge van egy kereskedésnek?");
            Console.WriteLine("Hörcsögök száma: "+allatKer.horcsogokSzama());


            //legnagyobb pok
            Console.WriteLine("\n");
            Console.WriteLine("c. Melyik a legnagyobb eszmei értékű tarantullája egy kereskedésnek?");
            Console.WriteLine(allatKer.legnagyobbErtekuTarantulla());

            //szerzodesek szama
            Console.WriteLine("\n");
            Partner adottPartner = new Partner("allatvevok");
            Console.WriteLine("d. Hány szerződést kötött egy adott kereskedés "+ adottPartner.getNev()+" partnerrel?");
            Console.WriteLine("Szerződések száma "+adottPartner.getNev()+" partnerrel: "+allatKer.adottPartnerrelKotottSzerzodesekSzama(adottPartner));


            //mekkora  a nyeresége? 
            Console.WriteLine("\n");
            Console.WriteLine("e. Mekkora egy kereskedésnek a nyeresége (eladási számláin szereplő árak összege mínusz a beszerzési számláin szereplő árak összege)?");
            Console.WriteLine("A(z) "+allatKer.getNev()+" nyeresége: " +allatKer.nyereseg());
            
            
            

        }      
    }
}