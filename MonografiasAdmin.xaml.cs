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
using Monografia;



namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para MonografiasAdmin.xaml
    /// </summary>
    public partial class MonografiasAdmin : UserControl
    {
        

        public MonografiasAdmin()
        {
            InitializeComponent();

            refrescarlistas();

            cbCarrera.ItemsSource = from c in carreras select c.Nombre;
            cbTutor.ItemsSource = from p in Prof select (p.Nombre + " " + p.Apellido);
        }

        ServicioCarreras SC = new ServicioCarreras();
        ServicioProfesor SP = new ServicioProfesor();
        ServicioMonografias SM = new ServicioMonografias();

        List<Carreras> carreras = new List<Carreras>();
        List<Profesores> Prof = new List<Profesores>();
        List<MonoGrafia> monoGrafias = new List<MonoGrafia>();

        MonoGrafia MonoGrafia;


        private void Actualizar_Click()
        {
            refrescarlistas();
            var monografia = monoGrafias.FirstOrDefault(x => x.Titulo == txtTitulo.Text);

            if(monografia == null)
            {
                MessageBox.Show("La monografía no existe");
                return;
            }

            TimeSpan tiempoPyR;
            TimeSpan tiempoDefensa;

            try
            {
               tiempoPyR = new TimeSpan(0, int.Parse(txtTPYR.Text), 0);
               tiempoDefensa = new TimeSpan(0, int.Parse(txtTDP.Text), 0);
            }
            catch
            {
                MessageBox.Show("El tiempo debe ser un número");
                return;
            }
            
            if(!int.TryParse(txtNota.Text, out int n))
            {
                MessageBox.Show("La nota debe ser un número");
                return;
            }

            if (int.Parse(txtNota.Text) < 0 || int.Parse(txtNota.Text) > 100)
            {
                MessageBox.Show("La nota debe estar entre 0 y 100");
                return;
            }
            
            monografia.Nota = n;
            monografia.TiempoPreYRes = tiempoPyR;
            monografia.TiempoDefensa = tiempoDefensa;

            if (!Prof.Any(Prof => (Prof.Nombre + " " + Prof.Apellido) == cbTutor.Text))
            {
                MessageBox.Show("El profesor no existe");
                return;
            }
            if (!carreras.Any(carreras => carreras.Nombre == cbCarrera.Text))
            {
                MessageBox.Show("La carrera no existe");
                return;
            }

            TimeSpan tiempoOR = new TimeSpan(0, 0, 0);

            if (TimeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Obtener el texto del elemento seleccionado
                string minutos = selectedItem.Content.ToString();

                // Extraer el número de minutos del texto
                int tmp = int.Parse(minutos.Replace(" minutos", "").Trim());

                // Crear un TimeSpan a partir del número de minutos
                tiempoOR = new TimeSpan(0, tmp, 0);
            }

            monografia.TiempoOrtogado = tiempoOR;
            monografia.Carrera = cbCarrera.Text;
            monografia.id_tutor = Prof.Where(p => (p.Nombre + " " + p.Apellido) == cbTutor.Text).Select(p => p.Id).FirstOrDefault();


            SM.ActualizarMonografiaTiempo(monografia.Id,monografia);
            MessageBox.Show("Monografía actualizada");

        }

        private void cbCarrera_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
               var text = comboBox.Text;
                
                var carrerasFiltradas = from c in carreras where c.Nombre.Contains(text) select c.Nombre;
                comboBox.ItemsSource = carrerasFiltradas;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = carrerasFiltradas.Any();
            }
        }


        private void cbTutor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;
                var ProfSug = from p in Prof
                              where (p.Nombre + " " + p.Apellido).ToLower().Contains(text.ToLower())
                              select (p.Nombre + " " + p.Apellido);
                comboBox.ItemsSource = ProfSug;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = ProfSug.Any();
            }
        }

        private void Guardar_Click()
        {
           refrescarlistas();
           

            if (txtTitulo.Text == "" || cbCarrera.Text == "" || cbTutor.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos");
                return;
            }
            if (!Prof.Any(Prof => (Prof.Nombre + " " + Prof.Apellido) == cbTutor.Text))
            {
                MessageBox.Show("El profesor no existe");
                return;
            }
            if (!carreras.Any(carreras => carreras.Nombre == cbCarrera.Text))
            {
                MessageBox.Show("La carrera no existe");
                return;
            }

            int idTut = Prof.Where(p => (p.Nombre + " " + p.Apellido) == cbTutor.Text).Select(p => p.Id).FirstOrDefault();

            TimeSpan tiempoOR = new TimeSpan(0, 0, 0);

            if (TimeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Obtener el texto del elemento seleccionado
                string minutos = selectedItem.Content.ToString();

                // Extraer el número de minutos del texto
                int tmp = int.Parse(minutos.Replace(" minutos", "").Trim());

                // Crear un TimeSpan a partir del número de minutos
                tiempoOR = new TimeSpan(0, tmp, 0);
            }

                MonoGrafia = new MonoGrafia(txtTitulo.Text,idTut,cbCarrera.Text,tiempoOR);
            MessageBox.Show("Monografía guardada");
            SM.AgregarMonografia(MonoGrafia);
            refrescarlistas();
        }




        void refrescarlistas()
        {
            monoGrafias = SM.ObtenerMonografias();
            carreras = SC.ObtenerCarreras();
            Prof = SP.ObtenerProfesores();
            refrescarDataMonos();
        }

        void refrescarDataMonos() 
        {
            dgMonoGrafias.ItemsSource = null;
            dgMonoGrafias.ItemsSource = monoGrafias;
        }

        private void cbBuscarportutor_TextChanged(object sender, TextChangedEventArgs e)
        {
            refrescarlistas();
            if (sender is ComboBox comboBox && comboBox.IsEditable)
            {
                // Guarda el texto actual del ComboBox
                var text = comboBox.Text;

                // Desactiva temporalmente el elemento seleccionado
                comboBox.SelectedItem = null;

                // Realiza las consultas
                var MonosFiltradas = from t in Prof
                                     join m in monoGrafias on t.Id equals m.id_tutor
                                     where (t.Nombre + " " + t.Apellido).ToLower().Contains(text.ToLower())
                                     select m;

                var Profiltrados = from p in Prof
                                   where (p.Nombre + " " + p.Apellido).ToLower().Contains(text.ToLower())
                                   select (p.Nombre + " " + p.Apellido);

                // Asigna las sugerencias al ComboBox
                comboBox.ItemsSource = Profiltrados.ToList();

                // Restablece el texto original manualmente
                comboBox.Text = text;

                // Mantén el menú desplegable abierto si hay sugerencias
                comboBox.IsDropDownOpen = Profiltrados.Any();

                // Actualiza la fuente de datos del DataGrid
                dgMonoGrafiasbuscar.ItemsSource = null;
                dgMonoGrafiasbuscar.ItemsSource = MonosFiltradas.ToList();

                // Obtén el TextBox interno del ComboBox y ajusta el cursor al final
                if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
                {
                    textBox.SelectionStart = textBox.Text.Length; // Coloca el cursor al final del texto
                }
            }
        }




        private void cbBuscarTitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is ComboBox comboBox && comboBox.IsEditable)
            {
                var text = comboBox.Text;

                var monosFiltradas = from m in monoGrafias where m.Titulo.Contains(text) select m.Titulo;
                var monosFiltradas2 = from m in monoGrafias where m.Titulo.Contains(text) select m;


                comboBox.ItemsSource = monosFiltradas;
                comboBox.Text = text;
                comboBox.IsDropDownOpen = monosFiltradas.Any();

                dgMonoGrafiasbuscar.ItemsSource = null;
                dgMonoGrafiasbuscar.ItemsSource = monosFiltradas2;
            }
        }

        private void dgMonoGrafiasbuscar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgMonoGrafiasbuscar.SelectedItem is MonoGrafia monografia)
            {
                txtTitulo.Text = monografia.Titulo;
                cbCarrera.Text = monografia.Carrera;
                cbTutor.Text = Prof.Where(p => p.Id == monografia.id_tutor).Select(p => p.Nombre + " " + p.Apellido).FirstOrDefault();
                
                txtNota.Text = monografia.Nota.ToString();
                txtTPYR.Text = monografia.TiempoPreYRes.Minutes.ToString();
                txtTDP.Text = monografia.TiempoDefensa.Minutes.ToString();

                
                TimeComboBox.SelectedItem = monografia.TiempoOrtogado.Minutes.ToString() + " " + "minutos";

            }
        }

        private void Guardar_Click_1(object sender, RoutedEventArgs e)
        {
            if(txtTitulo.Text == "" || cbCarrera.Text == "" || cbTutor.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos");
                return;
            }

            if (!Prof.Any(Prof => (Prof.Nombre + " " + Prof.Apellido) == cbTutor.Text))
            {
                MessageBox.Show("El profesor no existe");
                return;
            }

            if (!carreras.Any(carreras => carreras.Nombre == cbCarrera.Text))
            {
                MessageBox.Show("La carrera no existe");
                return;
            }


            if (!monoGrafias.Any(m => m.Titulo == txtTitulo.Text))
            {
                Guardar_Click();
                return;
            }
            else
            {
                Actualizar_Click();
            }
            refrescarlistas();
        }

        private void dgMonoGrafias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgMonoGrafias.SelectedItem is MonoGrafia monografia)
            {
                txtTitulo.Text = monografia.Titulo;
                cbCarrera.Text = monografia.Carrera;
                cbTutor.Text = Prof.Where(p => p.Id == monografia.id_tutor).Select(p => p.Nombre + " " + p.Apellido).FirstOrDefault();
                txtNota.Text = monografia.Nota.ToString();
                txtTPYR.Text = monografia.TiempoPreYRes.Minutes.ToString();
                txtTDP.Text = monografia.TiempoDefensa.Minutes.ToString();
                TimeComboBox.SelectedItem = monografia.TiempoOrtogado.Minutes.ToString() + " " + "minutos";
            }
        }
    } 
}
