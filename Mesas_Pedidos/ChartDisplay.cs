using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Mesas_Pedidos
{
    public partial class ChartDisplay : Form
    {

        string codemp;
        string codutil;


        private String[] args;
        ToolTip tooltip;
        int chartColor = 0;
        private ToolTip toolTip;
        string codempp, codut, opcao, nrvd, trimestre, mes, conta;
        int chartType = 0, ano, x=0, cor=0; //0 - Line ; 1 - Columns
        bool Linhas, barras, pie;

        List<List<KeyValuePair<String, Double>>> listKVP = new List<List<KeyValuePair<string, Double>>>();
        private string additionalInfo;
        List<string> minhaLista = new List<string>();
        private List<string> allGraficos = new List<string>(); // Lista de todas as imagens
        private List<string> activeGraficos = new List<string>(); // Lista de imagens ativas
        private int currentImageIndex = 0; // Índice da imagem atual
        private List<Image> allImages = new List<Image>(); // Lista de todas as imagens
        private List<Image> activeImages = new List<Image>(); // Lista de imagens ativas


        public ChartDisplay()
        {
            InitializeComponent();
          
        }








        private void button2_Click(object sender, EventArgs e)
        {
           
            chartColor++;
            switch (chartColor)
            { //blue red green yellow purple pink cyan
                case 1:
                    chart1.Series[minhaLista[0+cor]].Color = System.Drawing.Color.Red;
                    

                    break;
                case 2:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Green;
                    
                    break;
                case 3:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Yellow;
                    
                    break;
                case 4:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Purple;
                    
                    break;
                case 5:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Pink;
                   
                    break;
                case 6:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Cyan;
                    
                    break;
                default:
                    chart1.Series[minhaLista[0+ cor]].Color = System.Drawing.Color.Blue;
                    if (cor < minhaLista.Count)
                        cor++;
                    if (cor == minhaLista.Count)
                        cor=0;
                    chartColor = 0;
                    break;
            }
            
        }






    }
}
