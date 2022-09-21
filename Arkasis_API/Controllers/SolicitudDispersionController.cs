using Arkasis_API.Attributes;
using Arkasis_API.Conexiones;
using Arkasis_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("api/dispersion")]
    public class SolicitudDispersionController : Controller
    {
        IWebHostEnvironment _env;
        public SolicitudDispersionController(IWebHostEnvironment env)
        {
            _env = env;
        }
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
        public IActionResult GuardarSolicitud(SolicitudDispersion sd)
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
                    sd.IdCliente = arrayResult[0].Rows[0]["IdCliente"].ToString();
                    //Save the Byte Array as Image File.
                    saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoINEFrontal_B64, sd.StrFotoINEFrontal_nombre);
                    saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoINEReverso_B64, sd.StrFotoINEReverso_nombre);
                    saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoPerfil_B64, sd.StrFotoPerfil_nombre);
                    saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoComprobanteDomicilio_B64, sd.StrFotoComprobanteDomicilio_nombre);

                    return Ok(new { Mensaje = "Guardado correctamente", Success = true, Resultado = "1"});
                }
                else
                {
                    return Ok(new { Mensaje = "No se ha guardado", Success = false});
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se pudo guardar", Success = false });
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
                            sd.IdCliente = arrayResult[0].Rows[0]["IdCliente"].ToString();
                            //Save the Byte Array as Image File.
                            saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoINEFrontal_B64, sd.StrFotoINEFrontal_nombre);
                            saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoINEReverso_B64, sd.StrFotoINEReverso_nombre);
                            saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoPerfil_B64, sd.StrFotoPerfil_nombre);
                            saveImage(sd.IdSucursal, sd.IdCliente, sd.StrFotoComprobanteDomicilio_B64, sd.StrFotoComprobanteDomicilio_nombre);

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

        [HttpPost("resumen-usuario")]
        public IActionResult ObtenerResumenSolicitudesUsuario(Usuario usuario)
        {
            List<String> queries = new List<String>() { 
                $@"select 
	                cedX301 as strUsuario, 
	                sum(case when solX004 = 1 then 1 else 0 end) as IntTotalTramite,
	                sum(case when solX004 = 2 then 1 else 0 end) as IntTotalAutorizado,
	                sum(case when solX004 = 3 then 1 else 0 end) as IntTotalRechazado,
	                sum(case when solX004 = 4 then 1 else 0 end) as IntTotalCancelado,
	                sum(case when solX004 = 5 then 1 else 0 end) as IntTotalMinistrado,
	                count(*) IntTotalRegistros
                from arciced
                where solX003 > DATEADD(DD, -90, GETDATE()) AND cedX301 = '{usuario.User}'
                group by cedX301;" };

            ConexionSQL conexionSQL = new ConexionSQL();
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(queries.ToArray());

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    return Ok(new { Mensaje = "Ok", Success = true, Resultado = new ResumenSolicitudes(arrayResult[0].Rows[0]) });
                }
                else
                {
                    return Ok(new { Mensaje = "No se ha guardado ", Success = false});
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se pudo guardar ", Success = false});
            }
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
                AND dbo.arciaux.auxX013 > 0");
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
            queries.Add($@"select solX004 from arciced where cedX023 = '{sd.StrCURP}' AND (solX004 = '1' OR solX004 = '2' OR solX004 = '3')");
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
            queries.Add("DECLARE @idClienteDOCS NVARCHAR(50)");
            queries.Add("DECLARE @idLastDoc NVARCHAR(50)");
            queries.Add("DECLARE @idDoc NVARCHAR(50)");
            queries.Add("DECLARE @Identificador NVARCHAR(50)");

            queries.Add("DECLARE @idSolicitudGrupo Integer");
            queries.Add("DECLARE @idCiclo Integer");
            queries.Add("DECLARE @idPagos Integer");
            if (sd.IdCliente == "")
            {

                queries.Add($@"SET @Identificador = (select CONCAT('5', RIGHT('00000' + CAST( (FLOOR(RAND()*(9999-1)+1)) AS VARCHAR), 6)) as consecutivo);");


                queries.Add($@"SET @idCliente =  @Identificador");
                queries.Add($@"SET @idClienteDOCS =  @Identificador");
                queries.Add($@"SET @idGrupo =  @Identificador");
                queries.Add($@"SET @idCiclo = '1' ");
                queries.Add($@"SET @idPagos = {sd.IntPlazo} * 2 ");
            }
            else
            {
                queries.Add($@"SET @idCliente = {sd.IdCliente}");
                queries.Add($@"SET @idClienteDOCS = (select RIGHT('000000' + CAST('{sd.IdCliente}' AS VARCHAR), 7))");
                queries.Add($@"SET @idGrupo = (select top 1 cteX041 from arcicte where cteLlave = {sd.IdCliente})");
                queries.Add($@"SET @idCiclo = (select top 1 gruX009 from arcigru where gruLlave = @idGrupo)");
                queries.Add($@"SET @idPagos = {sd.IntPlazo} * 2 ");
            }

            //Generamos el id de cliente con un pad zero 7
            queries.Add("SET @idClienteDOCS = (select RIGHT('000000' + CAST(@idCliente AS VARCHAR), 7))");
            //Obtenemos el ultimo documento registrado
            queries.Add("SET @idLastDoc = (SELECT top 1 SUBSTRING(dgsLlave, 8, 3) from ARCICTEdg WHERE dgsX001c = @idCliente order by dgsX302 desc)");

            queries.Add(getQueryInsertDocument(1, sd, $@"Doc_Digitalizacion/{sd.IdSucursal}/{sd.StrFotoINEFrontal_nombre}", "INE FRONTAL"));
            queries.Add(getQueryInsertDocument(2, sd, $@"Doc_Digitalizacion/{sd.IdSucursal}/{sd.StrFotoINEReverso_nombre}", "INE REVERSO"));
            queries.Add(getQueryInsertDocument(3, sd, $@"Doc_Digitalizacion/{sd.IdSucursal}/{sd.StrFotoPerfil_nombre}", "FOTO PERFIL"));
            queries.Add(getQueryInsertDocument(4, sd, $@"Doc_Digitalizacion/{sd.IdSucursal}/{sd.StrFotoComprobanteDomicilio_nombre}", "COMPROBANTE DOMICILIO"));


            Double montoSolicitado = sd.DblMontoSolicitadoMejoraVivienda + sd.DblMontoSolicitadoEquipandoHogar;

            int tipoSolicitud = (sd.DblMontoSolicitadoMejoraVivienda > 0 && sd.DblMontoSolicitadoEquipandoHogar == 0) 
                                    ? 3
                                    : ((sd.DblMontoSolicitadoMejoraVivienda == 0 && sd.DblMontoSolicitadoEquipandoHogar > 0)
                                        ? 4
                                        : 5);

            queries.Add($@"insert into arcigrm
                                (grmX001, grmX002, grmX003, grmX004, grmX005, grmX006, grmX010, grmX012, grmX018, grmX019, grmX025, grmX036, grmX301, grmX302, grmX303, grmX304, grmX020, grmX021, grmX026, grmX022,grmX007,grmX011,grmX075, grmX005c, grmX006c,grmX076,grmX077,grmX078  ) values
                                ('{sd.IdSucursal}', @idGrupo, '{sd.StrNombreCompleto.ToUpper()}' , '1', '{sd.idPromotor}', '{sd.StrPromotor.ToUpper()}', '{montoSolicitado}', '{sd.IntPlazo}', '1', '1', '{sd.StrFechaAlta}', '{montoSolicitado}', '{sd.StrUsuario.ToUpper()}', GETDATE(), '{sd.StrUsuario.ToUpper()}', GETDATE(), 'INDIVIDUAL', '{sd.StrFechaAlta}', '{sd.StrFechaAlta}', '{tipoSolicitud}',@idCiclo,@idPagos,'2', '{sd.IdCordinador}', '{sd.StrCordinador}','{sd.StrReferenciaBancaria}','{sd.StrBanco}','{sd.IntQuedateCasa}')");


            if(sd.DblMontoSolicitadoMejoraVivienda > 0)
            {
                sd.StrProducto = "Mejora tu vivienda";
                queries.Add($@"insert into arciced
                                    (solX001, solX003, solX004, solX005, solX006, cedLlave, cedX003, cedX004, cedX005, cedX006, cedX007, cedX008, cedX009, cedX010, cedX012, cedX013, cedX014, cedX015, cedX016, cedX019, cedX020, cedX021, cedX023, cedX024, cedX025, cedX026, cedX027, cedX028, cedX030, cedX031, cedX033, cedX034, cedX036, cedX037, cedX040, cedX046, cedX047, cedX048, cedX049, cedX054c, cedX054d, cedX301, cedX302, cedX303, cedX304,cedX189,cedX190, cedX197, cedX131,cedX029,cedX011,cedX191,cedX194, cedX120, cedX121, cedX122, cedX123, cedX124, cedX125, cedX132) values
                                    ('{sd.IdSucursal}', '{sd.StrFechaAlta}', '1', 'EN TRAMITE', @idGrupo, @idCliente, '{sd.StrApellidoPaterno.ToUpper()}', '{sd.StrApellidoMaterno.ToUpper()}', '{sd.StrNombre1.ToUpper()}', '{sd.StrNombre2.ToUpper()}', '{sd.StrNombreCompleto.ToUpper()}', '{sd.StrDomicilio.ToUpper()}', '{sd.StrDomicilioNumExt.ToUpper()}', '{sd.StrDomicilioNumInt.ToUpper()}', '{sd.StrDomicilioColonia.ToUpper()}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP.ToUpper()}', '{sd.StrPais.ToUpper()}', '{sd.StrEstadoNacimiento.ToUpper()}', '{sd.StrNacionalidad.ToUpper()}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail.ToLower()}', '{sd.StrOcupacion.ToUpper()}', '{sd.StrActividad}', '{sd.StrNombreConyuge.ToUpper()}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge.ToUpper()}', '{sd.StrOcupacionConyuge.ToUpper()}', '{sd.DblIngresos}', '{sd.DblEgresos}', '{sd.StrUsuario.ToUpper()}', GETDATE(), '{sd.StrUsuario.ToUpper()}', GETDATE(), '{sd.DblMontoSolicitadoMejoraVivienda}','{sd.DblMontoSolicitadoMejoraVivienda}', '{sd.StrProducto}', '{sd.StrCNBV}','1','1','{sd.IntPlazo}', @idPagos, '{sd.StrDomicilio_mejoraVivienda}', '{sd.StrNumExt_mejoraVivienda}', '{sd.StrNumInt_mejoraVivienda}', '{sd.StrColonia_mejoraVivienda}', '{sd.StrMunicipio_mejoraVivienda}', '{sd.StrCodigoPostal_mejoraVivienda}', '{sd.StrActividad}')");
            }

            if (sd.DblMontoSolicitadoEquipandoHogar > 0)
            {
                //sd.StrProducto = "MEJORA HOGAR";
                queries.Add($@"insert into arciced
                                    (solX001, solX003, solX004, solX005, solX006, cedLlave, cedX003, cedX004, cedX005, cedX006, cedX007, cedX008, cedX009, cedX010, cedX012, cedX013, cedX014, cedX015, cedX016, cedX019, cedX020, cedX021, cedX023, cedX024, cedX025, cedX026, cedX027, cedX028, cedX030, cedX031, cedX033, cedX034, cedX036, cedX037, cedX040, cedX046, cedX047, cedX048, cedX049, cedX054c, cedX054d, cedX301, cedX302, cedX303, cedX304,cedX189,cedX190, cedX197, cedX131,cedX029,cedX011,cedX191,cedX194, cedX132) values
                                    ('{sd.IdSucursal}', '{sd.StrFechaAlta}', '1', 'EN TRAMITE', @idGrupo, @idCliente, '{sd.StrApellidoPaterno.ToUpper()}', '{sd.StrApellidoMaterno.ToUpper()}', '{sd.StrNombre1.ToUpper()}', '{sd.StrNombre2.ToUpper()}', '{sd.StrNombreCompleto.ToUpper()}', '{sd.StrDomicilio.ToUpper()}', '{sd.StrDomicilioNumExt.ToUpper()}', '{sd.StrDomicilioNumInt.ToUpper()}', '{sd.StrDomicilioColonia.ToUpper()}', '{sd.IdDomicilioEstado}', '{sd.StrDomicilioEstado}', '{sd.IdDomicilioMunicipio}', '{sd.StrDomicilioMunicipio}', '{sd.StrDomicilioCodigoPostal}', '{sd.StrTelefono}', '{sd.StrCelular}', '{sd.StrCURP.ToUpper()}', '{sd.StrPais.ToUpper()}', '{sd.StrEstadoNacimiento.ToUpper()}', '{sd.StrNacionalidad.ToUpper()}', '{sd.IdGenero}', '{sd.StrGenero}', '{sd.StrFechaNacimiento}', '{sd.IdEstadoCivil}', '{sd.StrNumeroINE}', '{sd.StrClaveINE}', '{sd.StrEmail.ToLower()}', '{sd.StrOcupacion.ToUpper()}', '{sd.StrActividad}', '{sd.StrNombreConyuge.ToUpper()}', '{sd.StrFechaNacimientoConyuge}', '{sd.StrLugarNacimientoConyuge.ToUpper()}', '{sd.StrOcupacionConyuge.ToUpper()}', '{sd.DblIngresos}', '{sd.DblEgresos}', '{sd.StrUsuario.ToUpper()}', GETDATE(), '{sd.StrUsuario.ToUpper()}', GETDATE(), '{sd.DblMontoSolicitadoEquipandoHogar}','{sd.DblMontoSolicitadoEquipandoHogar}', '{sd.StrProducto}', '{sd.StrCNBV}','1','1','{sd.IntPlazo}', @idPagos, '{sd.StrActividad}')");
            }



            // queries.Add("SELECT SCOPE_IDENTITY() as idSolicitud");
            queries.Add("SELECT @idCliente AS IdCliente");


            String queriesString = "";

            foreach (String query in queries)
            {
                queriesString += query + "\n";
            }

            return new QuerySolicitud(queriesString, ""); 
        }


        private String getQueryInsertDocument(int index, SolicitudDispersion sd, String fileName, String fileDescription)
        {
            List<String> queries = new List<string>();
            String queriesString = "";

            //Generamos el nuevo id del documento
            queries.Add($@"SET @idDoc = (SELECT CONCAT(@idClienteDOCS, IIF(@idLastDoc is null, '00{index}', RIGHT('000' + CAST( (CAST(@idLastDoc as int) + {index})  AS VARCHAR), 3)) ))");

            queries.Add($@"INSERT INTO ARCICTEdg 
                    (dgsX001, dgsLlave, dgsX001c, dgsX003, dgsX004, dgsX006, dgsX007, dgsX009, dgsX301, dgsX302) VALUES
                    ('{sd.IdSucursal}', @idDoc, @idCliente, '{fileName}', '{fileDescription}', 'Evidencia subida desde app', GETDATE(), 0, '{sd.StrUsuario}', GETDATE())");
            
            foreach (String query in queries)
            {
                queriesString += query + "\n";
            }

            return queriesString;
        }

        private String saveImage(String idSucursal, String idCliente, String base64String, String fileName)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);

            string directoryPath = Path.Combine("/ArkasisMicrocred_Pruebas", "Doc_Digitalizacion", idSucursal, ("CTE"+idCliente));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = directoryPath + "/" + fileName;

            System.IO.File.WriteAllBytes(filePath, imageBytes);

            return fileName;
        }


    }
}
