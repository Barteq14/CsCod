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
        ProjektSSIW.Interpreter.Petle petle = new ProjektSSIW.Interpreter.Petle();
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
            //string[] errors = new string[10];
            List<String> error = new List<string>(); // lista błędów jakie mogą wyskoczyć
            error.Add("błąd składni");
            error.Add("brak średnika na końcu!");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            

            
            if (tempArray.Count() <= 0) // sprawdzenie czy tablica jest pusta
            {
                label4.Text = "Nie wpisałeś żadnego kodu.";
            }
            else 
            {
                int size = tempArray.Length; // pobieram długość tablicy 

                if (tempArray[0] == "rush" && tempArray[size-1] == "save") // sprawdzam czy na poczatku jest 'rush' a na końcu 'save'
                {
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        label4.Text = "Wszystko jest OK";
                        listView1.Items.Add(tempArray[i]);
                    }
                    string pom = tempArray[1];
                    string[] subs = pom.Split(' ', '\t'); //tablica przechowujaca elementy oprocz ' '
                    string[] subs1 = pom.Split('(', ')', '+', '\t'); //tablica przechowujaca elementy oprocz '(' , ')' oraz '+'
                    /*
                    string knife = "knife";
                    
                    if (subs.Contains(knife))
                    {
                        textBox1.Text = knife;
                        
                        //then dodajemy
                    }
                    */




                    //Funkcje sprawdzanie czy jest tylko 1 ciąg w linijce
                    if (subs.Length == 1) //
                    {
                        char[] char_arr = subs[0].ToCharArray();//zamieniam stringa na tablicę charów
                        if (char_arr[char_arr.Length-1] == ')') // jeżeli ostatni znak w tym stringu to )
                        {
                            bool test1 = false;//flaga czy jest (
                            bool test2 = false;//flaga czy jest )
                            bool test3 = false;//sprawdź czy najpierw jest ( a później )
                            String pomocniczaIndexNazwaTypu=""; //nazwa operacji funkcji przed ( np. jak będzie ak47(test) no to tutaj będzie ak47
                            String pomocniczaIndexNazwaZmiennej = ""; // nazwa zmiennej funkcji
                            foreach (var item in char_arr) //sprawdzam wszystkie znaki tego stringa
                            {
                                if (item == '(') // flaga (
                                {
                                    test1 = true;
                                }
                                if (item == ')') //flaga )
                                {
                                    test2 = true;
                                }
                                if (test1 == false && test2 == false) //wpisuje do stringa jaka operacje funkcji
                                {
                                    pomocniczaIndexNazwaTypu = pomocniczaIndexNazwaTypu + item;
                                }
                                if (test2 == true && test1 == false) // jeżeli najpierw znajdzie w ciągu jest ) a później ( to błąd
                                {
                                    test3 = false;
                                    label4.Text = "blad1";
                                    break;
                                }
                                if (test1 == true && test2== false) // wpisuje do stringa nazwę funkcji
                                {
                                    if (item != '(') //ten if żeby nie wypisywało "(nazwa" tylko samo "nazwa"
                                    {
                                        pomocniczaIndexNazwaZmiennej = pomocniczaIndexNazwaZmiennej + item;
                                    }
                                }
                                if (test1 == true && test2 == true) // jeżeli wszystko ok to jest flaga ustawiana
                                {
                                    test3 = true;
                                    break;
                                }
                                
                            }
                            if (test3 != true)
                            {
                                label4.Text = "Źle wpisana nazwa zmiennej funkcji"; // komunikat
                            }
                            else
                            {
                                Boolean czyGitZmienna2 = true; // flaga
                                string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*+`~ąśćźżółęń";

                                char firstLetter = pomocniczaIndexNazwaZmiennej.FirstOrDefault();

                                if (Char.IsDigit(firstLetter))
                                {
                                    czyGitZmienna2 = false;
                                }
                                else
                                {
                                    foreach (var item in specialChar)
                                    {
                                        if (pomocniczaIndexNazwaZmiennej.Contains(item))
                                        {
                                            czyGitZmienna2 = false;
                                        }
                                    }
                                }
                                
                                if (czyGitZmienna2 == true)
                                {
                                    switch (pomocniczaIndexNazwaTypu) //przekazywanie do metody funkcji
                                    {
                                        case "ak47":
                                            funkcje.InterpretujReadLine(subs, subs.Length); //i dalej tutaj będzie robione
                                            break;
                                        case "m4a1s":
                                            funkcje.InterpretujReadLine(subs, subs.Length);
                                            break;
                                        case "m4a4":
                                            funkcje.InterpretujWrite(tempArray, subs.Length);
                                            break;
                                        case "usp":
                                            funkcje.InterpretujToString(tempArray, subs.Length);
                                            break;
                                        case "glock":
                                            funkcje.InterpretujToInt(tempArray, subs.Length);
                                            break;
                                        case "tec":
                                            funkcje.InterpretujToFloat(tempArray, subs.Length);
                                            break;
                                        default:
                                            label4.Text = "Taka funkcja nie istnieje";
                                            break;

                                    }
                                    label4.Text = pomocniczaIndexNazwaTypu + " " + pomocniczaIndexNazwaZmiennej;
                                }
                                else
                                {
                                    label4.Text = "Zła nazwa zmiennej";
                                }
                            }
                        }
                        else
                        {
                            label4.Text = "Źle wpisana funkcja";//komunikat
                        }
                    }


                    for(int i =0;i<size;i++)
                    {
                        string[] subs2 = tempArray[i].Split(' ', '(' ,'\t'); //tablica przechowujaca elementy oprocz ' '
                       
                       
                        switch (subs2[0])


                    
                        {
                            case "awp":
                               string[] s= petle.fore(tempArray, i);
                                
                                for(int j = 0; j < s.Length; j++)
                                {
                                    listView3.Items.Add(s[j]);
                                }
                                break;
                            case "scar":
                                petle.ife(tempArray,i);
                             
                                break;
                            case "negev":
                                petle.wailee(tempArray,i);
                              
                                break;



                               

                        }



                    }
                        /*foreach (var sub in subs)
                        string knife = "knife";

                        if (subs.Contains(knife))
                        {
                            textBox1.Text = knife;

                            //then dodajemy
                        }
                        */
                /*        foreach (var sub in subs)
                    {
                        listView2.Items.Add(sub);

                    }
                    foreach (var p in subs1)
                    {
                        listView3.Items.Add(p);
                    }*/
                }
                else
                {
                    label4.Text = error[0];
                }
          
            }
            zmienne.InterpretujZmienne(tempArray);
         
        }

        private void button2_Click(object sender, EventArgs e) //czyszczenie 
        {
            label4.Text = "";
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            //richTextBox1.Clear();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
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