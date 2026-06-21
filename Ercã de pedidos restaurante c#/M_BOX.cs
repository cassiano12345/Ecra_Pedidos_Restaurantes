using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mesas_Pedidos
{
    public partial class M_BOX : Form
    {
        public string mensagem { get; set; }
        public string mesa { get; set; }
        public string sala { get; set; }
        public string Nome_sala { get; set; } //Obter o nome da sala
        public object senderr { get; set; }

        public M_BOX()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            // Impede que a janela seja redimensionada 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void label(string mensagem)
        {
            richTextBox1.Text = mensagem;
            this.Text = $"Pedido Sala: {Nome_sala}  Mesa: {mesa} "; // Obter nome da sala
        }

        private void M_BOX_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Pedido_executado(senderr, sala, mesa);  // Chama o método público de Form1
            this.Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Delete_Pedido(senderr,sala,mesa);  // Chama o método público de Form1
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
