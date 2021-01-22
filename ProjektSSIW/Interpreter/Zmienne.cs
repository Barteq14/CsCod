using System;
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

            bool czyNawiasOtwarty = false;
            bool czygitnawias = true;

            int nawiasOtwarty = 0;
            int nawiasZamkniety = 0;

            if (czyString2(nazwaZmiennej2) == true) //jeżeli nazwaZmiennej to string
            {
                if (czyIstnieje(nazwaZmiennej2)) // jeżeli zmienna o takiej nazwie istnieje to blad
                {
                    bledy.Add(linijka + ": Istnieje zmienna o nazwie " + nazwaZmiennej2);
                }
                else //jeżeli nie ma takiej zmiennej to robi dalej
                {
                    
                    /*
                    for (int i = 0; i < ciag.Length; i++) // lecę po tablicy czarów całego ciągu
                    {
                        if(i != 0)
                        {
                            if (ciag[i] == '(')
                            {
                                if ((ciag[i + 1] == '+' || ciag[i + 1] == '*' || ciag[i + 1] == '/' || ciag[i + 1] == ';')) // ciag[i + 1] == '-' || 
                                {
                                    bledy.Add(linijka +": znak " + ciag[i+1] + " przed nawiasem rozpoczynającym");
                                }
                                nawiasOtwarty++;
                            }
                            if (ciag[i] == ')')
                            {
                                if ((ciag[i - 1] == '+' || ciag[i - 1] == '-' || ciag[i - 1] == '*' || ciag[i - 1] == '/' || ciag[i - 1] == ';'))
                                {
                                    bledy.Add(linijka + ": znak " + ciag[i + 1] + " przed nawiasem zamykającym");
                                }
                                nawiasZamkniety++;
                            }
                            else if (ciag[i] == ';')
                            {
                                if ((ciag[i - 1] == '+' || ciag[i - 1] == '-' || ciag[i - 1] == '*' || ciag[i - 1] == '/'))//blad
                                {
                                    bledy.Add(linijka + ": Na końcu znak " + ciag[i]);
                                }
                            }
                            if (ciag[i] == ')')
                            {
                                if ((ciag[i - 1] == '+' || ciag[i - 1] == '-' || ciag[i - 1] == '*' || ciag[i - 1] == '/' || ciag[i - 1] == ';'))
                                {
                                    bledy.Add(linijka + ": Przed zamykającym nawiasem jest znak " + ciag[i - 1]);
                                }
                            }
                        }
                        else
                        {
                            if(ciag[i] == '(')
                            {
                                nawiasOtwarty++;
                            }
                            else if((ciag[i] == '+' || ciag[i] == '-' || ciag[i] == '*' || ciag[i] == '/' || ciag[i] == ';'))
                            {
                                bledy.Add(linijka +": " +ciag[i] + " na początku działania");
                            }
                        }
                        
                    }

                    */

                    //Zmienne.konsola.Add(ciag.Remove(ciag.Length - 1));

                    string[] test = Regex.Split(ciag.Remove(ciag.Length - 1), "(?<=[()\\-+*/])|(?=[()\\-+*/])");
                    if ((test[0]== "" || test[0]==null || test[0] == " ") && test[1] == "(")
                    {
                        test = test.Where((v, i) => i != 0).ToArray();
                    }
                    
                    string ciag3 = "";

                    int pom4 = 0;
                    foreach (var match in test)
                    {
                        if (pom4 != 0)
                        {
                            if (test[pom4] == "(")
                            {
                                if ((test[pom4 + 1] == "+" || test[pom4 + 1] == "*" || test[pom4 + 1] == "/" || test[pom4 + 1] == ";")) // test[pom4 + 1] == '-' || 
                                {
                                    bledy.Add(linijka + ": znak " + test[pom4 + 1] + " po nawiasie rozpoczynającym");
                                }
                                if(pom4 != 0 && test[pom4] == "(")
                                {
                                    if (test[0] != "(" && !(test[pom4 - 1] == "+" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/"))
                                    {
                                        bledy.Add(linijka + ": brak znaku przed nawiasem");
                                    }
                                }
                                nawiasOtwarty++;
                            }
                            if (test[pom4] == ")")
                            {
                                if ((test[pom4 - 1] == "+" || test[pom4 - 1] == "-" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/" || test[pom4 - 1] == ";"))
                                {
                                    bledy.Add(linijka + ": znak " + test[pom4 + 1] + " przed nawiasem zamykającym");
                                }
                                nawiasZamkniety++;
                            }
                            else if (test[pom4] == ";")
                            {
                                if ((test[pom4 - 1] == "+" || test[pom4 - 1] == "-" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/"))//blad
                                {
                                    bledy.Add(linijka + ": Na końcu znak " + test[pom4]);
                                }
                            }
                            if (test[pom4] == ")")
                            {
                                if ((test[pom4 - 1] == "+" || test[pom4 - 1] == "-" || test[pom4 - 1] == "*" || test[pom4 - 1] == "/" || test[pom4 - 1] == ";"))
                                {
                                    bledy.Add(linijka + ": Przed zamykającym nawiasem jest znak " + test[pom4 - 1]);
                                }
                            }
                            if (test[pom4-1] == "/" && test[pom4] == "0")
                            {
                                bledy.Add(linijka +": nie można dzialić przez zero");
                            } 
                        }
                        else
                        {
                            if (test[pom4] == "(")
                            {
                                nawiasOtwarty++;
                            }
                            else if ((test[pom4] == "+" || test[pom4] == "-" || test[pom4] == "*" || test[pom4] == "/" || test[pom4] == ";"))
                            {
                                bledy.Add(linijka + ": " + test[pom4] + " na początku działania");
                            }
                        }
                        if (czyInt(match) == false)
                        {
                            if ((match == "/" || match == "*" || match == "+" || match == "-" || match == "(" || match == ")" || match == ";"))
                            {

                            }
                            else
                            {
                                int index = Zmienne.nazwaZmiennej.FindIndex(c => c == match);
                                if (index < 0) //żeby nie było double w int
                                {
                                    try
                                    {
                                        string[] subsPom = match.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                        if (subsPom.Count() == 2)
                                        {
                                            bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                            bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                            if (isNumeric1 == true && isNumeric2 == true)
                                            {
                                                bledy.Add(linijka + ": liczba " +match + " nie jest typu knife(int)");
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        bledy.Add(linijka + ": brak zmiennej o nazwie " + match);
                                    }
                                    
                                    
                                }
                                else
                                {
                                    if (Zmienne.typZmiennej[index] == "knife")
                                    {
                                        test[pom4] = (Zmienne.wartoscZmiennej[index]) + "";
                                        //bledy.Add("debug");
                                    }
                                    else
                                    {
                                        bledy.Add(linijka + ": zmienna o nazwie " + match + " to nie knife(int)");
                                    }
                                }
                            }
                        }
                        Zmienne.konsola.Add(test[pom4]);
                        ciag3 = ciag3 + test[pom4];
                        pom4++;
                    }

                    if (nawiasOtwarty != nawiasZamkniety)
                    {
                        bledy.Add(linijka + ": źle zrobione nawiasy");
                    }

                        //Zmienne.konsola.Add(ciag3);
                        if (Zmienne.bledy.Count > 0)
                    {
                        string item = bledy[bledy.Count - 1];
                        string item2 = item.Substring(0, 1);
                        string item3 = linijka + "";
                        if (item2 != item3)
                        {
                            try
                            {
                                //if(nawiasOtwarty == nawiasZamkniety)
                                //{
                                    DataTable dt = new DataTable();
                                    double answer = (double)dt.Compute(ciag3, "");
                                    wynik = Convert.ToInt32(answer);
                                    Zmienne.typZmiennej.Add("knife");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                    Zmienne.wartoscZmiennej.Add(wynik);
                                //}
                               //else
                                //{
                                //    bledy.Add(linijka + ": źle zrobione nawiasy");
                               // }
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
                    else
                    {
                        try
                        {
                            //if (nawiasOtwarty == nawiasZamkniety)
                            //{
                                DataTable dt = new DataTable();
                                double answer = (double)dt.Compute(ciag3, "");
                                wynik = Convert.ToInt32(answer);
                                Zmienne.typZmiennej.Add("knife");
                                Zmienne.nazwaZmiennej.Add(nazwaZmiennej2);
                                Zmienne.wartoscZmiennej.Add(wynik);
                            //}
                            //else
                            //{
                            //    bledy.Add(linijka + ": źle zrobione nawiasy");
                            //}
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
                bledy.Add(linijka + ": Zła nazwa zmiennej " + nazwaZmiennej);
            }
        }





        public bool InterpretujInt(string[] lines, int indeks)
        {
            int liczba;
            int liczba2;
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

            //probuje od nowa 

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
            char znak;
            int wynikZmiennej = 0;
            int liczbaZeZnaku = 0;

            if (!znakiPrawaStrona.Contains('(') && !znakiPrawaStrona.Contains(')'))
            {

                for (int o = 0; o < znakiPrawaStrona.Length; o++)
                {

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
                }


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
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow - int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                        if (znakiPrawaStrona[q] == '*')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow * int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                        if (znakiPrawaStrona[q] == '/')
                        {
                            if (czyInt2(znakiwNawiasie[q + 1]) == true)
                            {
                                wynikBezNawiasow = wynikBezNawiasow / int.Parse(znakiPrawaStrona[q + 1].ToString());
                            }
                        }
                    }


                }

                typZmiennej.Add("int");
                nazwaZmiennej.Add(lewa[1]);
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
                                            wynikPoNawiasie = wynikPoNawiasie * int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                    if (znakiPrawaStrona[q] == '/')
                                    {
                                        if (czyInt2(znakiPrawaStrona[q + 1]) == true)
                                        {
                                            wynikPoNawiasie = wynikPoNawiasie / int.Parse(znakiPrawaStrona[q + 1].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }

                                //obliczanie wyniku przed i w nawiasie
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
                        }

                    }


                    //obliczanie wyniku koncowego

                    for (int q = 0; q < znakiPrawaStrona.Length; q++)
                    {
                        foreach (var item in listaNawiasowOtw)
                        {
                            foreach (var item2 in listaNawiasowZam)
                            {
                                if (znakiPrawaStrona[item2 + 1] == '+')
                                {
                                    wynikKoncowy = wynikPrzed_i_wNawiasie + wynikPoNawiasie;
                                }
                                if (znakiPrawaStrona[item2 + 1] == '-')//jesli jest -1 na koncu to mi do wyniku po nawiasie wpisuje -1 a nie 1 
                                {
                                    wynikKoncowy = wynikPrzed_i_wNawiasie - wynikPoNawiasie;
                                }
                                if (znakiPrawaStrona[item2 + 1] == '*')
                                {
                                    wynikKoncowy = wynikPrzed_i_wNawiasie * wynikPoNawiasie;
                                }
                                if (znakiPrawaStrona[item2 + 1] == '/')
                                {
                                    wynikKoncowy = wynikPrzed_i_wNawiasie / wynikPoNawiasie;
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

        public void InterpretujString()
        {

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
            if (index > 0)
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
