using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSSIW.Interpreter
{
    public class Petle : Składnia
    {
        Zmienne zmienne = new Zmienne();
        int pozycjaZmiennejZWarunku = 0;
        int size = 0;
        int wartoscWarunku = 0;
        bool przechowanieWartosc = false;
        string[] podzialWarunku;


        List<int> klamraOtwierajace = new List<int>();
         List<int> klamraZamykajace = new List<int>();
        // List<int> nawiasyOtwierajace = new List<int>();
        // List<int> nawiasyZamykajace = new List<int>();
        //  Składnia skladnia = new Składnia();

        public void InterpretujPetle(string[] tempArray,int i)
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

       public void fore(string[] tempArray, int linijkaKodu)
        {
            Zmienne zmienne = new Zmienne();

            //tablica przechowujaca elementy oprocz ' '
            size = linijkaKodu;
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
                        
                       // sprawdzanieNawiasow(warunek1, d);
                        for(int i=0;i<warunek1.Length;i++)
                        {
                            switch (i)
                            {
                                case 0:
                                     string[] cos = warunek1[i].Split('=');
                                    if (warunek1[0].Contains("=")==true && cos.Length==2)
                                    {
                                             //dodac oblicznie i = 1+2 coś wtym stylu
                                                
                                        if(-1!=zmianaWartosciZmiennej(cos[0],cos[1])){

                                            pozycjaZmiennejZWarunku = zmianaWartosciZmiennej(cos[0], cos[1]);
                                        }
                                        else
                                        {
                                            cos = warunek1[i].Split(' ','=');
                                            for (int j = 0; j < cos.Length; j++)
                                            {
                                                if(cos[j] != "")
                                                {
                                                    if (cos[j] == knife)
                                                    {

                                                        zmienne.InterpretujInt(warunek1, 0);
                                                    }
                                                    else
                                                    {
                                                        Zmienne.bledy.Add("błąd składni" + size);

                                                        //   return;

                                                    }
                                                    break;
                                                }
                                            }                             
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                    break;
                                case 1:
                                    if (sprawdzenieOtwarcia(tempArray))
                                    {
                                        if (sprawdzeneZamkniecia(tempArray))
                                        {
                                                
                                                bool wartoscBool;
                                                 int g=-1;
                                                do
                                                {
                                                if (przechowanieWartosc == false)
                                                {
                                                 g  = zwracanieIndexuOperatora(warunek1);
                                                }
                                                    przechowanieWartosc = wykonanieISprawdzenie(warunek1, tempArray,g);
                                                    wartoscBool = podnoszenieZmienej(warunek1, tempArray);
                                                } while (wartoscBool);
                                            return;
                                        }
                                        else
                                        {
                                            //błąd
                                            return;
                                        }
                                    }// błąd
                                    return;
                            }
                            /*
                            
rush
awp(knife ss=1;ss<5;ss++){
}
save
                          */
                        }
                        //**********************
                    }// błąd zle wprowadony warunek
               }//błąd brak )
          }// błąd brak (   
        }

        public bool sprawdzeneZamkniecia(string[] tempArray)
        {
            for (int i = size + 1; i < tempArray.Length; i++)
            {
                string pom3 = tempArray[i].TrimStart(' ');
                pom3 = pom3.TrimEnd(' ');
                char[] klamra = pom3.ToCharArray();
                if (klamra[klamra.Length - 1] == '}')
                {
                    klamraOtwierajace.Add(i);
                    
                    return true;
                }   
            }
            return false;
         }
        public bool sprawdzenieOtwarcia(string[] tempArray)
        {
            string[] subs2 = tempArray[size].Split(' ');

            int size1 = subs2.Length;

            if (subs2[size1 - 1] != "{" && (subs2[size1 - 2] != "{" && subs2[size1 - 1] == ""))// sprawdzanie czy jest otwarcie metody
            {
                for (int i = size + 1; i < tempArray.Length; i++)
                {
                    string pom3 = tempArray[i].TrimStart(' ');
                   
                    char[] otwarcie = pom3.ToCharArray();
                    if (otwarcie[0]== '{')
                    {
                        size = i++;
                        klamraOtwierajace.Add(i);
                        return true;
                    }
                }
                //błąd
                return false;
            }
            klamraOtwierajace.Add(size);
            size = size++;
            return true;
        }

        public bool podnoszenieZmienej(string[] warunek1, string[] tempArray)
        {
            bool b = warunek1[2].Contains("++");
            bool c = warunek1[2].Contains("--");
            if (b || c)
            {
                string[] cos = warunek1[2].Split(new string[] { "++", "--" }, StringSplitOptions.None);

                if (cos.Length == 2 && cos[1] == "")
                {
                    if (przechowanieWartosc == true)
                    {
                        if (b)
                        {
                            zmianaWartosciFora(1);
                        }
                        else
                        {
                            zmianaWartosciFora(-1);
                        }
                        size = size + 1;
                        return true;
                    }
                    else return false;
                }
            }
             Zmienne.bledy.Add("błąd składni" + size);
            return false;
        }

        public int zmianaWartosciZmiennej(string nazwa,string a)
        {
            if (Zmienne.nazwaZmiennej.Contains(nazwa))
            {
                for (int j = 0; j < Zmienne.nazwaZmiennej.Count; j++)
                {
                    if (Zmienne.nazwaZmiennej[j] == nazwa && zmienne.czyInt(a))
                    {
                        Zmienne.wartoscZmiennej[j] = int.Parse(a);//zmiana wartosci zmiennej
                        return j;
                    }
                }
            }
            else
            {
                return -1;
            }
            return -1;
        }

        public int zwracanieIndexuOperatora(string[] warunek1)
        {
            string[] operatory = new string[] { "==", "<=", ">=", "<", ">" };

            for (int g = 0; g < operatory.Length; g++)
            {
                 podzialWarunku = warunek1[1].Split(operatory, StringSplitOptions.None);

                if (warunek1[1].Contains(operatory[g]) && podzialWarunku.Length == 2)
                {
                    return g;
                }
            }
            return -1;
        }

        public bool wykonanieISprawdzenie(string[] warunek1, string[] tempArray, int g)
        {
            for (int j = 0; j < podzialWarunku.Length - 1; j++)
            {
                if (podzialWarunku[j] != "" && Zmienne.nazwaZmiennej.Contains(podzialWarunku[j]) == true)
                {
                    bool czyPrawda =false;
                    if (j == 0)
                    {
                      czyPrawda=  wykonanie(1, g);
                        if (czyPrawda) { return true; }
                        else return false;
                    }
                    else if (j == 1)
                    {
                        czyPrawda=wykonanie(-1, g);
                        if (czyPrawda) { return true; }
                        else return false;
                    }
                }
                Zmienne.bledy.Add("Błąd składni ");
                return false;
            }
            return false;
        }

        public void zmianaWartosciFora(int d)
        {
            int wartosc = Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku];
            int pomocnicza = wartosc + d;
            Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] = pomocnicza;
        }
        
        public int sprawdzaniePozycjiWLiscie(string nazwa)
        { 
            int i;
            for (i = 0; i < Zmienne.nazwaZmiennej.Count; i++)
            {
                if (Zmienne.nazwaZmiennej.Contains(nazwa) == true)
                {

                    return i;
                }
            }
            return -1;
        }

        public bool wykonanie(int j,int g)
        {
            switch (g)
            {
                case 0:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (zmienne.czyInt(podzialWarunku[j]))
                        {
                            string ppo = podzialWarunku[j];
                            int p = int.Parse(ppo);
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == p)
                            {
                                // wwołanie funkcji
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Zmienne.bledy.Add("Błedny typ ");
                            return false;
                        }
                    }
                   
                case 1:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (zmienne.czyInt(podzialWarunku[j]))
                        {
                            string ppo = podzialWarunku[j];
                            int p = int.Parse(ppo);
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= p)
                            {
                                // wwołanie funkcji
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Zmienne.bledy.Add("Błedny typ ");
                            return false;
                        }
                    }
                case 2:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (zmienne.czyInt(podzialWarunku[j]))
                        {
                            string ppo = podzialWarunku[j];
                            int p = int.Parse(ppo);
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= p)
                            {
                                // wwołanie funkcji
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Zmienne.bledy.Add("Błedny typ ");
                            return false;
                        }

                    }
                case 3:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (zmienne.czyInt(podzialWarunku[j]))
                        {
                            string ppo = podzialWarunku[j];
                            int p = int.Parse(ppo);
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] < p)
                            {
                                // wwołanie funkcji
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Zmienne.bledy.Add("Błedny typ ");
                            return false;
                        }
                    }
                case 4:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (zmienne.czyInt(podzialWarunku[j]))
                        {
                            string ppo = podzialWarunku[j];
                            int p = int.Parse(ppo);
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > p)
                            {
                                // wwołanie funkcji
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Zmienne.bledy.Add("Błedny typ ");
                            return false;
                        }
                    }
                case -1:
                    //błąd
                    return false;
                

            }
            return false;
           
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
