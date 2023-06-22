
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Usuario.Models;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
namespace Usuarios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _conf;
        public UsuarioController(IConfiguration conf)
        {
            this._conf = conf;
        }


        public IActionResult Index()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {


                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from usuario";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(UsuarioViewModel model, DonanteViewModel model2)
        {
            using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
            {

                cnx.Open();
                string query = "INSERT INTO Usuario (curp, contrasena, rol) VALUES (@Curp,@Contrasena, @Rol);INSERT INTO donante (tipo_sangre, nombre, apellidos,anio_nacimiento,donacion_realizada,id_usuario) VALUES (@Tipo_sangre,@Nombre, @Apellidos,@Anio_nacimiento,@Donacion_realizada,@Id_usuario);";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                //tabla usuario
                cmd.Parameters.AddWithValue("@Curp", model.curp);
                cmd.Parameters.AddWithValue("@Contrasena", model.contrasena);
                cmd.Parameters.AddWithValue("@Rol", "usuario");
                //para tabla donante
                string idUsuario = Convert.ToString(ObtenerIdUsuario());
                cmd.Parameters.AddWithValue("@Tipo_sangre", model2.tipo_sangre);
                cmd.Parameters.AddWithValue("@Nombre", model2.nombre);
                cmd.Parameters.AddWithValue("@Apellidos", model2.apellidos);
                cmd.Parameters.AddWithValue("@Anio_nacimiento", model2.anio_nacimiento);
                cmd.Parameters.AddWithValue("@Donacion_realizada", "0");
                cmd.Parameters.AddWithValue("@Id_usuario", idUsuario);
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");

        }
        private int ObtenerIdDonante()
        {
            int idDonante = 0; // Valor predeterminado en caso de que no se pueda obtener el id

            // Lógica para obtener el valor de id_donante desde tu fuente de datos
            // Aquí puedes utilizar la lógica adecuada según tu caso, ya sea una consulta a la base de datos u otra fuente de datos

            try
            {
                // Ejemplo: Consulta a la base de datos para obtener el último id_donante insertado
                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                {
                    cnx.Open();
                    string query = "SELECT MAX(id_donante) FROM donante;";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);
                    idDonante = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de error al obtener el id_donante
                Console.WriteLine("Error al obtener el id_donante: " + ex.Message);
            }

            return idDonante;
        }

        private int ObtenerIdUsuario()
        {
            int idDonante = ObtenerIdDonante(); // Obtener el id_donante

            // Asignar el valor de id_donante al id_usuario
            int idUsuario = idDonante + 1;

            return idUsuario;
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(UsuarioViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "INSERT INTO Usuario (curp, contrasena,rol) VALUES (@Curp, @Contrasena, @Rol)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Curp", model.curp);
                            cmd.Parameters.AddWithValue("@Contrasena", model.contrasena);
                            cmd.Parameters.AddWithValue("@Rol", model.rol);

                            cmd.ExecuteNonQuery();
                            cnx.Close();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Editar(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM usuario WHERE id_usuario = @ID";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UsuarioViewModel model = new UsuarioViewModel
                                {
                                    id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                    curp = reader["curp"].ToString(),
                                    contrasena = reader["contrasena"].ToString(),
                                    rol = reader["rol"].ToString()
                                };

                                return View(model);
                            }
                        }
                    }
                    return RedirectToAction("index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Editar(UsuarioViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "UPDATE usuario SET curp = @Curp,  contrasena = @Contrasena, rol = @Rol WHERE id_usuario = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Curp", model.curp);
                            cmd.Parameters.AddWithValue("@Contrasena", model.contrasena);
                            cmd.Parameters.AddWithValue("@Rol", model.rol);
                            cmd.Parameters.AddWithValue("@id", model.id_usuario);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Eliminar(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "DELETE FROM Usuario WHERE id_usuario = @Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //=======================DONANTES========================
        public IActionResult Donantes()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from donante";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AnadirDonantes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AnadirDonantes(DonanteViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string id_usuario = Convert.ToString(ObtenerIdUsuario());
                            string query = "INSERT INTO donante(tipo_sangre,nombre,apellidos,anio_nacimiento,donacion_realizada,id_usuario) VALUES (@Tipo_sangre,@Nombre,@Apellidos, @Anio_Nacimiento, @Donacion_Realizada,@id_usuario)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.tipo_sangre);
                            cmd.Parameters.AddWithValue("@Nombre", model.nombre);
                            cmd.Parameters.AddWithValue("@Apellidos", model.apellidos);
                            cmd.Parameters.AddWithValue("@Anio_Nacimiento", model.anio_nacimiento);
                            cmd.Parameters.AddWithValue("@Donacion_Realizada", model.donacion_realizada);
                            cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                            cmd.ExecuteNonQuery();
                            cnx.Close();
                        }
                        return RedirectToAction("Donantes");
                    }
                    return View(model);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EditarDonantes(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM donante WHERE id_donante = @ID";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DonanteViewModel model = new DonanteViewModel
                                {
                                    id_donante = Convert.ToInt32(reader["id_donante"]),
                                    nombre = reader["nombre"].ToString(),
                                    apellidos = reader["apellidos"].ToString(),
                                    tipo_sangre = reader["tipo_sangre"].ToString(),
                                    anio_nacimiento = Convert.ToDateTime(reader["anio_nacimiento"]),
                                    donacion_realizada = Convert.ToInt32(reader["donacion_realizada"])
                                };

                                return View(model);
                            }
                        }
                    }
                    return RedirectToAction("donantes");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarDonante(DonanteViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "UPDATE donante SET nombre = @Nombre,  apellidos = @Apellidos, tipo_sangre = @Tipo_Sangre,anio_nacimiento = @Anio_nacimiento,donacion_realizada = @Donacion_realizada WHERE id_donante = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre", model.nombre);
                            cmd.Parameters.AddWithValue("@Apellidos", model.apellidos);
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.tipo_sangre);
                            cmd.Parameters.AddWithValue("@Anio_nacimiento", model.anio_nacimiento);
                            cmd.Parameters.AddWithValue("@Donacion_realizada", model.donacion_realizada);
                            cmd.Parameters.AddWithValue("@id", model.id_donante);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("donantes");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EliminarDonante(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "DELETE FROM donante WHERE id_donante = @Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //====================ALMACEN=========================================
        public IActionResult Almacenamiento()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from almacen";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AnadirAlmacenamiento()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AnadirAlmacenamiento(AlmacenViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "INSERT INTO almacen(tipo_sangre,fecha_expiracion,cantidad,id_donante) VALUES (@Tipo_sangre,@Fecha_expiracion, @Cantidad,@Id_donante)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            string id_donante = Convert.ToString(ObtenerIdDonante());
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.almacen_tipo_sangre);
                            cmd.Parameters.AddWithValue("@Fecha_expiracion", model.fecha_expiracion);
                            cmd.Parameters.AddWithValue("@Cantidad", model.cantidad);
                            cmd.Parameters.AddWithValue("@Id_donante", id_donante);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(model);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EditarAlmacenamiento(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM almacen WHERE id_almacen = @ID";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AlmacenViewModel model = new AlmacenViewModel
                                {
                                    id_almacen = Convert.ToInt32(reader["id_almacen"]),
                                    almacen_tipo_sangre = Convert.ToString(reader["tipo_sangre"]),
                                    fecha_expiracion = Convert.ToDateTime(reader["fecha_expiracion"]),
                                    cantidad = Convert.ToInt32(reader["cantidad"])
                                };

                                return View(model);
                            }
                        }
                    }
                    return RedirectToAction("almacenamiento");

                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarAlmacenamiento(AlmacenViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "UPDATE almacen SET tipo_sangre = @Tipo_sangre,  fecha_expiracion = @Fecha_expiracion, cantidad = @Cantidad WHERE id_almacen = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.almacen_tipo_sangre);
                            cmd.Parameters.AddWithValue("@Fecha_expiracion", model.fecha_expiracion);
                            cmd.Parameters.AddWithValue("@Cantidad", model.cantidad);
                            cmd.Parameters.AddWithValue("@id", model.id_almacen);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("almacenamiento");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EliminarAlmacenamiento(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "DELETE FROM almacen WHERE id_almacen = @Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //============================HOSPITALES==================================
        public IActionResult Hospitales()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from hospitales";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AnadirHospitales()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AnadirHospitales(HospitalesViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "INSERT INTO hospitales(nombre_hospital,direccion_hospital) VALUES (@Nombre_hospital,@Direccion_hospital)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre_hospital", model.nombre_hospital);
                            cmd.Parameters.AddWithValue("@Direccion_hospital", model.direccion_hospital);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("hospitales");
                    }
                    return View(model);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EditarHospitales(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM hospitales WHERE id_hospitales = @ID";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HospitalesViewModel model = new HospitalesViewModel
                                {
                                    id_hospitales = Convert.ToInt32(reader["id_hospitales"]),
                                    nombre_hospital = reader["nombre_hospital"].ToString(),
                                    direccion_hospital = reader["direccion_hospital"].ToString()
                                };

                                return View(model);
                            }
                        }
                    }
                    return RedirectToAction("hospitales");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarHospital(HospitalesViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "UPDATE hospitales SET nombre_hospital = @Nombre_hospital,  direccion_hospital = @Direccion_hospital WHERE id_hospitales = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre_hospital", model.nombre_hospital);
                            cmd.Parameters.AddWithValue("@Direccion_hospital", model.direccion_hospital);
                            cmd.Parameters.AddWithValue("@id", model.id_hospitales);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("hospitales");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EliminarHospital(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "DELETE FROM hospitales WHERE id_hospitales = @Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //==========================================CITAS====================================================================
        public IActionResult Citas()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from citas";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AnadirCitas()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AnadirCitas(CitaViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "INSERT INTO citas(fecha_cita,lugar_cita,id_donante) VALUES (@Fecha_cita,@Lugar_cita,@Id_donante)";
                            string id_donante = Convert.ToString(ObtenerIdDonante());
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Fecha_cita", model.fecha_cita);
                            cmd.Parameters.AddWithValue("@Lugar_cita", model.lugar_cita);
                            cmd.Parameters.AddWithValue("@Id_donante", id_donante);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("citas");
                    }
                    return View(model);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EditarCitas(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM citas WHERE id_citas = @ID";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CitaViewModel model = new CitaViewModel
                                {
                                    id_cita = Convert.ToInt32(reader["id_citas"]),
                                    fecha_cita = Convert.ToDateTime(reader["fecha_cita"]),
                                    lugar_cita = Convert.ToString(reader["lugar_cita"])
                                };

                                return View(model);
                            }
                        }
                    }
                    return RedirectToAction("citas");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarCitas(CitaViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "UPDATE citas SET fecha_cita = @Fecha_cita,  lugar_cita = @Lugar_cita WHERE id_citas = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Fecha_cita", model.fecha_cita);
                            cmd.Parameters.AddWithValue("@Lugar_cita", model.lugar_cita);
                            cmd.Parameters.AddWithValue("@id", model.id_cita);
                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("citas");
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult EliminarCita(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "DELETE FROM citas WHERE id_cita = @Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "banco");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //=======================================Donacion=====================================================================
        public IActionResult Donacion()
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DataTable tbl = new DataTable();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        String query = "select * from donacion";
                        MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
                        adp.Fill(tbl);
                        cnx.Close();
                    }
                    return View(tbl);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AnadirDonacion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AnadirDonacion(DonacionViewModel model)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    if (ModelState.IsValid)
                    {
                        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                        {
                            cnx.Open();
                            string query = "INSERT INTO donacion(id_donante, id_almacen, id_hospitales, fecha_envio) VALUES (@Id_donante, @Id_almacen, @Id_hospitales, @Fecha_envio)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Id_donante", model.id_donante );
                            cmd.Parameters.AddWithValue("@Id_almacen",model.id_almacen );
                            cmd.Parameters.AddWithValue("@Id_hospitales", model.id_hospitales);
                            cmd.Parameters.AddWithValue("@Fecha_envio", model.fecha_envio);


                            cmd.ExecuteNonQuery();
                        }
                        return RedirectToAction("citas");
                    }
                    return View(model);
                }
                else if (rol == "usuario")
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //=======================================LOGIN=======================================================================
        [HttpPost]
        public IActionResult Login(string curp, string contrasena)
        {
            if (ModelState.IsValid)
            {
                // Verificar la autenticación del usuario
                if (ValidarCredenciales(curp, contrasena) != -1)
                {
                    // Crear una cookie para almacenar los datos del usuario
                    ViewData["cookie"] = curp + contrasena;
                    var cookieOptions = new CookieOptions
                    {
                        // Configurar opciones de la cookie si es necesario
                        Expires = DateTime.Now.AddDays(7) // Por ejemplo, expira en 7 días
                    };

                    // Guardar los datos del usuario en la cookie
                    Response.Cookies.Append("UsuarioCookie", ValidarCredenciales(curp, contrasena).ToString());

                    return RedirectToAction("Index"); // Redireccionar a la página de inicio
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciales inválidas. Por favor, inténtalo de nuevo.");
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            // Eliminar la cookie de usuario al cerrar sesión
            Response.Cookies.Delete("UsuarioCookie");

            return RedirectToAction("Index"); // Redireccionar a la página de inicio
        }

        private int ValidarCredenciales(string curp, string contrasena)
        {
            // Aquí puedes realizar la lógica de validación de las credenciales
            // Conectarte a la base de datos y comparar el correo y la contraseña con los registros de usuarios

            // Ejemplo básico de validación (solo como referencia, debes implementar tu propia lógica):
            using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
            {
                cnx.Open();
                string query = "SELECT id_usuario FROM usuario WHERE curp = @Curp AND contrasena = @Contrasena";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@Curp", curp);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
        public string ObtenerRol(int id)
        {
            string rol = "";
            // Aquí puedes realizar la lógica de validación de las credenciales
            // Conectarte a la base de datos y comparar el correo y la contraseña con los registros de usuarios

            // Ejemplo básico de validación (solo como referencia, debes implementar tu propia lógica):
            using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
            {
                cnx.Open();
                string query = "SELECT rol FROM usuario WHERE id_usuario = @id";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", id);
                object result = cmd.ExecuteScalar();

                // Verificar si se obtuvo un resultado y realizar las acciones necesarias
                if (result != null)
                {
                    rol = result.ToString();
                    // Hacer algo con el rol obtenido
                }
                return rol;
            }
        }
    }
}
