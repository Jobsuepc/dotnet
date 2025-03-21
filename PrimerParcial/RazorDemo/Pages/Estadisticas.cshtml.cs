using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Models;

namespace RazorDemo.Pages
{
    public class EstadisticasModel : PageModel
    {
        public double AprovechamientoGeneral { get; set; }
        public List<Alumno> MejoresAlumnos { get; set; }
        public List<Alumno> PeoresAlumnos { get; set; }
        public double PorcentajeAprobados { get; set; }
        public double PorcentajeReprobados { get; set; }
        public double ModaCalificacion { get; set; }

        public void OnGet()
        {
            var alumnos = LeerAlumnosDesdeXML();
            
            AprovechamientoGeneral = alumnos.Average(a => a.Promedio);

            MejoresAlumnos = alumnos.OrderByDescending(a => a.Promedio).Take(5).ToList();
            PeoresAlumnos = alumnos.OrderBy(a => a.Promedio).Take(5).ToList();

            var totalAlumnos = alumnos.Count;
            var aprobados = alumnos.Count(a => a.Promedio >= 6);
            var reprobados = totalAlumnos - aprobados;

            PorcentajeAprobados = (double)aprobados / totalAlumnos * 100;
            PorcentajeReprobados = (double)reprobados / totalAlumnos * 100;

            ModaCalificacion = CalcularModa(alumnos.Select(a => a.Promedio).ToList());
        }

        private List<Alumno> LeerAlumnosDesdeXML()
        {
            string xmlFilePath = "Models/base.xml"; 
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);

            var alumnos = ds.Tables[0].AsEnumerable().Select(row =>
            {
                double parcial1 = Convert.ToDouble(row["parcial1"]);
                double parcial2 = Convert.ToDouble(row["parcial2"]);
                double parcial3 = Convert.ToDouble(row["parcial3"]);
                double extra = Convert.ToDouble(row["extra"]);
                double promedioParciales = (parcial1 + parcial2 + parcial3) / 3;
                double promedioFinal = extra > promedioParciales ? extra : promedioParciales;
                double promedio = promedioFinal;
                int id = Convert.ToInt32(row["id"]);
                
                return new Alumno
                {
                    Id = id,
                    Apellido = row.Field<string>("apellido") ?? string.Empty,
                    Nombre = row.Field<string>("nombre") ?? "Nombre no disponible",
                    Parcial1 = parcial1,
                    Parcial2 = parcial2,
                    Parcial3 = parcial3,
                    Extra = extra,
                    Promedio = promedio
                };
            }).ToList();

            return alumnos;
        }

        private double CalcularModa(List<double> lista)
        {
            var grupo = lista.GroupBy(i => i)
                             .OrderByDescending(g => g.Count())
                             .ThenBy(g => g.Key)
                             .FirstOrDefault();
            return grupo?.Key ?? 0;
        }
    }

}
