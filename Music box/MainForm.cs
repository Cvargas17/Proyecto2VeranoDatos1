using System;
using System.Windows.Forms;

namespace Music_box
{
    public class MainForm : Form
    {
        private Button btnAgregar;
        private Button btnMostrar;
        private ListBox lstNotas;

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
            lstNotas = new ListBox() { Left = 10, Top = 50, Width = 540, Height = 300 };

            btnAgregar.Click += BtnAgregar_Click;
            btnMostrar.Click += BtnMostrar_Click;

            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnMostrar);
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
    }
}