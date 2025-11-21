using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlocksArrowsHTML
{
    public partial class Form1 : Form
    {
        ComboBox cb = new ComboBox();
        int positionBottomCombobox = 0;
        int numeringCombobox = 3; // 2 comboboxes was drawed. Next numbering is 3.

        TextBox txt = new TextBox();
        List<AuxiliarTextCombobox> listCbox = null;
        Dictionary<string, int> dicTextAverage = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            WindowState = FormWindowState.Maximized;

            //lblText3.Visible = false;
            //lblText4.Visible = false;
            //lblText5.Visible = false;
            //lblText6.Visible = false;

            //txtText3.Visible = false;
            //txtText4.Visible = false;
            //txtText5.Visible = false;
            //txtText6.Visible = false;

            //txtText1.Text = "Estender toalha de mesa";
            //txtText2.Text = "Trazer pratos e talheres";
            //txtText3.Text = "Temperar salada";
            //txtText4.Text = "Esquentar comida";
            //txtText5.Text = "Fazer a bebida";
            //txtText6.Text = "Iniciar refeição";
            txtInicioEsquerda.Text = "10";
            txtInicioSuperior.Text = "20";
            txtComprimento.Text = "250";
            txtAltura.Text = "20";
            txtDistanciaRetangulos.Text = "80";

            GbTextboxes.Location = new Point(5, 20);
            GbTextboxes.Size = new Size((int)(Width * 0.3), (int)(Height * 0.7));
            panelTextboxes.Dock = DockStyle.Fill;
            panelTextboxes.AutoScroll = true;

            GbArrows.Width = (int)(Width * 0.43);
            GbArrows.Location = new Point(GbTextboxes.Right + 10, GbTextboxes.Top);
            panelComboboxes.Dock = DockStyle.Fill;
            panelComboboxes.AutoScroll = true;

            GbSetup.Width = (int)(Width * 0.24);
            GbSetup.Location = new Point(GbArrows.Right + 10, GbTextboxes.Top);
            panelSetup.Dock = DockStyle.Fill;

            Button BtnDrawTextbox = new Button();
            BtnDrawTextbox.Text = "+";
            BtnDrawTextbox.Font = new Font("Verdana", 12f);
            BtnDrawTextbox.Location = new Point((GbTextboxes.Width - BtnDrawTextbox.Width) / 2, GbTextboxes.Bottom + 40);
            Controls.Add(BtnDrawTextbox);
            BtnDrawTextbox.Click += BtnDrawTextbox_Click;

            DrawComboboxes();
        }

        private void BtnDrawTextbox_Click(object sender, EventArgs e)
        {
            int amountTextbox = panelTextboxes.Controls.OfType<TextBox>().Count() + 1;
            TextBox txt = new TextBox();
            txt.Location = new Point(10, 30*amountTextbox);
            txt.Size = new Size((int)(panelTextboxes.Width*0.9), 25);
            txt.Tag = "txt" + amountTextbox;
            panelTextboxes.Controls.Add(txt);
        }

        private void BtnMakeCode_Click(object sender, EventArgs e)
        {
            listCbox = AddTextsFromComboboxes();
            FileStream fs = new FileStream("canvas.html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            WriteHeader(sw);
            WriteCode(sw);
            WriteEndOfFile(sw);
            sw.Close();
            fs.Close();
        }

        private void WriteHeader(StreamWriter sw)
        {
            sw.WriteLine("<html lang=\"pt-br\">");
            sw.WriteLine("<head>");
            sw.WriteLine("    <meta charset=\"UTF-8\">");
            sw.WriteLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sw.WriteLine("    <title>Document</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine("    <canvas id=\"figura1\" width=\"320px\" height=\"800px\"></canvas>");
            sw.WriteLine("	");
            sw.WriteLine("  <script>");
        }

        private void WriteCode(StreamWriter sw)
        {
            List<string> listOfTexts = new List<string>(); // usado para armazenar os textos dos comboboxes
            //listOfTexts.Add(txtText1.Text);
            //listOfTexts.Add(txtText2.Text);
            //listOfTexts.Add(txtText3.Text);
            //listOfTexts.Add(txtText4.Text);
            //listOfTexts.Add(txtText5.Text);
            //listOfTexts.Add(txtText6.Text);
            int x = Convert.ToInt32(txtInicioEsquerda.Text);
            int y = Convert.ToInt32(txtInicioSuperior.Text);
            int comprimento = Convert.ToInt32(txtComprimento.Text);
            int altura = Convert.ToInt32(txtAltura.Text);
            int distancia = Convert.ToInt32(txtDistanciaRetangulos.Text);

            sw.WriteLine("let canvas = document.getElementById('figura1');");
            sw.WriteLine("let ctx = canvas.getContext('2d');");

            sw.WriteLine("ctx.font=\"16px Arial\";");

            for (int i = 0; i < listOfTexts.Count; i++)
            {
                sw.WriteLine($"ctx.fillText('{listOfTexts[i]}',{x},{y + i * distancia});");

                sw.WriteLine($"ctx.rect({x - 2},{y + i * distancia - (int)(altura * .74)},{comprimento},{altura});");
                dicTextAverage.Add(listOfTexts[i], (y + i * distancia - (int)(altura * .37)));
                
                sw.WriteLine("ctx.stroke();");

                if (i < listOfTexts.Count-1)
                {
                    // to down
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2}','{y + i * distancia - (int)(altura * .74) + altura}');");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2}','{y + i * distancia - (int)(altura * .74) + distancia}');");
                    sw.WriteLine("ctx.stroke();");                    
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2-9}','{y + i * distancia - (int)(altura * .74) + distancia-10}')");
                    sw.WriteLine($"ctx.lineTo('{comprimento/2}','{y + i * distancia - (int)(altura * .74) + distancia-1}')");
                    sw.WriteLine("ctx.stroke();");
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2 + 9}','{y + i * distancia - (int)(altura * .74) + distancia - 10}')");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2}','{y + i * distancia - (int)(altura * .74) + distancia - 1}')");
                    sw.WriteLine("ctx.stroke();");

                    // to up
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2+30}','{y + i * distancia - (int)(altura * .74) + altura}');");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2+30}','{y + i * distancia - (int)(altura * .74) + distancia}');");
                    sw.WriteLine("ctx.stroke();");
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2 - 9+30}','{y + i * distancia - (int)(altura * .74) + altura + 9}')");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2+30}','{y + i * distancia - (int)(altura * .74) + altura + 1}')");
                    sw.WriteLine("ctx.stroke();");
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2 + 9+30}','{y + i * distancia - (int)(altura * .74) + altura+9}')");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2+30}','{y + i * distancia - (int)(altura * .74) + altura + 1}')");
                    sw.WriteLine("ctx.stroke();");
                }
            }
            //string first = GetText("cb1");
            //string second = GetText("cb2");
            //if (first.Equals(second))
            //    MessageBox.Show("Origem e destino são os mesmos.");
            //else
            //    MessageBox.Show($"Desenhar seta de {first} para {second}");

            //for (int i = 0; i < listCbox.Count; i+=2)
            //{
            //    DrawHorizontalTrace(i);
            //}
        }

        private string GetText(string text)
        {
            string result = "";
            foreach (var combobox in panelComboboxes.Controls.OfType<ComboBox>())
            {
                if (combobox.Tag.Equals(text))
                {
                    result = combobox.Text;
                }
            }
            return result;
        }

        //private void DrawHorizontalTrace(int i)
        //{
        //    List<AuxiliarTextCombobox> listTextCb = AddTextsFromComboboxes();
        //    foreach (var item in listTextCb)
        //    {
        //        foreach (var key in dicTextAverage.Keys)
        //        {
        //            if (item.Text.Equals(key))
        //            {
        //                MessageBox.Show(item.IdCombobox);
        //            }
        //        }
        //    }
        //}


        private void WriteEndOfFile(StreamWriter sw)
        {
            sw.WriteLine("	</script>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
        }

        private void DrawComboboxes()
        {
            //cb.Items.Add(txtText1.Text);
            //cb.Items.Add(txtText2.Text);
            //cb.Items.Add(txtText3.Text);
            //cb.Items.Add(txtText4.Text);
            //cb.Items.Add(txtText5.Text);
            //cb.Items.Add(txtText6.Text);
            cb.Location = new Point(10, 20);
            cb.Tag = "cb1";
            cb.Size = new Size(250, 20);
            panelComboboxes.Controls.Add(cb);

            cb = new ComboBox();
            //cb.Items.Add(txtText1.Text);
            //cb.Items.Add(txtText2.Text);
            //cb.Items.Add(txtText3.Text);
            //cb.Items.Add(txtText4.Text);
            //cb.Items.Add(txtText5.Text);
            //cb.Items.Add(txtText6.Text);
            cb.Location = new Point(300, 20);
            cb.Tag = "cb2";
            cb.Size = new Size(250, 20);
            panelComboboxes.Controls.Add(cb);

            positionBottomCombobox = cb.Bottom;

            Button Btn = new Button();
            Btn.Text = "+";
            Btn.Font = new Font("Verdana", 12f);
            Btn.Size = new Size(40, 25);
            Btn.Location = new Point(GbArrows.Left + (GbArrows.Width - Btn.Width) / 2, GbArrows.Bottom + 15);
            Controls.Add(Btn);
            
            Btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string check1 = GetText($"cb{(numeringCombobox - 2)}");
            string check2 = GetText($"cb{(numeringCombobox - 1)}");
            if (check1 != string.Empty && check2 != string.Empty)
            {
                cb = new ComboBox();
                //cb.Items.Add(txtText1.Text);
                //cb.Items.Add(txtText2.Text);
                //cb.Items.Add(txtText3.Text);
                //cb.Items.Add(txtText4.Text);
                //cb.Items.Add(txtText5.Text);
                //cb.Items.Add(txtText6.Text);
                cb.Location = new Point(10, 20 + positionBottomCombobox);
                cb.Tag = "cb" + numeringCombobox;
                cb.Size = new Size(250, 20);
                panelComboboxes.Controls.Add(cb);

                numeringCombobox++;

                cb = new ComboBox();
                //cb.Items.Add(txtText1.Text);
                //cb.Items.Add(txtText2.Text);
                //cb.Items.Add(txtText3.Text);
                //cb.Items.Add(txtText4.Text);
                //cb.Items.Add(txtText5.Text);
                //cb.Items.Add(txtText6.Text);
                cb.Location = new Point(300, 20 + positionBottomCombobox);
                cb.Tag = "cb" + numeringCombobox;
                cb.Size = new Size(250, 20);
                panelComboboxes.Controls.Add(cb);

                positionBottomCombobox = cb.Bottom;
                numeringCombobox++;
            }
            else
            {
                MessageBox.Show("Texto em branco");
            }
        }

        private List<AuxiliarTextCombobox> AddTextsFromComboboxes()
        {
            List<AuxiliarTextCombobox> fromCboxToCbox = new List<AuxiliarTextCombobox>();
            for (int i = 1; i <= numeringCombobox; i++)
            {
                foreach (var item in panelComboboxes.Controls.OfType<ComboBox>())
                {
                    if (item.Tag.Equals("cb" + i) && item.Text != string.Empty)
                        fromCboxToCbox.Add(new AuxiliarTextCombobox(item.Text, "cb"+i));
                }
            }
            return fromCboxToCbox;
        }
    }
}
