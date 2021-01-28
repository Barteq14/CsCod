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
        public static int liniaKoncaWarunku = 0;
        public delegate void Delegat(string phrase);
        Petle petle = new Petle();
        //  Składnia składnia = new Składnia();
        //  Zmienne zmienne = new Zmienne();
        //Funkcje funkcje = new Funkcje();
      public static  List<int> klamraOtwierajace = new List<int>();
       public static List<int> klamraZamykajace = new List<int>();


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
                    Interpretacja inter = new Interpretacja();

                    for (int i = 1; i < tempArray.Length - 1; i++)
                    {

                        if( liniaKoncaWarunku < i)
                        {
                            //  if (liniaKoncaWarunku < tempArray.Length - 2)
                            //  {

                            inter.interpretuj(tempArray, i);
                          //  i = liniaKoncaWarunku;
                         //   }else
                        }
                       

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
            liniaKoncaWarunku = 0;
            klamraOtwierajace.Clear();
            klamraZamykajace.Clear();
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