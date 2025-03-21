using System;

namespace RazorDemo.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public double Parcial1 { get; set; }
        public double Parcial2 { get; set; }
        public double Parcial3 { get; set; }
        public double Extra { get; set; }
        public double Promedio { get; set; }
    }
}
