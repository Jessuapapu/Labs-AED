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
using Clinica;

namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para Clinica.xaml
    /// </summary>
    public partial class Clinica : UserControl
    {
        public Clinica()
        {
            InitializeComponent();
            EstablecerCb();
            EstablecerSer();
            EstablecerAños();
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.LayoutUpdated += UserControl_LayoutUpdated;    
        }

        private void UserControl_LayoutUpdated(object? sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                // Obtenemos la ventana principal de la aplicación
                Window mainWindow = Application.Current.MainWindow;

                // Calculamos la posición para que el Popup aparezca a la derecha de la ventana
                double rightPosition = mainWindow.Width + 200;
                double topPosition = (mainWindow.Height / 2 - popupPacientes.ActualHeight / 2) - 150;

                // Ajustamos la posición del Popup
                popupPacientes.HorizontalOffset = rightPosition;
                popupPacientes.VerticalOffset = topPosition;

                popupPacientes.IsOpen = true;
            }

            if (this.Visibility == Visibility.Collapsed)
            {
                // cerrar los popup
                popupPacientes.IsOpen = false;
            }
        }


        List<AgendaClinica> agenda = new List<AgendaClinica>();
        List<Pacientes> pacientes = new List<Pacientes>();

        AgendaClinica AgendaClinica;
        Pacientes Pacientes;




        // -------------------------------------------------- Botones


        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
           limpiar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            imprimirCitas();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
         


        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pacientes.Any(p => p.Nombre == txtNombre.Text && p.Cedula == txtCedula.Text))
                {
                    agendar();
                }
                else
                {
                    agregarPaciente();
                    agendar();
                }
                limpiar();
            }
            catch
            {
                MessageBox.Show("No se agrego correctamente");
            }
            imprimirPac();
            imprimirCitas();
        }

        private void cbmesBuscar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from a in agenda
                        join p in pacientes
                        on a.IdPaciente equals p.Id
                        orderby a.IdMes
                        where a.IdMes == cbmesBuscar.SelectedIndex
                        select new
                        {
                            a.TipoCita,
                            a.Dia,
                            a.Mes,
                            a.Año,
                            p.Nombre,
                        };

            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;
        }

        private void cbServicioOrdenar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from a in agenda
                        join p in pacientes
                        on a.IdPaciente equals p.Id
                        orderby a.IdMes
                        where a.IdTipoCita == cbServicioOrdenar.SelectedIndex
                        select new
                        {
                            a.TipoCita,
                            a.Dia,
                            a.Mes,
                            a.Año,
                            p.Nombre,
                        };

            
            
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;
        }


        private void btnOrdenarfecha_Click(object sender, RoutedEventArgs e)
        {

            var query = from a in agenda
                        join p in pacientes on a.IdPaciente equals p.Id
                        orderby a.Dia ascending
                        orderby a.IdMes ascending
                        orderby a.Año ascending
                        select new
                        {
                            a.TipoCita,
                            a.Dia,
                            a.Mes,
                            a.Año,
                            p.Nombre,
                        };

            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;

        }

        private void cbmes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int mes = cbmes.SelectedIndex;
            if (mes == 0 || mes == 2 || mes == 4 || mes == 6 || mes == 7 || mes == 9 || mes == 11)
            {
                EstablecerDia(31);
            }
            else if (mes == 3 || mes == 5 || mes == 8 || mes == 10)
            {
                EstablecerDia(30);
            }
            else if (mes == 1)
            {
                EstablecerDia(28);
            }
        }

        // -------------------------------------------------- items de comboBox


        void EstablecerCb()
        {
            cbmes.Items.Add("Enero");
            cbmes.Items.Add("Febrero");
            cbmes.Items.Add("Marzo");
            cbmes.Items.Add("Abril");
            cbmes.Items.Add("Mayo");
            cbmes.Items.Add("Junio");
            cbmes.Items.Add("Julio");
            cbmes.Items.Add("Agosto");
            cbmes.Items.Add("Septiembre");
            cbmes.Items.Add("Octubre");
            cbmes.Items.Add("Noviembre");
            cbmes.Items.Add("Diciembre");

            cbmesBuscar.Items.Add("Enero");
            cbmesBuscar.Items.Add("Febrero");
            cbmesBuscar.Items.Add("Marzo");
            cbmesBuscar.Items.Add("Abril");
            cbmesBuscar.Items.Add("Mayo");
            cbmesBuscar.Items.Add("Junio");
            cbmesBuscar.Items.Add("Julio");
            cbmesBuscar.Items.Add("Agosto");
            cbmesBuscar.Items.Add("Septiembre");
            cbmesBuscar.Items.Add("Octubre");
            cbmesBuscar.Items.Add("Noviembre");
            cbmesBuscar.Items.Add("Diciembre");
        }
        void EstablecerSer()
        {
            cbServicio.Items.Add("Medicina General");
            cbServicio.Items.Add("Odontología");
            cbServicio.Items.Add("Ortodoncia");
            cbServicio.Items.Add("Brackets");
            cbServicio.Items.Add("Limpieza Dental");
            cbServicio.Items.Add("Extracción");
            cbServicio.Items.Add("Endodoncia");
            cbServicio.Items.Add("Cirugía");
            cbServicio.Items.Add("Implantes");

            cbServicioOrdenar.Items.Add("Medicina General");
            cbServicioOrdenar.Items.Add("Odontología");
            cbServicioOrdenar.Items.Add("Ortodoncia");
            cbServicioOrdenar.Items.Add("Brackets");
            cbServicioOrdenar.Items.Add("Limpieza Dental");
            cbServicioOrdenar.Items.Add("Extracción");
            cbServicioOrdenar.Items.Add("Endodoncia");
            cbServicioOrdenar.Items.Add("Cirugía");
            cbServicioOrdenar.Items.Add("Implantes");
        }
        void EstablecerDia(int dias)
        {
            for (int i = 1; i <= dias; i++)
            {
                cbdia.Items.Add(i);
            }
        }
        void EstablecerAños()
        {
            for (int i = 2024; i <= 2030; i++)
            {
                cbaño.Items.Add(i);
            }
        }




        // -------------------------------------------------- Algoritmos de los botones

        void limpiar()
        {
            txtNombre.Text = "";
            txtCedula.Text = "";
            cbServicio.SelectedIndex = 0;
            cbdia.SelectedIndex = 0;
            cbmes.SelectedIndex = 0;
            cbaño.SelectedIndex = 0;
            txtId.Text = "";
        }


        void agendar() 
        {

            var query = (from p in pacientes
                         where p.Nombre == txtNombre.Text
                         select p.Id).First();

            AgendaClinica = new AgendaClinica(cbServicio.Text, cbdia.Text, cbmes.Text, cbaño.Text, agenda.Count, cbmes.SelectedIndex, query, cbServicio.SelectedIndex);
            agenda.Add(AgendaClinica);

        }

        void agregarPaciente() 
        {

            Pacientes = new Pacientes(txtNombre.Text, txtCedula.Text, pacientes.Count);
            pacientes.Add(Pacientes);

        }

        void imprimirPac() 
        {
        
        var query = from p in pacientes
                    select new
                    {
                        p.Nombre,
                        p.Cedula,
                        p.Id
                    };

            dgPacientes.ItemsSource = null;
            dgPacientes.ItemsSource = query;
        }

        void imprimirCitas() 
        {

            var query = from a in agenda
                        join p in pacientes on a.IdPaciente equals p.Id
                        select new
                        {
                            a.TipoCita,
                            a.Dia,
                            a.Mes,
                            a.Año,
                            p.Nombre,
                            a.Id
                        };

            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;

        }

        
    }
}
