using Microsoft.VisualBasic;
using NotasApp.Applications.Interfaces;
using NotasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotasApp.Presentation
{
    public partial class Form1 : Form
    {
        private IEstudianteServices estudianteServices;
        int selection = -1;
        string CarnetEstudent = String.Empty;
        public static float promedio = 0;
        public Form1(IEstudianteServices estudianteServices)
        {
            this.estudianteServices = estudianteServices;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            dgEstudiantes.ReadOnly = true;
            toolStripButtonUpdate.Enabled = false;
            toolStripButtonDelete.Enabled = false;
          
        }

        private void dgEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {  
                selection = e.RowIndex;
               
            }
            
        }

        private void promedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dgEstudiantes.RowCount > 0)
            {
                Estudiante estudiante = estudianteServices.FindById(GetItemSelect());
                MessageBox.Show($"Estudiante {estudiante.Nombres} " +
                                $"{estudiante.Apellidos}\n" +
                                $"Carnet: {estudiante.Carnet}\n" +
                                $"Promedio total:{funcPromedio(estudiante)}",
                                "Información de estudiante", MessageBoxButtons.OK);
                dgEstudiantes.ClearSelection();
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textName.TextLength == 0
                || textLastName.TextLength == 0
                || textCarnet.TextLength == 0
                || textEmail.TextLength == 0
                || textPhone.TextLength == 0
                || textAdress.TextLength == 0
                || textBoxCalculo.TextLength == 0
                || textBoxContabilidad.TextLength == 0
                || textBoxEstadistica.TextLength == 0
                || textBoxProg.TextLength == 0)
            {
                MessageBox.Show("Porfavor llene todos los datos", "Ingreso de datos no completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else
            {
                Estudiante estudiante = new Estudiante()
                {
                    Nombres = textName.Text,
                    Apellidos = textLastName.Text,
                    Carnet = textCarnet.Text,
                    Direccion = textAdress.Text,
                    Correo = textEmail.Text,
                    Phone = textPhone.Text,
                    Contabilidad = int.Parse(textBoxContabilidad.Text),
                    Estadistica = int.Parse(textBoxEstadistica.Text),
                    Matematica = int.Parse(textBoxCalculo.Text),
                    Programacion = int.Parse(textBoxProg.Text)
                };

                estudianteServices.Create(estudiante);
                LoadData();
                Clean();
            } 

            
        }

        private void LoadData() => dgEstudiantes.DataSource = estudianteServices.GetAll();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Value = Interaction.InputBox("Ingrese el Carnet del estudiante a buscar","Busqueda por carnet");
            if (!String.IsNullOrEmpty(Value))
            {
                CarnetEstudent = Value;
                string carnet = Value;
                ViewEstudent(carnet);
            }
            
        }

        public void ViewEstudent(string carnet)
        {
            IEnumerable<string> dni = from item in estudianteServices.GetAll()
                                   select item.Carnet;

            if (!dni.Contains(carnet))
            {
                MessageBox.Show($"No se encuentra el ID {carnet} en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
           
                toolStripButtonUpdate.Enabled = true;
                toolStripButtonDelete.Enabled = true;
                var estudiante = estudianteServices.FindByCarnet(carnet);
                promedio = funcPromedio(estudiante);
                LoadEstudent(true);

                textName.Text = estudiante.Nombres;
                textLastName.Text = estudiante.Apellidos;
                textCarnet.Text = estudiante.Carnet;
                textAdress.Text = estudiante.Direccion;
                textPhone.Text = estudiante.Phone;
                textEmail.Text = estudiante.Correo;

                groupBox1.Enabled = false;
                textBoxProg.Text = estudiante.Programacion.ToString();
                textBoxEstadistica.Text = estudiante.Estadistica.ToString();
                textBoxCalculo.Text = estudiante.Matematica.ToString();
                textBoxContabilidad.Text = estudiante.Contabilidad.ToString();

            }
        }

        public void Clean()
        {
            textName.Clear();
            textLastName.Clear();
            textCarnet.Clear();
            textAdress.Clear();
            textPhone.Clear();
            textEmail.Clear();

            textBoxProg.Clear();
            textBoxEstadistica.Clear();
            textBoxCalculo.Clear();
            textBoxContabilidad.Clear();

        }

        private void toolStripButtonCancell_Click(object sender, EventArgs e)
        {
            Clean();
            LoadEstudent(false);
            toolStripButtonUpdate.Enabled = false;
            toolStripButtonDelete.Enabled = false;
            groupBox1.Enabled=true;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var estudiante = estudianteServices.FindByCarnet(CarnetEstudent);
                estudianteServices.Delete(estudiante);
                Clean();
                LoadData();
                LoadEstudent(false);
                toolStripButtonUpdate.Enabled = false;
                toolStripButtonDelete.Enabled = false;
                groupBox1.Enabled = true;
            }
            
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(dgEstudiantes.RowCount > 0)
                {
                    if (MessageBox.Show("¿Desea eliminar este registro?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        var estudiante = estudianteServices.FindById(GetItemSelect());
                        estudianteServices.Delete(estudiante);
                        Clean();
                        LoadData();
                    }
                }
                
            }
            catch
            {
                throw;
            }
            
        }
        public Func<Estudiante, float> funcPromedio = (x) => (x.Estadistica + x.Contabilidad + x.Matematica + x.Programacion) / 4;

        public int GetItemSelect() => (int) dgEstudiantes.Rows[selection].Cells[0].Value;

        private void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea realizar el cambio?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var estudiante = estudianteServices.FindByCarnet(CarnetEstudent);
                LoadEstudent(false);
                estudiante.Nombres = textName.Text;
                estudiante.Apellidos = textLastName.Text;
                estudiante.Carnet = textCarnet.Text;
                estudiante.Phone = textPhone.Text;
                estudiante.Direccion = textAdress.Text;
                estudiante.Correo = textEmail.Text;

                estudianteServices.Update(estudiante);
                LoadData();
                Clean();
                groupBox1.Enabled = true;
                toolStripButtonUpdate.Enabled = false;
                toolStripButtonDelete.Enabled = false;
            }
            
        }

        private void textCarnet_TextChanged(object sender, EventArgs e)
        {
            if(textCarnet.Text.Length > 12)
            {
                errorProvider1.SetError(this.textCarnet, "Supera el tamaño de caracteres");
                
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
