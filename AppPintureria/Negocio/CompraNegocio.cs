using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {
        public void agregarCompra(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_Alta_Compra");
                datos.setearParametro("@idproveedor", compra.Proveedor.Id);
                datos.setearParametro("@total", compra.PrecioTotal);
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
        public long TraerUltimo()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM VW_TraerUltimo");
                datos.ejecutarLectura();

                long idcompra;

                if (datos.Lector.Read())
                {
                    idcompra = datos.Lector.GetInt64(0);
                    return idcompra;
                }

                return 0;
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
        public List<Compra> listarCompras(int idproveedor)
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM Compras WHERE IDProveedor = " + idproveedor + " AND FechaCreacion >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) AND FechaCreacion < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) +1, 0);");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.Proveedor = new Proveedor();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Recibo = (long)datos.Lector["Nro_Recibo"];
                    aux.Proveedor.Id = (int)datos.Lector["IDProveedor"];
                    aux.FechaCompra = (DateTime)datos.Lector["FechaCreacion"];
                    aux.FechaEntrega = (DateTime)datos.Lector["FechaEntrega"];
                    aux.PrecioTotal = (decimal)datos.Lector["Total"];
                    aux.Estado = (bool)datos.Lector["Estado"];

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
        public void confirmarCompra(long idcompra)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ConfirmarCompra");
                datos.setearParametro("@idcompra", idcompra);
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
        public bool estaConfirmada(long idcompra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT 1 FROM Compras WHERE Estado = 1 AND ID =" + idcompra);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    bool confirmada = false;
                    confirmada = datos.Lector.GetBoolean(0);
                    return confirmada;
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
    }
}
