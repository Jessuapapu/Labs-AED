using CapaNegocios;
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
    /// Lógica de interacción para MonoRelaciones.xaml
    /// </summary>
    public partial class MonoRelaciones : UserControl
    {
        public MonoRelaciones()
        {
            InitializeComponent();
            refrescarListas();
        }


        ServicioEstudiante ServicioEstudiante = new ServicioEstudiante();
        List<Estudiantes> Estudiantes = new List<Estudiantes>();

        ServicioProfesor ServicioProfesor = new ServicioProfesor();
        List<Profesores> Profesores = new List<Profesores>();

        ServicioCarreras ServicioCarreras = new ServicioCarreras();
        List<Carreras> Carreras = new List<Carreras>();

        Estudiantes Estudiante = new Estudiantes();
        Profesores Profesor = new Profesores();

        ServicioMonografias SM = new ServicioMonografias();
        List<MonoGrafia> monoGrafias = new List<MonoGrafia>();

        ServicioRoles SR = new ServicioRoles();
        List<Roles> Roles = new List<Roles>();

        Mono_Estus ME = new Mono_Estus();
        List<Estudiante_Mono> Estudiante_Monos = new List<Estudiante_Mono>();

        Mono_Profesor MP = new Mono_Profesor();
        List<Mono_Profesores> Profesor_Monos = new List<Mono_Profesores>();

        private void cbmonografias_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var monosFiltradas = from m in monoGrafias where m.Titulo.Contains(text) select m.Titulo;
                comboBox.ItemsSource = monosFiltradas;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = monosFiltradas.Any();

            }
        }

        private void cbprofesor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;
                var ProfSug = from p in Profesores
                              where (p.Nombre + " " + p.Apellido).ToLower().Contains(text.ToLower())
                              select (p.Nombre + " " + p.Apellido);
                comboBox.ItemsSource = ProfSug;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = ProfSug.Any();
            }
        }

        private void cbRol_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;
                var RolSug = from r in Roles
                             where r.Rol.ToLower().Contains(text.ToLower())
                             select r.Rol;
                comboBox.ItemsSource = RolSug;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = RolSug.Any();
            }
        }

        private void cbMonografiaAlum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var monosFiltradas = from m in monoGrafias where m.Titulo.Contains(text) select m.Titulo;
                comboBox.ItemsSource = monosFiltradas;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = monosFiltradas.Any();

            }
        }

        private void cAlumnos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var alumnosFiltrados = from a in Estudiantes where (a.Nombre.ToLower()).Contains(text.ToLower()) || (a.Carnet.ToLower()).Contains(text.ToLower()) select a;
                var alumnosFiltrados2 = from a in alumnosFiltrados select (a.Nombre + " " + a.Carnet);

                comboBox.ItemsSource = alumnosFiltrados2;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = alumnosFiltrados.Any();

            }
        }

        private void guardarrelacion2_Click(object sender, RoutedEventArgs e)
        {
            var monografiaver = monoGrafias.Where(m => m.Titulo == cbMonografiaAlum.SelectedItem.ToString());
            if (monografiaver.Count() == 3)
            {
                MessageBox.Show("No se puede asignar mas de 3 estudiantes a una monografia");
            }
            if (cbMonografiaAlum.Text == "" || cAlumnos.Text == "")
            {
                MessageBox.Show("Debe seleccionar un estudiante y una monografia");
            }
            else
            {
                var monografia = monoGrafias.FirstOrDefault(m => m.Titulo == cbMonografiaAlum.SelectedItem.ToString());
                var estudiante = Estudiantes.FirstOrDefault(a => (a.Nombre + " " + a.Carnet) == cAlumnos.SelectedItem.ToString());
                if (Estudiante_Monos.Any(r => r.IdMonoGrafia == monografia.Id && r.Carnet == estudiante.Carnet))
                {
                    MessageBox.Show("Ya existe una relacion entre el estudiante y la monografia");
                    return;
                }
                if (monografia == null || estudiante == null)
                {
                    MessageBox.Show("No se encontro la monografia o el estudiante");
                    return;
                }
                var relacion = new Estudiante_Mono
                {
                    IdMonoGrafia = monografia.Id,
                    Carnet = estudiante.Carnet
                };
                ME.IngresarRelacionRegistro(relacion);
                Estudiante_Monos = ME.ObtenerRelacionRegistro();
                MessageBox.Show("Relacion guardada");
            }
            refrescarListas();
        }

        private void guardarrelacion_Click(object sender, RoutedEventArgs e)
        {
            if (cbmonografias.SelectedItem == null || cbprofesor.SelectedItem == null || cbRol.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una monografia, un profesor y un rol");
            }
            else
            {
                var monografia = monoGrafias.FirstOrDefault(m => m.Titulo == cbmonografias.SelectedItem.ToString());
                var profesor = Profesores.FirstOrDefault(p => (p.Nombre + " " + p.Apellido) == cbprofesor.SelectedItem.ToString());
                var rol = Roles.FirstOrDefault(r => r.Rol == cbRol.SelectedItem.ToString());


                if (Profesor_Monos.Any(r => r.IdMonoGrafia == monografia.Id && r.IdProfesor == profesor.Id && r.Rol == rol.Id))
                {
                    MessageBox.Show("Ya existe una relacion entre el profesor y la monografia");
                    return;
                }

                if (monografia == null || profesor == null || rol == null)
                {
                    MessageBox.Show("No se encontro la monografia, el profesor o el rol");
                    return;
                }

                var relacion = new Mono_Profesores
                {
                    IdMonoGrafia = monografia.Id,
                    IdProfesor = profesor.Id,
                    Rol = rol.Id
                };
                MP.IngresarRelacionRegistro(relacion);
                Profesor_Monos = MP.ObtenerRelacionRegistro();
                MessageBox.Show("Relacion guardada");
            }
            refrescarListas();
        }

        private void borrarrrelacion_Click(object sender, RoutedEventArgs e)
        {
            if (cbmonografias.SelectedItem == null || cbprofesor.SelectedItem == null || cbRol.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una monografia, un profesor y un rol");
            }
            else
            {
                var monografia = monoGrafias.FirstOrDefault(m => m.Titulo == cbmonografias.SelectedItem.ToString());
                var profesor = Profesores.FirstOrDefault(p => (p.Nombre + " " + p.Apellido) == cbprofesor.SelectedItem.ToString());
                var rol = Roles.FirstOrDefault(r => r.Rol == cbRol.SelectedItem.ToString());
                var relacion = Profesor_Monos.FirstOrDefault(r => r.IdMonoGrafia == monografia.Id && r.IdProfesor == profesor.Id && r.Rol == rol.Id);
                MP.BorrarRelacionRegistro(relacion);
                Profesor_Monos = MP.ObtenerRelacionRegistro();
                MessageBox.Show("Relacion borrada");
            }
            refrescarListas();
        }

        private void borrarrrelacion2_Click(object sender, RoutedEventArgs e)
        {
            if (cbMonografiaAlum.SelectedItem == null || cAlumnos.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un estudiante y una monografia");
            }
            else
            {
                var monografia = monoGrafias.FirstOrDefault(m => m.Titulo == cbMonografiaAlum.SelectedItem.ToString());
                var estudiante = Estudiantes.FirstOrDefault(a => (a.Nombre + " " + a.Carnet) == cAlumnos.SelectedItem.ToString());
                var relacion = Estudiante_Monos.FirstOrDefault(r => r.IdMonoGrafia == monografia.Id && r.Carnet == estudiante.Carnet);
                ME.BorrarRelacionRegistro(relacion);
                Estudiante_Monos = ME.ObtenerRelacionRegistro();
                MessageBox.Show("Relacion borrada");
            }
            refrescarListas();
        }


        void refrescarListas()
        {
            monoGrafias = SM.ObtenerMonografias();
            Profesores = ServicioProfesor.ObtenerProfesores();
            Estudiantes = ServicioEstudiante.ObtenerEstudiantes();
            Roles = SR.ObtenerRoles();
            Estudiante_Monos = ME.ObtenerRelacionRegistro();
            Profesor_Monos = MP.ObtenerRelacionRegistro();


            cbmonografias.ItemsSource = monoGrafias.Select(m => m.Titulo);
            cbMonografiaAlum.ItemsSource = monoGrafias.Select(m => m.Titulo);
            cbprofesor.ItemsSource = Profesores.Select(p => p.Nombre + " " + p.Apellido);
            cAlumnos.ItemsSource = Estudiantes.Select(a => a.Nombre + " " + a.Carnet);
            cbRol.ItemsSource = Roles.Select(r => r.Rol);

            dgAlumnoMono.ItemsSource = null;
            dgProfesorMono.ItemsSource = null;
            
            var MonoFiltradas = Estudiante_Monos.ToList();

            var MonoFiltradas2 = Profesor_Monos.ToList();

            dgAlumnoMono.ItemsSource = MonoFiltradas2;
            dgProfesorMono.ItemsSource = MonoFiltradas;
        }

        private void dgProfesorMono_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProfesorMono.SelectedItem != null)
            {
                var relacion = (Mono_Profesores)dgProfesorMono.SelectedItem;
                var monografia = monoGrafias.FirstOrDefault(m => m.Id == relacion.IdMonoGrafia);
                var profesor = Profesores.FirstOrDefault(p => p.Id == relacion.IdProfesor);
                var rol = Roles.FirstOrDefault(r => r.Id == relacion.Rol);
                cbmonografias.SelectedItem = monografia.Titulo;
                cbprofesor.SelectedItem = profesor.Nombre + " " + profesor.Apellido;
                cbRol.SelectedItem = rol.Rol;
            }
        }

        private void dgAlumnoMono_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAlumnoMono.SelectedItem != null)
            {
                var relacion = (Estudiante_Mono)dgAlumnoMono.SelectedItem;
                var monografia = monoGrafias.FirstOrDefault(m => m.Id == relacion.IdMonoGrafia);
                var estudiante = Estudiantes.FirstOrDefault(a => a.Carnet == relacion.Carnet);
                cbMonografiaAlum.SelectedItem = monografia.Titulo;
                cAlumnos.SelectedItem = estudiante.Nombre + " " + estudiante.Carnet;
            }
        }

        private void cbNombreBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox combobox && combobox.IsEditable) {
                var text = combobox.Text;
                var MonoFiltradas = from MA in Estudiante_Monos
                                    join MPa in Profesor_Monos
                                    on MA.IdMonoGrafia equals MPa.IdMonoGrafia
                                    join p in Profesores
                                    on MPa.IdProfesor equals p.Id
                                    join Estu in Estudiantes
                                    on MA.Carnet equals Estu.Carnet
                                    join m in monoGrafias
                                    on MA.IdMonoGrafia equals m.Id
                                    where (p.Nombre + " " + p.Apellido).ToLower().Contains(text.ToLower())
                                    || (Estu.Nombre + " " + Estu.Carnet).ToLower().Contains(text.ToLower())
                                    select new
                                    {
                                        Monografia = m.Titulo,
                                        Estudiante = Estu.Nombre,
                                        Profesor = p.Nombre + " " + p.Apellido
                                    };


                combobox.ItemsSource = MonoFiltradas;
                combobox.Text = text;
                combobox.IsDropDownOpen = MonoFiltradas.Any();

                dgresultados.ItemsSource = null;
                dgresultados.ItemsSource = MonoFiltradas;
            }
        }


        private void cbCarreraBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox combobox && combobox.IsEditable)
            {
                var text = combobox.Text;
                var resultado = from MA in Estudiante_Monos
                                join MPa in Profesor_Monos
                                on MA.IdMonoGrafia equals MPa.IdMonoGrafia
                                join p in Profesores
                                on MPa.IdProfesor equals p.Id
                                join Estu in Estudiantes
                                on MA.Carnet equals Estu.Carnet
                                join c in Carreras
                                on Estu.Id_Carrera equals c.ID
                                join m in monoGrafias
                                on MA.IdMonoGrafia equals m.Id
                                where c.Nombre.ToLower().Contains(text.ToLower())
                                      || (p.Nombre + " " + p.Apellido).ToLower().Contains(text.ToLower())
                                      || Estu.Nombre.ToLower().Contains(text.ToLower())
                                select new
                                {
                                    Monografia = m.Titulo,
                                    Estudiante = Estu.Nombre,
                                    Profesor = p.Nombre + " " + p.Apellido,
                                    Carrera = c.Nombre
                                };

                combobox.ItemsSource = resultado;
                combobox.Text = text;
                combobox.IsDropDownOpen = resultado.Any();

                dgresultados.ItemsSource = null;
                dgresultados.ItemsSource = resultado;
;
            }
        }
    }
}
