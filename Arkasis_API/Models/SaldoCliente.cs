using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Arkasis_API.Models
{
    public class SaldoCliente
    {
        public String StrCurp { get; set; }
        public String StrFolioContrato { get; set; }
        public String DatFechaMinistracion { get; set; }
        public String DatFechaVencimiento { get; set; }
        public String IntTotalPagos { get; set; }
        public String DblCapital { get; set; }
        public String DblIntereses { get; set; }
        public String DblSeguro { get; set; }
        public String DblTotal { get; set; }
        public String DblAbono { get; set; }
        public String DblSaldo { get; set; }
        public String StrProducto { get; set; }

        public SaldoCliente(DataRow dataRow)
        {
            if (dataRow != null)
            {
                StrCurp = dataRow["StrCurp"].ToString();
                StrFolioContrato = dataRow["StrFolioContrato"].ToString();
                DatFechaMinistracion = dataRow["DatFechaMinistracion"].ToString();
                DatFechaVencimiento = dataRow["DatFechaVencimiento"].ToString();
                IntTotalPagos = dataRow["IntTotalPagos"].ToString();
                DblCapital = dataRow["DblCapital"].ToString();
                DblIntereses = dataRow["DblIntereses"].ToString();
                DblSeguro = dataRow["DblSeguro"].ToString();
                DblTotal = dataRow["DblTotal"].ToString();
                DblAbono = dataRow["DblAbono"].ToString();
                DblSaldo = dataRow["DblSaldo"].ToString();
                StrProducto = dataRow["StrProducto"].ToString();
            }
        }
    }
}
