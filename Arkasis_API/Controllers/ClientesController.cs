using Arkasis_API.Attributes;
using Arkasis_API.Conexiones;
using Arkasis_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
namespace Arkasis_API.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        [HttpPost]
        public IActionResult BuscarCliente(Cliente cliente)
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
				$@"SELECT top 100
					cteLlave as IdCliente,
					cteX028 as StrGenero,
					cteX023 as StrCurp,
					cteX003 as StrApellidoPaterno,
					cteX004 as StrApellidoMaterno,
					cteX005 as StrNombre1,
					cteX006 as StrNombre2,
					replace(convert(varchar, cteX030, 111), '/','-') as DatFechaNacimiento,
					cteX031 AS IdEdoCivil,
					cteX031c as StrEdoCivil,
					cteX020 as StrTelefono,
					cteX021 as StrCelular,
					cteX019 as StrCodigoPostal,
					cteX008 as StrDireccion,
					cteX009 as StrDireccionNumero,
					cteX010 as StrDireccionNumeroInterno,
					cteX012 as StrColonia,
					cteX013 as IdEstado,
					cteX014 as StrEstado,
					cteX015 as IdMunicipio,
					cteX016 as StrMunicipio,
					cteX041 as StrClaveGrupo,
					cteX131 as IdActividad,
					cteX132 as StrDescripcionActividad,
					cteX033 as StrNumeroElector,
					cteX034 as StrClaveElector,
					cteX024 as StrPaisNacimiento,
					cteX025 as StrEstadoNacimiento,
					cteX026 as StrNacionalidad,
					cteX036 as StrEmail,
					cteX046 as StrNombreConyuge,
					replace(convert(varchar, cteX047, 111), '/','-') as DatFechaNacimientoConyuge,
					cteX048 as StrLugarNacimientoConyuge,
					cteX049 as StrOcupacionConyuge,
					cteX037 as StrOcupacion
				FROM arcicte
				WHERE 
				concat(cteX016, ' ', cteX014) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%{cliente.StrMunicipio.Replace("'", "%").Replace(" ", "%")}%{cliente.StrEstado.Replace("'", "%").Replace(" ", "%")}%'
				AND CONCAT(cteX023, ' ', cteX034, ' ' ,cteX005, ' ', cteX006, ' ', cteX003, ' ', cteX003) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%{cliente.StrNombre1.Replace("'", "%").Replace(" ", "%")}%'
				order by CONCAT(cteX005, ' ', cteX006, ' ', cteX003, ' ', cteX003)";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Cliente> listaClientes = new List<Cliente>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
						listaClientes.Add(new Cliente(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaClientes.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron clientes", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }


		[HttpPost("curp")]
		public IActionResult BuscarClienteByCurp(Cliente cliente)
		{
			ConexionSQL conexionSQL = new ConexionSQL();
			String[] arrayConsultas = new string[1];
			arrayConsultas[0] =
				$@"SELECT top 50
					cteLlave as IdCliente,
					cteX028 as StrGenero,
					cteX023 as StrCurp,
					cteX003 as StrApellidoPaterno,
					cteX004 as StrApellidoMaterno,
					cteX005 as StrNombre1,
					cteX006 as StrNombre2,
					replace(convert(varchar, cteX030, 111), '/','-') as DatFechaNacimiento,
					cteX031 AS IdEdoCivil,
					cteX031c as StrEdoCivil,
					cteX020 as StrTelefono,
					cteX021 as StrCelular,
					cteX019 as StrCodigoPostal,
					cteX008 as StrDireccion,
					cteX009 as StrDireccionNumero,
					cteX010 as StrDireccionNumeroInterno,
					cteX012 as StrColonia,
					cteX013 as IdEstado,
					cteX014 as StrEstado,
					cteX015 as IdMunicipio,
					cteX016 as StrMunicipio,
					cteX041 as StrClaveGrupo,
					cteX131 as IdActividad,
					cteX132 as StrDescripcionActividad,
					cteX033 as StrNumeroElector,
					cteX034 as StrClaveElector,
					cteX024 as StrPaisNacimiento,
					cteX025 as StrEstadoNacimiento,
					cteX026 as StrNacionalidad,
					cteX036 as StrEmail,
					cteX046 as StrNombreConyuge,
					replace(convert(varchar, cteX047, 111), '/','-') as DatFechaNacimientoConyuge,
					cteX048 as StrLugarNacimientoConyuge,
					cteX049 as StrOcupacionConyuge,
					cteX037 as StrOcupacion
				FROM arcicte
				WHERE 
				cteX023 = '{cliente.StrCurp}'";

			DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

			if (arrayResult != null)
			{
				if (arrayResult[0].Rows.Count > 0)
				{
					List<Cliente> listaClientes = new List<Cliente>();

					foreach (DataRow row in arrayResult[0].Rows)
					{
						listaClientes.Add(new Cliente(row));
					}

					return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaClientes.ToArray() });
				}
				else
				{
					return Ok(new { Mensaje = "No se encontraron clientes", Success = false });
				}
			}
			else
			{
				return Ok(new { Mensaje = "No se encontraron registros", Success = false });
			}
		}

		[HttpPost("saldos")]
		public IActionResult ObtenerSaldos(Cliente cliente)
        {
			ConexionSQL conexionSQL = new ConexionSQL();
			String[] arrayConsultas = new string[1];
			arrayConsultas[0] =
				$@"SELECT dbo.arcicte.cteX023 AS strCurp, dbo.arciaux.auxX041 AS strFolioContrato, REPLACE(CONVERT(varchar, dbo.arciaux.auxX004, 111), '/', '-') AS datFechaMinistracion, REPLACE(CONVERT(varchar, dbo.arciaux.auxX005, 111), '/', '-') 
                  AS datFechaVencimiento, dbo.arciaux.auxX017 AS intTotalPagos, dbo.arciaux.auxX008 AS dblCapital, dbo.arciaux.auxX009 AS dblIntereses, dbo.arciaux.auxX009h AS dblSeguro, dbo.arciaux.auxX010 AS dblTotal, 
                  dbo.arciaux.auxX012 AS dblAbono, dbo.arciaux.auxX013 AS dblSaldo, dbo.arciaux.auxX033c AS strProducto
FROM     dbo.arcicte INNER JOIN
                  dbo.arciaux ON dbo.arcicte.cteLlave = dbo.arciaux.auxX002 
				WHERE (arcicte.cteX023 = '{cliente.StrCurp}')";

			DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

			if (arrayResult != null)
			{
				if (arrayResult[0].Rows.Count > 0)
				{
					List<SaldoCliente> listaSaldosCliente = new List<SaldoCliente>();

					foreach (DataRow row in arrayResult[0].Rows)
					{
						listaSaldosCliente.Add(new SaldoCliente(row));
					}

					return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaSaldosCliente.ToArray() });
				}
				else
				{
					return Ok(new { Mensaje = "No se encontraron saldos de cliente", Success = false });
				}
			}
			else
			{
				return Ok(new { Mensaje = "No se encontraron registros", Success = false });
			}
		}

		[HttpPost("solicitudes")]
		public IActionResult ObtenerSolicutudes(Cliente cliente)
        {
			ConexionSQL conexionSQL = new ConexionSQL();
			String[] arrayConsultas = new string[1];
			arrayConsultas[0] =
				$@"SELECT top 100
					solLlave IdSolicitud,
					grmLlave IdGrupoSol,
					replace(convert(varchar, solX003, 111), '/','-') StrFechaAlta,
					solX005 StrStatusSolicitud,
					solX001 IdSucursal,
					cedX301 StrUsuario,
					grmX005 idPromotor,
					grmX006 StrPromotor,
					grmX005c IdCordinador,
					grmX006c StrCordinador,
					cedLlave IdCliente,
					cedX003 StrApellidoPaterno,
					cedX004 StrApellidoMaterno,
					cedX005 StrNombre1,
					cedX006 StrNombre2,
					replace(convert(varchar, cedX030, 111), '/','-') StrFechaNacimiento,
					cedX027 IdGenero,
					cedX028 StrGenero,
					cedX023 StrCURP,
					cedX008 StrDomicilio,
					cedX019 StrDomicilioCodigoPostal,
					cedX009 StrDomicilioNumExt,
					cedX010 StrDomicilioNumInt,
					cedX012 StrDomicilioColonia,
					cedX013 IdDomicilioEstado,
					cedX014 StrDomicilioEstado,
					cedX015 IdDomicilioMunicipio,
					cedX016 StrDomicilioMunicipio,
					'' StrEstadoCivil,
					cedX031 IdEstadoCivil,
					cedX020 StrTelefono,
					cedX021 StrCelular,
					cedX037 StrOcupacion,
					'' IdActividad,
					cedX132 StrActividad,
					cedX033 StrNumeroINE,
					cedX034 StrClaveINE,
					cedX024 StrPais,
					cedX025 StrEstadoNacimiento,
					cedX026 StrNacionalidad,
					cedX036 StrEmail,
					cedX046 StrNombreConyuge,
					cedX048 StrLugarNacimientoConyuge,
					replace(convert(varchar, cedX047, 111), '/','-') StrFechaNacimientoConyuge,
					cedX049 StrOcupacionConyuge,
					grmX076 StrReferenciaBancaria,
					grmX077 StrBanco,
					case when cedX120 is null then COALESCE(cedX197,'') else '' END StrProducto,
					COALESCE(cedX191,'0') DblPlazo,
					COALESCE(grmX078, '0') IntQuedateCasa,
					case when cedX120 is not null then COALESCE(cedX189,0) else 0 end as DblMontoSolicitadoMejoraVivienda,
					case when cedX120 is null then COALESCE(cedX189,0) else 0 end as DblMontoSolicitadoEquipandoHogar,
					COALESCE(cedX190,0) DblMontoAutorizado,
					COALESCE(cedX054c,0) DblIngresos,
					COALESCE(cedX054d,0) DblEgresos,
					cedX131 StrCNBV,
					cedX120 StrDomicilio_mejoraVivienda,
					cedX125 StrCodigoPostal_mejoraVivienda,
					cedX121 StrNumExt_mejoraVivienda,
					cedX122 StrNumInt_mejoraVivienda,
					cedX123 StrColonia_mejoraVivienda,
					grmX015 IdTipoVencimiento,
					grmX016 StrTipoVencimiento,
					grmX011 IntNumPagos,
					'' IdEstado_mejoraVivienda,
					'' StrEstado_mejoraVivienda,
					'' IdMunicipio_mejoraVivienda,
					cedX124 StrMunicipio_mejoraVivienda,
					'' StrFotoINEFrontal_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'INE FRONTAL' ) StrFotoINEFrontal_nombre,
					'' StrFotoINEReverso_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'INE REVERSO' ) StrFotoINEReverso_nombre,
					'' StrFotoPerfil_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'FOTO PERFIL' ) StrFotoPerfil_nombre,
					'' StrFotoComprobanteDomicilio_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'COMPROBANTE DOMICILIO' ) StrFotoComprobanteDomicilio_nombre,
					gru.grmX015 IdTipoVencimiento,
					gru.grmX016 StrTipoVencimiento,
					gru.grmX011 IntNumPagos,
					gru.grmX012 DblPlazo,
					gru.grmX022 IdTipoContratoIndividual
				FROM arciced as sol
				JOIN arcigrm as gru on sol.solX006 = gru.grmX002
				WHERE 
					concat(cedX016, ' ', cedX014) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%{cliente.StrMunicipio.Replace("'", "%").Replace(" ", "%")}%{cliente.StrEstado.Replace("'", "%").Replace(" ", "%")}%'
					AND CONCAT(cedX003, ' ', cedX004, ' ' ,cedX005, ' ', cedX006, ' ', cedX023, ' ', cedX034) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%{cliente.StrNombre1.Replace("'", "%").Replace(" ", "%")}%'
				;";

			DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

			if (arrayResult != null)
			{
				if (arrayResult[0].Rows.Count > 0)
				{
					List<SolicitudDispersion> lista = new List<SolicitudDispersion>();

					foreach (DataRow row in arrayResult[0].Rows)
					{
						lista.Add(new SolicitudDispersion(row));
					}

					return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = lista.ToArray() });
				}
				else
				{
					return Ok(new { Mensaje = "No se encontraron solicitudes", Success = false });
				}
			}
			else
			{
				return Ok(new { Mensaje = "No se encontraron registros", Success = false });
			}
		}


		[HttpPost("ultimas-solicitudes")]
		public IActionResult ObtenerUltimasSolicutudes(Usuario usuario)
		{
			ConexionSQL conexionSQL = new ConexionSQL();
			String[] arrayConsultas = new string[1];
			arrayConsultas[0] =
				$@"SELECT top 10
					solLlave IdSolicitud,
					grmLlave IdGrupoSol,
					replace(convert(varchar, solX003, 111), '/','-') StrFechaAlta,
					solX005 StrStatusSolicitud,
					solX001 IdSucursal,
					cedX301 StrUsuario,
					grmX005 idPromotor,
					grmX006 StrPromotor,
					grmX005c IdCordinador,
					grmX006c StrCordinador,
					cedLlave IdCliente,
					cedX003 StrApellidoPaterno,
					cedX004 StrApellidoMaterno,
					cedX005 StrNombre1,
					cedX006 StrNombre2,
					replace(convert(varchar, cedX030, 111), '/','-') StrFechaNacimiento,
					cedX027 IdGenero,
					cedX028 StrGenero,
					cedX023 StrCURP,
					cedX008 StrDomicilio,
					cedX019 StrDomicilioCodigoPostal,
					cedX009 StrDomicilioNumExt,
					cedX010 StrDomicilioNumInt,
					cedX012 StrDomicilioColonia,
					cedX013 IdDomicilioEstado,
					cedX014 StrDomicilioEstado,
					cedX015 IdDomicilioMunicipio,
					cedX016 StrDomicilioMunicipio,
					'' StrEstadoCivil,
					cedX031 IdEstadoCivil,
					cedX020 StrTelefono,
					cedX021 StrCelular,
					cedX037 StrOcupacion,
					'' IdActividad,
					cedX132 StrActividad,
					cedX033 StrNumeroINE,
					cedX034 StrClaveINE,
					cedX024 StrPais,
					cedX025 StrEstadoNacimiento,
					cedX026 StrNacionalidad,
					cedX036 StrEmail,
					cedX046 StrNombreConyuge,
					cedX048 StrLugarNacimientoConyuge,
					replace(convert(varchar, cedX047, 111), '/','-') StrFechaNacimientoConyuge,
					cedX049 StrOcupacionConyuge,
					grmX076 StrReferenciaBancaria,
					grmX077 StrBanco,
					case when cedX120 is null then COALESCE(cedX197,'') else '' END StrProducto,
					COALESCE(cedX191,'0') DblPlazo,
					COALESCE(grmX078, '0') IntQuedateCasa,
					case when cedX120 is not null then COALESCE(cedX189,0) else 0 end as DblMontoSolicitadoMejoraVivienda,
					case when cedX120 is null then COALESCE(cedX189,0) else 0 end as DblMontoSolicitadoEquipandoHogar,
					COALESCE(cedX190,0) DblMontoAutorizado,
					COALESCE(cedX054c,0) DblIngresos,
					COALESCE(cedX054d,0) DblEgresos,
					cedX131 StrCNBV,
					cedX120 StrDomicilio_mejoraVivienda,
					cedX125 StrCodigoPostal_mejoraVivienda,
					cedX121 StrNumExt_mejoraVivienda,
					cedX122 StrNumInt_mejoraVivienda,
					cedX123 StrColonia_mejoraVivienda,
					grmX015 IdTipoVencimiento,
					grmX016 StrTipoVencimiento,
					grmX011 IntNumPagos,
					'' IdEstado_mejoraVivienda,
					'' StrEstado_mejoraVivienda,
					'' IdMunicipio_mejoraVivienda,
					cedX124 StrMunicipio_mejoraVivienda,
					'' StrFotoINEFrontal_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'INE FRONTAL' ) StrFotoINEFrontal_nombre,
					'' StrFotoINEReverso_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'INE REVERSO' ) StrFotoINEReverso_nombre,
					'' StrFotoPerfil_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'FOTO PERFIL' ) StrFotoPerfil_nombre,
					'' StrFotoComprobanteDomicilio_B64,
					(SELECT top 1 dgsX003 FROM ARCICTEdg WHERE dgsX001c = sol.cedLlave AND dgsX004 = 'COMPROBANTE DOMICILIO' ) StrFotoComprobanteDomicilio_nombre,
					gru.grmX022 IdTipoContratoIndividual
				FROM arciced as sol
				JOIN arcigrm as gru on sol.solX006 = gru.grmX002
				WHERE 
					cedX301 = '{usuario.User}'
				ORDER BY solX003 DESC
				;";

			DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

			if (arrayResult != null)
			{
				if (arrayResult[0].Rows.Count > 0)
				{
					List<SolicitudDispersion> lista = new List<SolicitudDispersion>();

					foreach (DataRow row in arrayResult[0].Rows)
					{
						lista.Add(new SolicitudDispersion(row));
					}

					return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = lista.ToArray() });
				}
				else
				{
					return Ok(new { Mensaje = "No se encontraron solicitudes", Success = false });
				}
			}
			else
			{
				return Ok(new { Mensaje = "No se encontraron registros", Success = false });
			}
		}
	}
}
