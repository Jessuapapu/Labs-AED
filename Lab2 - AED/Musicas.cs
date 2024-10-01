using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___AED
{
    public class musicas
    {
        public string nombre { get; set; }
        public string artista { get; set; }
        public string album { get; set; }
        public string path { get; set; }
        public string duracion { get; set; }

        public musicas(string nombre, string artista, string album, string path, string duracion)
        {
            this.nombre = nombre;
            this.artista = artista;
            this.album = album;
            this.path = path;
            this.duracion = duracion;
        }

        public override string ToString()
        {
            return nombre + " - " + artista + " - " + album + " - " + duracion;
        }

    }
}
