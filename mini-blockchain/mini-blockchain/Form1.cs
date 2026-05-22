using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//ALUMNA:Sarai Fregozo Aréchiga Código: 219523217
namespace mini_blockchain
{

    public partial class Form1 : Form
    {
        Blockchain bc = new Blockchain();
        public Form1()
        {
            InitializeComponent();

        }
        //Agrega un nuevo bloque con los datos ingresados por el usuario.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(tbDatos.Text))
            {
                MessageBox.Show("Escribe algún dato.");
                return;
            }

            bc.AddBlock(tbDatos.Text);
            MessageBox.Show("Bloque agregado correctamente.");
        }

        //Recarga la cadena desde la base de datos y actualiza la tabla.
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            // Carga de nuevo toda la cadena desde la base de datos
            bc = new Blockchain();

            // Limpia el DataGridView y asigna la nueva fuente de datos
            dgInformación.DataSource = null;

            // Se usa BindingSource para mejor la actualización de datos
            BindingSource bs = new BindingSource();
            bs.DataSource = bc.GetChain();
            dgInformación.DataSource = bs;

            // Ajustar columnas automáticamente
            dgInformación.AutoResizeColumns();
        }

        //Verifica la integridad de la cadena y muestra el resultado.
        private void btnVerificación_Click(object sender, EventArgs e)
        {
            if (bc.VerifyChain(out string msg))
                MessageBox.Show(msg, "Verificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(msg, "Cadena corrupta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
