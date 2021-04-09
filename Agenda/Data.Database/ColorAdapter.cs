using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class ColorAdapter
    {
        public Color BuscarColor(Color c)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Colores.Find(c.id_color);
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar color", e);
                throw excepcionManejada;
            }
        }

        public Color BuscarColorPorDescripcion(String desc)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Colores.Where(c => c.descripcion == desc).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar color", e);
                throw excepcionManejada;
            }
        }

        public List<Color> BuscarColores()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Colores.ToList();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar colores", e);
                throw excepcionManejada;
            }
        }
    }
}
