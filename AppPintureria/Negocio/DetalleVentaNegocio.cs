using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleVentaNegocio
    {
        public List<DetalleVenta> listarProductos()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ID, Nombre, Precio_Venta, Stock_Actual, Stock_Minimo FROM Productos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();
                    aux.Producto = new Producto();
                    aux.Producto.Id = (long)datos.Lector["ID"];
                    aux.Producto.Nombre = datos.Lector["Nombre"].ToString();
                    //aux.Producto.Precio_Venta = (decimal)datos.Lector["Precio_Venta"];
                    aux.Producto.StockActual = (int)datos.Lector["Stock_Actual"];
                    aux.Producto.StockMinimo = (int)datos.Lector["Stock_Minimo"];

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
        public void actualizarStock(DetalleVenta producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ActualizarStockVenta");
                datos.setearParametro("@idproducto", producto.Producto.Id);
                datos.setearParametro("@stock", producto.Cantidad);
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
        public void agregarProductos(List<DetalleVenta> listaVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            DetalleVentaNegocio negocio = new DetalleVentaNegocio();
            decimal total = 0;
            long idventa = 0;
            foreach (var productoVenta in listaVenta)
            {
                total += productoVenta.SubTotal;
                idventa = productoVenta.Venta.Id;
                negocio.agregarProducto(productoVenta);
                negocio.actualizarStock(productoVenta);
            }
            negocio.actualizarMontoTotal(idventa, total);
        }
        public void agregarProducto(DetalleVenta productoVenta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarProductoVenta");
                datos.setearParametro("@idventa", productoVenta.Venta.Id);
                datos.setearParametro("@idproducto", productoVenta.Producto.Id);
                datos.setearParametro("@cantidad", productoVenta.Cantidad);
                datos.setearParametro("@preciounitario", productoVenta.Precio_Venta_Unitario);
                datos.setearParametro("@subtotal", productoVenta.SubTotal);
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
        public void actualizarMontoTotal(long idventa, decimal total)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActualizarMontoEnVenta");
                datos.setearParametro("@idventa", idventa);
                datos.setearParametro("@total", total);
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
