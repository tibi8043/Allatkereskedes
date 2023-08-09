input felepitese.txt - Így néz ki az input váza. Ezt előnyös nem megadni inputként.
input1.txt - Ez egy tökéletes input fájl, nincsenek benne lehetetlen tranzakciók.
input2.txt - Ebben már vannak olyan esetek, amelyek nem lehetségesek, ezek kivételkezeléssel vannak elhárítva
testInput.txt - Ez a teszteléshez használt txt. Ha ezt adjuk meg, akkor létrehoz egy kereskedést az adott néven 0db partnerrel, számlával és állattal.

Nincs minden kivétel lekezelve, tehát ha számlát/állatot/partnert szeretnénk hozzáadni, akkor figylejünk arra hogy változtassuk meg a partnerek/számlák számát is!
Az eladási számláknál fontos hogy az állatot csak akkor tudjuk eladni hogyha az érték, az azonosító,a szín és az életkor is megegyezik.
Ahol számot vár a program oda számot adjunk meg ahol szöveget, oda szöveget különben kezeletlen kivételbe ütközünk.
Ha 2szer írjuk be ugyan azt a partnert kapunk egy üzenetet hogy a partner nem lett hozzáadva mert már van ilyen partner, de a tranzakciók azok megtörténnek.

A tesztelés 2 részre bomlik, letesztelem az alapmetódusokat, majd utána a lekérdezéseket.
Vannak kisebb eltérések az uml-hez képest az oop javára.