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
        int zamkniecie = 0;


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
                        
                        sprawdzanieNawiasow(warunek1, d);
                        for(int i=0;i<warunek1.Length;i++)
                        {
                           

                            switch (i)

                            {
                                case 0:
                                     string[] cos = warunek1[i].Split('=');
                                    if (warunek1[0].Contains("=")==true && cos.Length==2)
                                    {


                                        /* if (Zmienne.nazwaZmiennej.Contains(cos[0]))
                                         {
                                          for(int j=0; j < Zmienne.nazwaZmiennej.Count; j++)
                                             {
                                                if( Zmienne.nazwaZmiennej[j]== cos[0] && cos.Length==2)
                                                 {
                                                     //dodac oblicznie i = 1+2 coś wtym stylu
                                                     Zmienne.wartoscZmiennej[j] = cos[1];//zmiana wartosci zmiennej
                                                     pozycjaZmiennejZWarunku= j; 
                                                 }

                                             }*/
                                        if(-1!=zmianaWartosciZmiennej(cos[0],cos[1])){

                                            pozycjaZmiennejZWarunku = zmianaWartosciZmiennej(cos[0], cos[1]);
                                            wartoscWarunku = Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku];
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
                                        if (true == wykonanieISprawdzenie(warunek1, tempArray))
                                        {
                                            przechowanieWartosc = true;
                                            bool wartoscBool;
                                            do
                                            {
                                                wartoscBool = podnoszenieZmienej(warunek1, tempArray);
                                            } while (wartoscBool);
                                        }
                                        else return;
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
                    zamkniecie = i;
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
                        return true;
                    }
                       
                    
                }
                //błąd
                return false;

            }
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
                            wartoscWarunku = wartoscWarunku + 1;
                        }
                        else
                        {
                            wartoscWarunku = wartoscWarunku - 1;
                        }
                        size = size + 1;
                        przechowanieWartosc = wykonanieISprawdzenie(warunek1, tempArray);
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

                        //dodac oblicznie i = 1+2 coś wtym stylu
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

        public bool wykonanieISprawdzenie(string[] warunek1, string[] tempArray)
        {
            string[] bb = new string[] { "==", "<=", ">=", "<", ">" };



            

            for (int g = 0; g < bb.Length; g++)
            {
               string[] cos = warunek1[1].Split(bb, StringSplitOptions.None);

                if (warunek1[1].Contains(bb[g]) && cos.Length == 2)
                {
                   // cos = warunek1[i].Split(new string[] { "==", "<=", ">=", "<", ">", " " }, StringSplitOptions.None);

                    for (int j = 0; j < cos.Length-1; j++)
                    {
                        if (cos[j] != "" && Zmienne.nazwaZmiennej.Contains(cos[j]) == true)
                        {
                            
                            
                            switch (g)
                            {
                               

                                case 0:

                                    if (j == 0)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j++]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j++])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }else if(tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }

                                                // wwołanie funkcji
                                              podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j++]))
                                            {

                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == int.Parse(cos[j++]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else if (j == 1)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j--]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j--])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j--]))
                                            {
                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] == int.Parse(cos[j--]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add("Błedny warunek ");
                                        return false;
                                    }
                                    break;
                                case 1:
                                    if (j == 0)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j++]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j++])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j++]))
                                            {

                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= int.Parse(cos[j++]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else if (j == 1)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j--]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j--])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j--]))
                                            {
                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] <= int.Parse(cos[j--]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                               return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add("Błedny warunek ");
                                         return false;
                                    }


                                    break;
                                case 2:
                                    if (j == 0)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j++]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j++])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j++]))
                                            {

                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= int.Parse(cos[j++]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else if (j == 1)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j--]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j--])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j--]))
                                            {
                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] >= int.Parse(cos[j--]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                               return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add("Błedny warunek ");
                                         return false;
                                    }

                                    break;
                                case 3:
                                    if (j == 0)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[(j++)]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] < Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j++])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j++]))
                                            {

                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] < int.Parse(cos[j++]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
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
                                    }
                                    else if (j == 1)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j--]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] < Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j--])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j--]))
                                            {
                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] < int.Parse(cos[j--]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add("Błedny warunek ");
                                       return false;
                                    }

                                    break;
                                case 4:
                                    if (j == 0)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j++]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j++])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j++]))
                                            {

                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > int.Parse(cos[j++]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                Zmienne.bledy.Add("Błedny typ " );
                                                 return false;
                                            }
                                        }
                                    }
                                    else if (j == 1)
                                    {
                                        if (sprawdzaniePozycjiWLiscie(cos[j--]) != (-1))
                                        {
                                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(cos[j--])])
                                            {
                                                if (sprawdzeneZamkniecia(tempArray))
                                                {
                                                    return false;
                                                }
                                                else if (tempArray.Length == size - 1)
                                                {
                                                    //błąd
                                                    return false;
                                                }
                                                // wwołanie funkcji
                                                podnoszenieZmienej(warunek1, tempArray);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (zmienne.czyInt(cos[j--]))
                                            {
                                                if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] > int.Parse(cos[j--]))
                                                {
                                                    if (sprawdzeneZamkniecia(tempArray))
                                                    {
                                                        return false;
                                                    }
                                                    else if (tempArray.Length == size - 1)
                                                    {
                                                        //błąd
                                                        return false;
                                                    }
                                                    // wwołanie funkcji
                                                    podnoszenieZmienej(warunek1, tempArray);
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
                                    }
                                    else
                                    {
                                        Zmienne.bledy.Add("Błedny warunek " );
                                         return false;
                                    }

                                    break;

                            }

                            
                        }
                    }
                    Zmienne.bledy.Add("Błąd składni " );
                    return false;
                }
              

            }
            return false;
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

        private void sprawdzanieNawiasow(string[] warunek1, int d)
        {
         /*  char[] g = warunek1[d].ToCharArray();
            for(int i = 0; i < g.Length; i++)
            {
                if (warunek1[i].Contains("("))
                {
                    nawiasyOtwierajace.Add(i);
                    
                }
                else if (true==warunek1[i].Contains(")") && !nawiasyZamykajace.Any())
                {
                    
                    
                        nawiasyZamykajace.Add(i);
                    
                }

            }*/
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
