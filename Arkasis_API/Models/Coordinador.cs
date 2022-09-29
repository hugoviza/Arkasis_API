using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Coordinador
    {
        public String IdCoordinador { get; set; }
        public String IdSucursal { get; set; }
        public String StrNombre { get; set; }
        public String IdEmpresaCto { get; set; }

        public Coordinador(DataRow dataRow)
        {
            if (dataRow != null)
            {
                IdCoordinador = dataRow["IdCoordinador"].ToString();
                IdSucursal = dataRow["IdSucursal"].ToString();
                StrNombre = dataRow["StrNombre"].ToString();
                IdEmpresaCto = dataRow["idEmpresaCTo"].ToString();
            }
        }
    }
}
