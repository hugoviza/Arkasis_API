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
    [Route("api/municipios")]
    public class MunicipiosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT
                    mun.munX001 as IdEstado, 
	                edo.edoX002 as StrEstado, 
	                mun.munLlave as IdMunicipio,  
	                mun.munX003 as StrMunicipio
                FROM arcimun as mun
                JOIN arciedo as edo on (edo.edoLlave = mun.munX001)";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Municipio> listaMunicipios = new List<Municipio>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
                        listaMunicipios.Add(new Municipio(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaMunicipios.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron municipios", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }

        [HttpGet("total")]
        public IActionResult countMunicipios()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT
                    count(*) as total
                FROM arcimun as mun
                JOIN arciedo as edo on (edo.edoLlave = mun.munX001)";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    String total = "0";
                    if (arrayResult[0].Rows[0] != null)
                    {
                        total = arrayResult[0].Rows[0]["total"].ToString();
                    }
                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = total });
                }
                else
                {
                    return Ok(new { Mensaje = "Sin resultados", Success = false, Resultado = "0" });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false, Resultado = "0" });
            }

        }
    }
}
