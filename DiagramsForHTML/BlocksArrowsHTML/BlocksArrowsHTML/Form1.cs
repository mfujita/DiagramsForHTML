using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlocksArrowsHTML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            txtText1.Text = "Estender toalha de mesa";
            txtText2.Text = "Trazer pratos e talheres";
            txtText3.Text = "Temperar salada";
            txtText4.Text = "Esquentar comida";
            txtText5.Text = "Fazer a bebida";
            txtText6.Text = "Iniciar refeição";
            txtInicioEsquerda.Text = "10";
            txtInicioSuperior.Text = "20";
            txtComprimento.Text = "250";
            txtAltura.Text = "20";
            txtDistanciaRetangulos.Text = "80";
        }

        private void BtnMakeCode_Click(object sender, EventArgs e)
        {
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
            List<string> listOfTexts = new List<string>();
            listOfTexts.Add(txtText1.Text);
            listOfTexts.Add(txtText2.Text);
            listOfTexts.Add(txtText3.Text);
            listOfTexts.Add(txtText4.Text);
            listOfTexts.Add(txtText5.Text);
            listOfTexts.Add(txtText6.Text);
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
                sw.WriteLine("ctx.stroke();");

                if (i < listOfTexts.Count-1)
                {
                    sw.WriteLine("ctx.beginPath();");
                    sw.WriteLine($"ctx.moveTo('{comprimento / 2}','{y + i * distancia - (int)(altura * .74) + altura}');");
                    sw.WriteLine($"ctx.lineTo('{comprimento / 2}','{y + i * distancia - (int)(altura * .74) + distancia}');");
                    sw.WriteLine("ctx.stroke();");
                }
            }
        }


        private void WriteEndOfFile(StreamWriter sw)
        {
            sw.WriteLine("	</script>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
        }
    }
}
