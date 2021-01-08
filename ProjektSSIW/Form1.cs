using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSSIW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tworzymy tablicę naszych elementów z richTextBoxa
            string[] tempArray = richTextBox1.Lines;
            List<string> pomLista = new List<string>();
            //string[] errors = new string[10];
            List<String> error = new List<string>();
            error.Add("zła intrukcja poczatkowa! (rush)");
            error.Add("brak średnika na końcu!");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            error.Add("zła intrukcja poczatkowa! (begin)");
            //textBox1.Text = richTextBox1.Text;
            //wypisanie do textBox1 elementu o indexie 2 z naszej tablicy
            /*
            String linia2 = tempArray[2];
            textBox1.Text = linia2;
            */
            //wypisanie wszystkich elementów z tablicy do listView1
            /*
            for(int i = 0; i < tempArray.Length; i++)
            {
                listView1.Items.Add(tempArray[i]);
            }
            */
            if (tempArray.Count() <= 0)
            {
                label4.Text = "Nie wpisałeś żadnego kodu.";
            }
            else 
            { 
                if (tempArray[0] == "rush")
                {
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        label4.Text = "OK";
                        listView1.Items.Add(tempArray[i]);
                        string pom = tempArray[1];
                        string[] subs = pom.Split(' ', ';', '\t');
                        string[] subs1 = pom.Split('(', ')', '+', '\t');

                        foreach (var sub in subs)
                        {
                            listView2.Items.Add(sub);

                        }
                        foreach (var p in subs1)
                        {
                            listView3.Items.Add(p);
                        }
                    }
                }
                else
                {
                    label4.Text = error[0];
                }       
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            richTextBox1.Clear();
        }
    }
}
