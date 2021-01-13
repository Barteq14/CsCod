using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    class Petle
    {
        public void InterpretujPetle()
        {

            
              



        }
       public string[] fore(string[] tempArray, int size)
        {
             //tablica przechowujaca elementy oprocz ' '
           
            string pom = tempArray[size].TrimStart('a', 'w', 'p', ' ');
            char[] tab1 = pom.ToCharArray();
           

            //***********************
            //warunek pętli for
            if (tab1[0]=='(') {//sprawdzanie czy czy nawias jest po awp

                string pom1 = tempArray[size+1].TrimStart(' ');
                char[] tab = pom1.ToCharArray();
                string pom3 = pom.TrimEnd(' ', '{');
                char[] warunek = pom3.ToCharArray();

                if (warunek[warunek.Length -1] == ')')// sprawdzenie zamknięcia wrunku

                {
                    //**********************
                    string[] subs2 = tempArray[size].Split(' ');

                    int size1 = subs2.Length;

                    if (subs2[size1-1 ] == "{" || tab[0] == '{')// sprawdzanie czy jest otwarcie metody
                    {
                        for(int i = size + 1;i < tempArray.Length;i++ )
                       {
                           pom3 = tempArray[i].TrimEnd(' ');

                            char[] zamkniecie = pom3.ToCharArray();
                           if (zamkniecie[zamkniecie.Length-1 ] == '}')
                            {

                                
                                return subs2;
                           }

                        }
                       
                   }
               }
          }
            /* warunek
             * 
             * 


              }
              for (int i = 0; i < size; i++)
              {

              }*/
            string[] t= new string[1];
            return t ;
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
