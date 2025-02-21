using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM VW_ListaProveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.CUIT = (long)datos.Lector["CUIT"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];
                    //aux.Direccion = (string)datos.Lector["Direccion"];
                    //aux.Correo = (string)datos.Lector["Correo"];
                    //aux.Telefono = (string)datos.Lector["Telefono"];
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

        public List<Proveedor> listar2()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM Proveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.CUIT = (long)datos.Lector["CUIT"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];
                    //aux.Direccion = (string)datos.Lector["Direccion"];
                    //aux.Correo = (string)datos.Lector["Correo"];
                    //aux.Telefono = (string)datos.Lector["Telefono"];
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
        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_Alta_Proveedor");
                datos.setearParametro("@CUIT", nuevo.CUIT);
                datos.setearParametro("@Siglas", nuevo.Siglas);
                //datos.setearParametro("@Nombre", nuevo.Nombre);
                //datos.setearParametro("@Direccion", nuevo.Direccion);
                //datos.setearParametro("@Correo", nuevo.Correo);
                //datos.setearParametro("@Telefono", nuevo.Telefono);
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

        public void eliminarL(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_BajaProveedor");
                datos.setearParametro("@ID", proveedor.Id);
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

        public void activar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ActivarProveedor");
                datos.setearParametro("@ID", proveedor.Id);
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

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ModificarProveedor");
                datos.setearParametro("@ID", proveedor.Id);
                datos.setearParametro("@CUIT", proveedor.CUIT);
                datos.setearParametro("@Siglas", proveedor.Siglas);
                //datos.setearParametro("@Nombre", proveedor.Nombre);
                //datos.setearParametro("@Direccion", proveedor.Direccion);
                //datos.setearParametro("@Correo", proveedor.Correo);
                //datos.setearParametro("@Telefono", proveedor.Telefono);
                datos.setearParametro("@Activo", proveedor.Activo);
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
        public void agregarProducto(long idproducto, int idproveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_PRODUCT_X_PROV2");
                datos.setearParametro("@IDPRODUCTO", idproducto);
                datos.setearParametro("@IDPROVEEDOR", idproveedor);
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
        public List<Proveedor> listarxid(long codigoproducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ID,Siglas FROM Proveedores WHERE ID IN (SELECT IDProveedor FROM Productos_x_Proveedores WHERE IDProducto = " + codigoproducto + " AND Activo=1)");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
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
        public List<Proveedor> listarProvSinProductoAsociado(long codigoproducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ID,Siglas FROM Proveedores WHERE Activo = 1 AND ID NOT IN (SELECT IDProveedor FROM Productos_x_Proveedores WHERE IDProducto = " + codigoproducto + ")");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
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

        //public List<Proveedor> filtrar(string campo, string criterio, string filtro, string estado)
        //{
        //    List<Proveedor> lista = new List<Proveedor>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        string consulta = "select CUIT, Siglas, Nombre, Direccion, Correo, Telefono, Activo FROM Proveedores WHERE";

        //        if (campo == "CUIT")
        //        {
        //            switch (criterio)
        //            {
        //                case "Mayor a":
        //                    consulta += " CUIT > " + filtro;
        //                    break;
        //                case "Menor a":
        //                    consulta += " CUIT <" + filtro;
        //                    break;
        //                default:
        //                    consulta += " CUIT =" + filtro;
        //                    break;
        //            }
        //        }
        //        else if (campo == "Siglas")
        //        {
        //            switch (criterio)
        //            {
        //                case "Comienza con":
        //                    consulta += " Siglas LIKE '" + filtro + "%' ";
        //                    break;
        //                case "Termina con":
        //                    consulta += " Siglas LIKE '%" + filtro + "'";
        //                    break;
        //                default:
        //                    consulta += " Siglas LIKE '%" + filtro + "%'";
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            switch (campo)
        //            {
        //                case "Comienza con":
        //                    consulta += " Correo LIKE '" + filtro + "%' ";
        //                    break;
        //                case "Termina con":
        //                    consulta += " Correo LIKE '%" + filtro + "'";
        //                    break;
        //                default:
        //                    consulta += " Correo LIKE '%" + filtro + "%'";
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
        //            Proveedor aux = new Proveedor();
        //            aux.CUIT = (long)datos.Lector["CUIT"];
        //            aux.Siglas = (string)datos.Lector["Siglas"];
        //            aux.Nombre = (string)datos.Lector["Nombre"];
        //            aux.Direccion = (string)datos.Lector["Direccion"];
        //            aux.Correo = (string)datos.Lector["Correo"];
        //            aux.Telefono = (string)datos.Lector["Telefono"];
        //            aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

        //            lista.Add(aux);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public List<Proveedor> filtrar(string campo, string criterio, string filtro, string estado)
        //{
        //    List<Proveedor> lista = new List<Proveedor>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        // Iniciar la consulta
        //        string consulta = "select CUIT, Siglas, Nombre, Direccion, Correo, Telefono, Activo FROM Proveedores WHERE 1=1";

        //        // Agregar condición del campo y criterio solo si ambos están presentes
        //        if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(criterio) && !string.IsNullOrEmpty(filtro))
        //        {
        //            if (campo == "CUIT")
        //            {
        //                switch (criterio)
        //                {
        //                    case "Mayor a":
        //                        consulta += " AND CUIT > " + filtro;
        //                        break;
        //                    case "Menor a":
        //                        consulta += " AND CUIT < " + filtro;
        //                        break;
        //                    default:
        //                        consulta += " AND CUIT = " + filtro;
        //                        break;
        //                }
        //            }
        //            else if (campo == "Siglas")
        //            {
        //                switch (criterio)
        //                {
        //                    case "Comienza con":
        //                        consulta += " AND Siglas LIKE '" + filtro + "%'";
        //                        break;
        //                    case "Termina con":
        //                        consulta += " AND Siglas LIKE '%" + filtro + "'";
        //                        break;
        //                    default:
        //                        consulta += " AND Siglas LIKE '%" + filtro + "%'";
        //                        break;
        //                }
        //            }
        //            else if (campo == "Correo")
        //            {
        //                switch (criterio)
        //                {
        //                    case "Comienza con":
        //                        consulta += " AND Correo LIKE '" + filtro + "%'";
        //                        break;
        //                    case "Termina con":
        //                        consulta += " AND Correo LIKE '%" + filtro + "'";
        //                        break;
        //                    default:
        //                        consulta += " AND Correo LIKE '%" + filtro + "%'";
        //                        break;
        //                }
        //            }
        //        }

        //        // Agregar condición de estado, sin importar si los otros filtros están vacíos
        //        if (estado == "Activo")
        //            consulta += " AND Activo = 1";
        //        else if (estado == "Inactivo")
        //            consulta += " AND Activo = 0";

        //        datos.setearConsulta(consulta);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Proveedor aux = new Proveedor();
        //            aux.CUIT = (long)datos.Lector["CUIT"];
        //            aux.Siglas = (string)datos.Lector["Siglas"];
        //            aux.Nombre = (string)datos.Lector["Nombre"];
        //            aux.Direccion = (string)datos.Lector["Direccion"];
        //            aux.Correo = (string)datos.Lector["Correo"];
        //            aux.Telefono = (string)datos.Lector["Telefono"];
        //            aux.Activo = (bool)datos.Lector["Activo"];

        //            lista.Add(aux);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Proveedor> filtrar(string campo, string criterio, string filtro)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Iniciar la consulta
                string consulta = "select ID, CUIT, Siglas, Nombre, Direccion, Correo, Telefono, Activo FROM Proveedores WHERE 1=1";

                // Agregar condición del campo y criterio solo si ambos están presentes
                if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(criterio) && !string.IsNullOrEmpty(filtro))
                {
                    if (campo == "CUIT")
                    {
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += " AND CUIT > " + filtro;
                                break;
                            case "Menor a":
                                consulta += " AND CUIT < " + filtro;
                                break;
                            default:
                                consulta += " AND CUIT = " + filtro;
                                break;
                        }
                    }
                    else if (campo == "Siglas")
                    {
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += " AND Siglas LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " AND Siglas LIKE '%" + filtro + "'";
                                break;
                            default:
                                consulta += " AND Siglas LIKE '%" + filtro + "%'";
                                break;
                        }
                    }
                    else if (campo == "Correo")
                    {
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += " AND Correo LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " AND Correo LIKE '%" + filtro + "'";
                                break;
                            default:
                                consulta += " AND Correo LIKE '%" + filtro + "%'";
                                break;
                        }
                    }
                }



                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CUIT = (long)datos.Lector["CUIT"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];
                    //aux.Direccion = (string)datos.Lector["Direccion"];
                    //aux.Correo = (string)datos.Lector["Correo"];
                    //aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Proveedor> filtrarEstado(string estado)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select ID, CUIT, Siglas, Nombre, Direccion, Correo, Telefono, Activo FROM Proveedores";

                if (estado == "Activo")
                    consulta += " WHERE Activo = 1 ";
                else if (estado == "Inactivo")
                    consulta += " WHERE Activo = 0";
                else if (estado == "Todos")
                    consulta += " select * FROM Proveedores";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CUIT = (long)datos.Lector["CUIT"];
                    aux.Siglas = (string)datos.Lector["Siglas"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];
                    //aux.Direccion = (string)datos.Lector["Direccion"];
                    //aux.Correo = (string)datos.Lector["Correo"];
                    //aux.Telefono = (string)datos.Lector["Telefono"];
                    //aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existeCUITProveedor(long cuit)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ExisteCUITProveedor"); // Nombre del procedimiento almacenado
                datos.setearParametro("@CUIT", cuit);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = datos.Lector.GetInt32(0);
                    return count > 0; // Retorna true si el CUIT ya existe
                }

                return false; // Retorna false si no se encontró el CUIT
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

        public bool existeCUITProveedorModificado(long cuit, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("SP_ExisteCUITProveedorModificado"); // Nombre del procedimiento almacenado
                datos.setearParametro("@CUIT", cuit);
                datos.setearParametro("@IDProveedor", idProveedor);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = datos.Lector.GetInt32(0);
                    return count > 0; // Retorna true si el CUIT ya existe para otro proveedor
                }

                return false; // Retorna false si no se encontró el DNI duplicado
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
