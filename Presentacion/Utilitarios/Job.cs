using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace Presentacion.Utilitarios
{
    public class Job:IJob
    {
        CsEnvioCorreo _CsEnvioCorreo = new CsEnvioCorreo();
        CsBoletosWeb _CsBoletosWeb = new CsBoletosWeb();

        public void Execute(IJobExecutionContext context)
        {
            _CsBoletosWeb.ListaBoletosAnuladosWebJob();
            //VERIFICA SI HAY BOLETOS DUPLICADOS POR WEB
            if (_CsBoletosWeb.ListaBoletosAnuladosWebJob().Count() > 0)
            {
                _CsEnvioCorreo.EnviarMensaje("Boletos duplicados (Venta Web)", _CsBoletosWeb.ListaBoletosAnuladosWebJob());
            }
        }
    }
}
