using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Municipio
    {
        public String IdEstado { get; set; }
        public String StrEstado { get; set; }
        public String IdMunicipio{ get; set; }
        public String StrMunicipio { get; set; }

        public Municipio()
        {

        }

        public Municipio(DataRow dataRow)
        {
            if (dataRow != null)
            {
                IdEstado = dataRow["IdEstado"].ToString();
                StrEstado = dataRow["StrEstado"].ToString();
                IdMunicipio = dataRow["IdMunicipio"].ToString();
                StrMunicipio = dataRow["StrMunicipio"].ToString();
            }
        }
    }
}
