using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___AED
{
    public class Feligreses
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        static string[] Mes = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
        public int[] MesMonto { get; set; }
        
        public int MontoTotal { get; set; }

        public Feligreses(int id,string nombre, string direccion, string telefono, int[] mesmonto)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            MesMonto = new int[12];

            for(int i = 0; i < 12; i++)
            {
                MesMonto[i] = mesmonto[i];
            }

            MontoTotal += mesmonto.Sum();
        }

        public string toString()
        {
            return String.Format(Nombre + "\t" + Direccion + "\t" + Telefono + "\t" + MontoTotal);
        }

        public string toStringMeses()
        {
            return String.Format("enero: "+MesMonto[0] + " Frebrero: " + MesMonto[1] + " Marzo: " + MesMonto[2] + " Abril: " + MesMonto[3] + " Mayo: " + MesMonto[4] + " Junio: " + MesMonto[5] + " Julio: " + MesMonto[6] + " Agosto: " + MesMonto[7] + " Septiembre: " + MesMonto[8] + " Octubre: " + MesMonto[9] + " Noviembre: " + MesMonto[10] + " Diciembre: " + MesMonto[11]);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Dirección: {Direccion}, Teléfono: {Telefono}, Monto Total: {MontoTotal}";
        }

        public string toStringMes(int mesX, int mesY)
        {
            string str = "Rango de Meses:\n";
            string MesStr = "";

            for (int i = mesX; i <= mesY; i++)
            {
                MesStr = Mes[i];
                str = String.Concat(str, MesStr + ": " + MesMonto[i] + "\n");
            }

            return str;
        }

        public string tostringNombre()
        {
            return Nombre;
        }
    }
}
