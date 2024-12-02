using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografia
{
    public class Carreras
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Facultad { get; set; }

    /*
            CREATE TABLE Carreras
            (
                ID INT PRIMARY KEY IDENTITY(1,1),
                Nombre NVARCHAR(50) NOT NULL,
                Facultad NVARCHAR(50) NOT NULL
            );
    */
    }
}
