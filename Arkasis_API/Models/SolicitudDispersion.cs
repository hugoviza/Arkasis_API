using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class SolicitudDispersion
    {
        public int IdSolicitud { get; set; }
        public String StrFechaAlta { get; set; }
        public String StrStatusSolicitud { get; set; }
        public String IdSucursal { get; set; }
        public String idPromotor { get; set; }
        public String StrUsuarioPromotor { get; set; }
        public String StrPromotor { get; set; }
        public String IdCordinador { get; set; }
        public String StrCordinador { get; set; }
        public String IdCliente { get; set; }
        public String StrApellidoPaterno { get; set; }
        public String StrApellidoMaterno { get; set; }
        public String StrNombre1 { get; set; }
        public String StrNombre2 { get; set; }
        public String StrFechaNacimiento { get; set; }
        public String IdGenero { get; set; }
        public String StrGenero { get; set; }
        public String StrCURP { get; set; }
        public String StrDomicilio { get; set; }
        public String StrDomicilioCodigoPostal { get; set; }
        public String StrDomicilioNumExt { get; set; }
        public String StrDomicilioNumInt { get; set; }
        public String StrDomicilioColonia { get; set; }
        public String IdDomicilioEstado { get; set; }
        public String StrDomicilioEstado { get; set; }
        public String IdDomicilioMunicipio { get; set; }
        public String StrDomicilioMunicipio { get; set; }
        public String StrEstadoCivil { get; set; }
        public String IdEstadoCivil { get; set; }
        public String StrTelefono { get; set; }
        public String StrCelular { get; set; }
        public String StrOcupacion { get; set; }
        public String IdActividad { get; set; }
        public String StrActividad { get; set; }
        public String StrNumeroINE { get; set; }
        public String StrClaveINE { get; set; }
        public String StrPais { get; set; }
        public String StrEstadoNacimiento { get; set; }
        public String StrNacionalidad { get; set; }
        public String StrEmail { get; set; }
        public String StrNombreConyuge { get; set; }
        public String StrLugarNacimientoConyuge { get; set; }
        public String StrFechaNacimientoConyuge { get; set; }
        public String StrOcupacionConyuge { get; set; }
        public String StrReferenciaBancaria { get; set; }
        public String StrBanco { get; set; }
        public String StrProducto { get; set; }
        public Int16 IntPlazo { get; set; }
        public Int16 IntQuedateCasa { get; set; }
        public Double DblMontoSolicitadoMejoraVivienda { get; set; }
        public Double DblMontoSolicitadoEquipandoHogar { get; set; }
        public Double DblIngresos { get; set; }
        public Double DblEgresos { get; set; }
        public String StrCNBV { get; set; }

        public String StrDomicilio_mejoraVivienda { get; set; }
        public String StrCodigoPostal_mejoraVivienda { get; set; }
        public String StrNumExt_mejoraVivienda { get; set; }
        public String StrNumInt_mejoraVivienda { get; set; }
        public String StrColonia_mejoraVivienda { get; set; }
        public String IdEstado_mejoraVivienda { get; set; }
        public String StrEstado_mejoraVivienda { get; set; }
        public String IdMunicipio_mejoraVivienda { get; set; }
        public String StrMunicipio_mejoraVivienda { get; set; }


        public String StrNombreCompleto { get { return ($@"{StrNombre1} {StrNombre2}").Trim() + $@" {StrApellidoPaterno} {StrApellidoMaterno}"; } }
    }
}
