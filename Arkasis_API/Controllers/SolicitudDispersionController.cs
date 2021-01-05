﻿using Arkasis_API.Attributes;
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
        [HttpPost("nueva")]
        public IActionResult GuardarCliente(SolicitudDispersion sd)
        {
            List<String> queries = new List<string>();


            //Falta validar si el cliente ya está registrado
            //Falta validar si el usuario ya tiene alguna solicitud en tramite



            queries.Add("DECLARE @idGrupo NVARCHAR(50)");
            queries.Add("DECLARE @idCliente NVARCHAR(50)");
            queries.Add("DECLARE @idSolicitudGrupo Integer");

            if(sd.IdCliente == "")
            {
                queries.Add($@"insert into arcigru 
                                (gruX001, gruX003, gruX004, gruX005, gruX005c, gruX007, gruX014, gruX015, gruX016, gruX016c, gruX301, gruX302, gruX303, gruX304) values
                                ('{sd.IdSucursal}', '{sd.StrNombreCompleto}', '1', '{sd.idPromotor}', '{sd.StrPromotor}', '{sd.StrFechaAlta}', '{sd.IdDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '1', 'INDIVIDUAL', '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");
                queries.Add("SET @idGrupo = (SELECT SCOPE_IDENTITY())");


                queries.Add($@"insert into arcicte 
                                (cteX001, cteX003, cteX004, cteX005, cteX006, cteX007, cteX008, cteX009, cteX010, cteX012, cteX013, cteX014, cteX015, cteX016, cteX019, cteX020, cteX021, cteX023, cteX024, cteX025, cteX026, cteX027, cteX028, cteX030, cteX031, cteX031c, cteX033, cteX034, cteX036, cteX037, cteX040, cteX046, cteX047, cteX048, cteX049, cteX041, cteX301, cteX302, cteX303, cteX304 ) values
                                ('{sd.IdSucursal}', '{sd.StrApellidoPaterno}', '{sd.StrApellidoMaterno}', '{sd.StrNombre1}', '{sd.StrNombre2}', '{sd.StrNombreCompleto}', '{sd.StrDomicilio}', '{sd.StrDomicilioNumExt}', '{sd.StrDomicilioNumInt}', '{sd.StrDomicilioColonia}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP}', '{sd.StrPais}', '{sd.StrEstadoNacimiento}', '{sd.StrNacionalidad}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail}', '{sd.StrOcupacion}', '{sd.StrActividad}', '{sd.StrNombreConyuge}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge}', '{sd.StrOcupacionConyuge}', @idGrupo, '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");
                queries.Add("SET @idCliente = (SELECT SCOPE_IDENTITY())");
            }
            else
            {
                queries.Add($@"SET @idCliente = {sd.IdCliente}");
                queries.Add($@"SET @idGrupo = (select top 1 solX006 from arciced where cedLlave = {sd.IdCliente})");
            }

            queries.Add($@"insert into arcigrm
                                (grmX001, grmX002, grmX003, grmX004, grmX005, grmX006, grmX010, grmX012, grmX018, grmX019, grmX025, grmX036, grmX301, grmX302, grmX303, grmX304 ) values
                                ('{sd.IdSucursal}', @idGrupo, '{sd.StrNombreCompleto}' , '1', '{sd.idPromotor}', '{sd.StrPromotor}', '{sd.DblMontoAutorizado}', '{sd.IntPlazo}', '1', '1', '{sd.StrFechaAlta}', '{sd.DblMontoSolicitado}', '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");

            queries.Add($@"insert into arciced
                                (solX001, solX003, solX004, solX005, solX006, cedLlave, cedX003, cedX004, cedX005, cedX006, cedX007, cedX008, cedX009, cedX010, cedX012, cedX013, cedX014, cedX015, cedX016, cedX019, cedX020, cedX021, cedX023, cedX024, cedX025, cedX026, cedX027, cedX028, cedX030, cedX031, cedX033, cedX034, cedX036, cedX037, cedX040, cedX046, cedX047, cedX048, cedX049, cedX054c, cedX054d, cedX301, cedX302, cedX303, cedX304) values
                                ('{sd.IdSucursal}', '{sd.StrFechaAlta}', '1', 'TRÁMITE', @idGrupo, @idCliente, '{sd.StrApellidoPaterno}', '{sd.StrApellidoMaterno}', '{sd.StrNombre1}', '{sd.StrNombre2}', '{sd.StrNombreCompleto}', '{sd.StrDomicilio}', '{sd.StrDomicilioNumExt}', '{sd.StrDomicilioNumInt}', '{sd.StrDomicilioColonia}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP}', '{sd.StrPais}', '{sd.StrEstadoNacimiento}', '{sd.StrNacionalidad}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail}', '{sd.StrOcupacion}', '{sd.StrActividad}', '{sd.StrNombreConyuge}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge}', '{sd.StrOcupacionConyuge}', '{sd.DblIngresos}', '{sd.DblEgresos}', '{sd.StrUsuarioPromotor}', GETDATE(), '{sd.StrUsuarioPromotor}', GETDATE())");

            queries.Add("SELECT SCOPE_IDENTITY() as idSolicitud");


            String queriesString = "";

            foreach(String query in queries)
            {
                queriesString += query + "\n";
            }

            String[] arrayQuery = new String[1];
            arrayQuery[0] = queriesString;

            ConexionSQL conexionSQL = new ConexionSQL();
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayQuery);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    return Ok(new { Mensaje = "Guardado correctamente", Success = false });
                }
                else
                {
                    return Ok(new { Mensaje = "No se ha guardado ", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se pudo guardaar ", Success = false });
            }
        }
    }
}
