using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace Presentacion.Utilitarios
{
    public class CsBoletosWeb
    {
        CsUtilitarios _CsUtilitarios = new CsUtilitarios();
        public List<EBoletosWeb> list = new List<EBoletosWeb>();
        public List<EBoletosWeb> ListaBoletosAnuladosWebJob()
        {
            LBoletosWeb lp = new LBoletosWeb();
            //List<EBoletosWeb> lista = new List<EBoletosWeb>();
            list = lp.ListaBoletosAnuladosWeb("1", _CsUtilitarios.FechaActual(), _CsUtilitarios.FechaActual()
                , "", "", "", "");
            return list;
        }
    }
}
