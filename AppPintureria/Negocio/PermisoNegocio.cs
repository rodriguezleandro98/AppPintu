using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PermisoNegocio
    {
        public List<Permiso> listar()
        {
            List<Permiso> lista = new List<Permiso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select * FROM VW_ListaPermisos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Permiso aux = new Permiso();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.NombrePermiso = datos.Lector["NombrePermiso"].ToString();

                    lista.Add(aux);
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
        public void agregar(Permiso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_Alta_Permiso");
                datos.setearParametro("@NombrePermiso", nuevo.NombrePermiso);
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
        public void modificar(Permiso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ModificarPermiso");
                datos.setearParametro("@NombrePermiso", nuevo.NombrePermiso);
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
    }
}
