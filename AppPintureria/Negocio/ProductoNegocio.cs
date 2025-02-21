using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public Producto verDetalle(long id)
        {
            Producto aux = new Producto();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_DetalleProducto");
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Imagen.ImagenUrl = (string)datos.Lector["ImagenURL"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.NombreMarca = (string)datos.Lector["NombreMarca"];
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    aux.StockActual = (int)datos.Lector["Stock_Actual"];
                    aux.StockMinimo = (int)datos.Lector["Stock_Minimo"];
                    //aux.Precio_Compra = (decimal)datos.Lector["Precio_Compra"];
                    //aux.Precio_Venta = (decimal)datos.Lector["Precio_Venta"];
                    //aux.Porcentaje_Ganancia = (decimal)datos.Lector["Porcentaje_Ganancia"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                }
                return aux;
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
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM VW_ALLProducto");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Imagen.ImagenUrl = (string)datos.Lector["ImagenURL"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca.NombreMarca = (string)datos.Lector["NombreMarca"];
                    aux.Categoria.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    aux.StockActual = (int)datos.Lector["Stock_Actual"];
                    aux.StockMinimo = (int)datos.Lector["Stock_Minimo"];
                    //aux.Precio_Compra = (decimal)datos.Lector["Precio_Compra"];
                    //aux.Precio_Venta = (decimal)datos.Lector["Precio_Venta"];
                    //aux.Porcentaje_Ganancia = (decimal)datos.Lector["Porcentaje_Ganancia"];
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
        public void eliminarL(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_BajaProducto");
                datos.setearParametro("@ID", producto.Id);
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
        public void activar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActivarProducto");
                datos.setearParametro("@ID", producto.Id);
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
        public void agregar(Producto nuevoproducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ALTA_PRODUCTO");
                datos.setearParametro("@NOMBRE", nuevoproducto.Nombre);
                datos.setearParametro("@DESCRIPCION", nuevoproducto.Descripcion);
                datos.setearParametro("@IDMARCA", nuevoproducto.Marca.Id);
                datos.setearParametro("@IDCATEGORIA", nuevoproducto.Categoria.Id);
                datos.setearParametro("@IMAGENURL", nuevoproducto.Imagen.ImagenUrl);
                datos.setearParametro("@STOCKACTUAL", nuevoproducto.StockActual);
                datos.setearParametro("@STOCKMINIMO", nuevoproducto.StockMinimo);
                //datos.setearParametro("@PRECIOCOMPRA", nuevoproducto.Precio_Compra);
                //datos.setearParametro("@PRECIOVENTA", nuevoproducto.Precio_Venta);
                //datos.setearParametro("@PORCENTAJEGANANCIA", nuevoproducto.Porcentaje_Ganancia);
                //datos.setearParametro("@IDPROVEEDOR", nuevoproducto.Proveedores.First().Id);
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

        public void modificar(Producto modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_MODIFICAR_PRODUCTO");
                datos.setearParametro("ID", modificar.Id);
                datos.setearParametro("@NOMBRE", modificar.Nombre);
                datos.setearParametro("@DESCRIPCION", modificar.Descripcion);
                datos.setearParametro("@IDMARCA", modificar.Marca.Id);
                datos.setearParametro("@IDCATEGORIA", modificar.Categoria.Id);
                datos.setearParametro("@STOCKACTUAL", modificar.StockActual);
                datos.setearParametro("@STOCKMINIMO", modificar.StockMinimo);
                //datos.setearParametro("@PRECIOCOMPRA", modificar.Precio_Compra);
                //datos.setearParametro("@PRECIOVENTA", modificar.Precio_Venta);
                //datos.setearParametro("@PORCENTAJEGANANCIA", modificar.Porcentaje_Ganancia);
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

        public List<Producto> listarXProveedor(int idproveedor)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.ID, P.Nombre, P.Stock_Actual, P.Stock_Minimo, P.Precio_Compra, P.Activo FROM Productos AS P INNER JOIN Productos_x_Proveedores AS PXP ON PXP.IDProducto = P.ID WHERE P.ID = PXP.IDProducto AND PXP.IDProveedor =" + idproveedor + "AND P.Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.StockActual = (int)datos.Lector["Stock_Actual"];
                    aux.StockMinimo = (int)datos.Lector["Stock_Minimo"];
                    //aux.Precio_Compra = (decimal)datos.Lector["Precio_Compra"];
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
        public List<long> listarIDProducto(int proveedor)
        {
            List<long> lista = new List<long>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.ID, P.Stock_Actual, P.Stock_Minimo FROM Productos AS P INNER JOIN Productos_x_Proveedores AS PXP ON PXP.IDProducto = P.ID WHERE P.ID = PXP.IDProducto AND PXP.IDProveedor =" + proveedor + "AND P.Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();
                    aux.Id = (long)datos.Lector["ID"];
                    aux.StockActual = (int)datos.Lector["Stock_Actual"];
                    aux.StockMinimo = (int)datos.Lector["Stock_Minimo"];
                    if (aux.StockActual < aux.StockMinimo)
                    {
                        lista.Add(aux.Id);
                    }
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
    }
}
