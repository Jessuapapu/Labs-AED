using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografia
{
    public class Roles
    {
        public int Id { get; set; }
        public string Rol { get; set; }

        public Roles(int id,string rol)
        {

            Id = id;
            Rol = rol;

        }

        /*
            CREATE TABLE Roles
            (
                Id INT PRIMARY KEY IDENTITY(1,1),
                Rol NVARCHAR(50) NOT NULL
            );
        */
    }
}
