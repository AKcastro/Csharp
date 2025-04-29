1.	Construa um formulário que permita ao usuário abrir um arquivo de uma imagem(utilize uma caixa de dialogo).
2.	Crie um botão que transforme a imagem carregada para escala de cinza.
3.	Crie um botão que remova os efeitos aplicados na imamgem.
4.	Crie um botão que apresente apenas 80% do azul atual da imagem.
5.	 Crie um botão que apresente apenas 80% do azul, do verde e do amarelo.
6.	Crie um efeito que inverte o vermelho com o azul.
7.	Crie um efeito de preto e branco. Caso um dos valores de R, G ou B em um pixel seja maior que 127 os valores de RGB devem ser modificados para 255. Caso um dos valores de R, G ou B seja menor ou igual a 126 os valores de RGB devem ser modificados para 0.
8.	A declaração abaixo permite a criação de num novo bitmap com largura e altura predeterminadas. Pinte teste bitmap de amarelo. Bitmap image = new Bitmap(400, 400);
9.	Utilizando apenas lógica para programar crie uma linha a 45º Graus dentro do bitmap.
10.	Crie um botão que faça o seu próprio efeito na imagem carregada.

Código:
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Imagem2App
{
    public partial class Form1 : Form
    {
        private Image originalImage; 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagens (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Carregar a imagem no PictureBox
                pictureBox1.ImageLocation = openFileDialog.FileName;

                originalImage = new Bitmap(openFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image); 
                Bitmap grayImage = ConvertToGrayscale(originalImage); // Converte a imagem para escala de cinza

                pictureBox1.Image = grayImage; 
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para converter.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayscaleBitmap = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);

                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    grayscaleBitmap.SetPixel(x, y, grayColor);
                }
            }

            return grayscaleBitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox1.Image = originalImage;
            }
            else
            {
                MessageBox.Show("Nenhuma imagem foi carregada para remover efeitos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);
                Bitmap modifiedImage = ReduceBlueChannel(originalImage); // Aplica a redução do azul

                pictureBox1.Image = modifiedImage;
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para modificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap ReduceBlueChannel(Bitmap original)
        {
            Bitmap modifiedBitmap = new Bitmap(original.Width, original.Height);


            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Calcula 80% do valor azul atual
                    int reducedBlue = (int)(pixelColor.B * 0.8);
                    // Garante que o valor do azul não seja menor que 0 nem maior que 255
                    reducedBlue = Math.Max(0, Math.Min(255, reducedBlue));

                    // Cria um novo color com o azul reduzido
                    Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, reducedBlue);

                    // Define o novo pixel na imagem modificada
                    modifiedBitmap.SetPixel(x, y, newColor);
                }
            }

            return modifiedBitmap;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);
                Bitmap modifiedImage = ReduceBlueGreenYellowChannel(originalImage); // Aplica a redução do azul, verde e amarelo

                pictureBox1.Image = modifiedImage;
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para modificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap ReduceBlueGreenYellowChannel(Bitmap original)
        {
            Bitmap modifiedBitmap = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Calcula 80% do valor azul, verde e vermelho
                    int reducedBlue = (int)(pixelColor.B * 0.8);
                    int reducedGreen = (int)(pixelColor.G * 0.8);
                    int reducedRed = (int)(pixelColor.R * 0.8);

                    // Garante que os valores das cores não sejam menores que 0 nem maiores que 255
                    reducedBlue = Math.Max(0, Math.Min(255, reducedBlue));
                    reducedGreen = Math.Max(0, Math.Min(255, reducedGreen));
                    reducedRed = Math.Max(0, Math.Min(255, reducedRed));

                    // Cria um novo color com as cores reduzidas
                    Color newColor = Color.FromArgb(reducedRed, reducedGreen, reducedBlue);

                    modifiedBitmap.SetPixel(x, y, newColor);
                }
            }

            return modifiedBitmap;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);
                Bitmap modifiedImage = InvertRedAndBlue(originalImage); // Aplica a inversão de vermelho e azul

                pictureBox1.Image = modifiedImage;
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para modificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap InvertRedAndBlue(Bitmap original)
        {
            Bitmap modifiedBitmap = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Inverte os valores de vermelho (R) e azul (B)
                    int red = pixelColor.B; // A cor azul vira vermelha
                    int blue = pixelColor.R; // A cor vermelha vira azul
                    int green = pixelColor.G; // O verde permanece inalterado

                    // Cria uma nova cor com as cores invertidas
                    Color newColor = Color.FromArgb(red, green, blue);

                    // Define o novo pixel na imagem modificada
                    modifiedBitmap.SetPixel(x, y, newColor);
                }
            }

            return modifiedBitmap;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);
                Bitmap blackAndWhiteImage = ApplyBlackAndWhiteEffect(originalImage); // Aplica o efeito preto e branco

                pictureBox1.Image = blackAndWhiteImage;
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para modificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap ApplyBlackAndWhiteEffect(Bitmap original)
        {
            Bitmap modifiedBitmap = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Aplica a lógica de preto e branco: 
                    // Se qualquer valor R, G ou B for maior que 127, define como 255 (branco)
                    // Se qualquer valor R, G ou B for menor ou igual a 126, define como 0 (preto)
                    int red = (pixelColor.R > 127 || pixelColor.G > 127 || pixelColor.B > 127) ? 255 : 0;
                    int green = (pixelColor.R > 127 || pixelColor.G > 127 || pixelColor.B > 127) ? 255 : 0;
                    int blue = (pixelColor.R > 127 || pixelColor.G > 127 || pixelColor.B > 127) ? 255 : 0;

                    // Cria uma nova cor com os valores de preto ou branco
                    Color newColor = Color.FromArgb(red, green, blue);

                    // Define o novo pixel na imagem modificada
                    modifiedBitmap.SetPixel(x, y, newColor);
                }
            }

            return modifiedBitmap;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(400, 400);

            // Preenche a imagem com a cor amarela
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Yellow); // Preenche toda a área do Bitmap com a cor amarela

                // Desenha uma linha a 45º, do canto superior esquerdo ao canto inferior direito
                Pen pen = new Pen(Color.Black, 3); // Usando uma caneta preta com espessura 3
                g.DrawLine(pen, 0, 0, image.Width - 1, image.Height - 1); // Desenha a linha diagonal
            }

            pictureBox1.Image = image;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap image = new Bitmap(pictureBox1.Image);
                Bitmap brightImage = ApplyBrightnessEffect(image); // Aplica o efeito de brilho

                pictureBox1.Image = brightImage; 
            }
            else
            {
                MessageBox.Show("Nenhuma imagem carregada para modificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap ApplyBrightnessEffect(Bitmap original)
        {
            Bitmap brightBitmap = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Aumenta o brilho aumentando o valor de cada canal (R, G, B) de cada pixel
                    int red = Math.Min(pixelColor.R + 50, 255);   // Aumenta o valor do canal vermelho, mas não ultrapassa 255
                    int green = Math.Min(pixelColor.G + 50, 255); // Aumenta o valor do canal verde, mas não ultrapassa 255
                    int blue = Math.Min(pixelColor.B + 50, 255);  // Aumenta o valor do canal azul, mas não ultrapassa 255

                    // Cria uma nova cor com os valores modificados
                    Color brightColor = Color.FromArgb(red, green, blue);

                    // Define o novo pixel na imagem modificada
                    brightBitmap.SetPixel(x, y, brightColor);
                }
            }

            return brightBitmap;
        }
    }
}
