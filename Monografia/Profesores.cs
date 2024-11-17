using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografia
{
    public class Profesores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dirrecion { get; set; }
        public string Telefono { get; set; }
        public DateTime AñoNac { get; set; }

        public Profesores(int id,string nombre, string apellido, string dirrecion, string telefono, DateTime añoNac)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dirrecion = dirrecion;
            Telefono = telefono;
            AñoNac = añoNac;
        }


        /*
            CREATE TABLE Profesores
            (
	            Id INT PRIMARY KEY IDENTITY(1,1),
	            Nombre NVARCHAR(50) NOT NULL,
	            Apellido NVARCHAR(50) NOT NULL,
	            Dirrecion NVARCHAR(100) NOT NULL,
	            Telefono NVARCHAR(10) NOT NULL,
	            FechaNac DATE NOT NULL
            );
         */
    }
}
