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
        ProjektSSIW.Interpreter.Petle petle = new ProjektSSIW.Interpreter.Petle();
        Składnia składnia = new Składnia();
        Zmienne zmienne = new Zmienne();
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

                    for(int i =0;i<size;i++)
                    {
                        string[] subs2 = tempArray[i].Split(' ', '(' ,'\t'); //tablica przechowujaca elementy oprocz ' '
                        int size1 = subs2.Length;
                        for (int j = 0; j < size1; j++)
                        {
                            listView3.Items.Add(subs2[j]);
                        }
                     /*   switch (subs2[0])
                        {
                            case "awp":
                                  petle.fore(tempArray[i]);
                                  listView3.Items.Add(subs2[0]);
                                break;
                            case "scar":
                                petle.ife(tempArray[i]);
                             
                                break;
                            case "negev":
                                petle.wailee(tempArray[i]);
                              
                                break;



                               

                        }*/



                    }
                        /*
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