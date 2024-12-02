using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografia
{
    // Relaciones entre tablas o clases
        public class Mono_Profesores 
        {
            // Clase donde Se hace la referencia de los profesores que estan a cargo de una monografia
            public int Id { get; set; }
            public int IdProfesor { get; set; }
            public int IdMonoGrafia { get; set; }
            public int Rol { get; set; }

        /*
                CREATE TABLE Mono_Profesores
                (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    IdProfesor INT NOT NULL,
                    IdMonoGrafia INT NOT NULL,
                    Rol INT NOT NULL,
                    FOREIGN KEY (IdProfesor) REFERENCES Profesores(Id),
                    FOREIGN KEY (IdMonoGrafia) REFERENCES MonoGrafias(Id),
                    FOREIGN KEY (Rol) REFERENCES Roles(Id)
                );

        */
        }


        public class Estudiante_Mono
        {
            // Refencia entre los estudiantes y las monografias
            public int Id { get; set; }
            public string Carnet { get; set; }
            public int IdMonoGrafia { get; set; }
        
        /*
                CREATE TABLE Estudiante_Mono
                (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Carnet NVARCHAR(11) NOT NULL,
                    IdMonoGrafia INT NOT NULL,
                    FOREIGN KEY (Carnet) REFERENCES Estudiantes(Carnet),
                    FOREIGN KEY (IdMonoGrafia) REFERENCES MonoGrafias(Id)
                );

        */
        }
    
}
