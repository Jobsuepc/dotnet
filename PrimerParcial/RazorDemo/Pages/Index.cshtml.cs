using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.IO;

namespace RazorDemo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public DataTable AlumnosTable { get; set; } = new DataTable();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if (System.IO.File.Exists("Models/base.xml"))
        {
        
            DataSet dst = new DataSet();
            dst.ReadXml("Models/base.xml");

            if (dst.Tables.Count > 0)
            {
                AlumnosTable = dst.Tables[0]; 
                
            foreach (System.Data.DataRow row in AlumnosTable.Rows)
                {
                    double parcial1 = Convert.ToDouble(row["parcial1"]);
                    double parcial2 = Convert.ToDouble(row["parcial2"]);
                    double parcial3 = Convert.ToDouble(row["parcial3"]);
                    double extra = Convert.ToDouble(row["extra"]);

                    double promedioParciales = (parcial1 + parcial2 + parcial3) / 3;

                    double promedioFinal = extra > promedioParciales ? extra : promedioParciales;

                    row["promedio"] = Convert.ToInt32(promedioFinal);
                }
            }
        } 
    }
}
