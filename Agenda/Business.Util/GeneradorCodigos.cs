using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util
{
    public static class GeneradorCodigos
    {
        public static int GenerarCodigoDe6Digitos()
        {
            return new Random().Next(100000, 1000000);
        }
    }
}
