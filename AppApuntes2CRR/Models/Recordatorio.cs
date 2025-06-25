using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerGrupalMMVC.Models
{
    public class Recordatorio
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Texto { get; set; } = "";
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        
        public string FechaHoraFormateada => FechaHora.ToString("dd/MM/yyyy HH:mm");
        public string TiempoRestante
        {
            get
            {
                var diferencia = FechaHora - DateTime.Now;
                if (diferencia.TotalMinutes < 0)
                    return "Vencido";
                else if (diferencia.TotalHours < 24)
                    return $"En {diferencia.Hours}h {diferencia.Minutes}m";
                else
                    return $"En {diferencia.Days} días";
            }
        }
    }
}
