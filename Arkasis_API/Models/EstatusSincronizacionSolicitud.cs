using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class EstatusSincronizacionSolicitud
    {
        public String IdSolicitud { get; set; }
        public Boolean BitRegistrado { get; set; }
        public String StrMensaje{ get; set; }


        public EstatusSincronizacionSolicitud(int IdSolicitud, Boolean BitRegistrado, String StrMensaje)
        {
            this.IdSolicitud = IdSolicitud.ToString();
            this.BitRegistrado = BitRegistrado;
            this.StrMensaje = StrMensaje;
        }
    }
}
