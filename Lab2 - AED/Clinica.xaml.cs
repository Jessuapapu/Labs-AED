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

namespace Lab2___AED
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

        AgendaClinica[] Cita;
        int Tam, n = 0;
        bool Establecido = false;

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir(Cita);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Eliminar(int.Parse(txtId.Text));
            }
            catch
            {
                MessageBox.Show("Error en los datos");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modificar(int.Parse(txtId.Text), txtNombre.Text, cbServicio.Text, int.Parse(cbdia.Text), cbmes.Text, int.Parse(cbaño.Text));
            }
            catch
            {
                MessageBox.Show("Error en los datos");
            }
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar(int.Parse(txtId.Text));
            }
            catch
            {
                MessageBox.Show("Error en los datos");
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                AgregarCita(txtNombre.Text, cbServicio.Text, int.Parse(cbdia.Text), cbmes.Text, int.Parse(cbaño.Text));

            }
            catch 
            {
                MessageBox.Show("Error en los datos");
            }
        }
        private void btnEstablecer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EstablecerTamaño();
            }
            catch
            {
                MessageBox.Show("Error en los datos");
            }
        }

        private void btnOrdenarMes_Click(object sender, RoutedEventArgs e)
        {

            ImprimirMostrarMes(Cita);
        }

        private void btnOrdenarServicio_Click(object sender, RoutedEventArgs e)
        {
            ImprimirMostrarServicio(Cita);
        }

        private void btnOrdenarfecha_Click(object sender, RoutedEventArgs e)
        {
            ImprimirMostrarFecha(Cita);
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
            for(int i = 1; i <= dias; i++)
            {
                cbdia.Items.Add(i);
            }
        }
        void EstablecerAños() 
        { 
        for(int i = 2024; i <=2030; i++)
            {
                cbaño.Items.Add(i);
            }
        }




        // -------------------------------------------------- Algoritmos de los botones
        public void AgregarCita(string nombre, string tipoCita, int dia, string mes, int año)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                Cita[n] = new AgendaClinica(nombre, tipoCita, dia, mes, año, n, cbmes.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("No se puede agregar");
                return;
            }
            MessageBox.Show("Feligres agregado");
            n++;
        }

        public void EstablecerTamaño()
        {
            if (Establecido == true) { MessageBox.Show("ya has establecido"); return; }
            try
            {
                Tam = int.Parse(txtTam.Text);
                Cita = new AgendaClinica[Tam];

            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
                return;
            }

            for (int i = 0; i < Cita.Length; i++)
            {
                Cita[i] = new AgendaClinica("", "", 0, "", 0, n, 0);
            }
            Establecido = true;
            MessageBox.Show("Tamaño establecido");
        }

        public void Imprimir(AgendaClinica[] citas1)
        {
            dgImprimr.ItemsSource = null;
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            var Citas = new List<AgendaClinica>();

            for (int i = 0; i < n; i++)
            {
                if (citas1[i] != null)
                {
                    Citas.Add(citas1[i]);
                }
            }

            dgImprimr.ItemsSource = Citas;
        }

        public AgendaClinica[] OrdenarMes(AgendaClinica[] citas1) 
        {

            if (Establecido == false) { MessageBox.Show("No has establecido"); return citas1; }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (citas1[j].IdMes > citas1[j + 1].IdMes)
                    {
                        AgendaClinica aux = citas1[j];
                        citas1[j] = citas1[j + 1];
                        citas1[j + 1] = aux;
                    }
                }
            }
            MessageBox.Show("Ordenado por mes");
            return citas1;    

        }

        public void Modificar(int Id,string nombre, string tipoCita, int dia, string mes, int año)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                Cita[Id].Nombre = nombre;
                Cita[Id].TipoCita = tipoCita;
                Cita[Id].Dia = dia;
                Cita[Id].Mes = mes;
                Cita[Id].Año = año;
                Cita[Id].IdMes = cbmes.SelectedIndex;
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede modificar");
            }
        }

        public void Buscar(int id) 
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }

            for (int i = 0; i < n; i++)
                {
                    if (Cita[i].Id == id)
                    {
                        MessageBox.Show($"{Cita[i].toString()}");
                    }
                }
        }

        public void Eliminar(int Id)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                for (int i = Id; i < n; i++)
                {
                    Cita[i] = Cita[i + 1];
                }
                n--;
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar");
            }
        }

        public void ImprimirMostrarMes(AgendaClinica[] citas1) 
        { 
            var CitasOrdenadas = new List<AgendaClinica>();
            for (int i = 0; i < n; i++)
            {
                if (citas1[i].IdMes == cbmesBuscar.SelectedIndex)
                {
                    CitasOrdenadas.Add(citas1[i]);
                }
            }
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = CitasOrdenadas;
        }

        public void ImprimirMostrarServicio(AgendaClinica[] citas1)
        {
            var CitasOrdenadas = new List<AgendaClinica>();
            for (int i = 0; i < n; i++)
            {
                if (citas1[i].TipoCita == cbServicioOrdenar.Text)
                {
                    CitasOrdenadas.Add(citas1[i]);
                }
            }
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = CitasOrdenadas;
        }

        public void ImprimirMostrarFecha(AgendaClinica[] citas1)
        {
            var CitasOrdenadas = new List<AgendaClinica>();
            //Ordenar por fecha
            for (int i = 0; i < n; i++) 
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (citas1[j].Dia > citas1[j + 1].Dia)
                    {
                        AgendaClinica aux = citas1[j];
                        citas1[j] = citas1[j + 1];
                        citas1[j + 1] = aux;
                    }
                }
            }

            for(int i = 0; i < n; i++)
            {
                CitasOrdenadas.Add(citas1[i]);
            }
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = CitasOrdenadas;
        }

        void limpiar() 
        { 
        
            txtNombre.Text = "";
            cbServicio.SelectedIndex = 0;
            cbdia.SelectedIndex = 0;
            cbmes.SelectedIndex = 0;
            cbaño.SelectedIndex = 0;
            txtId.Text = "";

        }
    }
}
