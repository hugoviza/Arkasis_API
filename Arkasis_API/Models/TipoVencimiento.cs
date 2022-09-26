﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class TipoVencimiento
    {
        public String IdSucursal { get; set; }
        public String IdTipoVencimiento { get; set; }
        public String StrTipoVencimiento { get; set; }
        public Int16 NumDias { get; set; }

        public TipoVencimiento()
        {
            IdSucursal = "";
            IdTipoVencimiento = "";
            StrTipoVencimiento = "";
            NumDias = 0;
        }

        public TipoVencimiento(DataRow dataRow)
        {
            if (dataRow != null)
            {
                IdSucursal = dataRow["IdSucursal"].ToString();
                IdTipoVencimiento = dataRow["IdTipoVencimiento"].ToString();
                StrTipoVencimiento = dataRow["StrTipoVencimiento"].ToString();
                NumDias = Int16.Parse(dataRow["intNumDias"].ToString());
            }
        }
    }
}
