using System;
using System.Collections.Generic;
using System.Data;
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

namespace Lab2___AED
{
    /// <summary>
    /// Lógica de interacción para Banco.xaml
    /// </summary>
    public partial class Banco : UserControl
    {
       
        public Banco()
        {
            InitializeComponent();
            rellenar();
        }

        // Este evento se dispara cuando el UserControl ha cargado
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Obtenemos la ventana principal de la aplicación
            Window mainWindow = Application.Current.MainWindow;

            if (mainWindow != null)
            {
                // Calculamos la posición para que el Popup aparezca a la derecha de la ventana
                double rightPosition = mainWindow.Width + 200;
                double topPosition = (mainWindow.Height / 2 - popupTurnoC.ActualHeight / 2) - 150;

                // Ajustamos la posición del Popup
                popupTurnoC.HorizontalOffset = rightPosition;
                popupTurnoC.VerticalOffset = topPosition;

                // Calculamos la posición para que el Popup popupTurnoM aparezca justo debajo de la ventana
                double leftPositionM = (mainWindow.Width / 2 - popupTurnoM.ActualWidth / 2) - 250;
                double bottomPosition = mainWindow.Height + 40; // Ajuste de 20 píxeles debajo de la ventana

                // Ajustamos la posición del Popup popupTurnoM
                popupTurnoM.HorizontalOffset = leftPositionM;
                popupTurnoM.VerticalOffset = bottomPosition;
            }
        }
        // cerrar popup
        private void cerrarCola_Click(object sender, RoutedEventArgs e)
        {
            popupTurnoC.IsOpen = false;
        }

        private void Mostrarcola(object sender, RoutedEventArgs e) 
        {
            popupTurnoC.IsOpen = true;
        }

        private void cerrarMesa_Click(object sender, RoutedEventArgs e)
        {
            popupTurnoM.IsOpen = false;
        }

        private void MostrarMesa(object sender, RoutedEventArgs e)
        {
            popupTurnoM.IsOpen = true;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
                Encolar();
        }

        private void SiguienteCaja_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DesencolarCaja();
            }
            catch
            {
                MessageBox.Show("No hay personas en la cola");
            }
        }

        private void SiguienteMesa_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                DesencolarMesa();
            }
            catch
            {
                MessageBox.Show("No hay personas en la cola");
            }
        }


        // Declaraciones de botones

        Queue<Personas> cola = new Queue<Personas>();
        Queue<Personas> mesa = new Queue<Personas>();

        void rellenar()
        {
            cbServicios.Items.Add("Deposito");
            cbServicios.Items.Add("Retiro");
            cbServicios.Items.Add("Consulta");
            cbServicios.Items.Add("Pago de Servicios");
            cbServicios.Items.Add("Apertura de Cuenta");

            cbTipo.Items.Add("caja");
            cbTipo.Items.Add("Servicio Bancarios");
        }

        public void Encolar()
        {
            if(txtNombre.Text == "" || cbServicios.Text == "" || cbTipo.Text == "")
            {
                MessageBox.Show("Ingrese todos los datos");
                return;
            }
            Personas persona = new Personas(txtNombre.Text, cbServicios.Text);

            if (cbTipo.Text == "caja")
            {
                cola.Enqueue(persona);
            }
            else
            {
                mesa.Enqueue(persona);
            }
            MostrarColas();
        }

        public void MostrarColas()
        {
            var Personas = cola.Take(4);
            dgCaja.ItemsSource = Personas;
            txtSiguienteAPasarC.Text = cola.Peek().Nombre;
            var PersonasMesa = mesa.Take(4);
            txtSiguienteAPasarM.Text = mesa.Peek().Nombre;
            dgMesa.ItemsSource = PersonasMesa;
        }

        public void DesencolarMesa()
        {
            cola.Dequeue();
            MostrarColas();
        }

        public void DesencolarCaja()
        {
            mesa.Dequeue();
            MostrarColas();
        }

    }
}
