Faça um programa que, a partir de um texto digitado pelo usuário, conte o número de caracteres total e o número de palavras (palavra é definida por qualquer sequência de caracteres delimitada por espaços em branco) e exiba o resultado.

Código: 
using System;
using System.Linq;
using System.Windows.Forms;

namespace ContadorTexto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            string texto = txtTexto.Text;

            int totalCaracteres = texto.Length;

            // Split por espaço em branco e ignora strings vazias
            int totalPalavras = texto.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            lblResultado.Text = $"Total de caracteres: {totalCaracteres}\nTotal de palavras: {totalPalavras}";
        }
    }
}
