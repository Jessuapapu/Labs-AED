using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
    /// Lógica de interacción para Iglesia.xaml
    /// </summary>
    public partial class Iglesia : UserControl
    {

        Feligreses[] feligreses;
        int Tam, n = 0;
        bool Establecido = false;
        int[] meses = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<string> Meses = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public Iglesia()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MesIngresar.ItemsSource = Meses;
            cbIncio.ItemsSource = Meses;
            cbFinal.ItemsSource = Meses;

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir(feligreses);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Eliminar(int.Parse(txtId.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modificar(int.Parse(txtId.Text), txtNombre.Text, txtDireccion.Text, txtTelefono.Text, meses);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarFeligreses(txtNombre.Text, txtDireccion.Text, txtTelefono.Text, meses);
            meses = limpiarArreglo();

        }

        private void btnEstablecer_Click(object sender, RoutedEventArgs e)
        {
            EstablecerTamaño();
        }

        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < n - 1; i++)
            {
                Imprimir(OrdenarAporte(feligreses));
            }

        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar(int.Parse(txtId.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
            }
        }


        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Listar(int.Parse(txtId.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
            }
        }

        private void btnMostrarRango(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($" Feligrese: {feligreses[int.Parse(txtRangoId.Text)].tostringNombre()} \n {feligreses[int.Parse(txtRangoId.Text)].toStringMes(cbIncio.SelectedIndex, cbFinal.SelectedIndex)}");
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
            }
        }

        // -------------------------------------------------- Algoritmos de los botones
        public void AgregarFeligreses(string nombre, string direccion, string telefono, int[] mesmonto)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                feligreses[n] = new Feligreses(n, nombre, direccion, telefono, mesmonto);
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
                feligreses = new Feligreses[Tam];

            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
                return;
            }

            int[] inicializar = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < feligreses.Length; i++)
            {
                feligreses[i] = new Feligreses(n, "", "", "", inicializar);
            }
            Establecido = true;
            MessageBox.Show("Tamaño establecido");
        }

        public void Imprimir(Feligreses[] feligreses)
        {
            dgImprimr.ItemsSource = null;
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            var Feligreses = new List<Feligreses>();

            for (int i = 0; i < n; i++)
            {
                if (feligreses[i] != null)
                {
                    Feligreses.Add(feligreses[i]);
                }
            }

            dgImprimr.Columns.Clear();
            dgImprimr.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Id") });
            dgImprimr.Columns.Add(new DataGridTextColumn { Header = "Nombre", Binding = new Binding("Nombre") });
            dgImprimr.Columns.Add(new DataGridTextColumn { Header = "Dirección", Binding = new Binding("Direccion") });
            dgImprimr.Columns.Add(new DataGridTextColumn { Header = "Teléfono", Binding = new Binding("Telefono") });
            dgImprimr.Columns.Add(new DataGridTextColumn { Header = "Monto Total", Binding = new Binding("MontoTotal") });


            dgImprimr.ItemsSource = Feligreses;
        }


        public void Eliminar(int pos)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                for (int i = pos; i < n; i++)
                {
                    feligreses[i] = feligreses[i + 1];
                }
                n--;
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar");
            }
        }

        public void Modificar(int pos, string nombre, string direccion, string telefono, int[] mesmonto)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                feligreses[pos].Nombre = nombre;
                feligreses[pos].Direccion = direccion;
                feligreses[pos].Telefono = telefono;
                feligreses[pos].MesMonto = mesmonto;
                feligreses[pos].MontoTotal = mesmonto.Sum();

                feligreses = OrdenarId(feligreses);
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede modificar");
            }
        }

        public void Limpiar()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
        }

        public Feligreses[] OrdenarId(Feligreses[] feligreses1)
        {
            if (Establecido == false) return feligreses1;
            //Ordenar por Id
            for (int i = 0; i < n - 1; i++)
            {
                if (feligreses1[i].Id < feligreses1[i + 1].Id)
                {
                    Feligreses aux = feligreses1[i];
                    feligreses1[i] = feligreses1[i + 1];
                    feligreses1[i + 1] = aux;
                }
            }

            return feligreses1;
        }
        public Feligreses[] OrdenarAporte(Feligreses[] feligreses1)
        {
            if (Establecido == false) return feligreses1;
            //Ordenar por monto total
            for (int i = 0; i < n - 1; i++)
            {
                if (feligreses1[i].MontoTotal < feligreses1[i + 1].MontoTotal)
                {
                    Feligreses aux = feligreses1[i];
                    feligreses1[i] = feligreses1[i + 1];
                    feligreses1[i + 1] = aux;
                }
            }

            return feligreses1;
        }

        public void Listar(int Id)
        {
            for (int i = 0; i < n; i++)
            {
                if (feligreses[i].Id == Id)
                {
                    MessageBox.Show($"{feligreses[i].toString()} \nMeses Aportados \n {feligreses[i].toStringMeses()}");
                }
            }
        }

        private void btnAgregarMes(object sender, RoutedEventArgs e)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }
            try
            {
                meses[MesIngresar.SelectedIndex] = int.Parse(txtMesIngresar.Text);               
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número válido");
                return;
            }

            MessageBox.Show($"Mes {Meses[MesIngresar.SelectedIndex]} ingresado");
        }

        public void Buscar(int Id)
        {
            if (Establecido == false) { MessageBox.Show("No has establecido"); return; }

            for (int i = 0; i < n; i++)
            {
                if (feligreses[i].Id == Id)
                {
                    txtNombre.Text = feligreses[i].Nombre;
                    txtDireccion.Text = feligreses[i].Direccion;
                    txtTelefono.Text = feligreses[i].Telefono;
                    MessageBox.Show("ID encontrado");
                    return;
                }
            }
            MessageBox.Show("No se encontró el ID");
            return;

        }



        int[] limpiarArreglo()
        {
            int[] meses = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return meses;
        }
    }
}
