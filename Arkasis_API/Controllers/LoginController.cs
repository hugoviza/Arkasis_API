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
    [Route("api/login")]
    public class LoginController: ControllerBase
    {
        [HttpPost]
        public IActionResult buscarUsuario(Usuario usuario)
        {

            if(usuario.User == null)
            {
                return Ok(new {Mensaje = "Se requiere User", Success = false });
            } 

            if(usuario.Password == null)
            {
                return Ok(new { Mensaje = "Se requiere Password", Success = false });
            }

            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[2];
            arrayConsultas[0] = $"SELECT pswLlave As Usuario, pswX004 AS Password, pswX002 AS Nombre FROM arcipsw WHERE pswLlave = '{usuario.User}';";
            arrayConsultas[1] = $"SELECT pswLlave As Usuario, pswX004 AS Password, pswX002 AS Nombre FROM arcipsw WHERE pswX004 = '{usuario.Password}' AND pswLlave = '{usuario.User}';";
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if(arrayResult != null)
            {
                if(arrayResult[1].Rows.Count > 0)
                {
                    return Ok(new { Mensaje = "Usuario encontrado", Success = true, Resultado = new Usuario(arrayResult[1].Rows[0]) });
                } else if (arrayResult[0].Rows.Count > 0)
                {
                    return Ok(new { Mensaje = "Contraseña incorrecta", Success = false });
                } else
                {
                    return Ok(new {Mensaje = "Usuario no encontrado", Success = false });
                }

            }
            else
            {
                return Ok(new {Mensaje = "No se encontraron registros", Success = false });
            }
        }
    }
}
