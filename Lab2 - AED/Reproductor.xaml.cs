using Microsoft.Win32;
using System;
using System.Collections;
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
    /// Lógica de interacción para Reproductor.xaml
    /// </summary>
    public partial class Reproductor : UserControl
    {

        Stack<musicas> miReproduccion = new Stack<musicas>();
        Stack<musicas> Historial = new Stack<musicas>();
        string Duracion;
        musicas cancion;

        public Reproductor()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Pause_Click_1(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Anterior_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();

            if(Historial.Count > 0)
            {
                miReproduccion.Push(Historial.Peek() as musicas);
                Historial.Pop();

                musicas cancionAReproduccion = miReproduccion.Peek() as musicas;
                mediaElement.Source = new Uri(cancionAReproduccion.path);
                mediaElement.Play();
                mostrarPila();
                return;
            }

            MessageBox.Show("No hay canciones en el historial");
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();

            // validar si quedan canciones en la pila
            if (miReproduccion.Count > 1)
            {
                // guardar la cancion que se esta reproduciendo en el historial
                Historial.Push(miReproduccion.Peek() as musicas);

                // quitar la cancion que se esta reproduciendo
                miReproduccion.Pop();

                // reproducir la siguiente cancion
                musicas cancionAReproduccion = miReproduccion.Peek() as musicas;
                MessageBox.Show("Reproduciendo: " + cancionAReproduccion.nombre);
                mediaElement.Source = new Uri(cancionAReproduccion.path);
                mediaElement.Play();
                mostrarPila();
                return;
            }

           MessageBox.Show("No hay canciones en la lista de reproduccion");
        }

        private void BuscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            buscarCancion();
        }

        private void AgregarFilaReprocuccion(object sender, RoutedEventArgs e)
        {
            ObtenerParaPila();
        }


        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            miReproduccion.Pop();
            musicas cancionAReproduccion = miReproduccion.Peek() as musicas;
            mediaElement.Source = new Uri(cancionAReproduccion.path);
            mediaElement.Play();
            mostrarPila();
        }


        public void buscarCancion()
        {
            // metodo para pedir el path del archivo mp3 con un metodo de OpenFileDialog
            OpenFileDialog ArchivoMp3 = new OpenFileDialog();
            ArchivoMp3.Filter = "Musica mp3(*.mp3)|*.mp3";
            if (ArchivoMp3.ShowDialog() == true)
            {
                txtPath.Text = ArchivoMp3.FileName;
            }
        }


        public void ObtenerParaPila()
        {
            mediaElement.Source = new Uri(txtPath.Text);

            mediaElement.MediaOpened -= MediaElement_MediaOpened;

            mediaElement.MediaOpened += MediaElement_MediaOpened;

            mediaElement.Play();
            mediaElement.Pause();
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                // Obtener la duración del archivo de audio
                TimeSpan duracion = mediaElement.NaturalDuration.TimeSpan;
                Duracion = duracion.ToString(@"hh\:mm\:ss");

                apilarEnPila();


                limpiar();
                mostrarPila();
            }
        }

        public void apilarEnPila() 
        {
            // se obtienen los datos de la cancion para luego colocarlos en el objeto que sera enviado a la pila
            cancion = new musicas(txtNombreCancion.Text, txtAutor.Text, txtAlbum.Text, txtPath.Text, Duracion);
            miReproduccion.Push(cancion);

        }

        public void desapilarEnPila()
        {
            miReproduccion.Pop();
        }

        public void mostrarPila()
        {
            lstCanciones.Items.Clear();

            foreach (musicas cancion in miReproduccion.Reverse())
            {
                lstCanciones.Items.Add(cancion.ToString());
            }

            foreach (musicas cancion in Historial.Reverse())
            {
                lstHistorial.Items.Add(cancion.ToString());
            }

            lstCanciones.Items.Refresh();
        }

        void limpiar() 
        { 
            txtAlbum.Text = "";
            txtAutor.Text = "";
            txtNombreCancion.Text = "";
            txtPath.Text = "";
        }

    }
}
