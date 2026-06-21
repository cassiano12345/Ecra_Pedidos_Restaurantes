using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Mesas_Pedidos
{
    public partial class Form1 : Form
    {
        private Timer timer;
        public static String ConnectionString = Properties.Settings.Default.ConStr; //Obtendo os dados de ligação a base de dados
        private OracleChangeNotifier _oracleChangeNotifier;
        List<MeusDados> dados; // Lista para obter os dados
        private String[] args;
        public Form1()
        {
            InitializeComponent();
            args = new String[Environment.GetCommandLineArgs().Length];
            args = Environment.GetCommandLineArgs();
            this.Resize += new EventHandler(Form_Resize);
            // Criar instância da classe OracleChangeNotifier
            _oracleChangeNotifier = new OracleChangeNotifier(ConnectionString);
            // Delegar o evento para quando houver mudanças no banco de dados

            // Iniciar a escuta de mudanças
            dados = _oracleChangeNotifier.StartListening(args[1]);     // Obtendo os dados vindos da Base de dados
            CriarBotoes(dados.Count);
            timer = new Timer();
            timer.Interval = 6000; // Define o intervalo de 5 segundos.
            timer.Tick += atualizar; // Associa o evento Tick.
            timer.Start(); // Inicia o Timer.
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form_Resize(object sender, EventArgs e)
        {
            // Reorganizar os botões quando o formulário é redimensionado
            AjustarBotoes();
        }

        private void AjustarBotoes()
        {
            // Limpar todos os controles dentro do painel pai
            panel3.Controls.Clear();

            // Recriar os botões com base no novo tamanho do painel
            CriarBotoes(dados.Count);
        }

        // Sua função para criar botões permanece a mesma
        private void CriarBotoes(int N_botoes)
        {
            int tamanhoBotao = 300, tamanhoBotaox = 220;
            int espaco = 19;
            int larguraMaxima = panel3.ClientSize.Width;
            int alturaMaxima = panel3.ClientSize.Height;

            int linha = 0;
            int posicaoX = 0;
            int posicaoY = 0;

            for (int coluna = 0; coluna < N_botoes; coluna++)
            {
                Button botao = new Button();
                botao.Size = new Size(tamanhoBotaox, tamanhoBotao);

                if (posicaoX + tamanhoBotaox + espaco > larguraMaxima)
                {
                    linha++;
                    posicaoX = 0;
                }

                posicaoY = linha * (tamanhoBotao + espaco);

                if (posicaoY + tamanhoBotao + espaco > alturaMaxima)
                {
                    panel3.AutoScroll = true;
                }

                botao.Location = new Point(posicaoX, posicaoY);
                // Capturar o índice dentro do escopo do loop
                int indexAtual = coluna;
                if (dados[indexAtual].status == "P")
                {
                    botao.BackColor = Color.Salmon;
                }
                if (dados[indexAtual].status == "E")
                {
                    botao.BackColor = Color.LightGreen;
                }
                _oracleChangeNotifier.selecionar_sala(dados[indexAtual].sala, args[1]);
                string mensagem = $"{dados[indexAtual].Descricao}\n";
                string sala = dados[indexAtual].sala, mesa = dados[indexAtual].mesa, Nome_sala = _oracleChangeNotifier.Nom_sala;
                
                // Associar o evento Click com base no índice capturado
                botao.Click += (sender, e) => Botao_Click(sender, e, mensagem, sala, mesa, Nome_sala);
                // Associar o evento Paint com o índice correto
                botao.Paint += (sender, e) => Botao_Paint(sender, e, dados[indexAtual]);

                panel3.Controls.Add(botao);

                posicaoX += tamanhoBotaox + espaco;
            }
        }

        private void Botao_Paint(object sender, PaintEventArgs e, MeusDados dados)
        {
            Button botao = sender as Button;

            // Definir fontes e pincéis
            Font fonte = new Font("Arial", 12, FontStyle.Regular);
            Brush brush = Brushes.Black;

            // Definir posições para cada pedaço de texto (em pixels)
            Point salaPos = new Point(5, 5); // Superior esquerdo
            Point mesaPos = new Point(botao.Width - 53, 5); // Superior direito
                                                            // Quantidade no canto inferior esquerdo
            Point quantidadePos = new Point(5, botao.Height - 30); // Inferior esquerdo

            // Definir área de texto centralizada no botão
            Rectangle descricaoArea = new Rectangle(5, 5, botao.Width - 10, botao.Height - 10); // Margem de 5 pixels em cada lado
            
            _oracleChangeNotifier.selecionar_sala(dados.sala, args[1]);
            // Sala e Mesa
            e.Graphics.DrawString($"{_oracleChangeNotifier.Nom_sala}", fonte, brush, salaPos);
            //e.Graphics.DrawString($"M: {dados.mesa}", fonte, brush, mesaPos);
            if (dados.Descricao.Length > 214) {
                string descricaoCurta = $"{dados.Descricao.Substring(0, 225)}\nVer mais...";
                DrawTextWithWordWrap(e.Graphics, descricaoCurta, fonte, brush, descricaoArea);
            }
            else
            {
                DrawTextWithWordWrap(e.Graphics, dados.Descricao, fonte, brush, descricaoArea);
            }
            // Desenhar a descrição com quebra de linha automática
            
            e.Graphics.DrawString($"Mesa: {dados.mesa}", fonte, brush, quantidadePos); // Quantidade no canto inferior esquerdo
        }

        private void DrawTextWithWordWrap(Graphics graphics, string text, Font font, Brush brush, Rectangle area)
        {
            // Ajustar o layout do texto
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, // Centralizar horizontalmente
                LineAlignment = StringAlignment.Center // Centralizar verticalmente
            };

            // Desenhar o texto com quebra automática de linha
            graphics.DrawString(text, font, brush, area, stringFormat);
        }

        private void Botao_Click(object sender, EventArgs e, string mensagem, string sala, string mesa, string Nome_sala)
        {
            M_BOX M_BOX = new M_BOX();
            M_BOX.mesa = mesa;
            M_BOX.sala = sala;
            M_BOX.Nome_sala = Nome_sala; // Passar o nome da sala
            M_BOX.label(mensagem);
            M_BOX.ShowDialog(); // Mostrar a caixa de texto
        }

        public void RecriarBotoes()
        {
            // Limpar todos os botões antes de recriá-los
            panel3.Controls.Clear();
            // Recriar os botões com base nos dados atualizados
            CriarBotoes(dados.Count);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        // Método disparado ao fechar o formulário
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Parar a escuta antes de fechar o formulário
            _oracleChangeNotifier.StopListening();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Delete_Pedido(object sender, string sala, string mesa)
        {
            // Apagar pedidos e remover botão
            _oracleChangeNotifier.Delete_Pedido(sala, mesa, args[1]);
            // Remover o botão correspondente
            Button botao = sender as Button;
            if (botao != null)
            {
                // Remover o botão do panel
                panel3.Controls.Remove(botao);
                botao.Dispose();  // Libera os recursos associados ao botão

                // Opcional: Se precisar remover o dado da lista de dados correspondente ao botão
                var itemToRemove = dados.FirstOrDefault(d => d.sala == sala && d.mesa == mesa);
                if (itemToRemove != null)
                {
                    dados.Remove(itemToRemove);  // Atualiza a lista de dados
                }
            }

        }

        public void Pedido_executado(object sender, string sala, string mesa)
        {
            // Apagar pedidos e remover botão
            _oracleChangeNotifier.Pedido_Executado(sala, mesa, args[1]);
            // Remover o botão correspondente
            Button botao = sender as Button;
            if (botao != null)
            {
                // Remover o botão do panel
                panel3.Controls.Remove(botao);
                botao.Dispose();  // Libera os recursos associados ao botão

                // Opcional: Se precisar remover o dado da lista de dados correspondente ao botão
                var itemToRemove = dados.FirstOrDefault(d => d.sala == sala && d.mesa == mesa);
                if (itemToRemove != null)
                {
                    dados.Remove(itemToRemove);  // Atualiza a lista de dados
                }
            }
        }

        public void atualizar(object sender, EventArgs e)
        {
            dados = _oracleChangeNotifier.StartListening(args[1]);     // Obtendo os dados vindos da Base de dados
            RecriarBotoes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dados = _oracleChangeNotifier.StartListening(args[1]);     // Obtendo os dados vindos da Base de dados
            RecriarBotoes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
