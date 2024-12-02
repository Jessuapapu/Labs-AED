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
        public string Titulo { get; set; } = null!;
        public string Carrera { get; set; } = null!;
        public int id_tutor { get; set; }
        public TimeSpan TiempoOrtogado { get; set; } 
        public TimeSpan TiempoDefensa { get; set; }
        public TimeSpan TiempoPreYRes { get; set; }
        public int Nota {  get; set; }


        public MonoGrafia(string Titulo, int id_tutor, string carrera,
                      TimeSpan tiempoOrtogado = default,
                      TimeSpan tiempoDefensa = default,
                      TimeSpan tiempoPreYRes = default,
                      int nota = 0)
        {
            this.Titulo = Titulo;
            this.id_tutor = id_tutor;
            Carrera = carrera;
            TiempoOrtogado = tiempoOrtogado;
            TiempoDefensa = tiempoDefensa;
            TiempoPreYRes = tiempoPreYRes;
            this.Nota = nota;
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
