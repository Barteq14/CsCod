using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    public class Funkcje
    {
        Składnia składnia = new Składnia();

        public void InterpretujFunkcje(string[] tempArray)
        {
            

            for (int i = 1; i < tempArray.Length; i++)
            {
                string[] tab = tempArray[i].Split(' ');

                switch (tab[0])
                {
                    case "ak47":
                        InterpretujReadLine(tempArray, i);
                        break;
                    case "m4a1s":
                        InterpretujWriteLine(tempArray, i);
                        break;
                    case "m4a4":
                        InterpretujWrite(tempArray, i);
                        break;
                    case "usp":
                        InterpretujToString(tempArray, i);
                        break;
                    case "glock":
                        InterpretujToInt(tempArray, i);
                        break;
                    case "tec":
                        InterpretujToFloat(tempArray, i);
                        break;
                }
            }

            
        }

        public void InterpretujReadLine(string[] tempArray, object i)
        {

            


        }

        public void InterpretujToFloat(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public void InterpretujToInt(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public void InterpretujToString(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public void InterpretujWrite(string[] tempArray, int i)
        {
            throw new NotImplementedException();
        }

        public void InterpretujWriteLine(string[] tempArray, object i)
        {
            throw new NotImplementedException();
        }

        
    }
}
