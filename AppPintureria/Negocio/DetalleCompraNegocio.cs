using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleCompraNegocio
    {
        public List<DetalleCompra> listar(long idcompra)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT PXP.ID, PXP.IDCompra, P.ID AS IDProducto, PXP.Cantidad, PXP.CantidadVieja, PXP.Precio_UnitarioC, PXP.Subtotal, P.Nombre,P.Stock_Actual,P.Stock_Minimo FROM Productos_x_compra AS PXP INNER JOIN Productos as P ON P.ID = PXP.IDProducto WHERE IDCompra =" + idcompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();
                    aux.Compra = new Compra();
                    aux.Producto = new Producto();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Compra.Id = (long)datos.Lector["IDCompra"];
                    aux.Producto.Id = (long)datos.Lector["IDProducto"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.CantidadVieja = (int)datos.Lector["CantidadVieja"];
                    //aux.Producto.Precio_Compra = (decimal)datos.Lector["Precio_UnitarioC"];
                    aux.Subtotal = (decimal)datos.Lector["Subtotal"];
                    aux.Producto.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Producto.StockActual = aux.CantidadVieja;
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

        public List<DetalleCompra> listar2(long idcompra)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT PXP.ID, PXP.IDCompra, P.ID AS IDProducto, PXP.Cantidad, PXP.CantidadVieja, PXP.Precio_UnitarioC, PXP.Subtotal, P.Nombre,P.Stock_Minimo FROM Productos_x_compra AS PXP INNER JOIN Productos as P ON P.ID = PXP.IDProducto WHERE IDCompra =" + idcompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();
                    aux.Compra = new Compra();
                    aux.Producto = new Producto();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Compra.Id = (long)datos.Lector["IDCompra"];
                    aux.Producto.Id = (long)datos.Lector["IDProducto"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.CantidadVieja = (int)datos.Lector["CantidadVieja"];
                    //aux.Producto.Precio_Compra = (decimal)datos.Lector["Precio_UnitarioC"];
                    aux.Subtotal = (decimal)datos.Lector["Subtotal"];
                    aux.Producto.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Producto.StockMinimo = (int)datos.Lector["Stock_Minimo"];
                    aux.Producto.StockActual = aux.CantidadVieja;
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
        public List<DetalleCompra> listarProductos(int idproveedor)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.ID, P.Nombre, P.Precio_Compra, P.Stock_Actual, P.Stock_Minimo FROM Productos AS P INNER JOIN Productos_x_Proveedores AS PXP ON PXP.IDProducto = P.ID INNER JOIN Proveedores AS PROV ON PROV.ID = PXP.IDProveedor WHERE PROV.ID =" + idproveedor);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();
                    aux.Compra = new Compra();
                    aux.Producto = new Producto();
                    aux.Producto.Id = (long)datos.Lector["ID"];
                    aux.Producto.Nombre = datos.Lector["Nombre"].ToString();
                    //aux.Producto.Precio_Compra = (decimal)datos.Lector["Precio_Compra"];
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
        public void agregarProductos(List<DetalleCompra> listaArtOC)
        {
            AccesoDatos datos = new AccesoDatos();
            DetalleCompraNegocio negocio = new DetalleCompraNegocio();
            decimal total = 0;
            long idcompra = 0;
            foreach (var productodeOC in listaArtOC)
            {
                total += productodeOC.Subtotal;
                idcompra = productodeOC.Compra.Id;
                //negocio.agregarProducto(productodeOC);
            }
            //negocio.actualizarMontoTotal(idcompra, total);
        }

        public void actualizarStock(DetalleCompra producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ActualizarStock");
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
        public void agregarProducto(DetalleCompra productocompra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_AgregarProductoCompra");
                datos.setearParametro("@idcompra", productocompra.Compra.Id);
                datos.setearParametro("@idproducto", productocompra.Producto.Id);
                datos.setearParametro("@cantidad", productocompra.Cantidad);
                datos.setearParametro("@cantidadvieja", productocompra.CantidadVieja);
                //datos.setearParametro("@preciounitario", productocompra.Producto.Precio_Compra);
                datos.setearParametro("@subtotal", productocompra.Subtotal);
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
        public void actualizarMontoTotal(long idcompra, decimal total)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActualizarMontoEnCompra");
                datos.setearParametro("@idcompra", idcompra);
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
