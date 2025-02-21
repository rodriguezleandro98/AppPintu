using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT ID, IDPermiso, NombrePermiso FROM VW_ListaUsuarios WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia AND Activo = 1");
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Contrasenia", usuario.Contrasenia);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["ID"];
                    usuario.Permiso = new Permiso
                    {
                        Id = (int)datos.Lector["IDPermiso"],
                        NombrePermiso = (string)datos.Lector["NombrePermiso"]
                    };

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Usuario> listarConPermisos()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ID, IDPermiso, NombrePermiso, NombreUsuario, Contrasenia, Activo, Nombre, Apellido, CorreoElectronico, Telefono, ImagenURL FROM VW_ListaUsuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id = (int)datos.Lector["ID"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        Contrasenia = (string)datos.Lector["Contrasenia"],
                        Activo = (bool)datos.Lector["Activo"],
                        //Nombre = datos.Lector["Nombre"].ToString(),
                        //Apellido = datos.Lector["Apellido"].ToString(),
                        //CorreoElectronico = datos.Lector["CorreoElectronico"].ToString(),
                        //Telefono = datos.Lector["Telefono"].ToString(),
                        Imagen = new Imagen { ImagenUrl = datos.Lector["ImagenURL"].ToString() },
                        Permiso = new Permiso
                        {
                            Id = (int)datos.Lector["IDPermiso"],
                            NombrePermiso = (string)datos.Lector["NombrePermiso"]
                        }
                    };

                    lista.Add(usuario);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_Alta_Usuario");
                datos.setearParametro("@IDPermiso", nuevo.Permiso.Id);
                datos.setearParametro("@NombreUsuario", nuevo.NombreUsuario);
                datos.setearParametro("@Contrasenia", nuevo.Contrasenia);
                //datos.setearParametro("@Nombre", nuevo.Nombre);
                //datos.setearParametro("@Apellido", nuevo.Apellido);
                //datos.setearParametro("@CorreoElectronico", nuevo.CorreoElectronico);
                //datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@ImagenURL", nuevo.Imagen.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarL(Usuario aux)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_BajaUsuario");
                datos.setearParametro("@ID", aux.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void activar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActivarUsuario");
                datos.setearParametro("@ID", usuario.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarUsuario");
                datos.setearParametro("@ID", usuario.Id);
                datos.setearParametro("@IDPermiso", usuario.Permiso.Id);
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Contrasenia", usuario.Contrasenia);
                //datos.setearParametro("@Nombre", usuario.Nombre);
                //datos.setearParametro("@Apellido", usuario.Apellido);
                //datos.setearParametro("@CorreoElectronico", usuario.CorreoElectronico);
                //datos.setearParametro("@Telefono", usuario.Telefono);
                datos.setearParametro("@ImagenURL", usuario.Imagen.ImagenUrl);
                datos.setearParametro("@Activo", usuario.Activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        //public List<Usuario> filtrar(string campo, string criterio, string filtro, string estado)
        //{
        //    List<Usuario> lista = new List<Usuario>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        string consulta = "SELECT NombreUsuario, Contrasenia, IDPermiso, Activo, Nombre, Apellido, CorreoElectronico, Telefono, ImagenURL FROM Usuarios WHERE";

        //        if (campo == "IDPermiso")
        //        {
        //            switch (criterio)
        //            {
        //                case "Mayor a":
        //                    consulta += " IDPermiso > " + filtro;
        //                    break;
        //                case "Menor a":
        //                    consulta += " IDPermiso < " + filtro;
        //                    break;
        //                default:
        //                    consulta += " IDPermiso = " + filtro;
        //                    break;
        //            }
        //        }
        //        else if (campo == "NombreUsuario")
        //        {
        //            switch (criterio)
        //            {
        //                case "Comienza con":
        //                    consulta += " NombreUsuario LIKE '" + filtro + "%' ";
        //                    break;
        //                case "Termina con":
        //                    consulta += " NombreUsuario LIKE '%" + filtro + "'";
        //                    break;
        //                default:
        //                    consulta += " NombreUsuario LIKE '%" + filtro + "%'";
        //                    break;
        //            }
        //        }
        //        else if (campo == "Contrasenia")
        //        {
        //            switch (criterio)
        //            {
        //                case "Comienza con":
        //                    consulta += " Contrasenia LIKE '" + filtro + "%' ";
        //                    break;
        //                case "Termina con":
        //                    consulta += " Contrasenia LIKE '%" + filtro + "'";
        //                    break;
        //                default:
        //                    consulta += " Contrasenia LIKE '%" + filtro + "%'";
        //                    break;
        //            }
        //        }

        //        if (estado == "Activo")
        //            consulta += " AND Activo = 1 ";
        //        else if (estado == "Inactivo")
        //            consulta += " AND Activo = 0";

        //        datos.setearConsulta(consulta);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Usuario aux = new Usuario
        //            {
        //                NombreUsuario = (string)datos.Lector["NombreUsuario"],
        //                Contrasenia = (string)datos.Lector["Contrasenia"],
        //                Activo = bool.Parse(datos.Lector["Activo"].ToString()),
        //                Nombre = datos.Lector["Nombre"].ToString(),
        //                Apellido = datos.Lector["Apellido"].ToString(),
        //                CorreoElectronico = datos.Lector["CorreoElectronico"].ToString(),
        //                Telefono = datos.Lector["Telefono"].ToString(),
        //                Imagen = new Imagen { ImagenUrl = datos.Lector["ImagenURL"].ToString() },
        //                Permiso = new Permiso { Id = (int)datos.Lector["IDPermiso"] }
        //            };

        //            lista.Add(aux);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public List<Usuario> filtrar(string estado)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select ID, NombreUsuario, Contrasenia, Nombre, Apellido, CorreoElectronico, Telefono, FechaCreacion, Activo FROM Usuarios";

                if (estado == "Activo")
                    consulta += " WHERE Activo = 1 ";
                else if (estado == "Inactivo")
                    consulta += " WHERE Activo = 0";
                else if (estado == "Todos")
                    consulta = " select * FROM Usuarios";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    //aux.Permiso.Id = (int)datos.Lector["IdPermiso"];
                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Contrasenia = (string)datos.Lector["Contrasenia"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];
                    //aux.Apellido = (string)datos.Lector["Apellido"];
                    //aux.CorreoElectronico = (string)datos.Lector["CorreoElectronico"];
                    //aux.Telefono = (string)datos.Lector["Telefono"];
                    //aux.Imagen.ID = (int)datos.Lector["IDImagen"];
                    aux.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
