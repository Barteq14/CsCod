using ProjektSSIW.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW
{
    public class Interpretacja
    {
       // Składnia składnia = new Składnia();
         Zmienne zmienne = new Zmienne();
        Funkcje funkcje = new Funkcje();
        Sprawdzenie spr = new Sprawdzenie();
        public void interpretuj(string[] tempArray,int i)
        {
            string pom = tempArray[i];
            string[] tab = tempArray[i].Split(' ');
            int dlugosc = tab.Length;
            //WRITE WRITELN
            //Funkcje sprawdzanie czy jest tylko 1 ciąg w linijce, przydatne do write i writeln tylko
            //if (subs.Length == 1) //
            //{
            //Zmienne.konsola.Add(tab[3].Length.ToString());



            bool czyBylo = false;
            bool czyBylo2 = false;
            if (tab.Length == 1)
            {

                if (tab[0] == "ak47();")
                {
                    funkcje.InterpretujReadLine2(i);
                    czyBylo = true;
                    czyBylo2 = true;
                }
                if (pom == "}" || tab.Length == 2)
                {
                    czyBylo = true;
                    czyBylo2 = true;
                }
                if (tab.Length > 2)
                {
                    if (tab[3].Length < 4)
                    {
                        czyBylo2 = true;
                    }
                }
            }
            if (pom.Contains("awp")|| pom.Contains("negev") || tab.Length<3)
            {
                czyBylo = true;
                czyBylo2 = true;
            }
            if (pom.Length >= 5)
            {
                if (pom.Substring(0, 6) == "m4a1s(" && pom[pom.Length - 2] == ')' && pom.EndsWith(";"))
                {
                    String pomWnawiasach = pom.Substring(6, pom.Length - 7 - 1);
                    funkcje.InterpretujWriteLine(pomWnawiasach, i);
                    czyBylo2 = true;
                }
                else if (pom.Substring(0, 5) == "m4a1(" && pom[pom.Length - 2] == ')' && pom.EndsWith(";"))
                {
                    String pomWnawiasach = pom.Substring(5, pom.Length - 6 - 1);
                    funkcje.InterpretujWrite(pomWnawiasach, i);
                    czyBylo2 = true;
                }
            }
            if (tab.Length == 4 && tab[3] == "ak47();")
            {
                funkcje.InterpretujReadLine(tab, i);
            }
            if (czyBylo2 == false)
            {
                if (tab[3].Length >= 6)
                {
                    if (tab[0] == "knife" && tab[tab.Length - 1].EndsWith(";") && !(tab[3] == "ak47();") && (tab[3].Substring(0, 6) == "glock(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";")))
                    {
                        string pomocnicza = tab[3].Substring(6, tab[3].Length - 7 - 1);
                        funkcje.InterpretujToInt(tab[1], pomocnicza, i);
                        czyBylo = true;
                    }
                }
                if (tab[3].Length >= 4)
                {
                    if (tab[0] == "defuse" && tab[tab.Length - 1].EndsWith(";") && !(tab[3] == "ak47();") && (tab[3].Substring(0, 4) == "usp(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";")))
                    {
                        string pomocnicza = tab[3].Substring(4, tab[3].Length - 5 - 1);
                        funkcje.InterpretujToString(tab[1], pomocnicza, i);
                        czyBylo = true;
                    }
                }
                if (tab[3].Length >= 7)
                {
                    //czy double
                    if (tab[3].Length >= 7 && tab[0] == "grenade" && tab[tab.Length - 1].EndsWith(";") && !(tab[3] == "ak47();") && (tab[3].Substring(0, 7) == "deagle(" && tab[3][tab[3].Length - 2] == ')' && tab[3].EndsWith(";")))
                    {
                        string pomocnicza = tab[3].Substring(7, tab[3].Length - 8 - 1);
                        funkcje.InterpretujToString(tab[1], pomocnicza, i);
                        czyBylo = true;
                    }
                }


                if (czyBylo == false)
                {
                    if (tab[0] == "knife" && tab[tab.Length - 1].EndsWith(";"))
                    {
                        string pomknife = "";
                        for (int jk = 3; jk < tab.Length; jk++)
                        {
                            pomknife = pomknife + tab[jk];
                        }
                        zmienne.TomaszowyInt(tab[1], pomknife, i);
                        //czyBylo = true;
                    }
                    //czy double
                    if (tab[0] == "grenade" && tab[tab.Length - 1].EndsWith(";"))
                    {
                        string prawaStrona = "";
                        for (int tmp = 3; tmp < tab.Length; tmp++)
                        {
                            prawaStrona = prawaStrona + tab[tmp];
                        }
                        zmienne.BartkowyDouble(tab[1], prawaStrona, i);
                        //czyBylo = true;
                    }
                    if (tab[0] == "defuse" && tab[tab.Length - 1].EndsWith(";"))
                    {
                        string prawaStrona = "";
                        for (int tmp = 3; tmp < tab.Length; tmp++)
                        {
                            if (prawaStrona != "")
                            {
                                prawaStrona = prawaStrona + " " + tab[tmp];
                            }
                            else
                            {
                                prawaStrona = tab[tmp];
                            }
                        }
                        zmienne.BartkowyString(tab[1], prawaStrona, i);
                        //czyBylo = true;
                    }
                    //czy bool
                    if (tab[0] == "zeus" && tab.Length > 3 && tab[dlugosc - 1].EndsWith(";"))
                    {

                        string prawaStrona = "";
                        for (int tmp = 3; tmp < tab.Length; tmp++)
                        {
                            prawaStrona = prawaStrona + tab[tmp];
                        }
                        zmienne.BartkowyBoolean(tab[1], prawaStrona, i);
                        //czyBylo = true;
                    }
                }


            }




           spr.InterpretujPetle(tempArray, i); // Emil

            //zmienne.InterpretujZmienne(tempArray, i);



        }
    }
}
