using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util
{
    public class NumeroPaginacion
    {
        public NumeroPaginacion(int numero, bool actual)
        {
            _numero = numero;
            _actual = actual;
        }

        private int _numero;
        public int Numero
        {
            get
            {
                return _numero;
            }
        }

        private bool _actual;
        public bool Actual
        {
            get
            {
                return _actual;
            }
        }
    }
}
