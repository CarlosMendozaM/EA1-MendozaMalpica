using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEAP1_MendozaMalpica
{
    public partial class frmTipoDocumento : Form
    {
        public frmTipoDocumento()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            var adaptador = new dsAppTableAdapters.TipoDocumentoTableAdapter();
            var tabla = adaptador.GetData();
            dgvDocumento.DataSource = tabla;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmTipoDocumentoEdit();
            frm.ShowDialog();
            CargarDatos();
        }

        private int getId()
        {
            try
            {
                DataGridViewRow filaactual = dgvDocumento.CurrentRow;
                if (filaactual == null)
                {
                    return 0;
                }
                return int.Parse(dgvDocumento.Rows[filaactual.Index].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id > 0)
            {
                var frm = new frmTipoDocumentoEdit(id);
                frm.ShowDialog();
                CargarDatos();
            }else
            {
                MessageBox.Show("Seleccione un ID valido" , "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id>0)
            {
                DialogResult rspta = MessageBox.Show("¿Esta seguro de eliminar el registro?", "Sistemas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rspta == DialogResult.Yes)
                {
                    var adaptador = new dsAppTableAdapters.TipoDocumentoTableAdapter();
                    adaptador.Remove(id);

                    MessageBox.Show("Se elimino el registro", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
            }else
            {
                MessageBox.Show("Seleccione ID valido", "Sistemas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
