﻿using ProjektSSIW.Interpreter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjektSSIW
{
    public partial class Form1 : Form
    {
        public delegate void Delegat(string phrase);
        Petle petle = new Petle();
        Składnia składnia = new Składnia();
        Zmienne zmienne = new Zmienne();
        Funkcje funkcje = new Funkcje();


        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tworzymy tablicę naszych elementów z richTextBoxa
            string[] tempArray = richTextBox1.Lines;
            string[] array = richTextBox1.Lines;
            List<string> pomLista = new List<string>();


            //tymczasowe do sprawdzania
            //funkcje.przykladoweDane();
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            listView4.Clear();
            listView5.Clear();
            listView6.Clear();



            if (tempArray.Count() <= 0) // sprawdzenie czy tablica jest pusta
            {
                Zmienne.bledy.Add("Nie wpisałeś żadnego kodu.");
            }
            else
            {
                int size = tempArray.Length; // pobieram długość tablicy 

                if (tempArray[0] == "rush" && tempArray[size - 1] == "save") // sprawdzam czy na poczatku jest 'rush' a na końcu 'save'
                {


                    for (int i = 1; i < tempArray.Length - 1; i++)
                    {
                        string pom = tempArray[i];
                        string[] tab = tempArray[i].Split(' ');
                        int dlugosc = tab.Length;
                        //WRITE WRITELN
                        //Funkcje sprawdzanie czy jest tylko 1 ciąg w linijce, przydatne do write i writeln tylko
                        //if (subs.Length == 1) //
                        //{
                        if (pom.Length >= 5)
                        {
                            if (pom.Substring(0, 6) == "m4a1s(" && pom.EndsWith(")"))
                            {
                                String pomWnawiasach = pom.Substring(6, pom.Length - 7);
                                funkcje.InterpretujWriteLine(pomWnawiasach, i);
                            }
                            else if (pom.Substring(0, 5) == "m4a1(" && pom.EndsWith(")"))
                            {
                                String pomWnawiasach = pom.Substring(5, pom.Length - 6);
                                funkcje.InterpretujWrite(pomWnawiasach, i);
                            }
                        }
                        if (tab.Length == 4 && tab[3] == "ak47()")
                        {
                            funkcje.InterpretujReadLine(tab, i);
                        }
                        else if (tab.Length == 1 && tab[0] == "ak47()")
                        {
                            funkcje.InterpretujReadLine2(i);
                        }
                        //czy int 
                        if (tab[0] == "knife" && tab.Length == 4 && tab[3].EndsWith(";"))
                        {
                            string pomknife = "";
                            for (int jk = 3; jk < tab.Length; jk++)
                            {
                                pomknife = pomknife + tab[jk];
                            }
                            zmienne.TomaszowyInt(tab[1], pomknife, i);
                        }

                        //czy double
                        if (tab[0] == "grenade" && tab.Length == 4 && tab[3].EndsWith(";"))
                        {
                            string prawaStrona = "";
                            for (int tmp = 3; tmp < tab.Length; tmp++)
                            {
                                prawaStrona = prawaStrona + tab[tmp];
                            }
                            zmienne.BartkowyDouble(tab[1], prawaStrona, i);
                        }
                        
                        // czy string
                        if (tab[0] == "defuse" && tab.Length > 3 && tab[dlugosc-1].EndsWith(";"))
                        {
                            string prawaStrona = "";
                            for (int tmp = 3; tmp < tab.Length; tmp++)
                            {
                                prawaStrona = prawaStrona + " " + tab[tmp];
                            }
                            zmienne.BartkowyString(tab[1], prawaStrona, i);
                        }//dorobic kiedy zadeklaruje sie zmienna bez wartosci

                        //czy bool
                        if (tab[0] == "zeus" && tab.Length > 3 && tab[dlugosc - 1].EndsWith(";"))
                        {
                            
                            string prawaStrona = "";
                            for (int tmp = 3; tmp < tab.Length; tmp++)
                            {
                                prawaStrona = prawaStrona + tab[tmp];
                            }
                            zmienne.BartkowyBoolean(tab[1], prawaStrona,  i);
                        }


                        petle.InterpretujPetle(tempArray, i);

                        //zmienne.InterpretujZmienne(tempArray, i);



                    }





                    /*
                     rush
knife liczba = 21+3;
grenade zmienna = (2.3+1)+liczba;
defuse napis = "siemanko";
save
                     
                     
                     */





                    /*
                                        for(int i = 0; i < tempArray.Length; i++)
                                        {
                                            if (tempArray[i].Contains("knife") || tempArray[0] == "knife")//INT
                                            {
                                                zmienne.InterpretujInt2(tempArray, i);
                                            }
                                            if (tempArray[i].Contains("grenade"))//FLOAT
                                            {

                                            }
                                            if (tempArray[i].Contains("defuse"))//STRING
                                            {
                                                zmienne.InterpretujString(tempArray, i);
                                            }
                                            if (tempArray[i].Contains("zeus") || tempArray[0] == "zeus")
                                            {
                                                zmienne.InterpretujBoolean(tempArray,i);
                                            }
                                        }
                                        */






                    //wypisywanie zmiennych typów, nazw, wartości i prawdziwych wartości
                    foreach (var sub in Zmienne.typZmiennej)
                    {
                        listView1.Items.Add(sub);
                    }
                    foreach (var sub in Zmienne.nazwaZmiennej)
                    {
                        listView4.Items.Add(sub);
                    }
                    foreach (var sub in Zmienne.wartoscZmiennej)
                    {
                        listView5.Items.Add(sub + "");
                        listView6.Items.Add(sub.GetType() + "");
                    }
                    //dodawanie do konsoli
                    foreach (var sub in Zmienne.konsola)
                    {
                        listView2.Items.Add(sub);
                    }
                    //dodawanie do bledów
                    foreach (var p in Zmienne.bledy)
                    {
                        listView3.Items.Add(p);
                    }






                }

            }


        }

        private void button2_Click(object sender, EventArgs e) //czyszczenie 
        {
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            listView4.Clear();
            listView5.Clear();
            listView6.Clear();
            Zmienne.konsola.Clear();
            Zmienne.typZmiennej.Clear();
            Zmienne.nazwaZmiennej.Clear();
            Zmienne.wartoscZmiennej.Clear();
            Zmienne.bledy.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
/*
•	Obsługa podstawowych operacji arytmetycznych +, -, *, /.
•	Wykonywanie przynajmniej jednego rodzaju pętli,
•	Wykonywanie przynajmniej jednej instrukcji warunkowej,
•	Obsługa zmiennych i kilku typów danych,
•	Interakcja z użytkownikiem za pomocą:
o	Klawiatury – do wprowadzania danych (liczb, ciągów znaków),
o	Konsoli – do informowania użytkownika o wynikach operacji.
 */