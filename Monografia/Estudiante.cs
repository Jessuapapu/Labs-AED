using System.ComponentModel.DataAnnotations;

namespace Monografia
{
   public class Estudiantes
    {
        [Key]public string Carnet { get; set; }
        public int Id_Carrera { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dirrecion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNac { get; set; }

        

        /*
            CREATE TABLE Estudiantes
            (
	            Carnet NVARCHAR(11) PRIMARY KEY NOT NULL,
                Id_Carrera INT NOT NULL,
	            Nombre NVARCHAR(50) NOT NULL,
	            Apellido NVARCHAR(50) NOT NULL,
	            Dirrecion NVARCHAR(100) NOT NULL,
	            Telefono NVARCHAR(10) NOT NULL,
	            FechaNac DATE NOT NULL
            );
        */
    }
}
