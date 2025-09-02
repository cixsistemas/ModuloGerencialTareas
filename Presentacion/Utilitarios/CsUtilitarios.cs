using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Utilitarios
{
  public  class CsUtilitarios
    {
        public string FechaActual()
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd/MM/yyyy");
            return fecha_actual;
        }
       
    }
}
