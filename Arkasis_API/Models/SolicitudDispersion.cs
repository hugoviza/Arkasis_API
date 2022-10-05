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

        public String IdEmpresa { get; set; }

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
                IdSolicitud = dataRow["IdSolicitud"].ToString() != "" ? Int16.Parse(dataRow["IdSolicitud"].ToString()) : 0;
                StrFechaAlta = dataRow["StrFechaAlta"].ToString() != "" ? dataRow["StrFechaAlta"].ToString() : "";
                StrStatusSolicitud = dataRow["StrStatusSolicitud"].ToString() != "" ? dataRow["StrStatusSolicitud"].ToString() : "";
                IdSucursal = dataRow["IdSucursal"].ToString() != "" ? dataRow["IdSucursal"].ToString() : "";
                idPromotor = dataRow["idPromotor"].ToString() != "" ? dataRow["idPromotor"].ToString() : "";
                StrUsuario = dataRow["StrUsuario"].ToString() != "" ? dataRow["StrUsuario"].ToString() : "";
                StrPromotor = dataRow["StrPromotor"].ToString() != "" ? dataRow["StrPromotor"].ToString() : "";
                IdCordinador = dataRow["IdCordinador"].ToString() != "" ? dataRow["IdCordinador"].ToString() : "";
                StrCordinador = dataRow["StrCordinador"].ToString() != "" ? dataRow["StrCordinador"].ToString() : "";
                IdCliente = dataRow["IdCliente"].ToString() != "" ? dataRow["IdCliente"].ToString() : "";
                StrApellidoPaterno = dataRow["StrApellidoPaterno"].ToString() != "" ? dataRow["StrApellidoPaterno"].ToString() : "";
                StrApellidoMaterno = dataRow["StrApellidoMaterno"].ToString() != "" ? dataRow["StrApellidoMaterno"].ToString() : "";
                StrNombre1 = dataRow["StrNombre1"].ToString() != "" ? dataRow["StrNombre1"].ToString() : "";
                StrNombre2 = dataRow["StrNombre2"].ToString() != "" ? dataRow["StrNombre2"].ToString() : "";
                StrFechaNacimiento = dataRow["StrFechaNacimiento"].ToString() != "" ? dataRow["StrFechaNacimiento"].ToString() : "";
                IdGenero = dataRow["IdGenero"].ToString() != "" ? dataRow["IdGenero"].ToString() : "";
                StrGenero = dataRow["StrGenero"].ToString() != "" ? dataRow["StrGenero"].ToString() : "";
                StrCURP = dataRow["StrCURP"].ToString() != "" ? dataRow["StrCURP"].ToString() : "";
                StrDomicilio = dataRow["StrDomicilio"].ToString() != "" ? dataRow["StrDomicilio"].ToString() : "";
                StrDomicilioCodigoPostal = dataRow["StrDomicilioCodigoPostal"].ToString() != "" ? dataRow["StrDomicilioCodigoPostal"].ToString() : "";
                StrDomicilioNumExt = dataRow["StrDomicilioNumExt"].ToString() != "" ? dataRow["StrDomicilioNumExt"].ToString() : "";
                StrDomicilioNumInt = dataRow["StrDomicilioNumInt"].ToString() != "" ? dataRow["StrDomicilioNumInt"].ToString() : "";
                StrDomicilioColonia = dataRow["StrDomicilioColonia"].ToString() != "" ? dataRow["StrDomicilioColonia"].ToString() : "";
                IdDomicilioEstado = dataRow["IdDomicilioEstado"].ToString() != "" ? dataRow["IdDomicilioEstado"].ToString() : "";
                StrDomicilioEstado = dataRow["StrDomicilioEstado"].ToString() != "" ? dataRow["StrDomicilioEstado"].ToString() : "";
                IdDomicilioMunicipio = dataRow["IdDomicilioMunicipio"].ToString() != "" ? dataRow["IdDomicilioMunicipio"].ToString() : "";
                StrDomicilioMunicipio = dataRow["StrDomicilioMunicipio"].ToString() != "" ? dataRow["StrDomicilioMunicipio"].ToString() : "";
                StrEstadoCivil = dataRow["StrEstadoCivil"].ToString() != "" ? dataRow["StrEstadoCivil"].ToString() : "";
                IdEstadoCivil = dataRow["IdEstadoCivil"].ToString() != "" ? dataRow["IdEstadoCivil"].ToString() : "";
                StrTelefono = dataRow["StrTelefono"].ToString() != "" ? dataRow["StrTelefono"].ToString() : "";
                StrCelular = dataRow["StrCelular"].ToString() != "" ? dataRow["StrCelular"].ToString() : "";
                StrOcupacion = dataRow["StrOcupacion"].ToString() != "" ? dataRow["StrOcupacion"].ToString() : "";
                IdActividad = dataRow["IdActividad"].ToString() != "" ? dataRow["IdActividad"].ToString() : "";
                StrActividad = dataRow["StrActividad"].ToString() != "" ? dataRow["StrActividad"].ToString() : "";
                StrNumeroINE = dataRow["StrNumeroINE"].ToString() != "" ? dataRow["StrNumeroINE"].ToString() : "";
                StrClaveINE = dataRow["StrClaveINE"].ToString() != "" ? dataRow["StrClaveINE"].ToString() : "";
                StrPais = dataRow["StrPais"].ToString() != "" ? dataRow["StrPais"].ToString() : "";
                StrEstadoNacimiento = dataRow["StrEstadoNacimiento"].ToString() != "" ? dataRow["StrEstadoNacimiento"].ToString() : "";
                StrNacionalidad = dataRow["StrNacionalidad"].ToString() != "" ? dataRow["StrNacionalidad"].ToString() : "";
                StrEmail = dataRow["StrEmail"].ToString() != "" ? dataRow["StrEmail"].ToString() : "";
                StrNombreConyuge = dataRow["StrNombreConyuge"].ToString() != "" ? dataRow["StrNombreConyuge"].ToString() : "";
                StrLugarNacimientoConyuge = dataRow["StrLugarNacimientoConyuge"].ToString() != "" ? dataRow["StrLugarNacimientoConyuge"].ToString() : "";
                StrFechaNacimientoConyuge = dataRow["StrFechaNacimientoConyuge"].ToString() != "" ? dataRow["StrFechaNacimientoConyuge"].ToString() : "";
                StrOcupacionConyuge = dataRow["StrOcupacionConyuge"].ToString() != "" ? dataRow["StrOcupacionConyuge"].ToString() : "";
                StrReferenciaBancaria = dataRow["StrReferenciaBancaria"].ToString() != "" ? dataRow["StrReferenciaBancaria"].ToString() : "";
                StrBanco = dataRow["StrBanco"].ToString() != "" ? dataRow["StrBanco"].ToString() : "";
                StrProducto = dataRow["StrProducto"].ToString() != "" ? dataRow["StrProducto"].ToString() : "";
                DblPlazo = dataRow["DblPlazo"].ToString() != "" ? Double.Parse(dataRow["DblPlazo"].ToString()) : 0;
                IntQuedateCasa = dataRow["IntQuedateCasa"].ToString() != "" ? Convert.ToInt16(Double.Parse(dataRow["IntQuedateCasa"].ToString())) : 0;
                DblMontoSolicitadoMejoraVivienda = dataRow["DblMontoSolicitadoMejoraVivienda"].ToString() != "" ? Double.Parse(dataRow["DblMontoSolicitadoMejoraVivienda"].ToString()) : 0;
                DblMontoSolicitadoEquipandoHogar = dataRow["DblMontoSolicitadoEquipandoHogar"].ToString() != "" ? Double.Parse(dataRow["DblMontoSolicitadoEquipandoHogar"].ToString()) : 0;
                DblIngresos = dataRow["DblIngresos"].ToString() != "" ? Double.Parse(dataRow["DblIngresos"].ToString()) : 0;
                DblEgresos = dataRow["DblEgresos"].ToString() != "" ? Double.Parse(dataRow["DblEgresos"].ToString()) : 0;
                StrCNBV = dataRow["StrCNBV"].ToString() != "" ? dataRow["StrCNBV"].ToString() : "";
                StrDomicilio_mejoraVivienda = dataRow["StrDomicilio_mejoraVivienda"].ToString() != "" ? dataRow["StrDomicilio_mejoraVivienda"].ToString() : "";
                StrCodigoPostal_mejoraVivienda = dataRow["StrCodigoPostal_mejoraVivienda"].ToString() != "" ? dataRow["StrCodigoPostal_mejoraVivienda"].ToString() : "";
                StrNumExt_mejoraVivienda = dataRow["StrNumExt_mejoraVivienda"].ToString() != "" ? dataRow["StrNumExt_mejoraVivienda"].ToString() : "";
                StrNumInt_mejoraVivienda = dataRow["StrNumInt_mejoraVivienda"].ToString() != "" ? dataRow["StrNumInt_mejoraVivienda"].ToString() : "";
                StrColonia_mejoraVivienda = dataRow["StrColonia_mejoraVivienda"].ToString() != "" ? dataRow["StrColonia_mejoraVivienda"].ToString() : "";
                IdEstado_mejoraVivienda = dataRow["IdEstado_mejoraVivienda"].ToString() != "" ? dataRow["IdEstado_mejoraVivienda"].ToString() : "";
                StrEstado_mejoraVivienda = dataRow["StrEstado_mejoraVivienda"].ToString() != "" ? dataRow["StrEstado_mejoraVivienda"].ToString() : "";
                IdMunicipio_mejoraVivienda = dataRow["IdMunicipio_mejoraVivienda"].ToString() != "" ? dataRow["IdMunicipio_mejoraVivienda"].ToString() : "";
                StrMunicipio_mejoraVivienda = dataRow["StrMunicipio_mejoraVivienda"].ToString() != "" ? dataRow["StrMunicipio_mejoraVivienda"].ToString() : "";
                StrFotoINEFrontal_B64 = dataRow["StrFotoINEFrontal_B64"].ToString() != "" ? dataRow["StrFotoINEFrontal_B64"].ToString() : "";
                StrFotoINEFrontal_nombre = dataRow["StrFotoINEFrontal_nombre"].ToString() != "" ? dataRow["StrFotoINEFrontal_nombre"].ToString() : "";
                StrFotoINEReverso_B64 = dataRow["StrFotoINEReverso_B64"].ToString() != "" ? dataRow["StrFotoINEReverso_B64"].ToString() : "";
                StrFotoINEReverso_nombre = dataRow["StrFotoINEReverso_nombre"].ToString() != "" ? dataRow["StrFotoINEReverso_nombre"].ToString() : "";
                StrFotoPerfil_B64 = dataRow["StrFotoPerfil_B64"].ToString() != "" ? dataRow["StrFotoPerfil_B64"].ToString() : "";
                StrFotoPerfil_nombre = dataRow["StrFotoPerfil_nombre"].ToString() != "" ? dataRow["StrFotoPerfil_nombre"].ToString() : "";
                StrFotoComprobanteDomicilio_B64 = dataRow["StrFotoComprobanteDomicilio_B64"].ToString() != "" ? dataRow["StrFotoComprobanteDomicilio_B64"].ToString() : "";
                StrFotoComprobanteDomicilio_nombre = dataRow["StrFotoComprobanteDomicilio_nombre"].ToString() != "" ? dataRow["StrFotoComprobanteDomicilio_nombre"].ToString() : "";
                IdTipoVencimiento = dataRow["IdTipoVencimiento"].ToString() != "" ? Int32.Parse(dataRow["IdTipoVencimiento"].ToString()) : 0;
                StrTipoVencimiento = dataRow["StrTipoVencimiento"].ToString() != "" ? dataRow["StrTipoVencimiento"].ToString() : "";
                IntNumPagos = dataRow["IntNumPagos"].ToString() != "" ? Int32.Parse(dataRow["IntNumPagos"].ToString()) : 0;
                IdTipoContratoIndividual = dataRow["IdTipoContratoIndividual"].ToString() != "" ? dataRow["IdTipoContratoIndividual"].ToString() : "";
            }
        }
    }
}
