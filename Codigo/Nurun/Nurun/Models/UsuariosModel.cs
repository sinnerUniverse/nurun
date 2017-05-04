using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Models
{
    public class UsuariosModel
    {
        public Usuarios iniciarSesion(Usuarios usuario)
        {
            using (NurunEntities db = new NurunEntities())
            {
                var obj = db.Usuarios.Where(a => a.Usuario.Equals(usuario.Usuario) 
                    && a.Password.Equals(usuario.Password) 
                    && a.EstaActivo).FirstOrDefault();
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

        public Resultados asignarPaciente(string seleccionados, int idMedico)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                int[] selectedList = seleccionados.Split(',').Select(int.Parse).ToArray();

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Usuarios.Where(u => u.IdMedico == idMedico).ToList().ForEach(us => {
                            us.IdMedico = null;
                            us.ConfirmPassword = us.Password;
                        });
                        db.Usuarios.Where(u => selectedList.Contains(u.IdUsuario)).ToList().ForEach(us => {
                            us.IdMedico = idMedico;
                            us.FechaModificacion = DateTime.Now;
                            us.ConfirmPassword = us.Password;
                        });                       

                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        r.Resultado = true;
                    }
                    catch (Exception ex)
                    {
                        r.Mensaje = ex.Message;
                        r.Resultado = false;
                        dbContextTransaction.Rollback(); 
                    }

                    return r;
                }
            }
        } 

        public List<Usuarios> obtenerPacientes(int idMedico)
        {
            using (NurunEntities db = new NurunEntities())
            {
                var pacientesSinMedico = db.Usuarios.Where(u => u.EstaActivo
                    && object.Equals(null, u.IdMedico)
                    && u.IdRol != 2);

                var pacientesDelMedico = db.Usuarios.Where(u => u.EstaActivo
                    && u.IdMedico == idMedico
                    && u.IdRol != 2);

                return pacientesSinMedico.Union(pacientesDelMedico).ToList<Usuarios>(); ;
            }
        }

        public IEnumerable<SelectListItem> obtenerPacientesSelect()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var hospitales = db.Usuarios.Where(u => u.IdRol != 2).ToList<Usuarios>().Select(x =>
                        new SelectListItem
                        {
                            Value = x.IdUsuario.ToString(),
                            Text = string.Format("{0} {1}", x.Nombres, x.Apellidos)
                        });

                return new SelectList(hospitales, "Value", "Text");
            }
        }
    }
}