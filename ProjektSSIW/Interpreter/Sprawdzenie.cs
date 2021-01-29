using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    class Sprawdzenie: Składnia
    {

        public void InterpretujPetle(string[] tempArray, int i)
        {
            Petle petle = new Petle();

            string f = tempArray[i].Trim(' ');
            string[] subs2 = f.Split('('); //tablica przechowujaca elementy oprocz ' '

            switch (subs2[0])
            {
                case awp:
                   petle.fore(i, tempArray);
                    break;

                case negev:
                 petle.ife(i, tempArray);
                    break;

               // case scar:
                    //wailee(i);
                  //  break;
            }
        }
    }
}
