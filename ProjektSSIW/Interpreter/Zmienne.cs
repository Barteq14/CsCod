using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    public class Zmienne
    {

        Składnia skladnia = new Składnia();
        //listy z informacjami o zmiennej 
        public static List<dynamic> typZmiennej = new List<dynamic>(); //muszą być statyczne i mieć get set żeby można było w innych 
        public static List<dynamic> nazwaZmiennej = new List<dynamic>();
        public static List<dynamic> wartoscZmiennej = new List<dynamic>();
        public static List<String> konsola { get; set; } = new List<String>(); // lista dla konsoli
        public static List<String> bledy { get; set; } = new List<string>(); // lista błędów jakie mogą wyskoczyć

        public static string znakiArytmetyczne = @"+-*/";

        public static int IntLength = 5;
        public static int DoubleLength = 7;
        public static int StringLength = 6;
        public static int BoolLength = 4;

        //public static List<String> konsola { get; set; } = new List<String>(); // lista dla konsoli

        //przechowywane wyniki operacji
        public static int wynikBezNawiasow;
        public static int wynikWnawiasie;
        public static int wynikPrzedNawiasem;
        public static int wynikPoNawiasie;
        public static int wynikPrzed_i_wNawiasie;
        public static int wynikKoncowy;
        string message;
        //listy z indeksami
        List<int> IndeksyINT = new List<int>();
        List<int> IndeksyDOUBLE = new List<int>();
        List<int> IndeksyFLOAT = new List<int>();
        List<int> IndeksySTRING = new List<int>();
        List<int> IndeksyBOOLEAN = new List<int>();


        public void TomaszowyInt(string nazwaZmiennej2, string ciag, int linijka)
        {
            //konsola.Add(nazwaZmiennej2 + " = " + ciag + " \t linijka " + linijka);
            int wynik;

            int nawiasOtwarty = 0;
            int nawiasZamkniety = 0;
            int pom4 = 0;
            string ciag3 = "";
            

            if (czyString2(nazwaZmiennej2) == true) //jeżeli nazwaZmiennej to string
            {
                int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej2);
                if (index2 >= 0) //jeżeli nie ma takiej zmiennej
                {
                    bledy.Add(linijka + ": Istnieje zmienna o nazwie " + nazwaZmiennej2);
                }
                else //jeżeli nie ma takiej zmiennej to robi dalej
                {
                    string[] test = Regex.Split(ciag.Remove(ciag.Length - 1), "(?<=[()\\-+*/])|(?=[()\\-+*/])"); // rozdziela na tablicę stringów cały ciąg
                    if ((test[0] == "" || test[0] == null || test[0] == " ") && test[1] == "(") //jak rozdziela tablicę i na samym początku jest nawias otwierający to test[0] = " " więc usuwamy ten pierwszy element
                    {
                        test = test.Where((v, i) => i != 0).ToArray();
                    }
                    foreach (var match in test) //leci po tablicy stringów test
                    {
                        bool flaga = false;
                        if (pom4 != 0) // jeżeli nie będziemy sprawdzać test[0] to
                        {
                            if (test[pom4] == "(") // jeżeli znak "("
                            {
                                if ((test[pom4 + 1] == "+" || test[pom4 + 1] == "*" || test[pom4 + 1] == "/" || test[pom4 + 1] == ";")) // jeżeli po znaku ( jest znak (nie minus) to błąd
                                {
                                    bledy.Add(linijka + ": znak " + test[pom4 + 1] + " po nawiasie rozpoczynającym");
                                }
                                if (pom4 != 0 && test[pom4] == "(") // jeżeli przed ( nie ma znaku operacji i nie sprawdzamy pierwszego test[0] to blad
                                {
                                    if (test[0] != "(" && !(test[pom4 - 1] == "+" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/"))
                                    {
                                        bledy.Add(linijka + ": brak znaku przed nawiasem");
                                    }
                                }
                                nawiasOtwarty++; // do sprawdzenia czy dobra ilosc nawiasow
                            }
                            if (test[pom4] == ")") //jezeli jest nawias zamykajacy
                            {
                                if ((test[pom4 - 1] == "+" || test[pom4 - 1] == "-" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/" || test[pom4 - 1] == ";")) // jezeli przed ) jest jakis znak operacji to blad
                                {
                                    bledy.Add(linijka + ": znak " + test[pom4 + 1] + " przed nawiasem zamykającym");
                                }
                                nawiasZamkniety++;// do sprawdzenia czy dobra ilosc nawiasow
                            }
                            else if (test[pom4] == ";") // jezeli koniec to
                            {
                                if ((test[pom4 - 1] == "+" || test[pom4 - 1] == "-" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/"))//jezeli koniec a przed koncem jest jakis znak operacji to blad
                                {
                                    bledy.Add(linijka + ": Na końcu znak " + test[pom4]);
                                }
                            }
                            if (test[pom4 - 1] == "/" && test[pom4] == "0") // blad z dzieleniem przez zero
                            {
                                bledy.Add(linijka + ": nie można dzialić przez zero");
                            }
                        }
                        else //tutaj będzie sprawdzanie pierwszego elementu z tablicy test
                        {
                            if (test[pom4] == "(")
                            {
                                nawiasOtwarty++; // do sprawdzenia czy dobra ilosc nawiasow
                            }
                            else if ((test[pom4] == "+" || test[pom4] == "-" || test[pom4] == "*" || test[pom4] == "/" || test[pom4] == ";"))// jeżeli na samym początku znak operacji to blad
                            {
                                bledy.Add(linijka + ": " + test[pom4] + " na początku działania");
                            }
                        }
                        if (czyInt(match) == false) // element test[pom4] to nie int to
                        {
                            if ((match == "/" || match == "*" || match == "+" || match == "-" || match == "(" || match == ")" || match == ";"))
                            {

                            }
                            else // jeżeli to nie znak operacji to
                            {
                                int index = Zmienne.nazwaZmiennej.FindIndex(c => c == match);
                                if (index < 0) //jeżeli nie ma takiej zmiennej
                                {
                                    try// tutaj będziemy sprawdzać czy liczba jest doublem
                                    {
                                        string[] subsPom = match.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                        if (subsPom.Count() == 2)
                                        {
                                            bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                            bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                            if (isNumeric1 == true && isNumeric2 == true)
                                            {
                                                bledy.Add(linijka + ": liczba " + match + " nie jest typu knife(int)");
                                                flaga = true;
                                            }
                                        }
                                    }
                                    catch //jeżeli
                                    {
                                        //bledy.Add(linijka + ": blad??? " + match);
                                    }
                                    if (flaga == false)
                                    {
                                        bledy.Add(linijka +": nie istnieje zmienna o nazwie " +match);
                                    }
                                }
                                else // jeżeli jest zmienna to 
                                {
                                    if (Zmienne.typZmiennej[index] == "knife") // jezeli jest taka zmienna to sprawdza czy to jest int
                                    {
                                        test[pom4] = (Zmienne.wartoscZmiennej[index]) + "";
                                        //bledy.Add("debug");
                                    }
                                    else //jezeli to nie int to blad
                                    {
                                        bledy.Add(linijka + ": zmienna o nazwie " + match + " to nie knife(int)");
                                    }
                                }
                            }
                        }
                        //Zmienne.konsola.Add(test[pom4]);
                        ciag3 = ciag3 + test[pom4]; //dodaje ciag po prawej stronie wraz z przetworzonymi zmiennymi
                        pom4++; //pomocnicza
                    }
                    if (nawiasOtwarty != nawiasZamkniety) // jeżeli ilość nawiasów się nie zgadza
                    {
                        bledy.Add(linijka + ": źle zrobione nawiasy");
                    }
                    //Zmienne.konsola.Add(ciag3);
                    if (Zmienne.bledy.Count > 0) // jeżeli jest jakiś błąd to
                    {
                        string item = bledy[bledy.Count - 1]; 
                        string item2 = item.Substring(0, 1); 
                        string item3 = linijka + "";
                        if (item2 != item3) //sprawdza czy ostatni blad jest z linijki w której jest działanie wykonywane jeżeli nie to
                        {
                            try //są tutaj try oraz catch bo jak w działaniu jest samo - + to wystarczy (int)dt.Compute a jeżeli jest * / to trzeba (double)dt.Compute i później zmienić na inta
                            {
                                DataTable dt = new DataTable();
                                double answer = (double)dt.Compute(ciag3, "");
                                wynik = Convert.ToInt32(answer);
                                Zmienne.typZmiennej.Add("knife");
                                Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                Zmienne.wartoscZmiennej.Add(wynik);
                            }
                            catch
                            {
                                try
                                {
                                    DataTable dt = new DataTable();
                                    int answer = (int)dt.Compute(ciag3, "");
                                    wynik = Convert.ToInt32(answer);
                                    Zmienne.typZmiennej.Add("knife");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                    Zmienne.wartoscZmiennej.Add(wynik);
                                }
                                catch
                                {
                                    bledy.Add("blad ?");
                                }
                                //bledy.Add(linijka + ": błąd przy obliczaniu ddd");
                            }
                        }
                    }
                    else // jeżeli nie ma błędów z tej linijki to odrazu przechodzi do dodawania do list
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            double answer = (double)dt.Compute(ciag3, "");
                            wynik = Convert.ToInt32(answer);
                            Zmienne.typZmiennej.Add("knife");
                            Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                            Zmienne.wartoscZmiennej.Add(wynik);
                        }
                        catch
                        {
                            try
                            {
                                DataTable dt = new DataTable();
                                int answer = (int)dt.Compute(ciag3, "");
                                wynik = Convert.ToInt32(answer);
                                Zmienne.typZmiennej.Add("knife");
                                Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                Zmienne.wartoscZmiennej.Add(wynik);
                            }
                            catch
                            {
                                bledy.Add("blad ??");
                            }
                            //bledy.Add(linijka + ": błąd przy obliczaniu zzz");
                        }
                    }
                }
            }
            else// jeżeli nazwaZmiennej to nie string
            {
                bledy.Add(linijka + ": Zła nazwa zmiennej " + nazwaZmiennej2);
            }
        }





        public bool InterpretujInt(string[] lines, int indeks)
        {
            string msg = "";
            string linia = lines[indeks];
            char[] liniaChar = linia.ToCharArray();
            string[] pomINT = lines[indeks].Split(' '); //dziele wyrazy na podstawie spacji
            List<int> listaNawiasowOtw = new List<int>();
            List<int> listaNawiasowZam = new List<int>();
            List<char> znakiwNawiasie = new List<char>();

            int indeksRownasie = 0;
            wynikBezNawiasow = 0;
            wynikWnawiasie = 0;
            wynikPoNawiasie = 0;
            wynikPrzed_i_wNawiasie = 0;
            wynikKoncowy = 0;

            //knife liczba = 2;
            //knife cos = liczba + 1;

            for (int t = 0; t < liniaChar.Length; t++)
            {
                if (liniaChar[t] == '=')//sprawdzam na ktorym indeksie jest '='
                {
                    indeksRownasie = t;
                }
            }
            //dzielenie linii na 2 czesci (deklaracja | inicjalizacja)
            int dlugoscLewaStrona = 0 + indeksRownasie + 1;
            string lewaStrona = lines[indeks].Substring(0, dlugoscLewaStrona);//tablica lewa ze znakiem równa się

            int dlugoscPrawaStrona = linia.Length - indeksRownasie - 1;
            string prawaStrona = lines[indeks].Substring(indeksRownasie + 1, dlugoscPrawaStrona);//tablica prawa

            string[] lewa = lewaStrona.Split();

            for (int i = 0; i < lewa.Length; i++)
            {
                if (lewa[i] == "knife" && czyString(lewa[i + 1]) == true)
                {
                    typZmiennej.Add("int");
                    nazwaZmiennej.Add(lewa[i + 1]);
                    break;
                }
            }

            char[] znakiPrawaStrona = prawaStrona.ToCharArray();
            string[] prawaZeZmienna = prawaStrona.Split('+', '-', '*', '/');
            List<char> pozyskane2 = new List<char>();
            char[] pozyskane = new char[100];

            if (!znakiPrawaStrona.Contains('(') && !znakiPrawaStrona.Contains(')'))
            {

                for (int o = 0; o < znakiPrawaStrona.Length; o++)
                {
                    
                    if(czyInt2(znakiPrawaStrona[o]) == false && znakiPrawaStrona[o] != '+' && znakiPrawaStrona[o] != '-'  && znakiPrawaStrona[o] != '*' && znakiPrawaStrona[o] != '/')
                    {
                        pozyskane2.Add(znakiPrawaStrona[o]);
                    }
                    if (czyInt2(znakiPrawaStrona[o]) == true)//jesli pierwszy element to liczba to zapisuje pod wynik
                    {
                        wynikBezNawiasow = int.Parse(znakiPrawaStrona[o].ToString());
                        break;
                    }
                    else if (znakiPrawaStrona[o] == '-')
                    {
                        wynikBezNawiasow = 0;
                        break;
                    }
                    else
                    {
                        wynikBezNawiasow = 0;
                    }
                }

                string napis = "";
                for(int i = 0; i < pozyskane2.Count; i++)
                {
                    napis = napis + pozyskane2[i];
                }

                //if(napis.Contains(""))

                for (int q = 0; q < znakiPrawaStrona.Length; q++)
                {
                    if (znakiPrawaStrona[q] == ';')
                    {
                        msg = "Napotkałem srednik!";
                        break;
                    }
                    else
                    {
                        if (znakiPrawaStrona[q] == '+')
                        {
                            if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow + int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                        if (znakiPrawaStrona[q] == '-')
                        {
                            if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow - int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                        if (znakiPrawaStrona[q] == '*')
                        {
                            if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow * int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                        if (znakiPrawaStrona[q] == '/')
                        {
                            if (czyInt2(znakiPrawaStrona[q + 1]) == true && znakiPrawaStrona[q+1] != 0)
                            {
                                wynikBezNawiasow = wynikBezNawiasow / int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                            else
                            {
                                msg = "Nie wolno dzielic przez 0";
                                return false;
                            }
                        }
                    }


                }
                wartoscZmiennej.Add(wynikBezNawiasow);
            }
            else if (znakiPrawaStrona.Contains('(') && znakiPrawaStrona.Contains(')'))
            {

                for (int o = 0; o < znakiPrawaStrona.Length; o++)
                {
                    if (znakiPrawaStrona[o] == '(')
                    {
                        listaNawiasowOtw.Add(o);//zapisuje indeks nawiasu
                    }
                    if (znakiPrawaStrona[o] == ')')
                    {
                        listaNawiasowZam.Add(o);
                    }
                }

                if (listaNawiasowOtw.Count == listaNawiasowZam.Count)//sprawdzam czy nawiasów otwierających jest tyle samo co zamykających 
                {
                    for (int i = 0; i < znakiPrawaStrona.Length; i++)
                    {
                        foreach (var item in listaNawiasowOtw)
                        {
                            foreach (var item2 in listaNawiasowZam)
                            {
                                if (i > item && i < item2)
                                {
                                    znakiwNawiasie.Add(znakiPrawaStrona[i]);
                                }
                            }
                        }
                    }
                    //sprawdzam 1 znak
                    for (int o = 0; o < znakiwNawiasie.Count; o++)
                    {
                        if (czyInt2(znakiwNawiasie[o]) == true)//jesli pierwszy element to liczba to zapisuje pod wynik
                        {
                            wynikWnawiasie = int.Parse(znakiwNawiasie[o].ToString());
                            break;
                        }
                        else if (znakiwNawiasie[o] == '-')
                        {
                            wynikWnawiasie = 0;
                            break;
                        }
                    }


                    //operacje na liczbach w nawiasie
                    for (int q = 0; q < znakiwNawiasie.Count; q++)
                    {
                        if (znakiwNawiasie[q] == '+')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikWnawiasie = wynikWnawiasie + int.Parse(znakiwNawiasie[q + 1].ToString());
                            }
                        }
                        if (znakiwNawiasie[q] == '-')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikWnawiasie = wynikWnawiasie - int.Parse(znakiwNawiasie[q + 1].ToString());
                            }
                        }
                        if (znakiwNawiasie[q] == '*')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikWnawiasie = wynikWnawiasie * int.Parse(znakiwNawiasie[q + 1].ToString());
                            }
                        }
                        if (znakiwNawiasie[q] == '/')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikWnawiasie = wynikWnawiasie / int.Parse(znakiwNawiasie[q + 1].ToString());
                            }
                        }
                    }
                    //jesli mamy cos przed nawiasem
                    for (int q = 0; q < znakiPrawaStrona.Length; q++)
                    {
                        foreach (var item in listaNawiasowOtw)
                        {
                            foreach (var item2 in listaNawiasowZam)
                            {
                                if (q < item)
                                {
                                    if (wynikPrzedNawiasem == 0)
                                    {
                                        if (czyInt2(znakiPrawaStrona[q]) == true)//jesli pierwszy element to liczba to zapisuje pod wynik
                                        {
                                            wynikPrzedNawiasem = int.Parse(znakiPrawaStrona[q].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '+')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPrzedNawiasem = wynikPrzedNawiasem + int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '-')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPrzedNawiasem = wynikPrzedNawiasem - int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '*')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPrzedNawiasem = wynikPrzedNawiasem * int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '/')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPrzedNawiasem = wynikPrzedNawiasem / int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                }
                                else if(q > item2 && znakiPrawaStrona[q] == ';' && wynikPoNawiasie == 0)//jezeli nie ma nic po nawiasie , to obliczam wynik koncowy
                                {
                                    //obliczanie wyniku przed i w nawiasie
                                    if (znakiPrawaStrona[item - 1] == '+')
                                    {
                                        wynikPrzed_i_wNawiasie = wynikPrzedNawiasem + wynikWnawiasie;
                                        wartoscZmiennej.Add(wynikPrzed_i_wNawiasie);
                                        return true;
                                    }
                                    if (znakiPrawaStrona[item - 1] == '-')
                                    {
                                        wynikPrzed_i_wNawiasie = wynikPrzedNawiasem - wynikWnawiasie;
                                        wartoscZmiennej.Add(wynikPrzed_i_wNawiasie);
                                        return true;
                                    }
                                    if (znakiPrawaStrona[item - 1] == '*')
                                    {
                                        wynikPrzed_i_wNawiasie = wynikPrzedNawiasem * wynikWnawiasie;
                                        wartoscZmiennej.Add(wynikPrzed_i_wNawiasie);
                                        return true;
                                    }
                                    if (znakiPrawaStrona[item - 1] == '/')
                                    {
                                        wynikPrzed_i_wNawiasie = wynikPrzedNawiasem / wynikWnawiasie;
                                        wartoscZmiennej.Add(wynikPrzed_i_wNawiasie);
                                        return true;
                                    } 
                                }
                                else if (q > item2 && znakiPrawaStrona[q] != ';')//jezeli jest cos po nawiasie
                                {
                                    if (znakiPrawaStrona[q] == '+')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q]) == true)
                                        {
                                            wynikPoNawiasie = wynikPoNawiasie + int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '-')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPoNawiasie = wynikPoNawiasie - int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '*')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            if(wynikPoNawiasie == 0)
                                            {
                                                wynikPoNawiasie = int.Parse(znakiPrawaStrona[q + 1].ToString());
                                            }
                                            else
                                            {
                                                wynikPoNawiasie = wynikPoNawiasie * int.Parse(znakiPrawaStrona[q + 1].ToString());
                                            } 
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '/')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            if (wynikPoNawiasie == 0)
                                            {
                                                wynikPoNawiasie = int.Parse(znakiPrawaStrona[q + 1].ToString());
                                            }
                                            else
                                            {
                                                wynikPoNawiasie = wynikPoNawiasie / int.Parse(znakiPrawaStrona[q + 1].ToString());
                                            }
                                        }
                                    }
                                }
                                else if (znakiPrawaStrona[q] == ';')//jesli koniec to obliczam calosc 
                                {
                                    if (wynikPrzedNawiasem != 0 && wynikWnawiasie != 0)//jesli przed nawiasem  i w nawiasie sa liczby to obliczam to razem
                                    {
                                        if (znakiPrawaStrona[item - 1] == '+')
                                        {
                                            wynikPrzed_i_wNawiasie = wynikPrzedNawiasem + wynikWnawiasie;
                                        }
                                        if (znakiPrawaStrona[item - 1] == '-')
                                        { 
                                            wynikPrzed_i_wNawiasie = wynikPrzedNawiasem - wynikWnawiasie;
                                        }
                                        if (znakiPrawaStrona[item - 1] == '*')
                                        { 
                                            wynikPrzed_i_wNawiasie = wynikPrzedNawiasem * wynikWnawiasie;
                                        }
                                        if (znakiPrawaStrona[item - 1] == '/')
                                        {
                                            wynikPrzed_i_wNawiasie = wynikPrzedNawiasem / wynikWnawiasie; 
                                        }
                                    }

                                    if (znakiPrawaStrona[item2 + 1] == '+')
                                    {
                                        if(wynikPrzedNawiasem == 0)
                                        {
                                            wynikKoncowy = wynikWnawiasie + wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                        else
                                        {
                                            wynikKoncowy = wynikPrzed_i_wNawiasie + wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                    }
                                    if (znakiPrawaStrona[item2 + 1] == '-')//jesli jest -1 na koncu to mi do wyniku po nawiasie wpisuje -1 a nie 1 
                                    {
                                       
                                        if (wynikPrzedNawiasem == 0)
                                        {
                                            wynikKoncowy = wynikWnawiasie - wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                        else
                                        {
                                            wynikKoncowy = wynikPrzed_i_wNawiasie - wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                    }
                                    if (znakiPrawaStrona[item2 + 1] == '*')
                                    {
                                        if (wynikPrzedNawiasem == 0)
                                        {
                                            wynikKoncowy = wynikWnawiasie * wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                        else
                                        {
                                            wynikKoncowy = wynikPrzed_i_wNawiasie * wynikPoNawiasie; // czyli jesli mam przed nawiasem cos i po naw to inaczej musze liczyc 
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                    }
                                    if (znakiPrawaStrona[item2 + 1] == '/')
                                    {
                                        if (wynikPrzedNawiasem == 0)
                                        {
                                            wynikKoncowy = wynikWnawiasie / wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                        else
                                        {
                                            wynikKoncowy = wynikPrzed_i_wNawiasie / wynikPoNawiasie;
                                            wartoscZmiennej.Add(wynikKoncowy);
                                            return true;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }

                            }
                        }

                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }


            int a = 9;
            return false;

        }


        public void InterpretujDouble()
        {

        }

        public void InterpretujFloat()
        {

        }

        public bool InterpretujString(string[] lines, int indeks)
        {
            int indeksRownasie = 0;
            string[] pomSTRING = lines[indeks].Split();

            int pierwszaLiczba = 0;
            string msg = "";
            string wynikStringa = "";
            int indexZmiennej = 0;

            string linia = lines[indeks];
            char[] liniaChar = linia.ToCharArray();
            List<int> listaCudzyslowow = new List<int>();


            for (int t = 0; t < liniaChar.Length; t++)
            {
                if (liniaChar[t] == '=')//sprawdzam na ktorym indeksie jest '='
                {
                    indeksRownasie = t;
                    break;
                }
            }



            //dzielenie linii na 2 czesci (deklaracja | inicjalizacja)
            int dlugoscLewaStrona = 0 + indeksRownasie + 1;
            string lewaStrona = lines[indeks].Substring(0, dlugoscLewaStrona);//tablica lewa ze znakiem równa się

            int dlugoscPrawaStrona = linia.Length - indeksRownasie - 1;
            string prawaStrona = lines[indeks].Substring(indeksRownasie + 1, dlugoscPrawaStrona);//tablica prawa



            string[] lewa = lewaStrona.Split();

            char[] prawaChar = prawaStrona.ToCharArray();
            string[] prawa = prawaStrona.Split(' ');
            string[] prawa2 = prawaStrona.Split(';');

            for (int i = 0; i < lewa.Length; i++)
            {
                if (lewa[i] == "defuse" && czyString(lewa[i + 1]) == true)
                {
                    if (nazwaZmiennej.Contains(lewa[i + 1]))
                    {
                        bledy.Add("Linia: " + linia + " Istnieje już taka zmienna!");
                    }
                    else
                    {
                        typZmiennej.Add("string");
                        nazwaZmiennej.Add(lewa[i + 1]);
                        break;
                    }
                }
            }


            for (int i = 0; i < prawaChar.Length; i++)
            {
                if (prawaChar[i] == '"')
                {
                    listaCudzyslowow.Add(i);
                }
            }

            if (listaCudzyslowow.Count % 2 == 0)
            {
                for(int p = 0; p < listaCudzyslowow.Count; p++)
                {
                    for (int i = listaCudzyslowow[p]+1; i < prawaChar.Length; i++)
                    {
                        if(prawaChar[i] == ';')
                        {
                            msg = "napotkalem srednik!";
                            wartoscZmiennej.Add(wynikStringa);
                            return true;
                        }
                        else if(prawaChar[i] == '"')
                        {
                            if(prawaChar[i] == '+' || prawaChar[i+1] == '+')
                            {
                                wynikStringa = wynikStringa + " ";
                            }
                        }else if (prawaChar[i] == '+' && prawaChar[i+1] != '"')//tutaj jeszcze poprawic to narazie działa tylko jesli podamy wartosc stringa w cudzyslowach
                        {
                            indexZmiennej = i;   
                        }
                        else
                        {
                            wynikStringa = wynikStringa + prawaChar[i];
                        }
                    }
                }
            }
            else
            {
                bledy.Add("Linijka: " + indeks + "Nie poprawna ilośc cudzyslowow!");
                return false;
            }
            /*
                if (listaCudzyslowow.Count % 2 == 0)
            {
                for (int i = 0; i < prawaChar.Length; i++)
                {
                    for (int j = 0; j < listaCudzyslowow.Count; j++)
                    {
                        if(i >= listaCudzyslowow[j])
                        {
                            wynikStringa = wynikStringa + prawaChar[i];
                        }
                    }
                }
            }*/
        

            return false;
        }

        public bool InterpretujBoolean(string[] lines, int indeks)
        {
            int indeksRownasie = 0;
            string[] pomBOOL = lines[indeks].Split();
            bool wynikBool = true;
            int pierwszaLiczba = 0;
            string msg = "";


            string linia = lines[indeks];
            char[] liniaChar = linia.ToCharArray();


            for (int t = 0; t < liniaChar.Length; t++)
            {
                if (liniaChar[t] == '=')//sprawdzam na ktorym indeksie jest '='
                {
                    indeksRownasie = t;
                    break;
                }
            }



            //dzielenie linii na 2 czesci (deklaracja | inicjalizacja)
            int dlugoscLewaStrona = 0 + indeksRownasie + 1;
            string lewaStrona = lines[indeks].Substring(0, dlugoscLewaStrona);//tablica lewa ze znakiem równa się

            int dlugoscPrawaStrona = linia.Length - indeksRownasie - 1;
            string prawaStrona = lines[indeks].Substring(indeksRownasie + 1, dlugoscPrawaStrona);//tablica prawa


    
            string[] lewa = lewaStrona.Split();

            char[] prawaChar = prawaStrona.ToCharArray();
            string[] prawa = prawaStrona.Split(' ', ';');
            string[] prawa2 = prawaStrona.Split(';');

            for (int i = 0; i < lewa.Length; i++)
            {
                if (lewa[i] == "zeus" && czyString(lewa[i + 1]) == true)
                {
                    typZmiennej.Add("bool");
                    nazwaZmiennej.Add(lewa[i + 1]);
                    break;
                }
            }
            //zeus cos = false;
            //zeus cos = true;
            //zeus cos = 4 > 3;

            if (prawa.Contains("true") && prawa.Count() <= 3) //czy zawiera true
            {
                wartoscZmiennej.Add("antiterrorist");
                return true;
            }
            else if (prawa.Contains("false") && prawa.Count() <= 3) //czy zawiera false
            {
                wartoscZmiennej.Add("terrorist");
                return true;
            }
            else if (prawaChar.Contains('<') || prawaChar.Contains('>')) //czy zawiera znaki 
            {
                for (int i = 0; i < prawaChar.Length; i++)
                {
                    if (czyInt2(prawaChar[i]) == true)//jesli pierwszy element to liczba to zapisuje ją
                    {
                        pierwszaLiczba = int.Parse(prawaChar[i].ToString());
                        break;
                    }
                    else if (prawaChar[i] == '-')
                    {
                        pierwszaLiczba = 0;
                        break;
                    }
                }

                for (int i = 0; i < prawaChar.Length; i++)
                {
                    //sprawdzanie warunku
                    if (prawaChar[i] == '>')
                    {
                        if (czyInt2(prawaChar[i + 1]) == true)
                        {
                            if (pierwszaLiczba > int.Parse(prawaChar[i + 1].ToString()))
                            {
                                wynikBool = true;
                                wartoscZmiennej.Add("antiterrorist");
                                return true;
                            }
                            else
                            {
                                wynikBool = false;
                                wartoscZmiennej.Add("terrorist");
                                return true;
                            }
                        }

                    }
                    if (prawaChar[i] == '<')
                    {
                        if (czyInt2(prawaChar[i + 1]) == true)
                        {
                            if (pierwszaLiczba < int.Parse(prawaChar[i + 1].ToString()))
                            {
                                wynikBool = true;
                                wartoscZmiennej.Add("antiterrorist");
                                return true;
                            }
                            else
                            {
                                wynikBool = false;
                                wartoscZmiennej.Add("terrorist");
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                msg = "Niepoprawna wartość zmiennej typu 'zeus'!";
                return false;
            }
            return false;
        }

        public bool InterpretujInt2(string[] lines, int indeks)
        {
            string msg = "";
            string linia = lines[indeks];
            char[] liniaChar = linia.ToCharArray();
            string[] pomINT = lines[indeks].Split(' '); //dziele wyrazy na podstawie spacji
            List<int> listaNawiasowOtw = new List<int>();
            List<int> listaNawiasowZam = new List<int>();
            List<char> znakiwNawiasie = new List<char>();
            List<int> zmienneWykryte = new List<int>();
            List<string> liczby = new List<string>();
            List<string> znaki = new List<string>();
            List<string> liczby_w_nawiasie = new List<string>();
            List<string> znaki_w_nawiasie = new List<string>();
            List<string> zmienne = new List<string>();
            int indeksRownasie = 0;
            int wynik = 0;
            //knife liczba = 2;
            //knife cos = liczba + 1;

            for (int t = 0; t < liniaChar.Length; t++)
            {
                if (liniaChar[t] == '=')//sprawdzam na ktorym indeksie jest '='
                {
                    indeksRownasie = t;
                }
                else
                {
                    return false;
                }
            }
            //dzielenie linii na 2 czesci (deklaracja | inicjalizacja)
            int dlugoscLewaStrona = 0 + indeksRownasie + 1;
            string lewaStrona = lines[indeks].Substring(0, dlugoscLewaStrona);//tablica lewa ze znakiem równa się

            int dlugoscPrawaStrona = linia.Length - indeksRownasie - 1;
            string prawaStrona = lines[indeks].Substring(indeksRownasie + 1, dlugoscPrawaStrona);//tablica prawa

            string[] lewa = lewaStrona.Split();

            for (int i = 0; i < lewa.Length; i++)
            {
                if (lewa[i] == "knife" && czyString(lewa[i + 1]) == true)
                {
                    if (nazwaZmiennej.Contains(lewa[i + 1]))
                    {
                        bledy.Add("Istnieje juz taka zmienna!");
                        return false;
                    }
                    else
                    {
                        typZmiennej.Add("int");
                        nazwaZmiennej.Add(lewa[i + 1]);
                        break;
                    }
                }
            }

            char[] prawaChar = prawaStrona.ToCharArray();
            string temp = "";
            string temp2 = "";
            string tempNawias = "";
            string znakiNawias = "";
            string zmienneTemp = "";
            string znakiTemp = "";
            int result = 0;
            //dobra nie wiem jak te nawiasy ogarnąc ;/
            
            for(int i = 0; i < prawaChar.Length; i++)
            {/*
                if (czyInt2(prawaChar[i]) == true)
                {
                    temp = temp + prawaChar[i];
                }
                else if(prawaChar[i] == '+' || prawaChar[i] == '-' || prawaChar[i] == '*' || prawaChar[i] == '/')
                {
                    if (czyInt(temp) == false)
                    {
                        if (czyIstnieje(temp) == true)
                        {
                            int index = nazwaZmiennej.Find(c => c == temp);
                            liczby.Add(wartoscZmiennej[index]);
                            znaki.Add(prawaChar[i].ToString());
                            temp = "";
                        }
                        else
                        {
                            bledy.Add(linia + "Brak zmiennej o nazmie: " + temp);
                            return false;
                        }
                    }
                    else 
                    {

                    }
                }
                else
                {

                }*/
                //jesli liczba to do stringów , jesli znak to do listy, jestli litera to do listy 
               
                if(czyInt2(prawaChar[i]) == true && prawaChar[i+1] != ';' || prawaChar[i] == '(')
                {
                    temp = temp + prawaChar[i];
                }
                else if(czyInt2(prawaChar[i]) == false  && prawaChar[i] == '+' || prawaChar[i] == '-' || prawaChar[i] == '*' || prawaChar[i] == '/')
                {
                    liczby.Add(temp);
                    
                    temp2 = temp2 + prawaChar[i];
                    znaki.Add(temp2);
                    if(temp2 == "+")
                    {
                        result = result + int.Parse(temp);
                    }
                    if (temp2 == "-")
                    {
                        result = result - int.Parse(temp);
                    }
                    if (temp2 == "*")
                    {
                        if(result == 0)
                        {
                            result = int.Parse(temp);
                        }
                        else
                        {
                            result = result * int.Parse(temp);
                        }                     
                    }
                    if (temp2 == "/")
                    {
                        if (result == 0)
                        {
                            bledy.Add("Nie można dzielić zera!");
                        }
                        else
                        {
                            result = result / int.Parse(temp);
                        }
                    }
                    temp = "";
                    temp2 = "";
                }
                else if(czyInt2(prawaChar[i]) == true && prawaChar[i+1] == ';')
                {
                    temp = temp + prawaChar[i];
                    liczby.Add(temp);
                    temp = "";
                    
                }
                else if(prawaChar[i] == ' ')
                {

                }    
                else if(prawaChar[i] == ';')
                {
                    string zmienna2 = "";
                    foreach(var item in zmienne)
                    {
                        zmienna2 = zmienna2 + item;
                    }
                    if (nazwaZmiennej.Contains(zmienna2))
                    {

                    }
                    break;
                }else if(czyInt2(prawaChar[i]) == false)
                {
                    zmienneTemp = zmienneTemp + prawaChar[i];
                    zmienne.Add(zmienneTemp);
                    zmienneTemp = "";

                    if (prawaChar[i] == '+' || prawaChar[i] == '-' || prawaChar[i] == '*' || prawaChar[i] == '/')
                    {
                        temp2 = temp2 + prawaChar[i + 1];
                        znaki.Add(temp2);
                        temp2 = "";
                    }
                    
                }
            }

            //for(int i=0;i<)

            int a = 1;
            //musze sprawdzic 
            /*
             * czy dany element to liczba
             * czy dany element to zmienna jakas
             * czy kolejny element to liczba badz zmienna 
             * jakie mam znaki i na jakich miejscach 
             * i te walone nawiasy ...
             */

            string[] prawa = prawaStrona.Split('+');
            //if(prawa)


            return false;
        }
        
        /*
         metody do sprawdzania czy dany element ciagu jest liczbą czy stringiem
         */
        public bool czyIstnieje(string zmienna)
        {
            if (nazwaZmiennej.Contains(zmienna))
            {
                return true;
            }
            return false;
        }
        public bool czyString(string element)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*+`~ąśćźżółęń";

            //sprawdzanie czy element za knife jest stringiem
            char firstLetter = element.FirstOrDefault();

            foreach (var item in nazwaZmiennej)
            {
                if (element.Equals(item))
                {
                    return false;
                }
            }

            if (Char.IsDigit(firstLetter))
            {
                return false;
            }
            else
            {
                foreach (var item in specialChar)
                {
                    if (element.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool czyInt(string element)
        {

            //sprawdzam czy kolejne znaki w danym ciagu to liczby, jesli tak zwracam true jesli nie to false
            char[] tab = element.ToCharArray();
            foreach (var item in tab)
            {
                if (!char.IsDigit(item))
                    return false;
            }
            return true;
        }

        public bool czyInt2(char znak)
        {
            if (!char.IsDigit(znak))
            {
                return false;
            }
            return true;
        }



        public bool czyIstnieje(string nazwaZmiennej) //Tomaszowe 
        {
            int index = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej);
            if (index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool czyString2(string element) //Tomaszowe 
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*+`~ąśćźżółęń";

            //sprawdzanie czy element za knife jest stringiem
            char firstLetter = element.FirstOrDefault();


            if (Char.IsDigit(firstLetter))
            {
                return false;
            }
            else
            {
                foreach (var item in specialChar)
                {
                    if (element.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
        }




        /*
        public void InterpretujZmienne(string[] tempArray,int i)
        {

                string[] tab = tempArray[i].Split(' ');
                switch (tab[0])
                {
                    case "knife":
                        if (InterpretujInt(tempArray, i) == false)
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'int'.";
                        }
                        IndeksyINT.Add(i);
                        break;
                    case "grenade":
                        InterpretujDouble();
                        IndeksyDOUBLE.Add(i);
                        break;
                    case "rifle":
                        InterpretujFloat();
                        IndeksyFLOAT.Add(i);
                        break;
                    case "defuse":
                        InterpretujString();
                        IndeksySTRING.Add(i);
                        break;
                    case "zeus":
                        if(InterpretujBoolean(tempArray, i) == false)
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'bool'.";
                        } 
                        IndeksyBOOLEAN.Add(i);
                        break;
                }


                //zastępcze // nie zawsze knife musi byc na poczatku linii...
                
                    if (tab[i].Contains("knife") || tab[0] == "knife")//INT
                    {
                        if (InterpretujInt(tempArray, i) == false)
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'int'.";
                        }
                        IndeksyINT.Add(i);
                    }
                    if (tab[i].Contains("grenade"))//FLOAT
                    {
                        if (InterpretujInt(tempArray, i) == false)
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'int'.";
                        }
                        IndeksyINT.Add(i);
                    }
                    if (tab[i].Contains("defuse"))//STRING
                    {
                        if (InterpretujInt(tempArray, i) == false)
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'int'.";
                        }
                        IndeksyINT.Add(i);
                    }
                    if (tab[i].Contains("zeus") || tab[0] == "zeus")
                    {
                        if (InterpretujBoolean(tempArray, i) == false)//BOOLEAN
                        {
                            message = "Zła składnia deklaracji zmiennej typu 'bool'.";
                        }
                        IndeksyBOOLEAN.Add(i);
                    }
                
               
            }
        }*/






       
    }
}
