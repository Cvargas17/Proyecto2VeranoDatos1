using System;
using System.Windows.Forms;
using System.Collections.Generic;

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

        private ListaDobleEnlazada<string> listaTextos = new ListaDobleEnlazada<string>();
        private bool isLooping = false;

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
            btnGuardar = new Button() { Left = 460, Top = 10, Width = 90, Text = "Guardar" };
            btnPlay = new Button() { Left = 10, Top = 40, Width = 120, Text = "Play" };
            btnReversa = new Button() { Left = 140, Top = 40, Width = 120, Text = "Reversa" };
            btnLoop = new Button() { Left = 280, Top = 40, Width = 120, Text = "Loop: Off" };

            lstNotas = new ListBox() { Left = 10, Top = 80, Width = 540, Height = 270 };

            btnAgregar.Click += BtnAgregar_Click;
            btnMostrar.Click += BtnMostrar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnPlay.Click += BtnPlay_Click;
            btnReversa.Click += BtnReversa_Click;
            btnLoop.Click += BtnLoop_Click;

            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnMostrar);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnPlay);
            this.Controls.Add(btnReversa);
            this.Controls.Add(btnLoop);
            this.Controls.Add(lstNotas);
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            // Añade una nota de ejemplo
            var nota = new Notamusical(Nota.DO, Figura.NEGRA, 1.0);
            lstNotas.Items.Add(nota);
        }

        private void BtnMostrar_Click(object? sender, EventArgs e)
        {
            MessageBox.Show($"Hay {lstNotas.Items.Count} notas en la lista.", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            var texto = txtInput.Text?.Trim();
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Ingrese texto antes de guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listaTextos.Agregar(texto);
            lstNotas.Items.Add(texto);
            txtInput.Clear();
        }

        private void BtnPlay_Click(object? sender, EventArgs e)
        {
            if (listaTextos.ObtenerCantidad() == 0)
            {
                MessageBox.Show("No hay elementos para reproducir.", "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var en = listaTextos.ObtenerEnumerador();
            var items = new List<string>();
            while (en.MoveNext()) items.Add(en.Current);

            MessageBox.Show("Play: " + string.Join(", ", items), "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReversa_Click(object? sender, EventArgs e)
        {
            if (listaTextos.ObtenerCantidad() == 0)
            {
                MessageBox.Show("No hay elementos para reproducir.", "Reversa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var en = listaTextos.ObtenerEnumeradorInverso();
            var items = new List<string>();
            while (en.MoveNext()) items.Add(en.Current);

            MessageBox.Show("Reversa: " + string.Join(", ", items), "Reversa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnLoop_Click(object? sender, EventArgs e)
        {
            isLooping = !isLooping;
            btnLoop.Text = isLooping ? "Loop: On" : "Loop: Off";
            btnLoop.BackColor = isLooping ? System.Drawing.Color.LightGreen : System.Drawing.SystemColors.Control;
        }
    }
}