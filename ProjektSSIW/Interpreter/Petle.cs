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
       public string[] fore(string[] linijka,int size)
        {
            string[] subs2 = linijka[size].Split(')', '(', '\t'); //tablica przechowujaca elementy oprocz ' '
            int size1 = subs2.Length;
            if (subs2[size1-1] == "{")
            {
                return subs2;
            }
            /* warunek
             * 
             * 


              }
              for (int i = 0; i < size; i++)
              {

              }*/
            
            return s;
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
