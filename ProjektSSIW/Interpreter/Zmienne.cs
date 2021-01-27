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
                //sprawdzanko czy nie jest np. knife knife
                if (nazwaZmiennej2 == "ak47" || nazwaZmiennej2 == "knife" || nazwaZmiennej2 == "grenade" || nazwaZmiennej2 == "rifle" || nazwaZmiennej2 == "defuse" || nazwaZmiennej2 == "zeus" || nazwaZmiennej2 == "m4a1s" || nazwaZmiennej2 == "m4a4" || nazwaZmiennej2 == "usp" || nazwaZmiennej2 == "glock" || nazwaZmiennej2 == "tec" || nazwaZmiennej2 == "awp" || nazwaZmiennej2 == "scar" || nazwaZmiennej2 == "negev" || nazwaZmiennej2 == "deagle")
                {
                    bledy.Add(linijka + ": Nazwa zmiennej nie może być " + nazwaZmiennej2);
                }
                if (ciag.Contains("ak47") || ciag.Contains("knife") || ciag.Contains("grenade") || ciag.Contains("rifle") || ciag.Contains("defuse") || ciag.Contains("zeus") || ciag.Contains("m4a1s") || ciag.Contains("usp") || ciag.Contains("glock") || ciag.Contains("tec") || ciag.Contains("awp") || ciag.Contains("scar") || ciag.Contains("negev") || ciag.Contains("deagle"))
                {
                    //bledy.Add(indeks + ": Nazwa zmiennej nie może być ");
                }
                else
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
                                        {                                        //bledy.Add(linijka + ": blad??? " + match);
                                        }
                                        if (flaga == false)
                                        {
                                            if (match == "ak47();")// tutaj dodałem żeby nikt nie wpisywał funkcji gdy nie jest na poczatku
                                            {
                                                bledy.Add(linijka + ": ak47(); nie może tutaj być");
                                            }
                                            else if (match == "glock();")
                                            {
                                                bledy.Add(linijka + ": glock(); nie może tutaj być");
                                            }
                                            else if (match == "tec();")
                                            {
                                                bledy.Add(linijka + ": tec(); nie może tutaj być");
                                            }
                                            else if (match == "m4a1();")
                                            {
                                                bledy.Add(linijka + ": m4a1(); nie może tutaj być");
                                            }
                                            else if (match == "m4a1s();")
                                            {
                                                bledy.Add(linijka + ": m4a1s(); nie może tutaj być");
                                            }
                                            else if (match == "usp();")
                                            {
                                                bledy.Add(linijka + ": usp(); nie może tutaj być");
                                            }
                                            else
                                            {
                                                bledy.Add(linijka + ": nie istnieje zmienna o nazwie " + match);
                                            }
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


            }
            else
            {
                bledy.Add(linijka + ": nazwa zmiennej to nie string");
            }
        }


        public void BartkowyDouble(string nazwaZmiennej2, string ciag, int indeks)
        {
            double wynik;
            int nawiasOtwarty = 0;
            int nawiasZamkniety = 0;
            int temp = 0;
            string ciag3 = "";


            if (czyString2(nazwaZmiennej2) == true) //jeżeli nazwaZmiennej to string
            {
                if (nazwaZmiennej2 == "ak47" || nazwaZmiennej2 == "knife" || nazwaZmiennej2 == "grenade" || nazwaZmiennej2 == "rifle" || nazwaZmiennej2 == "defuse" || nazwaZmiennej2 == "zeus" || nazwaZmiennej2 == "m4a1s" || nazwaZmiennej2 == "m4a4" || nazwaZmiennej2 == "usp" || nazwaZmiennej2 == "glock" || nazwaZmiennej2 == "tec" || nazwaZmiennej2 == "awp" || nazwaZmiennej2 == "scar" || nazwaZmiennej2 == "negev" || nazwaZmiennej2 == "deagle")
                {
                    bledy.Add(indeks + ": Nazwa zmiennej nie może być " + nazwaZmiennej2);
                }
                if (ciag.Contains("ak47") || ciag.Contains("knife") || ciag.Contains("grenade") || ciag.Contains("rifle") || ciag.Contains("defuse") || ciag.Contains("zeus") || ciag.Contains("m4a1s") || ciag.Contains("usp") || ciag.Contains("glock") || ciag.Contains("tec") || ciag.Contains("awp") || ciag.Contains("scar") || ciag.Contains("negev") || ciag.Contains("deagle"))
                {
                    //bledy.Add(indeks + ": Nazwa zmiennej nie może być ");
                }
                else
                {
                    int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej2);
                    if (index2 >= 0) //jeżeli nie ma takiej zmiennej
                    {
                        bledy.Add(indeks + ": Istnieje zmienna o nazwie " + nazwaZmiennej2);
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
                            if (temp != 0) // jeżeli nie będziemy sprawdzać test[0] to
                            {
                                if (test[temp] == "(") // jeżeli znak "("
                                {
                                    if ((test[temp + 1] == "+" || test[temp + 1] == "*" || test[temp + 1] == "/" || test[temp + 1] == ";")) // jeżeli po znaku ( jest znak (nie minus) to błąd
                                    {
                                        bledy.Add(indeks + ": znak " + test[temp + 1] + " po nawiasie rozpoczynającym");
                                    }
                                    if (temp != 0 && test[temp] == "(") // jeżeli przed ( nie ma znaku operacji i nie sprawdzamy pierwszego test[0] to blad
                                    {
                                        if (test[0] != "(" && !(test[temp - 1] == "+" || test[temp - 1] == "*" || test[temp - 1] == "/"))
                                        {
                                            bledy.Add(indeks + ": brak znaku przed nawiasem");
                                        }
                                    }
                                    nawiasOtwarty++; // do sprawdzenia czy dobra ilosc nawiasow
                                }
                                if (test[temp] == ")") //jezeli jest nawias zamykajacy
                                {
                                    if ((test[temp - 1] == "+" || test[temp - 1] == "-" || test[temp - 1] == "*" || test[temp - 1] == "/" || test[temp - 1] == ";")) // jezeli przed ) jest jakis znak operacji to blad
                                    {
                                        bledy.Add(indeks + ": znak " + test[temp + 1] + " przed nawiasem zamykającym");
                                    }
                                    nawiasZamkniety++;// do sprawdzenia czy dobra ilosc nawiasow
                                }
                                else if (test[temp] == ";") // jezeli koniec to
                                {
                                    if ((test[temp - 1] == "+" || test[temp - 1] == "-" || test[temp - 1] == "*" || test[temp - 1] == "/"))//jezeli koniec a przed koncem jest jakis znak operacji to blad
                                    {
                                        bledy.Add(indeks + ": Na końcu znak " + test[temp]);
                                    }
                                }
                                if (test[temp - 1] == "/" && test[temp] == "0") // blad z dzieleniem przez zero
                                {
                                    bledy.Add(indeks + ": nie można dzialić przez zero");
                                }
                            }
                            else //tutaj będzie sprawdzanie pierwszego elementu z tablicy test
                            {
                                if (test[temp] == "(")
                                {
                                    nawiasOtwarty++; // do sprawdzenia czy dobra ilosc nawiasow
                                }
                                else if ((test[temp] == "+" || test[temp] == "-" || test[temp] == "*" || test[temp] == "/" || test[temp] == ";"))// jeżeli na samym początku znak operacji to blad
                                {
                                    bledy.Add(indeks + ": " + test[temp] + " na początku działania");
                                }
                            }
                            if (czyDouble(match) == false) // element test[pom4] to nie int to ///!!!!!!!!!!!!! napisac metodę czy double!
                            {
                                if ((match == "/" || match == "*" || match == "+" || match == "-" || match == "(" || match == ")" || match == ";"))
                                {

                                }
                                else // jeżeli to nie znak operacji to
                                {
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == match);
                                    if (index < 0) //jeżeli nie ma takiej zmiennej
                                    {
                                        if (flaga == false)
                                        {
                                            bledy.Add(indeks + ": nie istnieje zmienna o nazwie " + match);
                                        }
                                    }
                                    else // jeżeli jest zmienna to 
                                    {
                                        if (Zmienne.typZmiennej[index] == "grenade" || Zmienne.typZmiennej[index] == "knife") // jezeli jest taka zmienna to sprawdza czy to jest float
                                        {
                                            test[temp] = (Zmienne.wartoscZmiennej[index]) + "";
                                            //bledy.Add("debug");
                                        }
                                        else //jezeli to nie float to blad
                                        {
                                            bledy.Add(indeks + ": zmienna o nazwie " + match + " to nie grenade(double)");
                                        }
                                    }
                                }
                            }

                            //Zmienne.konsola.Add(test[pom4]);
                            ciag3 = ciag3 + test[temp]; //dodaje ciag po prawej stronie wraz z przetworzonymi zmiennymi
                            temp++; //pomocnicza
                        }
                        if (nawiasOtwarty != nawiasZamkniety) // jeżeli ilość nawiasów się nie zgadza
                        {
                            bledy.Add(indeks + ": źle zrobione nawiasy");
                        }
                        //Zmienne.konsola.Add(ciag3);
                        if (Zmienne.bledy.Count > 0) // jeżeli jest jakiś błąd to
                        {
                            string item = bledy[bledy.Count - 1];
                            string item2 = item.Substring(0, 1);
                            string item3 = indeks + "";
                            if (item2 != item3) //sprawdza czy ostatni blad jest z linijki w której jest działanie wykonywane jeżeli nie to
                            {
                                try //są tutaj try oraz catch bo jak w działaniu jest samo - + to wystarczy (int)dt.Compute a jeżeli jest * / to trzeba (double)dt.Compute i później zmienić na inta
                                {
                                    DataTable dt = new DataTable();
                                    var answer = dt.Compute(ciag3, "");
                                    var wynik1 = answer.ToString();
                                    wynik = double.Parse(wynik1, System.Globalization.CultureInfo.InvariantCulture)+0.0;
                                    Zmienne.typZmiennej.Add("grenade");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                    Zmienne.wartoscZmiennej.Add(answer);
                                }
                                catch
                                {
                                    try
                                    {
                                        DataTable dt = new DataTable();
                                        var answer = dt.Compute(ciag3, "");
                                        var wynik1 = answer.ToString();
                                        wynik = double.Parse(wynik1, System.Globalization.CultureInfo.InvariantCulture) + 0.0;
                                        Zmienne.typZmiennej.Add("grenade");
                                        Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                        Zmienne.wartoscZmiennej.Add(answer);
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
                                var answer = dt.Compute(ciag3, "");
                                var wynik1 = answer.ToString();
                                wynik = double.Parse(wynik1, System.Globalization.CultureInfo.InvariantCulture) + 0.0;
                                Zmienne.typZmiennej.Add("grenade");
                                Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                Zmienne.wartoscZmiennej.Add(answer);
                            }
                            catch
                            {
                                try
                                {
                                    DataTable dt = new DataTable();
                                    var answer = dt.Compute(ciag3, "");
                                    var wynik1 = answer.ToString();
                                    wynik = double.Parse(wynik1, System.Globalization.CultureInfo.InvariantCulture)+0.0;
                                    Zmienne.typZmiennej.Add("grenade");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                    Zmienne.wartoscZmiennej.Add(answer);
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
            }
            else// jeżeli nazwaZmiennej to nie string
            {
                bledy.Add(indeks + ": Zła nazwa zmiennej " + nazwaZmiennej2);
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


        public void BartkowyString(string nazwaZmiennej2, string ciag, int indeks)
        {

            string wynik;
            int temp = 0;
            string ciag3 = "";
            int zajaczek = 0; // kiedy zaczyna się zajaczek
            //int pom = 23;
            //string cos = "ja mam: " + pom + 2 + " lata";
            //string cos = 2 + "asdasd";

            if (czyString2(nazwaZmiennej2) == true) //jeżeli nazwaZmiennej to string
            {
                if (nazwaZmiennej2 == "ak47" || nazwaZmiennej2 == "knife" || nazwaZmiennej2 == "grenade" || nazwaZmiennej2 == "rifle" || nazwaZmiennej2 == "defuse" || nazwaZmiennej2 == "zeus" || nazwaZmiennej2 == "m4a1s" || nazwaZmiennej2 == "m4a4" || nazwaZmiennej2 == "usp" || nazwaZmiennej2 == "glock" || nazwaZmiennej2 == "tec" || nazwaZmiennej2 == "awp" || nazwaZmiennej2 == "scar" || nazwaZmiennej2 == "negev" || nazwaZmiennej2 == "deagle")
                {
                    bledy.Add(indeks + ": Nazwa zmiennej nie może być " + nazwaZmiennej2);
                }
                if (ciag.Contains("ak47") || ciag.Contains("knife") || ciag.Contains("grenade") || ciag.Contains("rifle") || ciag.Contains("defuse") || ciag.Contains("zeus") || ciag.Contains("m4a1s") || ciag.Contains("usp") || ciag.Contains("glock") || ciag.Contains("tec") || ciag.Contains("awp") || ciag.Contains("scar") || ciag.Contains("negev") || ciag.Contains("deagle"))
                {
                    //bledy.Add(indeks + ": Nazwa zmiennej nie może być ");
                }
                else
                {
                    int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej2);
                    if (index2 >= 0) //jeżeli nie ma takiej zmiennej
                    {
                        bledy.Add(indeks + ": Istnieje zmienna o nazwie " + nazwaZmiennej2);
                    }
                    else //jeżeli nie ma takiej zmiennej to robi dalej
                    {
                        string[] test = Regex.Split(ciag.Remove(ciag.Length - 1), "(?<=[()\\-+*/'])|(?=[()\\-+*/'])"); // rozdziela na tablicę stringów cały ciąg


                        if ((test[0] == "" || test[0] == null || test[0] == " ")) //jak rozdziela tablicę i na samym początku jest nawias otwierający to test[0] = " " więc usuwamy ten pierwszy element
                        {
                            test = test.Where((v, i) => i != 0).ToArray();
                        }
                        foreach (var match in test) //leci po tablicy stringów test
                        {
                            bool flaga = false;
                            if (temp != 0) // jeżeli nie będziemy sprawdzać test[0] to
                            {
                                if (test[temp] == ";") // jezeli koniec to
                                {
                                    if ((test[temp - 1] == "+" || test[temp - 1] == "-" || test[temp - 1] == "*" || test[temp - 1] == "/"))//jezeli koniec a przed koncem jest jakis znak operacji to blad
                                    {
                                        bledy.Add(indeks + ": Na końcu znak " + test[temp]);
                                    }
                                }
                                if (test[temp] == "'")//sprawdzam te zajaczki
                                {
                                    if (test[temp - 1] == "*" || test[temp - 1] == "/" || test[temp - 1] == ";")
                                    {
                                        bledy.Add(indeks + ": przed zakaczkiem nie powinno byc zadnego znaku");
                                    }
                                    if (test[temp + 1] == "*" || test[temp + 1] == "/" || test[temp + 1] == "-")
                                    {
                                        bledy.Add(indeks + ": po zajaczku moze wystepowac tylko +");
                                    }
                                    zajaczek++;
                                }
                            }
                            else //dodałem bo jak na początku jest słowo z zajączkach to pokazuje błąd że zła ilość
                            {
                                if (test[temp] == "'")//sprawdzam te zajaczki
                                {
                                    if (test[temp + 1] == "*" || test[temp + 1] == "/" || test[temp + 1] == "-")
                                    {
                                        bledy.Add(indeks + ": po zajaczku moze wystepowac tylko +");
                                    }
                                    zajaczek++;
                                }
                            }

                            if (czyString2(match) == true) // czemu mi daje tutaj false jak mam stringa ...
                            {
                                if (test[temp] == ";")
                                {
                                    break;
                                }

                                if (temp != 0 && temp + 1 == test.Length && test[temp] == "") //tutaj dodałem temp != 0  żeby błędu nie było
                                {
                                    break;
                                }
                                //string cos = 'mam lat: ' +pom+2+ 'yolo'; nie dziala 
                                //string cos = 'mam lat: ' +pom+2; dziala teraz 
                                if (temp != 0 && test[temp - 1] == "'" && test[temp + 1] == "'") //bo wykracza mi poza tablice, sprawdzi ze ' było przed ale juz po nie ma 
                                {
                                    ciag3 = ciag3 + test[temp];
                                }
                                else if (temp != 0 && test[temp - 1] == "+" && test[temp] != " " || test[temp + 1] == "+" && test[temp] != " ")
                                {
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == match);
                                    if (index < 0) //jeżeli nie ma takiej zmiennej
                                    {
                                        if (flaga == false)
                                        {
                                            bledy.Add(indeks + ": nie istnieje zmienna o nazwie " + match);
                                        }
                                    }
                                    else // jeżeli jest zmienna to 
                                    {
                                        if (Zmienne.typZmiennej[index] == "grenade" || Zmienne.typZmiennej[index] == "knife" || Zmienne.typZmiennej[index] == "defuse") // jezeli jest taka zmienna to sprawdza czy to jest float
                                        {
                                            test[temp] = (Zmienne.wartoscZmiennej[index]) + "";
                                            ciag3 = ciag3 + " " + test[temp];
                                            //bledy.Add("debug");
                                        }
                                        else //jezeli to nie zadna z powyzszych typow
                                        {
                                            bledy.Add(indeks + ": zmienna o nazwie " + match + " posaida zly typ, nie mozna dodać do string.");
                                        }
                                    }
                                }
                            }
                            else if (czyInt(match) == true)
                            {
                                ciag3 = ciag3 + test[temp].ToString();
                            }

                            temp++; //pomocnicza
                        }

                        //dodanie zmiennej
                        typZmiennej.Add("defuse");
                        nazwaZmiennej.Add(nazwaZmiennej2);
                        wartoscZmiennej.Add(ciag3);


                        if (zajaczek % 2 != 0)// jeżeli ilość zajączków jest nie parzysta
                        {
                            bledy.Add(indeks + ": zla ilosc zajaczkow");
                        }
                    }
                }
            }
            else// jeżeli nazwaZmiennej to nie string
            {
                bledy.Add(indeks + ": Zła nazwa zmiennej " + nazwaZmiennej2);
            }


        }


        public bool BartkowyBoolean(string nazwaZmiennej2, string ciag, int indeks) //sprawdzic bo nie wiem czemu ale moge utworzyc kolejna taka sama zmienna :/
        {
           
            char[] prawaChar = ciag.ToCharArray();
            string[] prawa = ciag.Split('<','>',';');// zle dzieli po spacji :/
            string[] prawa2 = ciag.Split(';');
            List<int> liczby = new List<int>();
            List<int> indeksy = new List<int>();
            bool firstZmienna = true;


            if (nazwaZmiennej2 == "ak47" || nazwaZmiennej2 == "knife" || nazwaZmiennej2 == "grenade" || nazwaZmiennej2 == "rifle" || nazwaZmiennej2 == "defuse" || nazwaZmiennej2 == "zeus" || nazwaZmiennej2 == "m4a1s" || nazwaZmiennej2 == "m4a4" || nazwaZmiennej2 == "usp" || nazwaZmiennej2 == "glock" || nazwaZmiennej2 == "tec" || nazwaZmiennej2 == "awp" || nazwaZmiennej2 == "scar" || nazwaZmiennej2 == "negev" || nazwaZmiennej2 == "deagle")
            {
                bledy.Add(indeks + ": Nazwa zmiennej nie może być " + nazwaZmiennej2);
            }
            if (ciag.Contains("ak47") || ciag.Contains("knife") || ciag.Contains("grenade") || ciag.Contains("rifle") || ciag.Contains("defuse") || ciag.Contains("zeus") || ciag.Contains("m4a1s") || ciag.Contains("usp") || ciag.Contains("glock") || ciag.Contains("tec") || ciag.Contains("awp") || ciag.Contains("scar") || ciag.Contains("negev") || ciag.Contains("deagle"))
            {
                //bledy.Add(indeks + ": Nazwa zmiennej nie może być ");
            }
            else
            {
                int index = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej2);

                if (index >= 0) // sprawdzam czy juz taka zme
                {
                    bledy.Add(indeks + ": Istnieje juz taka zmienna");
                    return false;
                }
                else
                {
                    if (prawa.Contains("true") && prawa.Count() <= 3) //czy zawiera true
                    {
                        typZmiennej.Add("zeus");
                        nazwaZmiennej.Add(nazwaZmiennej2);
                        wartoscZmiennej.Add("antiterrorist");
                        return true;
                    }
                    else if (prawa.Contains("false") && prawa.Count() <= 3) //czy zawiera false
                    {
                        typZmiennej.Add("zeus");
                        nazwaZmiennej.Add(nazwaZmiennej2);
                        wartoscZmiennej.Add("terrorist");
                        return true;
                    }
                    else if (ciag.Contains(">") || ciag.Contains("<"))
                    {

                        List<int> zmienne = new List<int>();
                        int ll = 0;
                        foreach (var item in prawa)
                        {

                            if (czyString2(item) == true && item != "")//sprawdzamy czy to string
                            {
                                int id = Zmienne.nazwaZmiennej.FindIndex(c => c == item);
                                if (typZmiennej[id] == "knife" || typZmiennej[id] == "grenade")
                                {
                                    zmienne.Add(wartoscZmiennej[id]);
                                    if (liczby.Count == 0)
                                    {
                                        firstZmienna = true;
                                    }
                                    indeksy.Add(ll);
                                }
                            }
                            else if (czyInt(item) == true && item != "")//sprawdzamy czy to liczba
                            {
                                liczby.Add(Int32.Parse(item));
                                if (zmienne.Count == 0)
                                {
                                    firstZmienna = false;
                                }
                                indeksy.Add(ll);
                            }
                            ll++;
                        }


                        if (ciag.Contains(">")) //jesli taki znak
                        {
                            if (zmienne.Count == 2)
                            {
                                for (int i = 0; i < zmienne.Count; i++)
                                {
                                    if (zmienne[i] > zmienne[i + 1])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }
                            else if (zmienne.Count == 1 && liczby.Count == 1)
                            {
                                if (firstZmienna == true)
                                {
                                    if (zmienne[0] > liczby[0])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (liczby[0] > zmienne[0])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }
                            else if (zmienne.Count == 0)//jesli sa same liczby
                            {
                                for (int i = 0; i < prawa.Length; i++)
                                {
                                    if (Int32.Parse(prawa[i]) > Int32.Parse(prawa[i + 1]))
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }

                        }
                        else if (ciag.Contains("<"))// jesli taki znak
                        {
                            if (zmienne.Count == 2)
                            {
                                for (int i = 0; i < zmienne.Count; i++)
                                {
                                    if (zmienne[i] < zmienne[i + 1])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }
                            else if (zmienne.Count == 1 && liczby.Count == 1)
                            {
                                if (firstZmienna == true)
                                {
                                    if (zmienne[0] < liczby[0])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (liczby[0] < zmienne[0])
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }
                            else if (zmienne.Count == 0)//jesli sa same liczby
                            {
                                for (int i = 0; i < prawa.Length; i++)
                                {
                                    if (Int32.Parse(prawa[i]) < Int32.Parse(prawa[i + 1]))
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("antiterrorist");
                                        return true;
                                    }
                                    else
                                    {
                                        typZmiennej.Add("zeus");
                                        nazwaZmiennej.Add(nazwaZmiennej2);
                                        wartoscZmiennej.Add("terrorist");
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        bledy.Add(indeks + ": Niepoprawna wartość zmiennej typu 'zeus'");
                        return false;
                    }
                }
            }
            return false;
        }
        
        /*
         metody do sprawdzania czy dany element ciagu jest liczbą czy stringiem
         */
       
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

        public bool czyDouble(string element)
        {

            //sprawdzam czy kolejne znaki w danym ciagu to liczby, jesli tak zwracam true jesli nie to false
            char[] tab = element.ToCharArray();

           
            foreach (var item in tab)
            {
                if (!char.IsDigit(item) && item != '.')
                {
                    return false;
                }

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

    }
}
