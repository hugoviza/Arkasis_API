using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Usuario
    {
        public String User { get; set; }
        public String Password { get; set; }
        public String Nombre { get; set; }

        public Usuario()
        {

        }

        public Usuario(DataRow dataRow)
        {
            if(dataRow != null)
            {
                User = dataRow["Usuario"].ToString();
                Password = dataRow["Password"].ToString();
                Nombre = dataRow["Nombre"].ToString();
            }
        }

    }
}
