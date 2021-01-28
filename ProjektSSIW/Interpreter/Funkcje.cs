using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProjektSSIW.Interpreter
{
    /// <summary>
    /// Defines the <see cref="Funkcje" />.
    /// </summary>
    public class Funkcje
    {

        Składnia składnia = new Składnia();
        Zmienne zmienne = new Zmienne();




        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        // P/Invoke required:
        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);

        //ŻEBY wywołało KONSOLĘ
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();


        /*
        public void InterpretujFunkcje(string pom, int i)
        {
            string[] tab = pom.Split(' ');
            //Zmienne.konsola.Add();
            
            if (pom.Length >= 5)
            {
                if (pom.Substring(0, 6) == "m4a1s(" && pom[pom.Length - 2] == ')' && pom.EndsWith(";"))
                {
                    String pomWnawiasach = pom.Substring(6, pom.Length - 7 - 1);
                    InterpretujWriteLine(pomWnawiasach, i);
                }
                else if (pom.Substring(0, 5) == "m4a1(" && pom[pom.Length - 2] == ')' && pom.EndsWith(";"))
                {
                    String pomWnawiasach = pom.Substring(5, pom.Length - 6 - 1);
                    InterpretujWrite(pomWnawiasach, i);
                }
            }
            else if (tab.Length == 1 && tab[0] == "ak47();")
            {
                InterpretujReadLine2(i);
            }


            if (tab.Length == 4)
            {
                if (tab[3].Length > 4)
                {
                    if (tab[0] == "defuse" && tab[3].Substring(0, 4) == "usp(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";") && pom.Length > 4)
                    {
                        //pom.Substring(0, 6) == "m4a1s("
                        string pomocnicza = tab[3].Substring(4, tab[3].Length - 5 - 1);
                        InterpretujToString(tab[1], pomocnicza, i);
                    }
                    else if (tab[0] == "knife" && tab[3].Substring(0, 6) == "glock(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";") && pom.Length > 5)
                    {
                        string pomocnicza = tab[3].Substring(6, tab[3].Length - 7 - 1);
                        InterpretujToInt(tab[1], pomocnicza, i);
                    }
                    else if (tab[0] == "grenade" && tab[3].Substring(0, 7) == "deagle(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";") && pom.Length > 6)
                    {
                        string pomocnicza = tab[3].Substring(7, tab[3].Length - 8 - 1);
                        InterpretujToDouble(tab[1], pomocnicza, i);
                    }
                }
            }

            if (tab[3] == "ak47();")
            {
                InterpretujReadLine(tab, i);
            }



        }
        */


        public void InterpretujWrite(string tempArray, int linijka)//m4a1
        {
            //tempArray = tempArray.Remove(tempArray.Length - 1);
            string[] subs = tempArray.Split('+'); //tablica przechowujaca elementy oprocz +
            int pomocnicza1 = 0;


            foreach (var item in subs)
            {
                String pom = item;

                if (pomocnicza1 == 0) //jeżeli pierwsza zmienna/wartość to tutaj będzie dodawać do konsoli nową linijkę
                {
                    if (pom.Length >= 2) // sprawdza czy są >=2 znaków w ciągu
                    {
                        if (pom[0] == '\'' && pom[pom.Length - 1] == '\'') // sprawdź czy na początku i końcu apostrofy ' '
                        {
                            if (Zmienne.konsola.Count == 0)
                            {
                                Zmienne.konsola.Add(pom.Substring(1, pom.Length - 2)); // dodaj do konsoli
                            }
                            else
                            {
                                Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom.Substring(1, pom.Length - 2);
                            }
                        }
                        else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'') //jeżeli nie ma apostrofów to sprawdza czy jest taka zmienna
                        {
                            bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                            if (isNumeric) // sprawdź czy item jest numerem
                            {
                                if (Zmienne.konsola.Count == 0)
                                {
                                    Zmienne.konsola.Add(pom + ""); //dodaj numer do konsoli
                                }
                                else
                                {
                                    Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                                }

                            }
                            else
                            {
                                string[] subsPom = pom.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                if (subsPom.Count() == 2)
                                {
                                    bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                    bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                    if (isNumeric1 == true && isNumeric2 == true)
                                    {
                                        if (Zmienne.konsola.Count == 0)
                                        {
                                            Zmienne.konsola.Add(pom + ""); //dodaj numer do konsoli
                                        }
                                        else
                                        {
                                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                                        }
                                    }
                                    else
                                    {
                                        tymczasowyBlad("Źle wpisana liczba", linijka); break;
                                    }
                                }
                                else
                                {
                                    //wyszukiwanie zmiennej
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == item);
                                    if (index < 0)
                                    {
                                        tymczasowyBlad("Brak zmiennej o nazwie " + pom, linijka); break;
                                    }
                                    else
                                    {
                                        if (Zmienne.konsola.Count == 0)
                                        {
                                            Zmienne.konsola.Add(Zmienne.wartoscZmiennej[index] + ""); //dodaj numer do konsoli
                                        }
                                        else if (Zmienne.konsola.Count > 0)
                                        {
                                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + Zmienne.wartoscZmiennej[index];
                                            //Zmienne.bledy.Add(linijka + " " + Zmienne.wartoscZmiennej[index]);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                    else
                    {
                        bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                        if (isNumeric) // sprawdź czy item jest numerem
                        {
                            if (Zmienne.konsola.Count == 0)
                            {
                                Zmienne.konsola.Add(pom);
                            }
                            else
                            {
                                Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                            }
                        }
                    }
                }
                else //tutaj sprawdza te kolejne zmienne/wartości i je dodaje do tego ostatniego writelina
                {
                    if (pom.Length >= 2) // sprawdza czy są >=2 znaków w ciągu
                    {
                        if (pom[0] == '\'' && pom[pom.Length - 1] == '\'') // sprawdź czy na początku i końcu apostrofy ' '
                        {
                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom.Substring(1, pom.Length - 2);
                        }
                        else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'')
                        {
                            bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                            if (isNumeric) // sprawdź czy item jest numerem
                            {
                                Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                            }
                            else
                            {
                                string[] subsPom = pom.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                if (subsPom.Count() == 2)
                                {
                                    bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                    bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                    if (isNumeric1 == true && isNumeric2 == true)
                                    {
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                                    }
                                    else
                                    {
                                        tymczasowyBlad("Źle wpisana liczba", linijka); break;
                                    }
                                }
                                else
                                {
                                    //wyszukiwanie zmiennej
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == item);
                                    if (index < 0)
                                    {
                                        tymczasowyBlad("Brak zmiennej o nazwie " + pom, linijka); break;
                                    }
                                    else
                                    {
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + Zmienne.wartoscZmiennej[index];
                                    }
                                }
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                    else
                    {
                        bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                        if (isNumeric) // sprawdź czy item jest numerem
                        {
                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                }
                pomocnicza1++;
            }

            bool flaga1 = false;
            foreach(var item in Zmienne.bledy)
            {
                if (item.Contains(linijka+":"))
                {
                    flaga1 = true;
                    break;
                }
            }
            if(flaga1 == true)
            {
                Zmienne.konsola.RemoveAt(Zmienne.konsola.Count - 1);
            }


        }




        public void InterpretujWriteLine(string tempArray, int linijka)//m4a1
        {
            //tempArray = tempArray.Remove(tempArray.Length - 1);
            string[] subs = tempArray.Split('+'); //tablica przechowujaca elementy oprocz +
            int pomocnicza1 = 0;


            foreach (var item in subs)
            {
                String pom = item;

                if (pomocnicza1 == 0) //jeżeli pierwsza zmienna/wartość to tutaj będzie dodawać do konsoli nową linijkę
                {
                    if (pom.Length >= 2) // sprawdza czy są >=2 znaków w ciągu
                    {
                        if (pom[0] == '\'' && pom[pom.Length - 1] == '\'') // sprawdź czy na początku i końcu apostrofy ' '
                        {
                                Zmienne.konsola.Add(pom.Substring(1, pom.Length - 2)); // dodaj do konsoli
                        }
                        else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'') //jeżeli nie ma apostrofów to sprawdza czy jest taka zmienna
                        {
                            bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                            if (isNumeric) // sprawdź czy item jest numerem
                            {
                                    Zmienne.konsola.Add(pom + ""); //dodaj numer do konsoli
                            }
                            else
                            {
                                string[] subsPom = pom.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                if (subsPom.Count() == 2)
                                {
                                    bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                    bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                    if (isNumeric1 == true && isNumeric2 == true)
                                    {
                                            Zmienne.konsola.Add(pom + ""); //dodaj numer do konsoli
                                    }
                                    else
                                    {
                                        tymczasowyBlad("Źle wpisana liczba", linijka); break;
                                    }
                                }
                                else
                                {
                                    //wyszukiwanie zmiennej
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == item);
                                    if (index < 0)
                                    {
                                        tymczasowyBlad("Brak zmiennej o nazwie " + pom, linijka); break;
                                    }
                                    else
                                    {
                                            Zmienne.konsola.Add(Zmienne.wartoscZmiennej[index] + ""); //dodaj numer do konsoli
                                    }
                                }
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                    else
                    {
                        bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                        if (isNumeric) // sprawdź czy item jest numerem
                        {
                                Zmienne.konsola.Add(pom);
                        }
                    }
                }
                else //tutaj sprawdza te kolejne zmienne/wartości i je dodaje do tego ostatniego writelina
                {
                    if (pom.Length >= 2) // sprawdza czy są >=2 znaków w ciągu
                    {
                        if (pom[0] == '\'' && pom[pom.Length - 1] == '\'') // sprawdź czy na początku i końcu apostrofy ' '
                        {
                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom.Substring(1, pom.Length - 2);
                        }
                        else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'')
                        {
                            bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                            if (isNumeric) // sprawdź czy item jest numerem
                            {
                                Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                            }
                            else
                            {
                                string[] subsPom = pom.Split('.', '\t'); //tablica przechowujaca elementy oprocz .
                                if (subsPom.Count() == 2)
                                {
                                    bool isNumeric1 = int.TryParse(subsPom[0], out int nn);// sprawdź czy item jest numerem
                                    bool isNumeric2 = int.TryParse(subsPom[1], out int nnn);// sprawdź czy item jest numerem
                                    if (isNumeric1 == true && isNumeric2 == true)
                                    {
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                                    }
                                    else
                                    {
                                        tymczasowyBlad("Źle wpisana liczba", linijka); break;
                                    }
                                }
                                else
                                {
                                    //wyszukiwanie zmiennej
                                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == item);
                                    if (index < 0)
                                    {
                                        tymczasowyBlad("Brak zmiennej o nazwie " + pom, linijka); break;
                                    }
                                    else
                                    {
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + Zmienne.wartoscZmiennej[index];
                                    }
                                }
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                    else
                    {
                        bool isNumeric = int.TryParse(pom, out int n);// sprawdź czy item jest numerem
                        if (isNumeric) // sprawdź czy item jest numerem
                        {
                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom;
                        }
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
                        }
                    }
                }
                pomocnicza1++;
            }

            bool flaga1 = false;
            foreach (var item in Zmienne.bledy)
            {
                if (item.Contains(linijka + ":"))
                {
                    flaga1 = true;
                    break;
                }
            }
            if (flaga1 == true)
            {
                Zmienne.konsola.RemoveAt(Zmienne.konsola.Count - 1);
            }


        }






        public void InterpretujToString(String nazwaZmiennej, string wNawiasie, int linijka) //usp
        {
            //Zmienne.konsola.Add(nazwaZmiennej + " " + wNawiasie + " " + linijka);
            bool flaga = false;

            //przykladoweDane();

            int index = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej);
            if (index >= 0)
            {
                //flaga = true;
                if(Zmienne.typZmiennej[index] == "defuse")
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie != "" && wNawiasie != null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "defuse")
                                {
                                    //Zmienne.nazwaZmiennej[index]
                                    Zmienne.wartoscZmiennej[index] = Convert.ToString(Zmienne.wartoscZmiennej[index2]);
                                    //Zmienne.typZmiennej[index] = "defuse";
                                }
                                else if (Zmienne.typZmiennej[index2] == "defuse")
                                {
                                    tymczasowyBlad("Nie można zamienić stringa w stringa ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na stringa ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
                else
                {
                    tymczasowyBlad("Istnieje zmienna o nazwie " + nazwaZmiennej, linijka);
                }
            }
            else
            {
                if (zmienne.czyString2(nazwaZmiennej) == true) //
                {
                    //sprawdzanko czy nie jest np. knife knife
                    if (nazwaZmiennej == "ak47" || nazwaZmiennej == "knife" || nazwaZmiennej == "grenade" || nazwaZmiennej == "rifle" || nazwaZmiennej == "defuse" || nazwaZmiennej == "zeus" || nazwaZmiennej == "m4a1s" || nazwaZmiennej == "m4a4" || nazwaZmiennej == "usp" || nazwaZmiennej == "glock" || nazwaZmiennej == "tec" || nazwaZmiennej == "awp" || nazwaZmiennej == "scar" || nazwaZmiennej == "negev")
                    {
                        flaga = true;
                    }
                }
                if (flaga == true)
                {
                    Zmienne.bledy.Add(linijka + ": Nazwa zmiennej nie może być " + nazwaZmiennej);
                }
                else
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie !="" && wNawiasie!=null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "defuse")
                                {
                                    Zmienne.wartoscZmiennej.Add(Convert.ToString(Zmienne.wartoscZmiennej[index2]));
                                    Zmienne.typZmiennej.Add("defuse");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej);
                                }
                                else if (Zmienne.typZmiennej[index2] == "defuse")
                                {
                                    tymczasowyBlad("Nie można zamienić stringa w stringa ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na stringa ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje  zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
            }
        }


        public void InterpretujToInt(String nazwaZmiennej, string wNawiasie, int linijka) //usp
        {
            //Zmienne.konsola.Add(nazwaZmiennej + " " + wNawiasie + " " + linijka);
            bool flaga = false;

            //przykladoweDane();

            int index = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej);
            if (index >= 0)
            {
                //flaga = true;
                if (Zmienne.typZmiennej[index] == "knife")
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie != "" && wNawiasie != null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "knife" && Zmienne.typZmiennej[index2] != "zeus")
                                {
                                    //Zmienne.nazwaZmiennej[index]
                                    Zmienne.wartoscZmiennej[index] = Convert.ToInt32(Zmienne.wartoscZmiennej[index2]);
                                    //Zmienne.typZmiennej[index] = "defuse";
                                }
                                else if (Zmienne.typZmiennej[index2] == "knife")
                                {
                                    tymczasowyBlad("Nie można zamienić int w int ", linijka);
                                }
                                else if(Zmienne.typZmiennej[index2] == "zeus")
                                {
                                    tymczasowyBlad("Nie można zamienić bool w int ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na int ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
                else
                {
                    tymczasowyBlad("Istnieje zmienna o nazwie " + nazwaZmiennej, linijka);
                }
            }
            else
            {
                if (zmienne.czyString2(nazwaZmiennej) == true) //
                {
                    //sprawdzanko czy nie jest np. knife knife
                    if (nazwaZmiennej == "ak47" || nazwaZmiennej == "knife" || nazwaZmiennej == "grenade" || nazwaZmiennej == "rifle" || nazwaZmiennej == "defuse" || nazwaZmiennej == "zeus" || nazwaZmiennej == "m4a1s" || nazwaZmiennej == "m4a4" || nazwaZmiennej == "usp" || nazwaZmiennej == "glock" || nazwaZmiennej == "tec" || nazwaZmiennej == "awp" || nazwaZmiennej == "scar" || nazwaZmiennej == "negev")
                    {
                        flaga = true;
                    }
                }
                if (flaga == true)
                {
                    Zmienne.bledy.Add(linijka + ": Nazwa zmiennej nie może być " + nazwaZmiennej);
                }
                else
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie != "" && wNawiasie != null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "knife" && Zmienne.typZmiennej[index2] != "zeus")
                                {
                                    Zmienne.wartoscZmiennej.Add(Convert.ToInt32(Zmienne.wartoscZmiennej[index2]));
                                    Zmienne.typZmiennej.Add("knife");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej);
                                }
                                else if (Zmienne.typZmiennej[index2] == "knife")
                                {
                                    tymczasowyBlad("Nie można zamienić int w int ", linijka);
                                }
                                else if (Zmienne.typZmiennej[index2] == "zeus")
                                {
                                    tymczasowyBlad("Nie można zamienić bool w int ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na int ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje  zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
            }
        }

        public void InterpretujToDouble(String nazwaZmiennej, string wNawiasie, int linijka) //deagle
        {
            //Zmienne.konsola.Add(nazwaZmiennej + " " + wNawiasie + " " + linijka);
            bool flaga = false;

            //przykladoweDane();

            int index = Zmienne.nazwaZmiennej.FindIndex(c => c == nazwaZmiennej);
            if (index >= 0)
            {
                if (Zmienne.typZmiennej[index] == "grenade")
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie != "" && wNawiasie != null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "grenade" && Zmienne.typZmiennej[index2] != "zeus")
                                {
                                    //Zmienne.nazwaZmiennej[index]
                                    Zmienne.wartoscZmiennej[index] = Convert.ToDouble(Zmienne.wartoscZmiennej[index2], System.Globalization.CultureInfo.InvariantCulture);
                                    //Zmienne.typZmiennej[index] = "defuse";
                                }
                                else if (Zmienne.typZmiennej[index2] == "grenade")
                                {
                                    tymczasowyBlad("Nie można zamienić double w double ", linijka);
                                }
                                else if (Zmienne.typZmiennej[index2] == "zeus")
                                {
                                    tymczasowyBlad("Nie można zamienić bool w int ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na double ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
                else
                {
                    tymczasowyBlad("Istnieje zmienna o nazwie " + nazwaZmiennej, linijka);
                }
            }
            else
            {
                if (zmienne.czyString2(nazwaZmiennej) == true) //
                {
                    //sprawdzanko czy nie jest np. knife knife
                    if (nazwaZmiennej == "ak47" || nazwaZmiennej == "knife" || nazwaZmiennej == "grenade" || nazwaZmiennej == "rifle" || nazwaZmiennej == "defuse" || nazwaZmiennej == "zeus" || nazwaZmiennej == "m4a1s" || nazwaZmiennej == "m4a4" || nazwaZmiennej == "usp" || nazwaZmiennej == "glock" || nazwaZmiennej == "tec" || nazwaZmiennej == "awp" || nazwaZmiennej == "scar" || nazwaZmiennej == "negev")
                    {
                        flaga = true;
                    }
                }
                if (flaga == true)
                {
                    Zmienne.bledy.Add(linijka + ": Nazwa zmiennej nie może być " + nazwaZmiennej);
                }
                else
                {
                    if (zmienne.czyString2(wNawiasie) && wNawiasie != "" && wNawiasie != null)
                    {
                        int index2 = Zmienne.nazwaZmiennej.FindIndex(c => c == wNawiasie);
                        if (index2 >= 0)
                        {
                            try
                            {
                                if (Zmienne.typZmiennej[index2] != "grenade" && Zmienne.typZmiennej[index2] != "zeus")
                                {
                                    Zmienne.wartoscZmiennej.Add(Convert.ToDouble(Zmienne.wartoscZmiennej[index2], System.Globalization.CultureInfo.InvariantCulture));
                                    Zmienne.typZmiennej.Add("grenade");
                                    Zmienne.nazwaZmiennej.Add(nazwaZmiennej);
                                }
                                else if (Zmienne.typZmiennej[index2] == "grenade")
                                {
                                    tymczasowyBlad("Nie można zamienić double w double ", linijka);
                                }
                                else if (Zmienne.typZmiennej[index2] == "zeus")
                                {
                                    tymczasowyBlad("Nie można zamienić bool w double ", linijka);
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Nie można zamienić na int ", linijka);
                            }
                        }
                        else
                        {
                            tymczasowyBlad("Nie istnieje  zmienna o nazwie " + wNawiasie, linijka);
                        }
                    }
                }
            }
        }





        public void pokazKonsole(string komunikat, int linijka)
        {
            //Console.WriteLine("Wpisz wartość knife (int): "); //do poprawy
            IntPtr handle = GetConsoleWindow();
            if (handle == IntPtr.Zero)
            {
                AllocConsole();
                handle = GetConsoleWindow();
            }
            else
            {
                ShowWindow(handle, 5);
            }
            Console.WriteLine(linijka + komunikat);


        }

        public void ukryjKonsole()
        {
            var handle = GetConsoleWindow();

            try
            {
                Console.Clear();
            }
            catch
            {

            }

            ShowWindow(handle, 0);


        }




        public void InterpretujReadLine2(int linijka)
        {
            try
            {
                pokazKonsole("Wypisz na konsolę", linijka);
                dynamic temporary = Console.ReadLine(); // sczytywanie
                ukryjKonsole();
                Zmienne.konsola.Add(temporary);
            }
            catch
            {
                tymczasowyBlad("Błąd", linijka);
            }
        }

        public void InterpretujReadLine(string[] tempArray, int linijka) //ak47() lub typ zmienna = ak47()
        {
            tempArray[tempArray.Length - 1] = tempArray[tempArray.Length - 1].Remove(tempArray[tempArray.Length - 1].Length - 1);

            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*+`~ąśćźżółęń";

            //sprawdzanie czy element za knife jest stringiem
            char firstLetter = tempArray[1].FirstOrDefault();
            bool pom1 = false;

            if (Char.IsDigit(firstLetter))
            {
                tymczasowyBlad("Źle wpisana nazwa zmiennej", linijka);
            }
            else
            {
                foreach (var item in specialChar)
                {
                    if (tempArray[1].Contains(item))
                    {
                        pom1 = true;
                        break;
                    }
                }
                if (pom1 == true)
                {
                    tymczasowyBlad("Źle wpisana nazwa zmiennej", linijka);
                }
                else
                {
                    if (tempArray.Length == 4 && tempArray[0] == "knife" && tempArray[2] == "=" && tempArray[3] == "ak47()") //int
                    {
                        int index = Zmienne.nazwaZmiennej.FindIndex(c => c == tempArray[1]);
                        if (index >= 0)
                        {
                            tymczasowyBlad("Istnieje zmienna o nazwie " + tempArray[1], linijka);
                        }
                        else
                        {
                            pokazKonsole("Wpisz zmienną typu knife (int)", linijka);
                            dynamic temporary = Console.ReadLine(); // sczytywanie
                            ukryjKonsole();

                            try
                            {
                                int temporary2 = Convert.ToInt32(temporary);
                                Zmienne.typZmiennej.Add(tempArray[0]);
                                Zmienne.nazwaZmiennej.Add(tempArray[1]);
                                Zmienne.wartoscZmiennej.Add(temporary2);
                                Zmienne.konsola.Add(temporary2.ToString());
                            }
                            catch
                            {
                                tymczasowyBlad("Źle wpisana zmienna knife (int).", linijka);
                            }

                        }
                    }
                    else if (tempArray.Length == 4 && tempArray[0] == "grenade" && tempArray[2] == "=" && tempArray[3] == "ak47()") //double
                    {
                        int index = Zmienne.nazwaZmiennej.FindIndex(c => c == tempArray[1]);
                        if (index >= 0)
                        {
                            tymczasowyBlad("Istnieje zmienna o nazwie " + tempArray[1], linijka);
                        }
                        else
                        {
                            pokazKonsole("Wpisz zmienną typu grenade (double)", linijka);
                            dynamic temporary = Console.ReadLine(); // sczytywanie
                            ukryjKonsole();

                            try
                            {
                                double temporary2 = double.Parse(temporary, System.Globalization.CultureInfo.InvariantCulture);
                                Zmienne.typZmiennej.Add(tempArray[0]);
                                Zmienne.nazwaZmiennej.Add(tempArray[1]);
                                Zmienne.wartoscZmiennej.Add(temporary2);
                                Zmienne.konsola.Add(temporary2.ToString());
                            }
                            catch
                            {
                                tymczasowyBlad("Źle wpisana zmienna grenade (double)", linijka);
                            }
                        }
                    }
                    else if (tempArray.Length == 4 && tempArray[0] == "defuse" && tempArray[2] == "=" && tempArray[3] == "ak47()") //string
                    {
                        int index = Zmienne.nazwaZmiennej.FindIndex(c => c == tempArray[1]);
                        if (index >= 0)
                        {
                            tymczasowyBlad("Istnieje zmienna o nazwie " + tempArray[1], linijka);
                        }
                        else
                        {
                            pokazKonsole("Wpisz zmienną typu defuse (string)", linijka);
                            string temporary = Console.ReadLine(); // sczytywanie
                            ukryjKonsole();

                            try
                            {
                                //string temporary2 = temporary;
                                Zmienne.typZmiennej.Add(tempArray[0]);
                                Zmienne.nazwaZmiennej.Add(tempArray[1]);
                                Zmienne.wartoscZmiennej.Add(temporary);
                                Zmienne.konsola.Add(temporary);
                            }
                            catch
                            {
                                tymczasowyBlad("Źle wpisana zmienna defuse (string).", linijka);
                            }
                        }
                    }
                    else if (tempArray.Length == 4 && tempArray[0] == "zeus" && tempArray[2] == "=" && tempArray[3] == "ak47()") //bool
                    {
                        int index = Zmienne.nazwaZmiennej.FindIndex(c => c == tempArray[1]);
                        if (index >= 0)
                        {
                            tymczasowyBlad("Istnieje zmienna o nazwie " + tempArray[1], linijka);
                        }
                        else
                        {
                            pokazKonsole("Wpisz zmienną typu zeus (bool)", linijka);
                            string temporary = Console.ReadLine(); // sczytywanie
                            ukryjKonsole();

                            try
                            {
                                if (temporary == "terrorist")
                                {
                                    Zmienne.typZmiennej.Add(tempArray[0]);
                                    Zmienne.nazwaZmiennej.Add(tempArray[1]);
                                    Zmienne.wartoscZmiennej.Add("terrorist");
                                    Zmienne.konsola.Add("false");
                                }
                                else if (temporary == "antiterrorist")
                                {
                                    Zmienne.typZmiennej.Add(tempArray[0]);
                                    Zmienne.nazwaZmiennej.Add(tempArray[1]);
                                    Zmienne.wartoscZmiennej.Add("antiterrorist");
                                    Zmienne.konsola.Add("true");
                                }
                            }
                            catch
                            {
                                tymczasowyBlad("Źle wpisana zmienna zeus (bool).", linijka);
                            }
                        }
                    }
                }
            }








        }







        public void przykladoweDane()
        {

            //jakieś tam przykładowe dane
            Zmienne.typZmiennej.Add("knife");//int
            Zmienne.nazwaZmiennej.Add("test1");
            Zmienne.wartoscZmiennej.Add(2);

            Zmienne.typZmiennej.Add("grenade");//double
            Zmienne.nazwaZmiennej.Add("test2");
            Zmienne.wartoscZmiennej.Add(Convert.ToDouble(255));

            Zmienne.typZmiennej.Add("defuse");//string
            Zmienne.nazwaZmiennej.Add("test3");
            Zmienne.wartoscZmiennej.Add("tete a tete");

            Zmienne.typZmiennej.Add("zeus");//bool
            Zmienne.nazwaZmiennej.Add("test4");
            Zmienne.wartoscZmiennej.Add(true);

            Zmienne.typZmiennej.Add("defuse");//string
            Zmienne.nazwaZmiennej.Add("test5");
            Zmienne.wartoscZmiennej.Add("46");

            Zmienne.typZmiennej.Add("defuse");//string
            Zmienne.nazwaZmiennej.Add("test6");
            Zmienne.wartoscZmiennej.Add("55.25");
        }











        public static void tymczasowyBlad(String komunikat, int linijka)
        {
            //Zmienne.konsola.Clear();
            Zmienne.bledy.Add(linijka + ": " + komunikat);
        }






    }
}