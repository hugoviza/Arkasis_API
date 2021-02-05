using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class ResumenSolicitudes
    {
        public int IntTotalTramite { get; set; }
        public int IntTotalAutorizado { get; set; }
        public int IntTotalRechazado { get; set; }
        public int IntTotalCancelado { get; set; }
        public int IntTotalMinistrado { get; set; }
        public int IntTotalRegistros { get; set; }

        public ResumenSolicitudes(DataRow dataRow)
        {
            this.IntTotalTramite = Int16.Parse(dataRow["IntTotalTramite"].ToString());
            this.IntTotalAutorizado = Int16.Parse(dataRow["IntTotalAutorizado"].ToString());
            this.IntTotalRechazado = Int16.Parse(dataRow["IntTotalRechazado"].ToString());
            this.IntTotalCancelado = Int16.Parse(dataRow["IntTotalCancelado"].ToString());
            this.IntTotalRegistros = Int16.Parse(dataRow["IntTotalRegistros"].ToString());
        }
    }
}
