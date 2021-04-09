using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class UsuarioLogic
    {
        private UsuarioAdapter usuarioData;

        public UsuarioLogic()
        {
            usuarioData = new UsuarioAdapter();
        }

        public Usuario BuscarUsuario(Usuario user)
        {
            return usuarioData.BuscarUsuario(user);
        }

        public Usuario ValidarUsuario(Usuario user)
        {
            return usuarioData.BuscarPorUsuarioYContrasenia(user);
        }

        public Usuario BuscarPorNombreUsuario(String nombre_usuario)
        {
            return usuarioData.BuscarPorNombreUsuario(nombre_usuario);
        }

        public void RegistrarUsuario(Usuario user)
        {
            usuarioData.RegistrarUsuario(user);
        }

        public void ActualizarUsuario(Usuario user)
        {
            usuarioData.ActualizarUsuario(user);
        }

        public bool ExisteUsuario(Usuario user)
        {
            return usuarioData.ExisteUsuario(user);
        }
    }
}
