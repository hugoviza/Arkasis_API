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
    [Route("api/catalogos")]
    public class CatalogosController : Controller
    {
        [HttpGet("actividades")]
        public IActionResult GetActividades()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT 
	                actLlave as IdActividad, 
	                actX003 as strActividad, 
	                actX005 as strCNBV
                FROM arciact ORDER BY strActividad;";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Actividad> listaResultados = new List<Actividad>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
                        listaResultados.Add(new Actividad(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaResultados.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron actividades", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }

        [HttpGet("actividades/total")]
        public IActionResult CountActividades()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT 
	                COUNT(*) AS total
                FROM arciact;";

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


        [HttpGet("municipios")]
        public IActionResult GetMunicipios()
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

        [HttpGet("municipios/total")]
        public IActionResult CountMunicipios()
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


        [HttpGet("sucursales")]
        public IActionResult GetSucursales()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"select 
	                maeLlave as IdSucursal,
	                maeX010 as strClaveSucursal,
	                maeX011 as strSucursal
                from arcimae as sc where SUBSTRING(maeLlavex, 4, 6)  = '014';";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Sucursal> listaResultados = new List<Sucursal>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
                        listaResultados.Add(new Sucursal(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaResultados.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron sucursales", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }

        [HttpGet("sucursales/total")]
        public IActionResult CountSucursales()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT 
	                COUNT(*) AS total
                FROM arcimae;";

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



        [HttpGet("coordinadores")]
        public IActionResult GetCoordinadores()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT 
	                venLlave as IdCoordinador,
	                venX001 as IdSucursal,
	                venX003 as strNombre
                FROM arciven;";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Coordinador> listaResultados = new List<Coordinador>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
                        listaResultados.Add(new Coordinador(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = listaResultados.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron promotores", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }


        [HttpGet("coordinadores/total")]
        public IActionResult CountCoordinadores()
        {
            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                @"SELECT 
	                COUNT(*) AS total
                FROM arciven;";

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
