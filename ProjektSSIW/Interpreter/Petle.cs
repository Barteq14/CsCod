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
        Interpretacja inter = new Interpretacja();
        Zmienne zmienne = new Zmienne();
        int pozycjaZmiennejZWarunku = 0;
        int size = 0;
        int wartoscWarunku = 0;
        bool przechowanieWartosc = false;
        string[] podzialWarunku;
        string[] tempArray;
        public int  liniaKoncaWarunku = 0;

       
        // List<int> nawiasyOtwierajace = new List<int>();
        // List<int> nawiasyZamykajace = new List<int>();
        //  Składnia skladnia = new Składnia();

       

       public void fore(int linijkaKodu,string[] tablica)
        {
            tempArray = tablica;
            

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
                                                    {//
                                                        string nazwa1 = warunek1[0].TrimStart('k', 'n', 'i', 'f', 'e', ' ');
                                                        string[] nazwa2 = nazwa1.Split('=');
                                                     zmienne.TomaszowyInt(nazwa2[0].TrimEnd(' '),nazwa2[1].TrimStart(' ')+";", size);
                                                        //zmienne.InterpretujInt(warunek1, 0);
                                                      pozycjaZmiennejZWarunku = sprawdzaniePozycjiWLiscie(nazwa2[0].TrimEnd(' '));
                                                    }
                                                    else
                                                    {
                                                        Zmienne.bledy.Add("błąd składni" + size);

                                                          return;

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
                                   
                                        
                                                
                                                bool wartoscBool =false;
                                                 int g=-1;
                                                do
                                                {
                                                if (przechowanieWartosc == false)
                                                {
                                            string pomocnicza = warunek1[1].TrimStart(' ');
                                                 g  = zwracanieIndexuOperatora(pomocnicza);
                                                }
                                                    przechowanieWartosc = wykonanieISprawdzenie( g);
                                                    wartoscBool = podnoszenieZmienej(warunek1);
                                                } while (wartoscBool);
                                            return;
                                      
                            }
                            /*
                            
rush
awp( knife ss = 1; ss < 5; ss++){
}
save
                          */
                        }
                        //**********************
                    }
                    Zmienne.bledy.Add(" błąd zle wprowadony warunek " + size);// błąd zle wprowadony warunek
                }
                Zmienne.bledy.Add("błąd brak ) " + size);//błąd brak )
            }
            Zmienne.bledy.Add("błąd brak ( " + size);// błąd brak (   
        }

     /*   public bool sprawdzeneZamkniecia()
        {
           if (klamraOtwierajace.Count != klamraZamykajace.Count)
            {
                for (int i = size + 1; i < tempArray.Length; i++)
                {
                    string pom3 = tempArray[i].TrimStart(' ');
                    pom3 = pom3.TrimEnd(' ');
                    char[] klamra = pom3.ToCharArray();
                    if (klamra[klamra.Length - 1] == '}')
                    {
                        klamraZamykajace.Add(i);

                        return true;
                    }
                }
                return false;
            }
            return true;
         }*/
        public void sprawdzenieOtwarcia(string[] tablica) 
        {
                for (int i = size ; i < tablica.Length; i++)
                {
                    string pom3 = tablica[i].Trim(' ');
                   
                    char[] otwarcie = pom3.ToCharArray();
                    if (otwarcie[0]== '{'|| otwarcie[otwarcie.Length-1]=='{')
                    {
                        
                      Form1.klamraOtwierajace.Add(i);
                      
                    }else  if (otwarcie[otwarcie.Length - 1] == '}')
                    {
                       Form1.klamraZamykajace.Add(i);

                       
                    }
            }
        }

        public bool podnoszenieZmienej(string[] warunek1)
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
                        if (b && (sprawdzaniePozycjiWLiscie(cos[0])!=-1))
                        {
                            zmianaWartosciFora(1);
                            return true;
                        }
                        else if(c && (sprawdzaniePozycjiWLiscie(cos[0]) != -1))
                        {
                            zmianaWartosciFora(-1);
                            return true;
                        }
                        
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

        public int zwracanieIndexuOperatora(string warunek1)
        {
            string[] operatory = new string[] { "==", "<=", ">=", "<", ">" ,"!="};

            for (int g = 0; g < operatory.Length; g++)
            {
                 podzialWarunku = warunek1.Split(operatory, StringSplitOptions.None);

                if (warunek1.Contains(operatory[g]) && podzialWarunku.Length == 2)
                {
                    return g;
                }
            }
            return -1;
        }

        public bool wykonanieISprawdzenie( int g)
        {
            if (podzialWarunku.Length != 0)
            {
              podzialWarunku[0] = podzialWarunku[0].Trim();
              podzialWarunku[1] = podzialWarunku[1].TrimStart(' ');
                for (int j = 0; j < podzialWarunku.Length - 1; j++)
                {
                    if (podzialWarunku[j] != "" && Zmienne.nazwaZmiennej.Contains(podzialWarunku[j]) == true)
                    {
                        bool czyPrawda = false;
                        if (j == 0)
                        {
                            czyPrawda = wykonanie(1, g);
                            if (czyPrawda) { return true; }
                            else return false;
                        }
                        else if (j == 1)
                        {
                            czyPrawda = wykonanie(-1, g);
                            if (czyPrawda) { return true; }
                            else return false;
                        }
                    }
                    Zmienne.bledy.Add("Błąd składni " + size);
                    return false;
                }
                Zmienne.bledy.Add("Błąd składni " + size);
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
            if (Zmienne.nazwaZmiennej.Count != 0)
            {


                int i;
                for (i = 0; i <= Zmienne.nazwaZmiennej.Count - 1; i++)
                {
                    if (Zmienne.nazwaZmiennej[i] == nazwa)
                    {

                        return i;
                    }

                }
            }
            return -1;
        }

        public void wywołanieKodu()
        {
            int x = size+1;
         
            List<string> linijka = new List<string>();
          //  string[] linijka =new string[tempArray.Length ];
            for (int j = 0; j <= Form1.klamraOtwierajace.Count-1; j++)
            {
                if(Form1.klamraOtwierajace[j]==(size))
                {
                    while((x)!=Form1.klamraZamykajace[(Form1.klamraZamykajace.Count-j-1)]){
                        if (x>tempArray.Length)
                        {
                            Zmienne.bledy.Add("Brak } " + size);
                          
                            return;
                        }
                        linijka.Add( tempArray[x]);
                        
                        x++;
                    }
                    liniaKoncaWarunku = x++;
                    string[] d =new string[linijka.Count] ;
                    for(int i = 0; i< linijka.Count; i++)
                    {
                        d[i] = linijka[i];
                        inter.interpretuj(d, i);
                    }

                    //wywołanie funkcji z linijka i indexem 0
                    return;
                }
            }
            Zmienne.bledy.Add("Brak { " + size);
           
            //blad
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
                            wywołanieKodu();
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
                                wywołanieKodu();
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
                            wywołanieKodu();
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
                                wywołanieKodu();
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
                            wywołanieKodu();
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
                                wywołanieKodu();
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
                            wywołanieKodu();
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
                                wywołanieKodu();
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
                            wywołanieKodu();
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
                                wywołanieKodu();
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
                case 5:
                    if (sprawdzaniePozycjiWLiscie(podzialWarunku[j]) != (-1))
                    {
                        if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] != Zmienne.wartoscZmiennej[sprawdzaniePozycjiWLiscie(podzialWarunku[j])])
                        {
                            // wwołanie funkcji
                            wywołanieKodu();
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
                            if (Zmienne.wartoscZmiennej[pozycjaZmiennejZWarunku] != p)
                            {
                                // wwołanie funkcji
                                wywołanieKodu();
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
                    Zmienne.bledy.Add("Błedy w warunku " + size);
                    return false;
                

            }
            return false;
           
        }

        public void ife(int linijkaKodu, string[] tablica)
        {
            size = linijkaKodu;
            string pom = tablica[size].TrimStart('n', 'e', 'g', 'v',' ');//negev
            char[] tab1 = pom.ToCharArray();

            
            if (tab1[0] == '(')   //sprawdzanie czy  nawias jest po negev
            {
                
                string pom3 = pom.TrimEnd(' ', '{');
                char[] warunek = pom3.ToCharArray();

                if (warunek[warunek.Length - 1] == ')')// sprawdzenie zamknięcia wrunku     
                {
                    pom3 = pom3.TrimEnd(')');
                    pom3 = pom3.TrimStart('(');
                    string[] nazwa = pom3.Split(new string[] { "==", "!=" ,"<",">","<=",">="}, StringSplitOptions.None);
                    if (sprawdzaniePozycjiWLiscie(nazwa[0]) != (-1) || sprawdzaniePozycjiWLiscie(nazwa[1]) != (-1))
                    {

                        bool wartoscBool = false;
                        bool czyPrawda = false;
                        int g = -1;
                        do
                        {
                            if (czyPrawda == false)
                            {
                                g = zwracanieIndexuOperatora(pom3);
                            }
                            czyPrawda = wykonanieISprawdzenie( g);
                            
                        } while (wartoscBool);
                        return;
                    }
                 

                }
                Zmienne.bledy.Add("Brak ) " + size);
            }
            Zmienne.bledy.Add("Brak ( " + size);

            /*         for (int j = 0; j < size; j++)
             {
            rush
 knife ss=1;
 negev(ss==1){
 kljl
 gjl
 }
 save

             }*/
        }
        public void wailee( int size)
        {
            for (int j = 0; j < size; j++)
            {
               
            }
        }
    }
}
