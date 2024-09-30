using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___AED
{
    public class AgendaClinica
    {
        public string Nombre { get; set; }
        public string TipoCita { get; set; }
        public int Dia { get; set; }
        public string Mes { get; set; }
        public int Año { get; set; }
        public int Id { get; set; }
        public int IdMes { get; set; }


        public AgendaClinica(string nombre, string tipoCita, int dia, string mes, int año, int id, int idMes)
        {
            Nombre = nombre;
            TipoCita = tipoCita;
            Dia = dia;
            Mes = mes;
            Año = año;
            Id = id;
            IdMes = idMes;
        }

   
        public string toString()
        {
            return $"Nombre: {Nombre}\nTipo de Cita: {TipoCita}\nFecha: {Dia}/{Mes}/{Año}\nId: {Id}";
        }
    }

}
