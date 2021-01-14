using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSSIW.Interpreter
{
    class Petle : Składnia
    {
        Zmienne zmienne = new Zmienne();
        public void InterpretujPetle(string[] tempArray)
        {
            Form1 ff = new Form1();

            for (int i = 0; i <tempArray.Length; i++)
            {
                string[] subs2 = tempArray[i].Split(' ', '('); //tablica przechowujaca elementy oprocz ' '


                switch (subs2[0])



                {
                    case awp:
                         fore(tempArray, i);

                    
                        break;
                    case scar:
                        ife(tempArray, i);

                        break;
                    case negev:
                        wailee(tempArray, i);

                        break;





                }


            }



        }
       public void fore(string[] tempArray, int size)
        {
             //tablica przechowujaca elementy oprocz ' '
           
            string pom = tempArray[size].TrimStart('a', 'w', 'p', ' ');
            char[] tab1 = pom.ToCharArray();
           

            //***********************
            //warunek pętli for
            if (tab1[0]=='(')   //sprawdzanie czy czy nawias jest po awp
            {
              

                string pom1 = tempArray[size+1].TrimStart(' ');
                char[] tab = pom1.ToCharArray();
                string pom3 = pom.TrimEnd(' ', '{');
                char[] warunek = pom3.ToCharArray();
                
                if (warunek[warunek.Length -1] == ')')// sprawdzenie zamknięcia wrunku     
                {
                    pom3 = pom3.TrimEnd(')');
                    pom3 = pom3.TrimStart('(');
                    string[] warunek1 = pom3.Split(';');
                    int d = warunek1.Length;
                    if(d  == 3) //sprawdzenie czy warunek jest poprawny
                    {

                        for(int i=0;i<warunek1.Length;i++)
                        {
                           

                            switch (i)

                            {
                                case 0:
                                    string[] cos = warunek1[i].Split(' ', '=');
                                    if (warunek1[0].Contains("="))
                                        {
                                       

                                    }

                                    break;
                                case 1:
                                    string[] bb = new string[] { "==", "<=", ">=", "<", ">" };
                            
                                    for(int g = 0; g < bb.Length; g++)
                                    {

                                  
                                     if(  warunek1[g].Contains(bb[g]))
                                        {
                                            return;
                                        }

                                    }
                                   // cos = warunek1[i].Split(new string[] { "==","<=",">=","<",">" }, StringSplitOptions.None);

                                    break;
                                case 2:
                                    bool b = warunek1[2].Contains("++" );
                                    bool c = warunek1[2].Contains("--");
                                    if (b||c)
                                    {
                                        cos= warunek1[i].Split(new string[] { "++", "--" }, StringSplitOptions.None);
                                   
                                        if(cos[1]==""&&  cos.Length==2)
                                        {
                                            return;
                                        }
                                    }
                                    break;
                            }
                            /*
                            
rush
awp( ss=s;ss<5;ss++){
}
save
                          */
                        }

                        //**********************



                        string[] subs2 = tempArray[size].Split(' ');

                        int size1 = subs2.Length;

                        if (subs2[size1 - 1] == "{" || tab[0] == '{')// sprawdzanie czy jest otwarcie metody
                        {
                            for (int i = size + 1; i < tempArray.Length; i++)
                            {
                                pom3 = tempArray[i].TrimEnd(' ');

                                char[] zamkniecie = pom3.ToCharArray();
                                if (zamkniecie[zamkniecie.Length - 1] == '}')
                                {
                                    return;
                                    // zapisanie do  tablicy wyników

                                }

                            }

                        }//błąd brak {
                    }// błąd zle wprowadony warunek
               }//błąd brak )
          }// błąd brak (

           
           
        }
        public void ife(string[] linijka , int size)
        {
            for (int j = 0; j < size; j++)
            {
                
            }
        }
        public void wailee(string[] linijka, int size)
        {
            for (int j = 0; j < size; j++)
            {
               
            }
        }
    }
}
