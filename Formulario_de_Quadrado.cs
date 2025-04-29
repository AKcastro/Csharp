## Windows Forms
*Pequeno aplicativo feito com Windows Forms que calcula a **área**, **perímetro** e **diagonal** de um quadrado com base no valor informado pelo usuário.*

#### Funcionalidades:
- Entrada do lado do quadrado
- Cálculo automático de:
  - Área
  - Perímetro
  - Diagonal

#### Tecnologias usadas
- C#
- Windows Forms (.NET Framework)
- Visual Studio

Código:
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aula02
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtLado.Text, out double lado))
            {
                double area = lado * lado;
                double perimetro = 4 * lado;
                double diagonal = Math.Round(Math.Sqrt(2) * lado, 2);
                lblArea.Text = area.ToString("F2");
                lblPerimetro.Text = perimetro.ToString("F2");
                lblDiagonal.Text = diagonal.ToString("F2");
            }
            else
            {
                MessageBox.Show("Digite um valor numérico válido para o lado do quadrado.");
            }
        }
    }
}
