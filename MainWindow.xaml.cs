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

        private MonografiasAdmin monografiasAdmin;
        private MonografiasAdminAgr monografiasAdminAgr;

        public MainWindow()
        {
            InitializeComponent();

            monografiasAdmin = new MonografiasAdmin();
            monografiasAdminAgr = new MonografiasAdminAgr();

            // Agregar los controles al StackPanel
            miStackPanel.Children.Add(monografiasAdmin);
            miStackPanel.Children.Add(monografiasAdminAgr);

            // Suscribir el evento
            monografiasAdminAgr.ActuProfesores += monografiasAdmin.RecibirProfesores;

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
            MostrarControl(MonografiasAdminAgr);
            OcultarVisibilidad(MonografiasAdmin);

        }

        private void btnClinica_Click(object sender, RoutedEventArgs e)
        {
            MostrarControl(MonografiasAdmin);
            OcultarVisibilidad(MonografiasAdminAgr);
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
    }
}