using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class UsuariosModel
    {
        public Usuarios iniciarSesion(Usuarios usuario)
        {
            using (NurunEntities db = new NurunEntities())
            {
                var obj = db.Usuarios.Where(a => a.Usuario.Equals(usuario.Usuario) && a.Password.Equals(usuario.Password) && a.EstaActivo).FirstOrDefault();
                return obj;
            }
        }

        public Resultados crearUsuario(Usuarios objUser)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                try
                {
                    objUser.IdRol = 1;
                    objUser.FechaCreacion = DateTime.Now;
                    db.Usuarios.Add(objUser);
                    db.SaveChanges();
                    r.Resultado = true;
                }
                catch (Exception ex)
                {
                    r.Mensaje = ex.Message;
                    r.Resultado = false;
                }

                return r;
            }
        }

        public Resultados activarUsuario(int idUsuario)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                try
                {
                    var usuario = db.Usuarios.First(u => u.IdUsuario == idUsuario);
                    usuario.EstaActivo = true;
                    usuario.ConfirmPassword = usuario.Password;
                    usuario.FechaModificacion = DateTime.Now;
                    db.SaveChanges();
                    r.Resultado = true;
                }
                catch (Exception ex)
                {
                    r.Mensaje = ex.Message;
                    r.Resultado = false;
                }

                return r;
            }
        }

        public List<Usuarios> obtenerUsuarios()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var usuarios = db.Usuarios.Where(u => u.IdRol != 2 && !u.EstaActivo).ToList<Usuarios>();
                return usuarios;
            }
        }
    }
}