using ProjektSSIW.Interpreter;
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
        /*    listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            listView4.Clear();
            listView5.Clear();
            listView6.Clear();*/



            if (tempArray.Count() <= 0) // sprawdzenie czy tablica jest pusta
            {
                Zmienne.bledy.Add("Nie wpisałeś żadnego kodu.");
            }
            else
            {
                int size = tempArray.Length; // pobieram długość tablicy 

                if (tempArray[0] == "rush" && tempArray[size - 1] == "save") // sprawdzam czy na poczatku jest 'rush' a na końcu 'save'
                {
                    petle.sprawdzenieOtwarcia(tempArray);

                    for (int i = 1; i < tempArray.Length - 1; i++)
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
                            if (pom == "}"|| tab.Length==2)
                            {
                                czyBylo = true;
                                czyBylo2 = true;
                            }
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
                        if(czyBylo2 == false)
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
                                if (tab[0] == "knife" ) 
                                {
                                    if (tab[dlugosc - 1].EndsWith(";"))//zabezpieczenie w przypadku braku srednika
                                    {
                                        string pomknife = "";
                                        for (int jk = 3; jk < tab.Length; jk++)
                                        {
                                            pomknife = pomknife + tab[jk];
                                        }
                                        zmienne.TomaszowyInt(tab[1], pomknife, i);
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add(i + ": Brak średnika na końcu.");
                                    }
                                }
                                //czy double
                                if (tab[0] == "grenade") //&& tab[tab.Length - 1].EndsWith(";"))
                                {

                                    if (tab[dlugosc - 1].EndsWith(";"))
                                    {
                                        string prawaStrona = "";
                                        for (int tmp = 3; tmp < tab.Length; tmp++)
                                        {
                                            prawaStrona = prawaStrona + tab[tmp];
                                        }
                                        zmienne.BartkowyDouble(tab[1], prawaStrona, i);
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add(i + ": Brak średnika na końcu.");
                                    }
                                }
                                if (tab[0] == "defuse" )//&& tab[tab.Length - 1].EndsWith(";"))
                                {
                                    if (tab[dlugosc - 1].EndsWith(";"))
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
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add(i + ": Brak średnika na końcu.");
                                    }
                                }
                                //czy bool
                                if (tab[0] == "zeus" )// && tab[dlugosc - 1].EndsWith(";"))
                                {
                                    if (tab[dlugosc - 1].EndsWith(";"))
                                    {
                                        string prawaStrona = "";
                                        for (int tmp = 3; tmp < tab.Length; tmp++)
                                        {
                                            prawaStrona = prawaStrona + tab[tmp];
                                        }
                                        zmienne.BartkowyBoolean(tab[1], prawaStrona, i);
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add(i + ": Brak średnika na końcu.");
                                    }
                                }
                              
                            }


                        }




                        petle.InterpretujPetle(tempArray, i); // Emil

                        //zmienne.InterpretujZmienne(tempArray, i);



                    }









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