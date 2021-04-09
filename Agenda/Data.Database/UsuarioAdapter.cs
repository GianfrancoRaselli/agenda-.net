using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class UsuarioAdapter
    {
        public Usuario BuscarUsuario(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Usuarios.Include("ZonaHoraria").Where(u => u.id_usuario == user.id_usuario).SingleOrDefault();
                }
            }
            catch(Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar usuario", e);
                throw excepcionManejada;
            }
        }

        public Usuario BuscarPorUsuarioYContrasenia(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Usuarios.Include("ZonaHoraria").Where(u => u.nombre_usuario.Equals(user.nombre_usuario) && u.contrasenia.Equals(user.contrasenia)).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar usuario", e);
                throw excepcionManejada;
            }
        }

        public Usuario BuscarPorNombreUsuario(String nombre_usuario)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.Usuarios.Include("ZonaHoraria").Where(u => u.nombre_usuario.Equals(nombre_usuario)).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar usuario", e);
                throw excepcionManejada;
            }
        }

        public void RegistrarUsuario(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Usuarios.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al registrar usuario", e);
                throw excepcionManejada;
            }
        }

        public void ActualizarUsuario(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    Usuario userAActualizar = context.Usuarios.Find(user.id_usuario);

                    if (userAActualizar != null)
                    {
                        userAActualizar.nombre_usuario = user.nombre_usuario;
                        userAActualizar.contrasenia = user.contrasenia;
                        userAActualizar.nombre_apellido = user.nombre_apellido;
                        userAActualizar.telefono = user.telefono;
                        userAActualizar.email = user.email;
                        userAActualizar.id_zona_horaria = user.id_zona_horaria;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al actualizar usuario", e);
                throw excepcionManejada;
            }
        }

        public bool ExisteUsuario(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    if(context.Usuarios.Where(u => u.nombre_usuario.Equals(user.nombre_usuario)).SingleOrDefault() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al validar existencia del usuario", e);
                throw excepcionManejada;
            }
        }
    }
}
