using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public void agregarVenta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_Alta_Venta");
                //datos.setearParametro("@idcliente", venta.Cliente.Id);
                datos.setearParametro("@total", venta.PrecioTotal);
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
                datos.setearConsulta("SELECT * FROM VW_TraerUltimaVenta");
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
    }
}
