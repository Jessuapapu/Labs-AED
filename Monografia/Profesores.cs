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
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Dirrecion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime FechaNac { get; set; }

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
