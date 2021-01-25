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
					cteX030 as DatFechaNacimiento,
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
					cteX047 as DatFechaNacimientoConyuge,
					cteX048 as StrLugarNacimientoConyuge,
					cteX049 as StrOcupacionConyuge,
					cteX037 as StrOcupacion
				FROM arcicte
				WHERE 
				concat(cteX016, ' ', cteX014)like '%{cliente.StrMunicipio.Replace("'", "%").Replace(" ", "%")}%{cliente.StrEstado.Replace("'", "%").Replace(" ", "%")}%'
				AND CONCAT(cteX023, ' ', cteX034, ' ' ,cteX005, ' ', cteX006, ' ', cteX003, ' ', cteX003) like '%{cliente.StrNombre1.Replace("'", "%").Replace(" ", "%")}%'
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
					cteX030 as DatFechaNacimiento,
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
					cteX047 as DatFechaNacimientoConyuge,
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
				$@"SELECT top  100
					arcicte.cteX023 as strCurp,
					arciaux.auxX041 AS strFolioContrato, 
					arciaux.auxX004 AS datFechaMinistracion, 
					arciaux.auxX005 AS datFechaVencimiento, 
					arciaux.auxX017 AS intTotalPagos, 
					arciaux.auxX008 AS dblCapital,
					arciaux.auxX009 AS dblIntereses, 
					arciaux.auxX009h AS dblSeguro, 
					arciaux.auxX010 AS dblTotal, 
					arciaux.auxX012 AS dblAbono,
					arciaux.auxX013 AS dblSaldo,
					arciaux.auxX032 AS strProducto 
					FROM 
					arcicte 
				INNER JOIN arciaux ON arcicte.cteX023 = arciaux.auxX023x 
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
	}
}
