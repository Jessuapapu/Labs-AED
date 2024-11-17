using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografia
{
    public class MonoGrafia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Carrera { get; set; }
        public TimeSpan TiempoOrtogado { get; set; }
        public TimeSpan TiempoDefensa { get; set; }
        public TimeSpan TiempoPreYRes { get; set; }
        public int nota {  get; set; }


        public MonoGrafia(int Id, string titulo, string telefono, string carrera, TimeSpan TO,TimeSpan TD,TimeSpan TPR )
        {
           this.Id = Id;
            Titulo = titulo;
            Carrera = carrera;
            TiempoDefensa = TD;
            TiempoOrtogado = TO;
            TiempoPreYRes = TPR;
        }
        /*
             CREATE TABLE MonoGrafias(
	            Id INT PRIMARY KEY IDENTITY(1,1),
	            Titulo NVARCHAR(100) NOT NULL,
	            Carrera NVARCHAR(50) NOT NULL,
                Nota INT NOT NULL,
	            TiempoOrtogado TIME NOT NULL,
	            TiempoDefensa TIME NOT NULL,
	            TiempoPreYRes TIME NOT NULL,
             );
         */
    }
}
