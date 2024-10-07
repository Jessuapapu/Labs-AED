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

        }
    }
}
