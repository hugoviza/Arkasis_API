using Arkasis_API.Attributes;
using Arkasis_API.Conexiones;
using Arkasis_API.Models;
using Arkasis_API.Helpers;
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
    [Route("api/dispositivos")]
    public class DispositivosController : Controller
    {

        [HttpPost]
        public IActionResult GetDispositivos(Dispositivo dispositivo)
        {

            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[1];

            String filtro = "";
            if (dispositivo != null && dispositivo.IdDispositivo != 0) filtro += $" AND dispositivo.appLlave = '{dispositivo.IdDispositivo}'";
            if (dispositivo != null && dispositivo.UUIDDispositivo != null) filtro += $" AND dispositivo.appX004 = '{dispositivo.UUIDDispositivo}'";
            if (dispositivo != null && dispositivo.IdSucursal != null) filtro += $" AND dispositivo.appX001 = '{dispositivo.IdSucursal}'";
            if (dispositivo != null && dispositivo.Plataforma != null) filtro += $" AND dispositivo.appX003 = '{dispositivo.Plataforma}'";
            if (dispositivo != null && dispositivo.UsuarioAlta != null) filtro += $" AND dispositivo.appX302 = '{dispositivo.UsuarioAlta}'";

            arrayConsultas[0] =
                $@"SELECT
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE 1 = 1
                {filtro}";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[0].Rows.Count > 0)
                {
                    List<Dispositivo> data = new List<Dispositivo>();

                    foreach (DataRow row in arrayResult[0].Rows)
                    {
                        data.Add(new Dispositivo(row));
                    }

                    return Ok(new { Mensaje = "Consulta ok", Success = true, Resultado = data.ToArray() });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron Dispositivos", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }

        [HttpPut]
        public IActionResult GuardarDispositivo(Dispositivo dispositivo)
        {

            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[2];

            // Validamos parámetros requeridos
            if (dispositivo == null) return BadRequest("Se requieren datos de dispositivo");

            if (dispositivo.IdSucursal == null) return BadRequest("Se requiere valor para el parámetro IdSucursal");
            if (dispositivo.UsuarioAlta == null) return BadRequest("Se requiere valor para el parámetro UsuarioAlta");

            String sucursalMD5 = Helper.GetMD5Hash(dispositivo.IdSucursal);
            String timeMD5 = Helper.GetMD5Hash(DateTime.Now.ToString());
            String allowedChars = "YDQzkWSxNyGSlEhnSlxTxNdbpDPXS7Zw41CDJmqSNOUauAXBj5PA3Jh6Xl5GAsBVLPJn2uVmgG6Fbavt73zSoAK8ErrdH7PZJ6VgB0oBbddffPzVG2kH8KGFXvnyr0OgKqCepJuhTxnELJTMvQbDXbbxeZSakMkrGu38qGPf7ncvEbhHBYWx2cu0V6dnrsd58nM2SzD8cRC2cAraEIy2RlSdo5Ctb7loelxx8oV7c0I4TMjX8iSTFOmJOyxoIqXf";
            Random random = new Random();
            String TokenActivacion = "";
            
            TokenActivacion += Helper.PadLeftZero(sucursalMD5.Substring(0, 4)) + "-";
            TokenActivacion += Helper.PadLeftZero(timeMD5.Substring(random.Next(0, timeMD5.Length - 4), 4)) + "-";
            TokenActivacion += Helper.PadLeftZero(
                        allowedChars.Substring(random.Next(0, allowedChars.Length - 1), 1)
                        + allowedChars.Substring(random.Next(0, allowedChars.Length - 1), 1)
                        + allowedChars.Substring(random.Next(0, allowedChars.Length - 1), 1)
                        + allowedChars.Substring(random.Next(0, allowedChars.Length - 1), 1)
                    );

            String timeNow = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
            String IdSucursal = $"NULL";
            String IdDispositivo = $"NULL";
            String Plataforma = $"NULL";
            String UUIDDispositivo = $"NULL";
            String FechaHoraRegistroUUIDDispositivo = $"NULL";
            String FechaHoraRegistroTokenActivacion = $"'{timeNow}'";
            String EstatusTokenAcivacion = $"1";
            String UsuarioAlta = $"NULL";
            String FechaHoraAlta = $"'{timeNow}'";
            String UsuarioModificacion = $"NULL";
            String FechaHoraModificacion = $"NULL";


            if (dispositivo != null && dispositivo.IdSucursal != null) IdSucursal = $"'{dispositivo.IdSucursal}'";
            if (dispositivo != null && dispositivo.Plataforma != null) Plataforma = $"'{dispositivo.Plataforma}'";
            if (dispositivo != null && dispositivo.UUIDDispositivo != null) UUIDDispositivo = $"'{dispositivo.UUIDDispositivo}'";
            if (dispositivo != null && dispositivo.FechaHoraRegistroUUIDDispositivo != null) FechaHoraRegistroUUIDDispositivo = $"'{dispositivo.FechaHoraRegistroUUIDDispositivo}'";
            if (dispositivo != null && dispositivo.TokenActivacion != null) TokenActivacion = $"'{dispositivo.TokenActivacion}'";
            if (dispositivo != null && dispositivo.FechaHoraRegistroTokenActivacion != null) FechaHoraRegistroTokenActivacion = $"'{dispositivo.FechaHoraRegistroTokenActivacion}'";
            // EstatusTokenAcivacion = $"'{dispositivo.EstatusTokenAcivacion}'";
            if (dispositivo != null && dispositivo.UsuarioAlta != null) UsuarioAlta = $"'{dispositivo.UsuarioAlta}'";
            if (dispositivo != null && dispositivo.FechaHoraAlta != null) FechaHoraAlta = $"'{dispositivo.FechaHoraAlta}'";
            if (dispositivo != null && dispositivo.UsuarioModificacion != null) UsuarioModificacion = $"'{dispositivo.UsuarioModificacion}'";
            if (dispositivo != null && dispositivo.FechaHoraModificacion != null) FechaHoraModificacion = $"'{dispositivo.FechaHoraModificacion}'";

            arrayConsultas[0] =
                $@"INSERT INTO arciapp 
                    (appX001, 
                    appX003, appX004, appX006, 
                    appX005, appX008, appX007,
                    appX301, appX302, 
                    appX303, appX304)
                    VALUES
                    ({IdSucursal},
                    {Plataforma}, {UUIDDispositivo}, {FechaHoraRegistroUUIDDispositivo},
                    '{TokenActivacion.ToLower()}', {EstatusTokenAcivacion}, {FechaHoraRegistroTokenActivacion},
                    {UsuarioAlta}, {FechaHoraAlta},
                    {UsuarioModificacion}, {FechaHoraModificacion})";

            arrayConsultas[1] =
                $@"SELECT TOP 1
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appX005 = '{TokenActivacion.ToLower()}'
                AND appX004 IS NULL
                AND appX001 = {IdSucursal}
                ORDER BY appLlave DESC
                ";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[1].Rows.Count > 0)
                {
                    Dispositivo data = new Dispositivo();

                    foreach (DataRow row in arrayResult[1].Rows)
                    {
                        data = new Dispositivo(row);
                    }

                    return Ok(new { Mensaje = "Dispositivo agregado", Success = true, Resultado = data });
                }
                else
                {
                    return Ok(new { Mensaje = "No se encontraron Dispositivos", Success = false });
                }
            }
            else
            {
                return Ok(new { Mensaje = "No se encontraron registros", Success = false });
            }
        }

        [HttpPut("UUID")]
        public IActionResult ActualizarDispositivoToken(Dispositivo dispositivo)
        {

            ConexionSQL conexionSQL = new ConexionSQL();

            // Validamos parámetros requeridos
            if (dispositivo == null) return BadRequest("Se requieren datos de dispositivo");

            if (dispositivo.TokenActivacion == null) return BadRequest("Se requiere valor para el parámetro TokenActivacion");
            if (dispositivo.UUIDDispositivo == null) return BadRequest("Se requiere valor para el parámetro UUIDDispositivo");
            if (dispositivo.Plataforma == null) return BadRequest("Se requiere valor para el parámetro Plataforma");
            // if (dispositivo.UsuarioModificacion == null) return BadRequest("Se requiere valor para el parámetro UsuarioModificacion");

            // Consultamos la data del dispositivo
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                $@"SELECT
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appX004 = '{dispositivo.UUIDDispositivo}'";
            Dispositivo? uuidDispositivo = null;
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);
            if (arrayResult != null && arrayResult[0].Rows.Count > 0)
            {
                List<Dispositivo> data = new List<Dispositivo>();
                foreach (DataRow row in arrayResult[0].Rows)
                {
                    uuidDispositivo = new Dispositivo(row);
                }
            }


            // consultamos la data del token
            arrayConsultas[0] =
                $@"SELECT
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appX005 = '{dispositivo.TokenActivacion}'";
            Dispositivo? tokenDispositivo = null;
            arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);
            if (arrayResult != null && arrayResult[0].Rows.Count > 0)
            {
                List<Dispositivo> data = new List<Dispositivo>();
                foreach (DataRow row in arrayResult[0].Rows)
                {
                    tokenDispositivo = new Dispositivo(row);
                }
            }

            // Validamos que exista el registro
            if (tokenDispositivo == null) return BadRequest("El token ingresado no existe");
            if (tokenDispositivo.UUIDDispositivo != "") return BadRequest("El token ingresado no está disponible");
            if (tokenDispositivo.EstatusTokenAcivacion != 1) return BadRequest("El token ingresado no está activo");
            if (uuidDispositivo != null) return BadRequest("El dispositivo ya se encuetra registrado");


            String timeNow = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
            arrayConsultas = new string[2];

            arrayConsultas[0] =
                $@"UPDATE arciapp SET 
                        appX003 = '{dispositivo.Plataforma}',
                        appX004 = '{dispositivo.UUIDDispositivo}',
                        appX006 = '{timeNow}'
                WHERE appLlave = '{tokenDispositivo.IdDispositivo}' ";

            arrayConsultas[1] =
                $@"SELECT TOP 1
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appLlave = '{tokenDispositivo.IdDispositivo}'
                ORDER BY appLlave DESC
                ";

            arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[1].Rows.Count > 0)
                {
                    Dispositivo data = new Dispositivo();

                    foreach (DataRow row in arrayResult[1].Rows)
                    {
                        data = new Dispositivo(row);
                    }

                    return Ok(new { Mensaje = "Dispositivo editado", Resultado = data });
                }
                else
                {
                    return StatusCode(500, new { Mensaje = "No es posible editar dispositivo" });
                }
            }
            else
            {
                return StatusCode(500, new { Mensaje = "Error al editar registro" });
            }
        }

        [HttpPut("Bloquear")]
        public IActionResult BloquearDispositivoToken(Dispositivo dispositivo)
        {

            ConexionSQL conexionSQL = new ConexionSQL();

            // Validamos parámetros requeridos
            if (dispositivo == null) return BadRequest("Se requieren datos de dispositivo");

            if (dispositivo.IdDispositivo == 0) return BadRequest("Se requiere valor para el parámetro IdDispositivo");

            // Consultamos la data del dispositivo
            String[] arrayConsultas = new string[1];
            arrayConsultas[0] =
                $@"SELECT
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appLlave = '{dispositivo.IdDispositivo}'";
            Dispositivo? tokenDispositivo = null;
            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);
            if (arrayResult != null && arrayResult[0].Rows.Count > 0)
            {
                List<Dispositivo> data = new List<Dispositivo>();
                foreach (DataRow row in arrayResult[0].Rows)
                {
                    tokenDispositivo = new Dispositivo(row);
                }
            }

            // Validamos que exista el registro
            if (tokenDispositivo == null) return BadRequest("El dispositivo no existe");

            arrayConsultas = new string[2];
            arrayConsultas[0] =
                $@"UPDATE arciapp SET 
                        appX008 = '{dispositivo.EstatusTokenAcivacion}'
                WHERE appLlave = '{tokenDispositivo.IdDispositivo}' ";

            arrayConsultas[1] =
                $@"SELECT TOP 1
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appLlave = '{tokenDispositivo.IdDispositivo}'
                ORDER BY appLlave DESC
                ";

            arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[1].Rows.Count > 0)
                {
                    Dispositivo data = new Dispositivo();

                    foreach (DataRow row in arrayResult[1].Rows)
                    {
                        data = new Dispositivo(row);
                    }

                    return Ok(new { Mensaje = "Dispositivo bloqueado", Resultado = data });
                }
                else
                {
                    return StatusCode(500, new { Mensaje = "No es posible editar dispositivo" });
                }
            }
            else
            {
                return StatusCode(500, new { Mensaje = "Error al editar registro" });
            }
        }


        [HttpDelete]
        public IActionResult EliminarDispositivo(Dispositivo dispositivo)
        {

            ConexionSQL conexionSQL = new ConexionSQL();
            String[] arrayConsultas = new string[2];

            // Validamos parámetros requeridos
            if (dispositivo == null) return BadRequest("Se requieren datos de dispositivo");
            if (dispositivo.IdDispositivo == 0) return BadRequest("Se requiere valor para el parámetro IdDispositivo");

            arrayConsultas[0] =
                $@"DELETE FROM arciapp WHERE appLlave = '{dispositivo.IdDispositivo}'";

            arrayConsultas[1] =
                $@"SELECT TOP 1
                    dispositivo.appX001 AS IdSucursal,
                    dispositivo.appLlave AS IdDispositivo,
                    dispositivo.appX003 AS Plataforma,
                    dispositivo.appX004 AS UUIDDispositivo,
                    dispositivo.appX006 AS FechaHoraRegistroUUIDDispositivo,
                    dispositivo.appX005 AS TokenActivacion,
                    dispositivo.appX007 AS FechaHoraRegistroTokenActivacion,
                    dispositivo.appX008 AS EstatusTokenAcivacion,
                    dispositivo.appX301 AS UsuarioAlta,
                    dispositivo.appX302 AS FechaHoraAlta,
                    dispositivo.appX303 AS UsuarioModificacion,
                    dispositivo.appX304 AS FechaHoraModificacion
                FROM arciapp as dispositivo
                WHERE appLlave = '{dispositivo.IdDispositivo}'
                ORDER BY appLlave DESC
                ";

            DataTable[] arrayResult = conexionSQL.EjecutarQueries(arrayConsultas);

            if (arrayResult != null)
            {
                if (arrayResult[1].Rows.Count == 0)
                {
                    return Ok(new { Mensaje = "Dispositivo eliminado" });
                }
                else
                {
                    return StatusCode(500, new { Mensaje = "No se pudo eliminar dispositivo" });
                }
            }
            else
            {
                return StatusCode(500, new { Mensaje = "Se produjo un error al eliminar dispositivo" });
            }
        }


    }
}
