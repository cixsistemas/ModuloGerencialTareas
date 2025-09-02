using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Datos
{
    public class DBoletosWeb
    {
        ////================================================
        //CADENA DE CONEXION
        private string ConexionRuta = ConfigurationManager.AppSettings.Get("CadenaConexion");
        //==================================================
        private SqlTransaction Transaccion;

        public List<EBoletosWeb> ListaBoletosAnuladosWeb(string Opcion, string FechaInicial, string FechaFinal, string NroTarjeta
       , string Numero, string Origen, string Destino)
        {
            List<EBoletosWeb> lista = new List<EBoletosWeb>();
            SqlConnection SqlConexion = null;

            try
            {
                using (SqlConexion = new SqlConnection(ConexionRuta))
                {
                    using (SqlCommand Command = new SqlCommand("[dbo].[BD_Pje_VentaWeb_Visa_EnvioCorreo]", SqlConexion))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                        Command.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                        Command.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                        Command.Parameters.Add(new SqlParameter("@NroTarjeta", NroTarjeta));
                        Command.Parameters.Add(new SqlParameter("@Numero", Numero));
                        Command.Parameters.Add(new SqlParameter("@Origen", Origen));
                        Command.Parameters.Add(new SqlParameter("@Destino", Destino));
                        SqlConexion.Open();

                        SqlDataReader reader = Command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.HasRows)//si reader tiene filas por leer
                            {
                                EBoletosWeb entidad = new EBoletosWeb();
                                entidad.BOLETO = Convert.ToString(reader["BOLETO"]);
                                entidad.F_COMPRA = Convert.ToString(reader["F_COMPRA"]);
                                entidad.PROGRAMACION = Convert.ToString(reader["PROGRAMACION"]);
                                entidad.RUTA = Convert.ToString(reader["RUTA"]);
                                entidad.ITINERARIO = Convert.ToString(reader["F_VIAJE"])
                                    + " - " + Convert.ToString(reader["HORA"]);
                                entidad.NOMBRE = Convert.ToString(reader["NOMBRE"]);
                                entidad.DNI = Convert.ToString(reader["DNI"]);
                                entidad.TELEFONO = Convert.ToString(reader["TELEFONO"]);
                                entidad.PRECIO = Convert.ToString(reader["PRECIO"]);
                                entidad.ASIENTO = Convert.ToString(reader["ASIENTO"]);
                                entidad.F_ANULA = Convert.ToString(reader["F_ANULA"]);
                                entidad.ESTADO = Convert.ToString(reader["ESTADO"]);
                                entidad.id_venta = Convert.ToInt32(reader["id_venta"]);
                                lista.Add(entidad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                SqlConexion.Close();
            }
            return lista;
        }


        //*** MANTENIMIENTO
        public void MatenimientoBoletosAnuladosWeb(List<EBoletosWeb> lst, string _opcion)
        {
            int respuesta = -1;
            string mensaje = "";

            using (SqlConnection SqlConexion = new SqlConnection(ConexionRuta))
            {
                SqlConexion.Open();
                Transaccion = SqlConexion.BeginTransaction();

                foreach (var oElement in lst)
                {
                    using (SqlCommand comando = new SqlCommand("[dbo].[BD_Pje_PaMantenimiento_BD_Pje_Auditoria_VentaWeb]", SqlConexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Transaction = Transaccion;

                        comando.Parameters.Add(new SqlParameter("@Boleta", oElement.BOLETO));
                        comando.Parameters.Add(new SqlParameter("@FechaCompra", oElement.F_COMPRA));
                        comando.Parameters.Add(new SqlParameter("@Programacion", oElement.PROGRAMACION));
                        comando.Parameters.Add(new SqlParameter("@Ruta", oElement.RUTA));
                        comando.Parameters.Add(new SqlParameter("@Itinerario", oElement.ITINERARIO));
                        comando.Parameters.Add(new SqlParameter("@Pasajero", oElement.NOMBRE));
                        comando.Parameters.Add(new SqlParameter("@Dni", oElement.DNI));
                        comando.Parameters.Add(new SqlParameter("@Telefono", oElement.TELEFONO));
                        comando.Parameters.Add(new SqlParameter("@Precio", oElement.PRECIO));
                        comando.Parameters.Add(new SqlParameter("@Asiento", oElement.ASIENTO));
                        comando.Parameters.Add(new SqlParameter("@FechaAnulacion", oElement.F_ANULA));
                        comando.Parameters.Add(new SqlParameter("@Estado", oElement.ESTADO));
                        comando.Parameters.Add(new SqlParameter("@id_venta", oElement.id_venta));

                        comando.Parameters.Add(new SqlParameter("@Opcion", _opcion));

                        comando.Parameters.Add(new SqlParameter("@Rpta", System.Data.SqlDbType.Int)).Value = 0;
                        comando.Parameters["@Rpta"].Direction = System.Data.ParameterDirection.Output;
                        comando.Parameters.Add(new SqlParameter("@Mensaje", System.Data.DbType.String)).Value = "";
                        comando.Parameters["@Mensaje"].Size = 300;
                        comando.Parameters["@Mensaje"].Direction = System.Data.ParameterDirection.Output;

                        comando.ExecuteNonQuery();
                        respuesta = (int)comando.Parameters["@Rpta"].Value;
                        mensaje = comando.Parameters["@Mensaje"].Value.ToString();

                    }
                }

                if (respuesta > 0)
                {
                    //ok = true;
                    Transaccion.Commit();
                    //MessageBox.Show(documento.Parameters["@pMensaje"].Value.ToString(), "Botica", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    //ok = false;
                    Transaccion.Rollback();
                    //MessageBox.Show(documento.Parameters["@pMensaje"].Value.ToString(), "Botica", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //if ((int)comando.Parameters["@Rpta"].Value > 0)
                //{
                //    Transaccion.Commit();
                //    //MessageBox.Show(comando.Parameters["@Mensaje"].Value.ToString(), "Botica", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //}
                //else
                //{
                //    Transaccion.Rollback();
                //    //MessageBox.Show(comando.Parameters["@Mensaje"].Value.ToString(), "Botica", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}

                SqlConexion.Close();

            }


        }

    }
}
