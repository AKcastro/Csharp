Criar um programa que gere 1000K de números aleatórios de 1 a 118  e armazene em um arquivo. Apresente um relatório estatístico dos dados contendo:
a.	Média entre os valores.
b.	Desvio Padrão
c.	Variância amostral
d.	Variância populacional
e.	Apresente um relatório de curiosidades dos números.
f.	Quantos números primos existem.
g.	Um histograma dos números primos.

Código:

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Estatistica
{
    public partial class Form1 : Form
    {
        private List<int> numeros = new List<int>();
        private const int quantidade = 1_000_000;
        private const int min = 1;
        private const int max = 118;
        private const string caminhoArquivo = "dados.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerarNumeros_Click(object sender, EventArgs e)
        {
            numeros.Clear();
            Random rand = new Random();

            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                for (int i = 0; i < quantidade; i++)
                {
                    int num = rand.Next(min, max + 1);
                    numeros.Add(num);
                    sw.WriteLine(num);
                }
            }

            MessageBox.Show("Números gerados e salvos com sucesso!");
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            if (numeros.Count == 0)
            {
                if (File.Exists(caminhoArquivo))
                {
                    numeros = File.ReadAllLines(caminhoArquivo).Select(int.Parse).ToList();
                }
                else
                {
                    MessageBox.Show("Gere os números primeiro.");
                    return;
                }
            }

            double media = numeros.Average();
            double varianciaPop = numeros.Select(n => Math.Pow(n - media, 2)).Average();
            double varianciaAmostral = numeros.Select(n => Math.Pow(n - media, 2)).Sum() / (quantidade - 1);
            double desvioPadrao = Math.Sqrt(varianciaPop);
            var primos = numeros.Where(IsPrimo).ToList();
            var maisFrequente = numeros.GroupBy(n => n).OrderByDescending(g => g.Count()).First().Key;
            var distintos = numeros.Distinct().Count();

            var histograma = primos.GroupBy(p => p)
                                   .OrderBy(p => p.Key)
                                   .ToDictionary(g => g.Key, g => g.Count());

            // Exibir relatório
            txtRelatorio.Text = $"Relatório Estatístico:\r\n";
            txtRelatorio.AppendText($"- Média: {media:F2}\r\n");
            txtRelatorio.AppendText($"- Desvio Padrão: {desvioPadrao:F2}\r\n");
            txtRelatorio.AppendText($"- Variância Populacional: {varianciaPop:F2}\r\n");
            txtRelatorio.AppendText($"- Variância Amostral: {varianciaAmostral:F2}\r\n\r\n");

            txtRelatorio.AppendText("Curiosidades:\r\n");
            txtRelatorio.AppendText($"- Número mais frequente: {maisFrequente}\r\n");
            txtRelatorio.AppendText($"- Total de distintos: {distintos}\r\n");
            txtRelatorio.AppendText($"- Total de primos: {primos.Count}\r\n");

            // Atualiza gráfico
            chartPrimos.Series.Clear();
            var serie = new Series("Primos")
            {
                ChartType = SeriesChartType.Column
            };

            foreach (var kvp in histograma)
            {
                serie.Points.AddXY(kvp.Key, kvp.Value);
            }

            chartPrimos.Series.Add(serie);
        }

        private bool IsPrimo(int numero)
        {
            if (numero < 2) return false;
            for (int i = 2; i <= Math.Sqrt(numero); i++)
                if (numero % i == 0) return false;
            return true;
        }
    }
}

