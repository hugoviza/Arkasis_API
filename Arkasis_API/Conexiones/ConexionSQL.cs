using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkasis_API.Conexiones
{
    public class ConexionSQL {

        private SqlConnection _connection = null;
        private Boolean _transaccionAutomatica = true;
        private SqlTransaction _transaccion = null;

        public ConexionSQL(Boolean iniciarTransaccion = true) 
        {
            _transaccionAutomatica = iniciarTransaccion;
            Conectar();
        }

        private void Conectar() 
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                _connection = new SqlConnection(configuration.GetConnectionString("ConexionSecsa"));
                _connection.Open();
                _transaccion = null;
            }
            catch (SqlException e)
            {
                _connection = null;
                _transaccion = null;
            }
        }

        public void Begin()
        {
            if (_connection != null && _transaccion == null)
            {
                _transaccion = _connection.BeginTransaction();
            }
        }


        public void Commit()
        {
            if (_connection != null && _transaccion != null)
            {
                _transaccion.Commit();
                _transaccion = null;
            }
        }

        public DataTable[] EjecutarQueries(String[] arrayQueries)
        {
            if(_connection != null && arrayQueries.Length > 0)
            {
                try
                {
                    DataTable[] arrayDataTable = new DataTable[arrayQueries.Length];

                    if (_transaccionAutomatica)
                    {
                        Begin();
                    }

                    for (int indexQuery = 0; indexQuery < arrayQueries.Length; indexQuery++)
                    {
                        using(SqlCommand command = new SqlCommand(arrayQueries[indexQuery], _connection, _transaccion))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);

                                arrayDataTable[indexQuery] = dataTable;
                            }
                        }

                    }

                    if(_transaccionAutomatica)
                    {
                        Commit();
                    }

                    return arrayDataTable;

                } catch(SqlException e)
                { 
                    return null;
                }
            } else
            {
                return null;
            }
        }
    }
}
