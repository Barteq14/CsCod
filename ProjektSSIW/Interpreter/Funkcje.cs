﻿using System;
using System.Linq;

namespace ProjektSSIW.Interpreter
{
    /// <summary>
    /// Defines the <see cref="Funkcje" />.
    /// </summary>
    public class Funkcje
    {

        Składnia składnia = new Składnia();
        Zmienne zmienne = new Zmienne();

        /*
        public void InterpretujFunkcje(string[] tempArray)
        {
            for (int i = 1; i < tempArray.Length; i++)
            {
                string[] tab = tempArray[i].Split(' ');

                switch (tab[0])
                {
                    case "ak47":
                        InterpretujReadLine(tempArray, i);
                        break;
                    case "m4a1s":
                        InterpretujWriteLine(tempArray, i);
                        break;
                    case "m4a4":
                        InterpretujWrite(tempArray, i);
                        break;
                    case "usp":
                        InterpretujToString(tempArray, i);
                        break;
                    case "glock":
                        InterpretujToInt(tempArray, i);
                        break;
                    case "tec":
                        InterpretujToFloat(tempArray, i);
                        break;
                }
            }
        }
        */


        public void InterpretujWrite(string tempArray, int linijka)//m4a1
        {
            //temp
            if (Zmienne.typZmiennej.Count == 0)
            {
                przykladoweDane();
            }


            string[] subs = tempArray.Split('+', '\t'); //tablica przechowujaca elementy oprocz +
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
                                        else
                                        {
                                            Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + Zmienne.wartoscZmiennej[index];
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
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom.Substring(1, pom.Length - 2);
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
        }




        public void InterpretujWriteLine(string tempArray, int linijka)//m4a1s
        {
            //temp
            if (Zmienne.typZmiennej.Count == 0)
            {
                przykladoweDane();
            }


            string[] subs = tempArray.Split('+', '\t'); //tablica przechowujaca elementy oprocz +
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
                                        Zmienne.konsola.Add(pom + "");
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
                                        Zmienne.konsola.Add(Zmienne.wartoscZmiennej[index] + "");
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
                        else
                        {
                            tymczasowyBlad("Źle wpisana wartość/zmienna", linijka); break;
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
                                        Zmienne.konsola[Zmienne.konsola.Count - 1] = Zmienne.konsola[Zmienne.konsola.Count - 1] + pom.Substring(1, pom.Length - 2);
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
        }






        public void InterpretujReadLine(string[] tempArray, int linijka)
        {


            //zmienne.konsola.Add("test");




        }

        public void InterpretujToFloat(string[] tempArray, int linijka)
        {
            throw new NotImplementedException();
        }

        public void InterpretujToInt(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public void InterpretujToString(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public static void przykladoweDane()
        {

            //jakieś tam przykładowe dane
            Zmienne.typZmiennej.Add("knife");//int
            Zmienne.nazwaZmiennej.Add("test1");
            Zmienne.wartoscZmiennej.Add(2);

            Zmienne.typZmiennej.Add("grenade");//double
            Zmienne.nazwaZmiennej.Add("test2");
            Zmienne.wartoscZmiennej.Add(5);

            Zmienne.typZmiennej.Add("rifle");//float
            Zmienne.nazwaZmiennej.Add("test3");
            Zmienne.wartoscZmiennej.Add("tete a tete");

            Zmienne.typZmiennej.Add("defuse");//string
            Zmienne.nazwaZmiennej.Add("test4");
            Zmienne.wartoscZmiennej.Add("tete a tete");

            Zmienne.typZmiennej.Add("zeus");//bool
            Zmienne.nazwaZmiennej.Add("test5");
            Zmienne.wartoscZmiennej.Add("true");
        }

        public static void tymczasowyBlad(String komunikat, int linijka)
        {
            //Zmienne.konsola.Clear();
            Zmienne.bledy.Add(linijka + ": " + komunikat);
        }





    }
}