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
using Monografia;



namespace Labs_AED
{
    /// <summary>
    /// Lógica de interacción para MonografiasAdmin.xaml
    /// </summary>
    public partial class MonografiasAdmin : UserControl
    {
        

        public event Action<List<MonoGrafia>> ActuMonografias;

        public event Action<List<Mono_Profesores>> ActuMonoProfesores;
        public event Action<List<Estudiante_Mono>> ActuEstudianteMono;

        private List<MonoGrafia> MonoGra = new List<MonoGrafia>();

        private List<Mono_Profesores> MonoPro = new List<Mono_Profesores>();
        private List<Estudiante_Mono> EstuMono = new List<Estudiante_Mono>();


        public MonografiasAdmin()
        {
            InitializeComponent();
        }


        private void EnviarListas()
        {
            ActuMonografias?.Invoke(MonoGra);
            ActuMonoProfesores?.Invoke(MonoPro);
            ActuEstudianteMono?.Invoke(EstuMono);
        }

        public void RecibirProfesores(List<Profesores> profesores)
        {
            MessageBox.Show("Recibido");
            foreach (var item in profesores)
            {
                
                cbTutor.Items.Add(item.Nombre);
            }
        }


        private void txtTituloX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTutorX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    } 
}
