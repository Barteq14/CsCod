using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSSIW.Interpreter
{
    public class Zmienne 
    {
        Składnia składnia = new Składnia();

        List<int> IndeksyINT = new List<int>();
        List<int> IndeksyDOUBLE = new List<int>();
        List<int> IndeksyFLOAT = new List<int>();
        List<int> IndeksySTRING = new List<int>();
        List<int> IndeksyBOOLEAN = new List<int>();

        public void InterpretujZmienne(string[] tempArray)
        {


            for (int i = 1; i < tempArray.Length; i++)
            {

                string[] tab = tempArray[i].Split(' ');
                switch (tab[0])
                {
                    case "knife":
                        InterpretujInt(tempArray,i);
                        IndeksyINT.Add(i);
                        break;
                    case "grenade":
                        InterpretujDouble();
                        IndeksyDOUBLE.Add(i);
                        break;
                    case "rifle":
                        InterpretujFloat();
                        IndeksyFLOAT.Add(i);
                        break;
                    case "defuse":
                        InterpretujString();
                        IndeksySTRING.Add(i);
                        break;
                    case "zeus":
                        InterpretujBoolean();
                        IndeksyBOOLEAN.Add(i);
                        break;
                }

            }

            int a = 2;

        }
        public void InterpretujInt(string[] lines,int indeks)
        {

            string[] pomINT = lines[indeks].Split(' ');
            if(pomINT[0] == "knife")
            {
                /*
                if (pomINT[1].GetType.Equals("String"))
                {

                }*/
            }

            int a = 2;
           
        }

        public void InterpretujDouble()
        {

        }

        public void InterpretujFloat()
        {

        }

        public void InterpretujString()
        {

        }

        public void InterpretujBoolean()
        {

        }
    }
}
