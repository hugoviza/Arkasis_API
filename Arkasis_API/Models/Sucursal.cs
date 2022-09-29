using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Sucursal
    {
        public String IdSucursal { get; set; }
        public String StrSucursal { get; set; }
        public String StrClaveSucursal { get; set; }

        public String IdEmpresaCto { get; set; }

        public Sucursal()
        {
            IdSucursal = "";
            StrSucursal = "";
            StrClaveSucursal = "";
            IdEmpresaCto = "";
        }

        public Sucursal(DataRow dataRow)
        {
            if (dataRow != null)
            {
                IdSucursal = dataRow["IdSucursal"].ToString();
                StrSucursal = dataRow["StrSucursal"].ToString();
                StrClaveSucursal = dataRow["StrClaveSucursal"].ToString();
                IdEmpresaCto = dataRow["IdEmpresaCto"].ToString();
            }
        }
    }
}
