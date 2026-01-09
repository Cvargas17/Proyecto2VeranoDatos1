using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Miusic_box;
using Microsoft.VisualBasic;

namespace Music_box
{
    public class MainForm : Form
    {
        private Button btnAgregar;
        private Button btnMostrar;
        private ListBox lstNotas;
        private TextBox txtInput;
        private Button btnGuardar;
        private Button btnPlay;
        private Button btnReversa;
        private Button btnLoop;
        private Button btnPausa;

        private ListaDobleEnlazada<Notamusical> listaNotas = new ListaDobleEnlazada<Notamusical>();
        private bool isLooping = false;
        private bool isPaused = false;
        private double duracionNegra = 0.5; // duración en segundos para una negra
        private System.Threading.Thread reproduccionThread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Music Box - Interfaz";
            this.Width = 600;
            this.Height = 400;

            btnAgregar = new Button() { Left = 10, Top = 10, Width = 120, Text = "Agregar nota" };
            btnMostrar = new Button() { Left = 140, Top = 10, Width = 120, Text = "Mostrar notas" };
            txtInput = new TextBox() { Left = 280, Top = 12, Width = 170 };
            btnGuardar = new Button() { Left = 460, Top = 10, Width = 120, Text = "Cambiar Duración" };
            btnPlay = new Button() { Left = 10, Top = 40, Width = 120, Text = "Play" };
            btnReversa = new Button() { Left = 140, Top = 40, Width = 120, Text = "Reversa" };
            btnLoop = new Button() { Left = 280, Top = 40, Width = 120, Text = "Loop: Off" };
            btnPausa = new Button() { Left = 410, Top = 40, Width = 140, Text = "Pausa", Enabled = false };

            lstNotas = new ListBox() { Left = 10, Top = 80, Width = 540, Height = 270 };

            btnAgregar.Click += BtnAgregar_Click;
            btnMostrar.Click += BtnMostrar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnPlay.Click += BtnPlay_Click;
            btnReversa.Click += BtnReversa_Click;
            btnLoop.Click += BtnLoop_Click;
            btnPausa.Click += BtnPausa_Click;

            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnMostrar);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnPlay);
            this.Controls.Add(btnReversa);
            this.Controls.Add(btnLoop);
            this.Controls.Add(btnPausa);
            this.Controls.Add(lstNotas);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Ingrese una nota y figura.\nEjemplo: DO NEGRA",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] partes = input.Split(' ');
            if (partes.Length < 2)
            {
                MessageBox.Show("Ingrese una nota y figura.\nEjemplo: DO NEGRA",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Enum.TryParse(partes[0], out Nota nota))
            {
                MessageBox.Show("Nota inválida. Use: DO, RE, MI, FA, SOL, LA, SI.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Enum.TryParse(partes[1], out Figura figura))
            {
                MessageBox.Show("Figura inválida. Use: REDONDA, BLANCA, NEGRA, CORCHEA, SEMICORCHEA.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Notamusical nuevaNota = new Notamusical(nota, figura, duracionNegra);

            listaNotas.Agregar(nuevaNota);
            lstNotas.Items.Add(nuevaNota.ToString());
            txtInput.Clear();
        }

        private void BtnMostrar_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Hay {listaNotas.ObtenerCantidad()} notas en la lista.", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Mostrar diálogo para cambiar la duración de la negra
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Ingrese la nueva duración de la negra (segundos).\nDuración actual: {duracionNegra:0.###}",
                "Cambiar Duración",
                duracionNegra.ToString());

            if (string.IsNullOrEmpty(input))
                return;

            if (double.TryParse(input, out double nuevaDuracion) && nuevaDuracion > 0)
            {
                duracionNegra = nuevaDuracion;
                MessageBox.Show($"Duración de negra actualizada a {duracionNegra:0.###} segundos.", 
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor ingrese un número válido mayor a 0.", 
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (listaNotas.ObtenerCantidad() == 0)
            {
                MessageBox.Show("No hay elementos para reproducir.", "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isPaused = false;
            btnPausa.Enabled = true;
            btnPausa.Text = "Pausa";
            reproduccionThread = new System.Threading.Thread(() => ReproducirNotas(false))
            {
                IsBackground = true
            };
            reproduccionThread.Start();
        }

        private void ReproducirNotas(bool reversa)
        {
            var enumerador = reversa ? listaNotas.ObtenerEnumeradorInverso() : listaNotas.ObtenerEnumerador();
            
            do
            {
                foreach (var nota in enumerador)
                {
                    if (isPaused)
                    {
                        // Esperar mientras esté pausado
                        while (isPaused)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    nota.Reproducir();
                    System.Threading.Thread.Sleep(100); // pequeña pausa entre notas
                }
                
                // Si no estamos en loop, salir del bucle
                if (!isLooping)
                    break;
                    
                // Reiniciar el enumerador para el siguiente ciclo de loop
                enumerador = reversa ? listaNotas.ObtenerEnumeradorInverso() : listaNotas.ObtenerEnumerador();
            } while (isLooping);
            
            // Deshabilitar el botón de pausa al terminar
            this.Invoke((MethodInvoker)(() =>
            {
                btnPausa.Enabled = false;
                btnPausa.Text = "Pausa";
                isPaused = false;
            }));
        }

        private void BtnReversa_Click(object sender, EventArgs e)
        {
            if (listaNotas.ObtenerCantidad() == 0)
            {
                MessageBox.Show("No hay elementos para reproducir.", "Reversa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isPaused = false;
            btnPausa.Enabled = true;
            btnPausa.Text = "Pausa";
            reproduccionThread = new System.Threading.Thread(() => ReproducirNotas(true))
            {
                IsBackground = true
            };
            reproduccionThread.Start();
        }

        private void BtnLoop_Click(object sender, EventArgs e)
        {
            isLooping = !isLooping;
            btnLoop.Text = isLooping ? "Loop: On" : "Loop: Off";
            btnLoop.BackColor = isLooping ? System.Drawing.Color.LightGreen : System.Drawing.SystemColors.Control;
        }

        private void BtnPausa_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            btnPausa.Text = isPaused ? "Reanudar" : "Pausa";
            btnPausa.BackColor = isPaused ? System.Drawing.Color.Yellow : System.Drawing.SystemColors.Control;
        }
    }
}