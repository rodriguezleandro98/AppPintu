using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM VW_ListaMarcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.NombreMarca = (string)datos.Lector["NombreMarca"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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
        public void agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_Alta_Marca");
                datos.setearParametro("@NombreMarca", nuevo.NombreMarca);
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
        public void eliminarL(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_BajaMarca");
                datos.setearParametro("@ID", marca.Id);
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

        public void activar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActivarMarca");
                datos.setearParametro("@ID", marca.Id);
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
        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ModificarMarca");
                datos.setearParametro("@ID", marca.Id);
                datos.setearParametro("@NombreMarca", marca.NombreMarca);
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
        public List<Marca> filtrar(string estado)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select ID, NombreMarca, Activo FROM Marcas ";

                if (estado == "Activo")
                    consulta += " WHERE Activo = 1 ";
                else if (estado == "Inactivo")
                    consulta += " WHERE Activo = 0";
                else if (estado == "Todos")
                    consulta = " select * FROM Marcas";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.NombreMarca = (string)datos.Lector["NombreMarca"];
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

        public bool existeMarca(string nombreMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ExisteMarca"); // Nombre del procedimiento almacenado
                datos.setearParametro("@NombreMarca", nombreMarca);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = datos.Lector.GetInt32(0);
                    return count > 0; // Retorna true si ya existe la marca
                }

                return false; // Retorna false si no se encontró la marca
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

        public bool existeNombreMarcaModificado(string nombreMarca, int idMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ExisteNombreMarcaModificado"); // Procedimiento almacenado
                datos.setearParametro("@NombreMarca", nombreMarca);
                datos.setearParametro("@IDMarca", idMarca);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = datos.Lector.GetInt32(0);
                    return count > 0; // Si count > 0, significa que la marca ya existe
                }

                return false; // La marca no existe
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

        public bool tieneProductosActivos(int idMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_TieneProductosActivosMarca"); // Nombre del procedimiento almacenado
                datos.setearParametro("@IdMarca", idMarca);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = datos.Lector.GetInt32(0);
                    return count > 0; // Retorna true si hay productos activos asociados a la marca
                }

                return false; // Retorna false si no se encontraron productos activos
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
