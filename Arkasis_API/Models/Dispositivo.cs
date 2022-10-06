using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Dispositivo
    {
        public String IdSucursal { get; set; }
        public Int32 IdDispositivo { get; set; }
        public String Plataforma { get; set; }
        public String UUIDDispositivo { get; set; }
        public String FechaHoraRegistroUUIDDispositivo { get; set; }
        public String TokenActivacion { get; set; }
        public String FechaHoraRegistroTokenActivacion{ get; set; }
        public Int32 EstatusTokenAcivacion { get; set; }

        public String UsuarioAlta { get; set; }
        public String FechaHoraAlta { get; set; }
        public String UsuarioModificacion { get; set; }
        public String FechaHoraModificacion { get; set; }

        public Dispositivo()
        {

        }

        public Dispositivo(DataRow dataRow)
        {
            IdSucursal = dataRow["IdSucursal"].ToString();
            IdDispositivo = Int32.Parse(dataRow["IdDispositivo"].ToString());
            Plataforma = dataRow["Plataforma"].ToString();
            UUIDDispositivo = dataRow["UUIDDispositivo"].ToString();
            FechaHoraRegistroUUIDDispositivo = dataRow["FechaHoraRegistroUUIDDispositivo"].ToString();
            TokenActivacion = dataRow["TokenActivacion"].ToString();
            FechaHoraRegistroTokenActivacion = dataRow["FechaHoraRegistroTokenActivacion"].ToString();
            EstatusTokenAcivacion = Int32.Parse(dataRow["EstatusTokenAcivacion"].ToString());
            UsuarioAlta = dataRow["UsuarioAlta"].ToString();
            FechaHoraAlta = dataRow["FechaHoraAlta"].ToString();
            UsuarioModificacion = dataRow["UsuarioModificacion"].ToString();
            FechaHoraModificacion = dataRow["FechaHoraModificacion"].ToString();

        }
    }
}
