using Arkasis_API.Attributes;
using Arkasis_API.Conexiones;
using Arkasis_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("api/dispersion")]
    public class SolicitudDispersionController : Controller
    {
        class QuerySolicitud
        {
            public String StrQuery { get; set; }
            public String StrMensaje { get; set; }

            public QuerySolicitud(String StrQuery, String StrMensaje)
            {
                this.StrQuery = StrQuery;
                this.StrMensaje = StrMensaje;
            }

        }

        [HttpPost("nueva")]
        public IActionResult GuardarCliente(SolicitudDispersion sd)
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            QuerySolicitud querySolicitud = GenerarQueryGuardarSolicitud(conexionSQL, sd);
            if(querySolicitud.StrMensaje != "")
            {
                return Ok(new { Mensaje = querySolicitud.StrMensaje, Success = false });
            }

            String[] arrayQuery = new String[1];
            arrayQuery[0] = querySolicitud.StrQuery;
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayQuery);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    return Ok(new { Mensaje = "Guardado correctamente", Success = true, Resultado = "1" });
                }
                else
                {
                    return Ok(new { Mensaje = "No se ha guardado ", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se pudo guardar ", Success = false });
            }
        }

        [HttpPost("batch/nueva")]
        public IActionResult BatchGuardarCliente(List<SolicitudDispersion> listaSolicitudes)
        {
            List<EstatusSincronizacionSolicitud> listaResponse = new List<EstatusSincronizacionSolicitud>();
            ConexionSQL conexionSQL = new ConexionSQL();

            foreach (SolicitudDispersion sd in listaSolicitudes)
            {
                QuerySolicitud querySolicitud = GenerarQueryGuardarSolicitud(conexionSQL, sd);
                if (querySolicitud.StrMensaje != "")
                {
                    listaResponse.Add(new EstatusSincronizacionSolicitud(sd.IdSolicitud, false, querySolicitud.StrMensaje));
                }
                else
                {
                    String[] arrayQuery = new String[1];
                    arrayQuery[0] = querySolicitud.StrQuery;
                    DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayQuery);

                    if (arrayResult != null)
                    {
                        if (arrayResult[0].Rows.Count > 0)
                        {
                            listaResponse.Add(new EstatusSincronizacionSolicitud(sd.IdSolicitud, true, ""));
                        }
                        else
                        {
                            listaResponse.Add(new EstatusSincronizacionSolicitud(sd.IdSolicitud, false, "Error al guardar"));
                        }
                    }
                    else
                    {
                        listaResponse.Add(new EstatusSincronizacionSolicitud(sd.IdSolicitud, false, "Error al guardar"));
                    }
                }
            }

            return Ok(new { Mensaje = "Procesado correctamente", Success = true, Resultado = listaResponse.ToArray()});
        }


        private QuerySolicitud GenerarQueryGuardarSolicitud(ConexionSQL conexionSQL, SolicitudDispersion sd)
        {
            List<String> queries = new List<string>();

            //Validamos la curp del cliente
            queries.Add($@"select cteLlave as IdCliente from arcicte where cteX023 like '%{sd.StrCURP}%'");
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(queries.ToArray());

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    sd.IdCliente = arrayResult[0].Rows[0]["IdCliente"].ToString();
                } else if(arrayResult[0].Rows.Count > 1)
                {
                    //Si hay mas de un cliente con el mismo curp entonces no guardamos nada
                    return new QuerySolicitud("", "Curp duplicada en base de datos");
                }
            }

            //Validamos que el cliente no tenga algun credito vivo
            queries.Clear();
            queries.Add(
                $@"SELECT 
                    dbo.arcicte.cteX023 as strCurp
                FROM dbo.arcicte 
                INNER JOIN dbo.arciaux ON dbo.arcicte.cteLlave = dbo.arciaux.auxX001 
                WHERE(dbo.arcicte.cteX023 = '{sd.StrCURP}')
                AND dbo.arciaux.auxX013 > 0
                order by datFechaVencimiento desc");
            //Si ya tiene creditos omitimos su registro
            arrayResult = conexionSQL.EjecutarQueries(queries.ToArray());
            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    return new QuerySolicitud("", "El cliente ya tiene un crédito activo");
                }
            }

            //Validamos que no tenga otra solicitud activa
            queries.Clear();
            queries.Add($@"select solX004 from arciced where cedX023 = '{sd.StrCURP}' AND solX004 = '1'");
            arrayResult = conexionSQL.EjecutarQueries(queries.ToArray());

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    //Si ya tiene solicitudes en tramite pues no guardamos otra nueva
                    return new QuerySolicitud("", "El cliente ya tiene una solicitud en trámite");
                }
            }

            queries.Clear();
            queries.Add("DECLARE @idGrupo NVARCHAR(50)");
            queries.Add("DECLARE @idCliente NVARCHAR(50)");
            queries.Add("DECLARE @idSolicitudGrupo Integer");

            if (sd.IdCliente == "")
            {

                /*queries.Add($@"SET @idCliente = (select RIGHT('000000' + CAST( (max(cteLlave) + 1) AS VARCHAR), 7) as consecutivo from arcicte)");
                queries.Add($@"SET @idGrupo = (select RIGHT('000000' + CAST( (max(gruLlave) + 1) AS VARCHAR), 7) as consecutivo from arcigru)");

                queries.Add($@"insert into arcigru 
                                (gruLlave, gruX001, gruX003, gruX004, gruX005, gruX005c, gruX007, gruX014, gruX015, gruX016, gruX016c, gruX301, gruX302, gruX303, gruX304) values
                                (@idGrupo, '{sd.IdSucursal}', '{sd.StrNombreCompleto}', '1', '{sd.idPromotor}', '{sd.StrPromotor}', '{sd.StrFechaAlta}', '{sd.IdDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '1', 'INDIVIDUAL', '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");


                queries.Add($@"insert into arcicte 
                                (cteLlave, cteX001, cteX003, cteX004, cteX005, cteX006, cteX007, cteX008, cteX009, cteX010, cteX012, cteX013, cteX014, cteX015, cteX016, cteX019, cteX020, cteX021, cteX023, cteX024, cteX025, cteX026, cteX027, cteX028, cteX030, cteX031, cteX031c, cteX033, cteX034, cteX036, cteX037, cteX040, cteX046, cteX047, cteX048, cteX049, cteX041, cteX301, cteX302, cteX303, cteX304 ) values
                                (@idCliente, '{sd.IdSucursal}', '{sd.StrApellidoPaterno}', '{sd.StrApellidoMaterno}', '{sd.StrNombre1}', '{sd.StrNombre2}', '{sd.StrNombreCompleto}', '{sd.StrDomicilio}', '{sd.StrDomicilioNumExt}', '{sd.StrDomicilioNumInt}', '{sd.StrDomicilioColonia}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP}', '{sd.StrPais}', '{sd.StrEstadoNacimiento}', '{sd.StrNacionalidad}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail}', '{sd.StrOcupacion}', '{sd.StrActividad}', '{sd.StrNombreConyuge}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge}', '{sd.StrOcupacionConyuge}', @idGrupo, '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");
                */

                /*
                queries.Add($@"insert into arcigru 
                                (gruX001, gruX003, gruX004, gruX005, gruX005c, gruX007, gruX014, gruX015, gruX016, gruX016c, gruX301, gruX302, gruX303, gruX304) values
                                ('{sd.IdSucursal}', '{sd.StrNombreCompleto}', '1', '{sd.idPromotor}', '{sd.StrPromotor}', '{sd.StrFechaAlta}', '{sd.IdDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '1', 'INDIVIDUAL', '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");
                queries.Add("SET @idGrupo = (SELECT SCOPE_IDENTITY())");


                queries.Add($@"insert into arcicte 
                                (cteX001, cteX003, cteX004, cteX005, cteX006, cteX007, cteX008, cteX009, cteX010, cteX012, cteX013, cteX014, cteX015, cteX016, cteX019, cteX020, cteX021, cteX023, cteX024, cteX025, cteX026, cteX027, cteX028, cteX030, cteX031, cteX031c, cteX033, cteX034, cteX036, cteX037, cteX040, cteX046, cteX047, cteX048, cteX049, cteX041, cteX301, cteX302, cteX303, cteX304 ) values
                                ('{sd.IdSucursal}', '{sd.StrApellidoPaterno}', '{sd.StrApellidoMaterno}', '{sd.StrNombre1}', '{sd.StrNombre2}', '{sd.StrNombreCompleto}', '{sd.StrDomicilio}', '{sd.StrDomicilioNumExt}', '{sd.StrDomicilioNumInt}', '{sd.StrDomicilioColonia}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP}', '{sd.StrPais}', '{sd.StrEstadoNacimiento}', '{sd.StrNacionalidad}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail}', '{sd.StrOcupacion}', '{sd.StrActividad}', '{sd.StrNombreConyuge}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge}', '{sd.StrOcupacionConyuge}', @idGrupo, '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");
                queries.Add("SET @idCliente = (SELECT SCOPE_IDENTITY())");
                */

                queries.Add($@"SET @idCliente = NULL");
                queries.Add($@"SET @idGrupo = NULL");
            }
            else
            {
                queries.Add($@"SET @idCliente = {sd.IdCliente}");
                queries.Add($@"SET @idGrupo = (select top 1 solX006 from arciced where cedLlave = {sd.IdCliente})");
            }

            queries.Add($@"insert into arcigrm
                                (grmX001, grmX002, grmX003, grmX004, grmX005, grmX006, grmX010, grmX012, grmX018, grmX019, grmX025, grmX036, grmX301, grmX302, grmX303, grmX304, grmX020, grmX021, grmX026 ) values
                                ('{sd.IdSucursal}', @idGrupo, '{sd.StrNombreCompleto.ToUpper()}' , '1', '{sd.idPromotor}', '{sd.StrPromotor.ToUpper()}', '{sd.DblMontoAutorizado}', '{sd.IntPlazo}', '1', '1', '{sd.StrFechaAlta}', '{sd.DblMontoSolicitado}', '{sd.StrUsuarioPromotor.ToUpper()}', GETDATE(), '{sd.StrUsuarioPromotor.ToUpper()}', GETDATE(), 'INDIVIDUAL', '{sd.StrFechaAlta}', '{sd.StrFechaAlta}')");

            queries.Add($@"insert into arciced
                                (solX001, solX003, solX004, solX005, solX006, cedLlave, cedX003, cedX004, cedX005, cedX006, cedX007, cedX008, cedX009, cedX010, cedX012, cedX013, cedX014, cedX015, cedX016, cedX019, cedX020, cedX021, cedX023, cedX024, cedX025, cedX026, cedX027, cedX028, cedX030, cedX031, cedX033, cedX034, cedX036, cedX037, cedX040, cedX046, cedX047, cedX048, cedX049, cedX054c, cedX054d, cedX301, cedX302, cedX303, cedX304) values
                                ('{sd.IdSucursal}', '{sd.StrFechaAlta}', '1', 'TRÁMITE', @idGrupo, @idCliente, '{sd.StrApellidoPaterno.ToUpper()}', '{sd.StrApellidoMaterno.ToUpper()}', '{sd.StrNombre1.ToUpper()}', '{sd.StrNombre2.ToUpper()}', '{sd.StrNombreCompleto.ToUpper()}', '{sd.StrDomicilio.ToUpper()}', '{sd.StrDomicilioNumExt.ToUpper()}', '{sd.StrDomicilioNumInt.ToUpper()}', '{sd.StrDomicilioColonia.ToUpper()}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP.ToUpper()}', '{sd.StrPais.ToUpper()}', '{sd.StrEstadoNacimiento.ToUpper()}', '{sd.StrNacionalidad.ToUpper()}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail.ToLower()}', '{sd.StrOcupacion.ToUpper()}', '{sd.StrActividad}', '{sd.StrNombreConyuge.ToUpper()}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge.ToUpper()}', '{sd.StrOcupacionConyuge.ToUpper()}', '{sd.DblIngresos}', '{sd.DblEgresos}', '{sd.StrUsuarioPromotor.ToUpper()}', GETDATE(), '{sd.StrUsuarioPromotor.ToUpper()}', GETDATE())");

            queries.Add("SELECT SCOPE_IDENTITY() as idSolicitud");


            String queriesString = "";

            foreach (String query in queries)
            {
                queriesString += query + "\n";
            }

            return new QuerySolicitud(queriesString, ""); 
        }
    }
}
