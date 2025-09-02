using Datos;
using Entidades;
using System.Collections.Generic;

namespace Logica
{
    public class LBoletosWeb
    {
        public List<EBoletosWeb> ListaBoletosAnuladosWeb(string Opcion, string FechaInicial, string FechaFinal, string NroTarjeta
       , string Numero, string Origen, string Destino)
        {
            DBoletosWeb con = new DBoletosWeb();
            List<EBoletosWeb> lista = new List<EBoletosWeb>();
            lista = con.ListaBoletosAnuladosWeb(Opcion, FechaInicial, FechaFinal, NroTarjeta
                , Numero, Origen, Destino);
            return lista;
        }

        public void MatenimientoBoletosAnuladosWeb(List<EBoletosWeb> lst, string _opcion)
        {
            DBoletosWeb con = new DBoletosWeb();
            con.MatenimientoBoletosAnuladosWeb(lst, _opcion);
        }
    }
}
