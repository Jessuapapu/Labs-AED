using Monografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para MonografiasAdminAgr.xaml
    /// </summary>
    public partial class MonografiasAdminAgr : UserControl
    {
        private List<Estudiantes> Estu = new List<Estudiantes>();
        private List<Profesores> Prof = new List<Profesores>();

        public event Action<List<Estudiantes>> ActuEstudiantes;
        public event Action<List<Profesores>> ActuProfesores;

        public MonografiasAdminAgr()
        {
            InitializeComponent();
        }


        private void EnviarListas()
        {
            ActuEstudiantes?.Invoke(Estu);
            MessageBox.Show("Estudiantes enviados");
            
            MessageBox.Show("Profesores enviados");
        }

        private void agregarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            try { 
                Estudiantes estudiante = new Estudiantes(txtCarnet.Text, txtNombre.Text, txtApellido.Text, txtDirrecion.Text, txtTelefono.Text, DateTime.Parse(dpFechaNac.Text));

                Estu.Add(estudiante);
                
            }
            catch
            {
                MessageBox.Show("Ingrese correctamente los datos del estudiante");
                return;
            }
            EnviarListas();
        }

        private void agregarProfesor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Profesores profesor = new Profesores(Prof.Count(),txtNombrePR.Text, txtApellidoPR.Text, txtDirrecionPR.Text, txtTelefonoPR.Text, DateTime.Parse(dpFechaNacPR.Text));
                Prof.Add(profesor);
                ActuProfesores?.Invoke(Prof);
            }
            catch
            {
                MessageBox.Show("Ingrese correctamente los datos del profesor");
                return;
            }
            EnviarListas();
        }

        private void txtApellidoX_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Consulta de estudiantes
            var queryEstudiantes = from estudiante in Estu
                                   where estudiante.Apellido == txtApellidoX.Text
                                   select estudiante;

            // Consulta de profesores
            var queryProfesores = from profesor in Prof
                                  where profesor.Apellido == txtApellidoX.Text
                                  select profesor;

            dgResult.Items.Clear();
            dgResult.ItemsSource = queryEstudiantes;
            dgResult.ItemsSource = queryProfesores;
            dgResult.Items.Refresh();
        }

        private void dpFechaX_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Consulta de estudiantes
            var queryEstudiantes = from estudiante in Estu
                                   where estudiante.AñoNac == DateTime.Parse(dpFechaX.Text)
                                   select estudiante;

            // Consulta de profesores
            var queryProfesores = from profesor in Prof
                                  where profesor.AñoNac == DateTime.Parse(dpFechaX.Text)
                                  select profesor;

            dgResult.Items.Clear();
            dgResult.ItemsSource = queryEstudiantes;
            dgResult.ItemsSource = queryProfesores;
            dgResult.Items.Refresh();
        }

        private void txtNombreX_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Consulta de estudiantes
            var queryEstudiantes = from estudiante in Estu
                                   where estudiante.Nombre == txtNombreX.Text
                                   select estudiante;

            // Consulta de profesores
            var queryProfesores = from profesor in Prof
                                  where profesor.Nombre == txtNombreX.Text
                                  select profesor;

            dgResult.Items.Clear();
            dgResult.ItemsSource = queryEstudiantes;
            dgResult.ItemsSource = queryProfesores;
            dgResult.Items.Refresh();
        }
    }
}
