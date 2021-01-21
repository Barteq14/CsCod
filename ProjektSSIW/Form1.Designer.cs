
namespace ProjektSSIW
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.listView5 = new System.Windows.Forms.ListView();
            this.listView6 = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Witaj w programie";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(117, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 53);
            this.button1.TabIndex = 2;
            this.button1.Text = "Wykonaj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(763, 559);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(117, 92);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(573, 372);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(696, 255);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(140, 147);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Podaj sekwencję którą chcesz wykonać:";
            // 
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(696, 21);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(366, 206);
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // listView3
            // 
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(1068, 21);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(311, 206);
            this.listView3.TabIndex = 11;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.List;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 485);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 53);
            this.button2.TabIndex = 12;
            this.button2.Text = "Wyczyść";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(849, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Konsola";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1225, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Błędy";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(869, 556);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(730, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "typZmiennej";
            // 
            // listView4
            // 
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(852, 255);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(140, 147);
            this.listView4.TabIndex = 18;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.List;
            // 
            // listView5
            // 
            this.listView5.HideSelection = false;
            this.listView5.Location = new System.Drawing.Point(1016, 255);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(140, 147);
            this.listView5.TabIndex = 19;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.List;
            // 
            // listView6
            // 
            this.listView6.HideSelection = false;
            this.listView6.Location = new System.Drawing.Point(1206, 255);
            this.listView6.Name = "listView6";
            this.listView6.Size = new System.Drawing.Size(140, 147);
            this.listView6.TabIndex = 20;
            this.listView6.UseCompatibleStateImageBehavior = false;
            this.listView6.View = System.Windows.Forms.View.List;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(884, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "nazwaZmiennej";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1047, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "wartoscZmiennej";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1214, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "prawdziwy typ Zmiennej";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 641);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView6);
            this.Controls.Add(this.listView5);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ListView listView6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

