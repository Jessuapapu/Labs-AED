﻿using System;
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
    /// Lógica de interacción para MainWindow.xaml
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
            Presentacion.Visibility = Visibility.Visible;
            IglesiaUI.Visibility = Visibility.Collapsed;
            ClinicaUI.Visibility = Visibility.Collapsed;
        }

        private void btnIglesia_Click(object sender, RoutedEventArgs e)
        {
            IglesiaUI.Visibility = Visibility.Visible;
            Presentacion.Visibility = Visibility.Collapsed;
            ClinicaUI.Visibility = Visibility.Collapsed;
        }

        private void btnClinica_Click(object sender, RoutedEventArgs e)
        {
            ClinicaUI.Visibility = Visibility.Visible;
            Presentacion.Visibility = Visibility.Collapsed;
            IglesiaUI.Visibility = Visibility.Collapsed;
        }
    }
}
