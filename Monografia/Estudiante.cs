namespace Monografia
{
   public class Estudiantes
    {
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dirrecion { get; set; }
        public string Telefono { get; set; }
        public DateTime AñoNac { get; set; }

        public Estudiantes(string carnet,string nombre, string apellido, string dirrecion, string telefono, DateTime añoNac)
        {
            Carnet = carnet;
            Nombre = nombre;
            Apellido = apellido;
            Dirrecion = dirrecion;
            Telefono = telefono;
            AñoNac = añoNac;
        }

        /*
            CREATE TABLE Estudiantes
            (
	            Carnet NVARCHAR(11) PRIMARY KEY NOT NULL,
	            Nombre NVARCHAR(50) NOT NULL,
	            Apellido NVARCHAR(50) NOT NULL,
	            Dirrecion NVARCHAR(100) NOT NULL,
	            Telefono NVARCHAR(10) NOT NULL,
	            FechaNac DATE NOT NULL
            );
        */
    }
}
