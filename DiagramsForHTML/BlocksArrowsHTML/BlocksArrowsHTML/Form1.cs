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
            txtDistanciaRetangulos.Text = "100";
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
            sw.WriteLine("    <canvas id=\"figura1\" width=\"320px\" height=\"1000px\"></canvas>");
            sw.WriteLine("	");
            sw.WriteLine("  <script>");
        }

        private void WriteCode(StreamWriter sw)
        {
            int x = Convert.ToInt32(txtInicioEsquerda.Text);
            int y = Convert.ToInt32(txtInicioSuperior.Text);
            int comprimento = Convert.ToInt32(txtComprimento.Text);
            int altura = Convert.ToInt32(txtAltura.Text);
            int distancia = Convert.ToInt32(txtDistanciaRetangulos.Text);

            sw.WriteLine("let canvas = document.getElementById('figura1');");
            sw.WriteLine("let ctx = canvas.getContext('2d');");

            sw.WriteLine("ctx.font=\"16px Arial\";");
            sw.WriteLine($"ctx.fillText('{txtText1.Text}',{x},{y+ 0*distancia});");

            sw.WriteLine($"ctx.rect({x-2},{y+0*distancia-(int)(altura*.7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

            sw.WriteLine($"ctx.fillText('{txtText2.Text}', {x}, {y + 1*distancia});");

            sw.WriteLine($"ctx.rect({x - 2},{y + 1*distancia - (int)(altura * .7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

            sw.WriteLine($"ctx.fillText('{txtText3.Text}', {x}, {y + 2*distancia});");

            sw.WriteLine($"ctx.rect({x - 2},{y + 2*distancia - (int)(altura * .7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

            sw.WriteLine($"ctx.fillText('{txtText4.Text}', {x}, {y + 3 * distancia});");

            sw.WriteLine($"ctx.rect({x - 2},{y + 3 * distancia - (int)(altura * .7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

            sw.WriteLine($"ctx.fillText('{txtText5.Text}', {x}, {y + 4 * distancia});");

            sw.WriteLine($"ctx.rect({x - 2},{y + 4 * distancia - (int)(altura * .7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

            sw.WriteLine($"ctx.fillText('{txtText6.Text}', {x}, {y + 5 * distancia});");

            sw.WriteLine($"ctx.rect({x - 2},{y + 5 * distancia - (int)(altura * .7)},{comprimento},{altura});");
            sw.WriteLine("ctx.stroke();");

        }

        private void WriteEndOfFile(StreamWriter sw)
        {
            sw.WriteLine("	</script>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
        }
    }
}
