using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace APIPizzeria.Models
{
    public class Pizza
    {
        public int ID_Pizza { get; set; }
        public string Nombre {  get; set; }
        public string Descripcion { get; set; }

        [Range (1, double.MaxValue)]
        public double PrecioUnitario { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
    }
}
