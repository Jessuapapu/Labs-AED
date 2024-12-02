using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labs_AED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }


        //metodo para mover la pestaña
        private void BarraSuperior_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove(); // Permite mover la ventana desde la barra superior
            }
        }

        // metodos para cerrar, minimizar y maximizar la ventana
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BotonPresentacion_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnIglesia_Click(object sender, RoutedEventArgs e)
        {
           MostrarControl(MonografiasAdmin);
            OcultarVisibilidad(MonografiasAdminAgr);
            OcultarVisibilidad(MonoRelaciones);
        }

        private void btnClinica_Click(object sender, RoutedEventArgs e)
        {
            MostrarControl(MonografiasAdminAgr);
            OcultarVisibilidad(MonografiasAdmin);
            OcultarVisibilidad(MonoRelaciones);
        }

        private void OcultarVisibilidad(UserControl control)
        {
            if (control.Visibility == Visibility.Visible)
            {
                Storyboard desaparecer = (Storyboard)FindResource("DesaparecerStoryboard");
                desaparecer.Begin(control);
                control.Visibility = Visibility.Collapsed;
            }
        }

        private void MostrarControl(UserControl control)
        {
            if (control.Visibility != Visibility.Visible)
            {   
                Storyboard aparecer = (Storyboard)FindResource("AparecerStoryboard");
                aparecer.Begin(control);
                control.Visibility = Visibility.Visible;
            }
        }

        private void MonografiasAdmin_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Relaciones_Click(object sender, RoutedEventArgs e)
        {
            MostrarControl(MonoRelaciones);
            OcultarVisibilidad(MonografiasAdmin);
            OcultarVisibilidad(MonografiasAdminAgr);
        }
    }
}