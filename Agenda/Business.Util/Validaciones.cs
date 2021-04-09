using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util
{
    public static class Validaciones
    {
        public static bool EsUnNumero(String cadena)
        {
            try
            {
                int.Parse(cadena);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
