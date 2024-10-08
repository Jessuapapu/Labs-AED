using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___AED
{
    public class Personas 
    {

        public string Nombre { get; set; }
        public string Consulta { get; set; }

        public Personas(string Nombre,string Consulta)
        {
            this.Nombre = Nombre;
            this.Consulta = Consulta;
        }
    
    }
}
