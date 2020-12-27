using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class Cliente
    {
        public String IdCliente { get; set; }
        public String StrGenero { get; set; }
        public String StrCurp { get; set; }
        public String StrApellidoPaterno { get; set; }
        public String StrApellidoMaterno { get; set; }
        public String StrNombre1 { get; set; }
        public String StrNombre2 { get; set; }
        public String DatFechaNacimiento { get; set; }
        public String StrEdoCivil { get; set; }
        public String StrTelefono { get; set; }
        public String StrCelular { get; set; }
        public String StrCodigoPostal { get; set; }
        public String StrDireccion { get; set; }
        public String StrDireccionNumero { get; set; }
        public String StrDireccionNumeroInterno { get; set; }
        public String StrColonia { get; set; }
        public String IdEstado { get; set; }
        public String StrEstado { get; set; }
        public String IdMunicipio { get; set; }
        public String StrMunicipio { get; set; }
        public String StrClaveGrupo { get; set; }
        public String IdActividad { get; set; }
        public String StrDescripcionActividad { get; set; }
        public String StrNumeroElector { get; set; }
        public String StrClaveElector { get; set; }
        public String StrPaisNacimiento { get; set; }
        public String StrEstadoNacimiento { get; set; }
        public String StrNacionalidad { get; set; }
        public String StrEmail { get; set; }
        public String StrNombreConyugue { get; set; }
        public String DatFechaNacimientoConyugue { get; set; }
        public String StrLugarNacimientoConyugue { get; set; }
        public String StrOcupacion { get; set; }

        public Cliente()
        {
            IdCliente = "";
            StrGenero = "";
            StrCurp = "";
            StrApellidoPaterno = "";
            StrApellidoMaterno = "";
            StrNombre1 = "";
            StrNombre2 = "";
            DatFechaNacimiento = "";
            StrEdoCivil = "";
            StrTelefono = "";
            StrCelular = "";
            StrCodigoPostal = "";
            StrDireccion = "";
            StrDireccionNumero = "";
            StrDireccionNumeroInterno = "";
            StrColonia = "";
            IdEstado = "";
            StrEstado = "";
            IdMunicipio = "";
            StrMunicipio = "";
            StrClaveGrupo = "";
            IdActividad = "";
            StrDescripcionActividad = "";
            StrNumeroElector = "";
            StrClaveElector = "";
            StrPaisNacimiento = "";
            StrEstadoNacimiento = "";
            StrNacionalidad = "";
            StrEmail = "";
            StrNombreConyugue = "";
            DatFechaNacimientoConyugue = "";
            StrLugarNacimientoConyugue = "";
            StrOcupacion = "";
        }

        public Cliente(DataRow dataRow)
        {
            if(dataRow != null)
            {
                IdCliente = dataRow["IdCliente"].ToString();
                StrGenero = dataRow["StrGenero"].ToString();
                StrCurp = dataRow["StrCurp"].ToString();
                StrApellidoPaterno = dataRow["StrApellidoPaterno"].ToString();
                StrApellidoMaterno = dataRow["StrApellidoMaterno"].ToString();
                StrNombre1 = dataRow["StrNombre1"].ToString();
                StrNombre2 = dataRow["StrNombre2"].ToString();
                DatFechaNacimiento = dataRow["DatFechaNacimiento"].ToString();
                StrEdoCivil = dataRow["StrEdoCivil"].ToString();
                StrTelefono = dataRow["StrTelefono"].ToString();
                StrCelular = dataRow["StrCelular"].ToString();
                StrCodigoPostal = dataRow["StrCodigoPostal"].ToString();
                StrDireccion = dataRow["StrDireccion"].ToString();
                StrDireccionNumero = dataRow["StrDireccionNumero"].ToString();
                StrDireccionNumeroInterno = dataRow["StrDireccionNumeroInterno"].ToString();
                StrColonia = dataRow["StrColonia"].ToString();
                IdEstado = dataRow["IdEstado"].ToString();
                StrEstado = dataRow["StrEstado"].ToString();
                IdMunicipio = dataRow["IdMunicipio"].ToString();
                StrMunicipio = dataRow["StrMunicipio"].ToString();
                StrClaveGrupo = dataRow["StrClaveGrupo"].ToString();
                IdActividad = dataRow["IdActividad"].ToString();
                StrDescripcionActividad = dataRow["StrDescripcionActividad"].ToString();
                StrNumeroElector = dataRow["StrNumeroElector"].ToString();
                StrClaveElector = dataRow["StrClaveElector"].ToString();
                StrPaisNacimiento = dataRow["StrPaisNacimiento"].ToString();
                StrEstadoNacimiento = dataRow["StrEstadoNacimiento"].ToString();
                StrNacionalidad = dataRow["StrNacionalidad"].ToString();
                StrEmail = dataRow["StrEmail"].ToString();
                StrNombreConyugue = dataRow["StrNombreConyugue"].ToString();
                DatFechaNacimientoConyugue = dataRow["DatFechaNacimientoConyugue"].ToString();
                StrLugarNacimientoConyugue = dataRow["StrLugarNacimientoConyugue"].ToString();
                StrOcupacion = dataRow["StrOcupacion"].ToString();
            }
        }
    }
}
