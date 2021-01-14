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
        //listy z informacjami o zmiennej 
        public static List<dynamic> typZmiennej = new List<dynamic>(); //muszą być statyczne i mieć get set żeby można było w innych 
        public static List<dynamic> nazwaZmiennej = new List<dynamic>();
        public static List<dynamic> wartoscZmiennej = new List<dynamic>();
        public static List<String> konsola { get; set; } = new List<String>(); // lista dla konsoli
        public static List<String> bledy { get; set; } = new List<string>(); // lista błędów jakie mogą wyskoczyć

        //listy z indeksami
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
        }
        public bool InterpretujInt(string[] lines,int indeks)
        {
            int liczba;
            int liczba2;
            string ok ="";
            string[] pomINT = lines[indeks].Split(' ');
         
     

            int dlugosc = pomINT.Length;
            if (pomINT[0] == "knife")
            {
                if(czyString(pomINT[1]) == true)
                {
                    if (pomINT[2] == "=")
                    {
                        if(czyInt(pomINT[3]) == true)
                        {
                            liczba = int.Parse(pomINT[3]);
                            if(pomINT.Length > 4) // sprawdzenie czy linia posiada wiecej nic 4 podstawowe elementy
                            {
                                if(pomINT[4] == "+" || pomINT[4] == "-" || pomINT[4] == "*" || pomINT[4] == "/")
                                {
                                    if(czyInt(pomINT[5]) == true)
                                    {
                                        liczba2 = int.Parse(pomINT[5]);
                                        typZmiennej.Add("int"); //zapisuje informacje o zmiennej
                                        nazwaZmiennej.Add(pomINT[1]);
                                        if(pomINT[4] == "+")
                                        {
                                            wartoscZmiennej.Add(liczba + liczba2);
                                        }else if(pomINT[4] == "-")
                                        {
                                            wartoscZmiennej.Add(liczba - liczba2);
                                        }else if(pomINT[4] == "*")
                                        {
                                            wartoscZmiennej.Add(liczba * liczba2);
                                        }
                                        else if(pomINT[4] == "/")
                                        {
                                            wartoscZmiennej.Add(liczba / liczba2);
                                        }
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                typZmiennej.Add("int");
                                nazwaZmiennej.Add(pomINT[1]);
                                wartoscZmiennej.Add(pomINT[3]);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;

        }

        public bool czyString(string element)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*+`~ąśćźżółęń";

            //sprawdzanie czy element za knife jest stringiem
            char firstLetter = element.FirstOrDefault();

            if (Char.IsDigit(firstLetter))
            {
                return false;
            }
            else
            {
                foreach (var item in specialChar)
                {
                    if (element.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool czyInt(string element)
        {
            
            //sprawdzam czy kolejne znaki w danym ciagu to liczby, jesli tak zwracam true jesli nie to false
            char[] tab = element.ToCharArray();
            foreach(var item in tab)
            {
                if (!char.IsDigit(item))
                    return false;
            }
            return true;
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
