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

                if (tempArray[0] == "rush" && tempArray[size - 1] == "save") // sprawdzam czy na poczatku jest 'rush' a na końcu 'save'
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
                        if (char_arr[char_arr.Length - 1] == ')') // jeżeli ostatni znak w tym stringu to )
                        {
                            bool test1 = false;//flaga czy jest (
                            bool test2 = false;//flaga czy jest )
                            bool test3 = false;//sprawdź czy najpierw jest ( a później )
                            String[] test4 = new String[2];
                            test4[0] = ""; test4[1] = "";//
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
                                    test4[0] = test4[0] + item;
                                }
                                if (test2 == true && test1 == false) // jeżeli najpierw znajdzie w ciągu jest ) a później ( to błąd
                                {
                                    test3 = false;
                                    label4.Text = "blad1";
                                    break;
                                }
                                if (test1 == true && test2 == false) // wpisuje do stringa nazwę funkcji
                                {
                                    if (item != '(') //ten if żeby nie wypisywało "(nazwa" tylko samo "nazwa"
                                    {
                                        test4[1] = test4[1] + item;
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
                                string specialChar2 = @"\|!#$%&/()=?»«@£§€{}.-;<>_,*`~ąśćźżółęń";

                                char firstLetter2 = test4[0].FirstOrDefault();

                                if (Char.IsDigit(firstLetter2))
                                {
                                    czyGitZmienna2 = false;
                                }
                                else
                                {
                                    foreach (var item in specialChar2)
                                    {
                                        if (test4[1].Contains(item))
                                        {
                                            czyGitZmienna2 = false;
                                        }
                                    }
                                }

                                if (czyGitZmienna2 == true)
                                {
                                    switch (test4[0]) //przekazywanie do metody funkcji
                                    {
                                        /*
                                        case "ak47":
                                            funkcje.InterpretujReadLine(subs, subs.Length); //i dalej tutaj będzie robione
                                            break;
                                        */
                                        case "m4a1s":
                                            funkcje.InterpretujWriteLine(test4, subs.Length);
                                            break;
                                        case "m4a4":
                                            funkcje.InterpretujWrite(test4, subs.Length);
                                            break;
                                        /*
                                    case "usp":
                                        funkcje.InterpretujToString(tempArray, subs.Length);
                                        break;
                                    case "glock":
                                        funkcje.InterpretujToInt(tempArray, subs.Length);
                                        break;
                                    case "tec":
                                        funkcje.InterpretujToFloat(tempArray, subs.Length);
                                        break;
                                        */
                                        default:
                                            label4.Text = "Taka funkcja nie istnieje";
                                            break;

                                    }
                                    label4.Text = test4[0] + " " + test4[1];
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

                   

                    petle.InterpretujPetle(tempArray);
                   


                     /*   foreach (var sub in Zmienne.konsola)
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