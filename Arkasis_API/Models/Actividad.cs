using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Actividad
    {
        public String IdActividad { get; set; }
        public String StrActividad { get; set; }
        public String StrCNBV { get; set; }

        public Actividad()
        {
            IdActividad = "";
            StrActividad = "";
            StrCNBV = "";
        }

        public Actividad(DataRow dataRow)
        {
            if (dataRow != null)
            {
                IdActividad = dataRow["IdActividad"].ToString();
                StrActividad = dataRow["StrActividad"].ToString();
                StrCNBV = dataRow["StrCNBV"].ToString();
            }
        }
    }
}
