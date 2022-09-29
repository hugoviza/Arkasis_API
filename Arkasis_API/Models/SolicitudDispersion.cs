using System;
using System.Collections.Generic;
using System.Data;
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
        public String StrUsuario { get; set; }
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
        public Double DblPlazo { get; set; }
        public Int32 IntQuedateCasa { get; set; }
        public Double DblMontoSolicitadoMejoraVivienda { get; set; }
        public Double DblMontoSolicitadoEquipandoHogar { get; set; }
        public Double DblIngresos { get; set; }
        public Double DblEgresos { get; set; }
        public String StrCNBV { get; set; }
        public Int32 IdTipoVencimiento { get; set; }
        public String StrTipoVencimiento { get; set; }
        public Int32 IntNumPagos { get; set; }
        public String IdTipoContratoIndividual { get; set; }

        public String StrDomicilio_mejoraVivienda { get; set; }
        public String StrCodigoPostal_mejoraVivienda { get; set; }
        public String StrNumExt_mejoraVivienda { get; set; }
        public String StrNumInt_mejoraVivienda { get; set; }
        public String StrColonia_mejoraVivienda { get; set; }
        public String IdEstado_mejoraVivienda { get; set; }
        public String StrEstado_mejoraVivienda { get; set; }
        public String IdMunicipio_mejoraVivienda { get; set; }
        public String StrMunicipio_mejoraVivienda { get; set; }

        public String StrFotoINEFrontal_B64 { get; set; }
        public String StrFotoINEFrontal_nombre { get; set; }
        public String StrFotoINEReverso_B64 { get; set; }
        public String StrFotoINEReverso_nombre { get; set; }
        public String StrFotoPerfil_B64 { get; set; }
        public String StrFotoPerfil_nombre { get; set; }
        public String StrFotoComprobanteDomicilio_B64 { get; set; }
        public String StrFotoComprobanteDomicilio_nombre { get; set; }

        public String StrNombreCompleto { get { return ($@"{StrNombre1} {StrNombre2}").Trim() + $@" {StrApellidoPaterno} {StrApellidoMaterno}"; } }

        public SolicitudDispersion()
        {

        }

        public SolicitudDispersion(DataRow dataRow)
        {
            if(dataRow != null)
            {
                IdSolicitud = dataRow["IdSolicitud"] != null ? Int16.Parse(dataRow["IdSolicitud"].ToString()) : 0;
                StrFechaAlta = dataRow["StrFechaAlta"] != null ? dataRow["StrFechaAlta"].ToString() : "";
                StrStatusSolicitud = dataRow["StrStatusSolicitud"] != null ? dataRow["StrStatusSolicitud"].ToString() : "";
                IdSucursal = dataRow["IdSucursal"] != null ? dataRow["IdSucursal"].ToString() : "";
                idPromotor = dataRow["idPromotor"] != null ? dataRow["idPromotor"].ToString() : "";
                StrUsuario = dataRow["StrUsuario"] != null ? dataRow["StrUsuario"].ToString() : "";
                StrPromotor = dataRow["StrPromotor"] != null ? dataRow["StrPromotor"].ToString() : "";
                IdCordinador = dataRow["IdCordinador"] != null ? dataRow["IdCordinador"].ToString() : "";
                StrCordinador = dataRow["StrCordinador"] != null ? dataRow["StrCordinador"].ToString() : "";
                IdCliente = dataRow["IdCliente"] != null ? dataRow["IdCliente"].ToString() : "";
                StrApellidoPaterno = dataRow["StrApellidoPaterno"] != null ? dataRow["StrApellidoPaterno"].ToString() : "";
                StrApellidoMaterno = dataRow["StrApellidoMaterno"] != null ? dataRow["StrApellidoMaterno"].ToString() : "";
                StrNombre1 = dataRow["StrNombre1"] != null ? dataRow["StrNombre1"].ToString() : "";
                StrNombre2 = dataRow["StrNombre2"] != null ? dataRow["StrNombre2"].ToString() : "";
                StrFechaNacimiento = dataRow["StrFechaNacimiento"] != null ? dataRow["StrFechaNacimiento"].ToString() : "";
                IdGenero = dataRow["IdGenero"] != null ? dataRow["IdGenero"].ToString() : "";
                StrGenero = dataRow["StrGenero"] != null ? dataRow["StrGenero"].ToString() : "";
                StrCURP = dataRow["StrCURP"] != null ? dataRow["StrCURP"].ToString() : "";
                StrDomicilio = dataRow["StrDomicilio"] != null ? dataRow["StrDomicilio"].ToString() : "";
                StrDomicilioCodigoPostal = dataRow["StrDomicilioCodigoPostal"] != null ? dataRow["StrDomicilioCodigoPostal"].ToString() : "";
                StrDomicilioNumExt = dataRow["StrDomicilioNumExt"] != null ? dataRow["StrDomicilioNumExt"].ToString() : "";
                StrDomicilioNumInt = dataRow["StrDomicilioNumInt"] != null ? dataRow["StrDomicilioNumInt"].ToString() : "";
                StrDomicilioColonia = dataRow["StrDomicilioColonia"] != null ? dataRow["StrDomicilioColonia"].ToString() : "";
                IdDomicilioEstado = dataRow["IdDomicilioEstado"] != null ? dataRow["IdDomicilioEstado"].ToString() : "";
                StrDomicilioEstado = dataRow["StrDomicilioEstado"] != null ? dataRow["StrDomicilioEstado"].ToString() : "";
                IdDomicilioMunicipio = dataRow["IdDomicilioMunicipio"] != null ? dataRow["IdDomicilioMunicipio"].ToString() : "";
                StrDomicilioMunicipio = dataRow["StrDomicilioMunicipio"] != null ? dataRow["StrDomicilioMunicipio"].ToString() : "";
                StrEstadoCivil = dataRow["StrEstadoCivil"] != null ? dataRow["StrEstadoCivil"].ToString() : "";
                IdEstadoCivil = dataRow["IdEstadoCivil"] != null ? dataRow["IdEstadoCivil"].ToString() : "";
                StrTelefono = dataRow["StrTelefono"] != null ? dataRow["StrTelefono"].ToString() : "";
                StrCelular = dataRow["StrCelular"] != null ? dataRow["StrCelular"].ToString() : "";
                StrOcupacion = dataRow["StrOcupacion"] != null ? dataRow["StrOcupacion"].ToString() : "";
                IdActividad = dataRow["IdActividad"] != null ? dataRow["IdActividad"].ToString() : "";
                StrActividad = dataRow["StrActividad"] != null ? dataRow["StrActividad"].ToString() : "";
                StrNumeroINE = dataRow["StrNumeroINE"] != null ? dataRow["StrNumeroINE"].ToString() : "";
                StrClaveINE = dataRow["StrClaveINE"] != null ? dataRow["StrClaveINE"].ToString() : "";
                StrPais = dataRow["StrPais"] != null ? dataRow["StrPais"].ToString() : "";
                StrEstadoNacimiento = dataRow["StrEstadoNacimiento"] != null ? dataRow["StrEstadoNacimiento"].ToString() : "";
                StrNacionalidad = dataRow["StrNacionalidad"] != null ? dataRow["StrNacionalidad"].ToString() : "";
                StrEmail = dataRow["StrEmail"] != null ? dataRow["StrEmail"].ToString() : "";
                StrNombreConyuge = dataRow["StrNombreConyuge"] != null ? dataRow["StrNombreConyuge"].ToString() : "";
                StrLugarNacimientoConyuge = dataRow["StrLugarNacimientoConyuge"] != null ? dataRow["StrLugarNacimientoConyuge"].ToString() : "";
                StrFechaNacimientoConyuge = dataRow["StrFechaNacimientoConyuge"] != null ? dataRow["StrFechaNacimientoConyuge"].ToString() : "";
                StrOcupacionConyuge = dataRow["StrOcupacionConyuge"] != null ? dataRow["StrOcupacionConyuge"].ToString() : "";
                StrReferenciaBancaria = dataRow["StrReferenciaBancaria"] != null ? dataRow["StrReferenciaBancaria"].ToString() : "";
                StrBanco = dataRow["StrBanco"] != null ? dataRow["StrBanco"].ToString() : "";
                StrProducto = dataRow["StrProducto"] != null ? dataRow["StrProducto"].ToString() : "";
                DblPlazo = dataRow["DblPlazo"] != null ? Double.Parse(dataRow["DblPlazo"].ToString()) : 0;
                IntQuedateCasa = dataRow["IntQuedateCasa"] != null ? Convert.ToInt16(Double.Parse(dataRow["IntQuedateCasa"].ToString())) : 0;
                DblMontoSolicitadoMejoraVivienda = dataRow["DblMontoSolicitadoMejoraVivienda"] != null ? Double.Parse(dataRow["DblMontoSolicitadoMejoraVivienda"].ToString()) : 0;
                DblMontoSolicitadoEquipandoHogar = dataRow["DblMontoSolicitadoEquipandoHogar"] != null ? Double.Parse(dataRow["DblMontoSolicitadoEquipandoHogar"].ToString()) : 0;
                DblIngresos = dataRow["DblIngresos"] != null ? Double.Parse(dataRow["DblIngresos"].ToString()) : 0;
                DblEgresos = dataRow["DblEgresos"] != null ? Double.Parse(dataRow["DblEgresos"].ToString()) : 0;
                StrCNBV = dataRow["StrCNBV"] != null ? dataRow["StrCNBV"].ToString() : "";
                StrDomicilio_mejoraVivienda = dataRow["StrDomicilio_mejoraVivienda"] != null ? dataRow["StrDomicilio_mejoraVivienda"].ToString() : "";
                StrCodigoPostal_mejoraVivienda = dataRow["StrCodigoPostal_mejoraVivienda"] != null ? dataRow["StrCodigoPostal_mejoraVivienda"].ToString() : "";
                StrNumExt_mejoraVivienda = dataRow["StrNumExt_mejoraVivienda"] != null ? dataRow["StrNumExt_mejoraVivienda"].ToString() : "";
                StrNumInt_mejoraVivienda = dataRow["StrNumInt_mejoraVivienda"] != null ? dataRow["StrNumInt_mejoraVivienda"].ToString() : "";
                StrColonia_mejoraVivienda = dataRow["StrColonia_mejoraVivienda"] != null ? dataRow["StrColonia_mejoraVivienda"].ToString() : "";
                IdEstado_mejoraVivienda = dataRow["IdEstado_mejoraVivienda"] != null ? dataRow["IdEstado_mejoraVivienda"].ToString() : "";
                StrEstado_mejoraVivienda = dataRow["StrEstado_mejoraVivienda"] != null ? dataRow["StrEstado_mejoraVivienda"].ToString() : "";
                IdMunicipio_mejoraVivienda = dataRow["IdMunicipio_mejoraVivienda"] != null ? dataRow["IdMunicipio_mejoraVivienda"].ToString() : "";
                StrMunicipio_mejoraVivienda = dataRow["StrMunicipio_mejoraVivienda"] != null ? dataRow["StrMunicipio_mejoraVivienda"].ToString() : "";
                StrFotoINEFrontal_B64 = dataRow["StrFotoINEFrontal_B64"] != null ? dataRow["StrFotoINEFrontal_B64"].ToString() : "";
                StrFotoINEFrontal_nombre = dataRow["StrFotoINEFrontal_nombre"] != null ? dataRow["StrFotoINEFrontal_nombre"].ToString() : "";
                StrFotoINEReverso_B64 = dataRow["StrFotoINEReverso_B64"] != null ? dataRow["StrFotoINEReverso_B64"].ToString() : "";
                StrFotoINEReverso_nombre = dataRow["StrFotoINEReverso_nombre"] != null ? dataRow["StrFotoINEReverso_nombre"].ToString() : "";
                StrFotoPerfil_B64 = dataRow["StrFotoPerfil_B64"] != null ? dataRow["StrFotoPerfil_B64"].ToString() : "";
                StrFotoPerfil_nombre = dataRow["StrFotoPerfil_nombre"] != null ? dataRow["StrFotoPerfil_nombre"].ToString() : "";
                StrFotoComprobanteDomicilio_B64 = dataRow["StrFotoComprobanteDomicilio_B64"] != null ? dataRow["StrFotoComprobanteDomicilio_B64"].ToString() : "";
                StrFotoComprobanteDomicilio_nombre = dataRow["StrFotoComprobanteDomicilio_nombre"] != null ? dataRow["StrFotoComprobanteDomicilio_nombre"].ToString() : "";
                IdTipoVencimiento = dataRow["IdTipoVencimiento"] != null ? Int32.Parse(dataRow["IdTipoVencimiento"].ToString()) : 0;
                StrTipoVencimiento = dataRow["StrTipoVencimiento"] != null ? dataRow["StrTipoVencimiento"].ToString() : "";
                IntNumPagos = dataRow["IntNumPagos"] != null ? Int32.Parse(dataRow["IntNumPagos"].ToString()) : 0;
                IdTipoContratoIndividual = dataRow["IdTipoContratoIndividual"] != null ? dataRow["IdTipoContratoIndividual"].ToString() : "";
            }
        }
    }
}
