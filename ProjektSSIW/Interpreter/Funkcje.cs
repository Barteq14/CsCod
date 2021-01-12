using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    public class Funkcje
    {
        Składnia składnia = new Składnia();
        Zmienne zmienne = new Zmienne();

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



        public void InterpretujWrite(string[] tempArray, int linijka)
        {
            string pom = tempArray[1];
            string[] subs = pom.Split(' ', '\t'); //tablica przechowujaca elementy oprocz ' '

            //temp
            if (Zmienne.typZmiennej.Count == 0)
            {
                przykladoweDane();
            }



            if (subs.Length == 1)//jeżeli w nawiasie jeden string to
            {
                if (pom[0] == '\'' && pom[pom.Length - 1] == '\'') // sprawdź czy na początku i końcu apostrofy ' '
                {
                    if(Zmienne.konsola.Count == 0)// jeżeli nie ma nic w konsoli to dodaje linijke
                    {
                        Zmienne.konsola.Add(pom.Substring(1, pom.Length - 2)); // dodaj do konsoli
                    }
                    else////jeżeli jest to nadpisuje
                    {
                        Zmienne.konsola[Zmienne.konsola.Count-1] = Zmienne.konsola[Zmienne.konsola.Count-1] + pom.Substring(1, pom.Length - 2);
                    }
                }
                else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'')
                {
                    //wyszukiwanie zmiennej
                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == pom);
                    if (index < 0)
                    {
                        Zmienne.konsola.Clear();
                        Zmienne.bledy.Add(linijka + ": Nie ma zmiennej z m4a1");
                    }
                    else
                    {
                        if (Zmienne.konsola.Count == 0) // jeżeli nie ma nic w konsoli to dodaje linijke
                        {
                            Zmienne.konsola.Add(Zmienne.wartoscZmiennej[index] + "");
                        }
                        else //jeżeli jest to nadpisuje
                        {
                            Zmienne.konsola[Zmienne.konsola.Count-1] = Zmienne.konsola[Zmienne.konsola.Count-1] + Zmienne.wartoscZmiennej[index]+"";
                        }
                    }
                }
                else
                {
                    Zmienne.konsola.Clear();
                    Zmienne.bledy.Add(linijka + ": Źle wpisana zmienna/wartość w m4a1");
                }
            }
            else //jeżeli więcej niż jeden string to będzie sprawdzać dalej
            {
                foreach (var item in subs)
                {

                }
            }



        }


        public void InterpretujWriteLine(string[] tempArray, int linijka)
        {

            //temp
            if (Zmienne.typZmiennej.Count == 0)
            {
                przykladoweDane();
            }


            string pom = tempArray[1];
            string[] subs = pom.Split(' ', '\t'); //tablica przechowujaca elementy oprocz ' '


            if (subs.Length == 1)//jeżeli w nawiasie jeden string to
            {
                if (pom[0]== '\'' && pom[pom.Length-1]== '\'') // sprawdź czy na początku i końcu apostrofy ' '
                {
                    Zmienne.konsola.Add(pom.Substring(1, pom.Length - 2)); // dodaj do konsoli
                }
                else if (pom[0] != '\'' && pom[pom.Length - 1] != '\'')
                {
                    //wyszukiwanie zmiennej
                    int index = Zmienne.nazwaZmiennej.FindIndex(c => c == pom);
                    if (index < 0)
                    {
                        Zmienne.konsola.Clear();
                        Zmienne.bledy.Add(linijka +": Nie ma zmiennej z m4a1s");
                    }
                    else
                    {
                        Zmienne.konsola.Add(Zmienne.wartoscZmiennej[index]+"");
                    }
                }
                else
                {
                    Zmienne.konsola.Clear();
                    Zmienne.bledy.Add(linijka +": Źle wpisana zmienna/wartość w m4a1s"); 
                }
            }
            else //jeżeli więcej niż jeden string to będzie sprawdzać dalej
            {
                foreach (var item in subs)
                {

                }
            }

            

            


                //Zmienne.konsola.Add(item);


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
            Zmienne.wartoscZmiennej.Add("tete a tete");
        }


    }
}
