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
using CapaNegocios;

namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para MonografiasAdminAgr.xaml
    /// </summary>
    public partial class MonografiasAdminAgr : UserControl
    {
 
        ServicioEstudiante ServicioEstudiante = new ServicioEstudiante();
        List<Estudiantes> Estudiantes = new List<Estudiantes>();

        ServicioProfesor ServicioProfesor = new ServicioProfesor();
        List<Profesores> Profesores = new List<Profesores>();

        ServicioCarreras ServicioCarreras = new ServicioCarreras();
        List<Carreras> Carreras = new List<Carreras>();


        Estudiantes Estudiante = new Estudiantes();
        Profesores Profesor = new Profesores();

        int id = 0;
        public MonografiasAdminAgr()
        {
            InitializeComponent();
            refrescarlistas();
            
        }



        private void agregarEstudiante_Click(object sender, RoutedEventArgs e)
        {
           refrescarlistas();
            
            var veri = Estudiantes.Where(x => x.Carnet == txtCarnet.Text).FirstOrDefault();
            if (veri != null)
            {
                actualizarAlumno();
               
            }
            else
            {
                ingresarEstu();
            }
            refrescarlistas();
        }

        private void agregarProfesor_Click(object sender, RoutedEventArgs e)
        {
            if(txtNombrePR.Text == "" || txtApellidoPR.Text == "" ||
                dpFechaNacPR.SelectedDate == null || txtDirrecionPR.Text == "" ||
                txtTelefonoPR.Text == "" || dpFechaNacPR.Text == "")
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }
            ingresarProfesor();

        }

        private void ActualizarProfesor_Click(object sender, RoutedEventArgs e)
        {
            if(id == 0) {
                MessageBox.Show("Por favor seleccione un profesor");
                return;
            }
            if (txtNombrePR.Text == "" || txtApellidoPR.Text == "" ||
                dpFechaNacPR.SelectedDate == null || txtDirrecionPR.Text == "" ||
                txtTelefonoPR.Text == "" || dpFechaNacPR.Text == "")
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }
            ActualizarProfesor();
        }


        void refrescarlistas()
        {
            Estudiantes = ServicioEstudiante.ObtenerEstudiantes();
            Profesores = ServicioProfesor.ObtenerProfesores();
            Carreras = ServicioCarreras.ObtenerCarreras();

            dgEstudiantes.ItemsSource = null;
            dgProfesores.ItemsSource = null;

            dgProfesores.ItemsSource = Profesores;
            dgEstudiantes.ItemsSource = Estudiantes;
        }
        void ingresarEstu()
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" ||
                dpFechaNac.SelectedDate == null || txtCarnet.Text == "" ||
                txtDirrecion.Text == "" || txtTelefono.Text == "" || dpFechaNac.Text == "")
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }

            Estudiante.Nombre = txtNombre.Text;
            Estudiante.Apellido = txtApellido.Text;
            Estudiante.FechaNac = dpFechaNac.SelectedDate.Value;
            Estudiante.Carnet = txtCarnet.Text;

            var Id_carrera = from c in Carreras where c.Nombre == cbcarrera.Text select c.ID;

            Estudiante.Id_Carrera = Id_carrera.First();
            Estudiante.Dirrecion = txtDirrecion.Text;
            Estudiante.Telefono = txtTelefono.Text;

            ServicioEstudiante.AgregarEstudiante(Estudiante);
            MessageBox.Show("Estudiante agregado");
            refrescarlistas();
        }

        void actualizarAlumno() 
        {
        
            var estudiante = Estudiantes.Where(x => x.Carnet == txtCarnet.Text).FirstOrDefault();
            if (estudiante != null) {
                estudiante.Nombre = txtNombre.Text;
                estudiante.Apellido = txtApellido.Text;
                estudiante.FechaNac = dpFechaNac.SelectedDate.Value;
                estudiante.Carnet = txtCarnet.Text;

                var Id_carrera = from c in Carreras where c.Nombre == cbcarrera.Text select c.ID;

                estudiante.Id_Carrera = Id_carrera.First();
                estudiante.Dirrecion = txtDirrecion.Text;
                estudiante.Telefono = txtTelefono.Text;

                ServicioEstudiante.ModificarEstudiante(estudiante.Carnet,estudiante);
                MessageBox.Show("Estudiante actualizado");
                refrescarlistas();
            }

        }

        void ingresarProfesor()
        {
            if (txtNombrePR.Text == "" || txtApellidoPR.Text == "" ||
             dpFechaNacPR.SelectedDate == null || txtDirrecionPR.Text == "" ||
             txtTelefonoPR.Text == "" || dpFechaNacPR.Text == "")
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }

            Profesor.Nombre = txtNombrePR.Text;
            Profesor.Apellido = txtApellidoPR.Text;
            Profesor.FechaNac = dpFechaNacPR.SelectedDate.Value;
            Profesor.Dirrecion = txtDirrecionPR.Text;
            Profesor.Telefono = txtTelefonoPR.Text;

            MessageBox.Show("Profesor agregado");
            ServicioProfesor.AgregarProfesor(Profesor);
            refrescarlistas();
        }

        void ActualizarProfesor() 
        {
            if (txtNombrePR.Text == "" || txtApellidoPR.Text == "" ||
                 dpFechaNacPR.SelectedDate == null || txtDirrecionPR.Text == "" ||
                 txtTelefonoPR.Text == "" || dpFechaNacPR.Text == "")
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }

            var profesor = Profesores.Where(x => x.Id == id).FirstOrDefault();
            if (profesor != null) {
                profesor.Nombre = txtNombrePR.Text;
                profesor.Apellido = txtApellidoPR.Text;
                profesor.FechaNac = dpFechaNacPR.SelectedDate.Value;
                profesor.Dirrecion = txtDirrecionPR.Text;
                profesor.Telefono = txtTelefonoPR.Text;

                ServicioProfesor.ModificarProfesor(id, profesor);
                MessageBox.Show("Profesor actualizado");
                refrescarlistas();
            }

        }

        private void cbcarrera_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var carrerasFiltradas = from c in Carreras where c.Nombre.Contains(text) select c.Nombre;
                comboBox.ItemsSource = carrerasFiltradas;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = carrerasFiltradas.Any();
            }
            refrescarlistas();
        }

        private void dgEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgEstudiantes.SelectedItem != null)
            {
                var estudiante = (Estudiantes)dgEstudiantes.SelectedItem;
                txtNombre.Text = estudiante.Nombre;
                txtApellido.Text = estudiante.Apellido;
                dpFechaNac.SelectedDate = estudiante.FechaNac;
                txtCarnet.Text = estudiante.Carnet;
                txtDirrecion.Text = estudiante.Dirrecion;
                txtTelefono.Text = estudiante.Telefono;
                cbcarrera.Text = Carreras.Where(x => x.ID == estudiante.Id_Carrera).FirstOrDefault().Nombre;
            }
        }

        private void dgProfesores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( dgProfesores.SelectedItem != null)
            {
                var profesor = (Profesores)dgProfesores.SelectedItem;
                txtNombrePR.Text = profesor.Nombre;
                txtApellidoPR.Text = profesor.Apellido;
                dpFechaNacPR.SelectedDate = profesor.FechaNac;
                txtDirrecionPR.Text = profesor.Dirrecion;
                txtTelefonoPR.Text = profesor.Telefono;
                id = profesor.Id;
            }
        }

        private void cbNombreProfesor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var profesoresFiltrados = from p in Profesores where (p.Nombre.ToLower()).Contains(text.ToLower()) select p.Nombre;
                comboBox.ItemsSource = profesoresFiltrados;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = profesoresFiltrados.Any();

                dgResultProfesores.ItemsSource = null;
                dgResultProfesores.ItemsSource = profesoresFiltrados;
            }
        }

        private void cbNombreAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var alumnosFiltrados = from a in Estudiantes where (a.Nombre.ToLower()).Contains(text.ToLower()) select a;
                var alumnosFiltrados2 = from a in alumnosFiltrados select (a.Nombre + " " + a.Carnet);

                comboBox.ItemsSource = alumnosFiltrados2;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = alumnosFiltrados.Any();

                dgResultAlumnos.ItemsSource = null;
                dgResultAlumnos.ItemsSource = alumnosFiltrados;
            }
        }

        private void cbCarnet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var alumnosFiltrados = from a in Estudiantes where (a.Carnet.ToLower()).Contains(text.ToLower()) select a;
                var alumnosFiltrados2 = from a in alumnosFiltrados select (a.Nombre + "" + a.Carnet);
                comboBox.ItemsSource = alumnosFiltrados2;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = alumnosFiltrados.Any();

                dgResultAlumnos.ItemsSource = null;
                dgResultAlumnos.ItemsSource = alumnosFiltrados;
            }
        }

        private void cbCarrera_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

              
                var estudiantescarreras  = from c in Carreras join es in Estudiantes on c.ID equals es.Id_Carrera where c.Nombre.Contains(text) select es;
                var estudiantescarreras2 = from c in Carreras where c.Nombre.Contains(text) select c.Nombre;

                comboBox.ItemsSource = estudiantescarreras2;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = estudiantescarreras.Any();

                dgResultAlumnos.ItemsSource = null;
                dgResultAlumnos.ItemsSource = estudiantescarreras;
            }
        }
    }
}
