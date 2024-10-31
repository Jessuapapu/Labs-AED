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
using Feligreses;

namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para Iglesia.xaml
    /// </summary>
    public partial class Iglesia : UserControl
    {
 
        int[] meses = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        List<string> Mesess = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        Feligres feligres;
        Monto monto;
        List<Feligres> feligreses = new List<Feligres>();
        List<Monto> montos = new List<Monto>();
        public Iglesia()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MesIngresar.ItemsSource = Mesess;
            cbIncio.ItemsSource = Mesess;
            cbFinal.ItemsSource = Mesess;
            
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // usando removeall y LinQ para eliminar un objeto de la lista
            // Se usa LinQ para el criterio de eliminación
            try
            {
                feligreses.RemoveAll(feligreses => feligreses.Id == int.Parse(txtId.Text));
                montos.RemoveAll(montos => montos.Id == int.Parse(txtId.Text));
            }
            catch
            {
                MessageBox.Show("No se elimino correctamente");
            }
            imprimir();

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ModificarInfo = feligreses.First(f => f.Id == int.Parse(txtId.Text));
                var ModificarMontos = montos.First(m => m.Id == int.Parse(txtId.Text));
                
                ModificarInfo.Nombre = txtNombre.Text;
                ModificarInfo.Direccion = txtDireccion.Text;
                ModificarInfo.Telefono = txtTelefono.Text;

                ModificarMontos.MesMonto = meses;
            }
            catch 
            {
                MessageBox.Show("No se modifico correctamente");
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try 
            {

                feligres = new Feligres(feligreses.Count, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, meses);

                monto = new Monto(feligreses.Count, meses);
                feligreses.Add(feligres);
                montos.Add(monto);
            }
            catch 
            {
                MessageBox.Show("Datos no ingresados correctamente");
            }


            
            imprimir();
            meses = limpiarArreglo();
        }

        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            var query = from f in feligreses
                        join m in montos
                        on f.Id equals m.Id
                        orderby m.MontoTotal descending
                        select new { f.Id, f.Nombre, f.Direccion, f.Telefono, m.MontoTotal };

            dgImprimr.AutoGenerateColumns = true;
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;
        }


        private void btnOrdenar_Copiar_Click(object sender, RoutedEventArgs e)
        {
            var query = from f in feligreses
                        join m in montos
                        on f.Id equals m.Id
                        orderby m.MontoTotal 
                        select new { f.Id, f.Nombre, f.Direccion, f.Telefono, m.MontoTotal };

            dgImprimr.AutoGenerateColumns = true;
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = (from f in feligreses
                             where f.Id == int.Parse(txtId.Text)
                             select new { Id = f.Id, Nombre = f.Nombre, Direccion = f.Direccion, telefono = f.Telefono }).First();

                var query2 = (from m in montos
                              where m.Id == int.Parse(txtId.Text)
                              select m.MesMonto).First();

                meses = query2;

                txtNombre.Text = query.Nombre;
                txtTelefono.Text = query.telefono;
                txtDireccion.Text = query.Direccion;
            }
            catch
            {
                MessageBox.Show("No se encontro");
            }
        }


        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMostrarRango(object sender, RoutedEventArgs e)
        {
            try
            {
                var query2 = (from m in montos
                              where m.Id == int.Parse(txtRangoId.Text)
                              select m.MesMonto).First();

                string str = "Rango de Meses\n";
                string MesStr = "";
                for (int i = cbIncio.SelectedIndex; i < cbFinal.SelectedIndex + 1; i++)
                {
                    MesStr = Mesess[i];
                    str = str + MesStr + ": " + query2[i].ToString() + "\n";
                }

                MessageBox.Show(str);
            }
            catch
            {
                MessageBox.Show("No se encontro");
            }
        }

        private void Meses_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                meses[MesIngresar.SelectedIndex] = int.Parse(txtMesIngresar.Text);
            }
            catch
            {
                MessageBox.Show("Datos no ingresados correctamente");
            }
        }

        private void MesIngresar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtMesIngresar.Text = meses[MesIngresar.SelectedIndex].ToString();
        }

        // -------------------------------------------------- Algoritmos de los botones


        public void Limpiar()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
        }
        int[] limpiarArreglo()
        {
            int[] meses = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return meses;
        }

        void imprimir()
        {
            // Consulta anidada con join para establecer una consulta de feligreses y montos
            var query = from f in feligreses
                        join m in montos
                        on f.Id equals m.Id
                        select new { f.Id, f.Nombre, f.Direccion, f.Telefono, m.MontoTotal };

            dgImprimr.AutoGenerateColumns = true;
            dgImprimr.ItemsSource = null;
            dgImprimr.ItemsSource = query;
        }
    }
}
