using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ColorLogic
    {
        private ColorAdapter colorData;

        public ColorLogic()
        {
            colorData = new ColorAdapter();
        }

        public Color BuscarColor(Color color)
        {
            return colorData.BuscarColor(color);
        }

        public Color BuscarColorPorDescripcion(String descripcion)
        {
            return colorData.BuscarColorPorDescripcion(descripcion);
        }

        public List<Color> BuscarColores()
        {
            return colorData.BuscarColores();
        }
    }
}
