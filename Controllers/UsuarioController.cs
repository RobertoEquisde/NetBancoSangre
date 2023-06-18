
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
        public IActionResult Registro(UsuarioViewModel model , DonanteViewModel model2)
        {
            using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
            {
                cnx.Open();
                string query = "INSERT INTO Usuario (curp, contrasena, rol) VALUES (@Curp,@Contrasena, @Rol);";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@Curp", model.curp);
                cmd.Parameters.AddWithValue("@Contrasena", model.contrasena);
                cmd.Parameters.AddWithValue("@Rol", model.rol);
                
                /*
                cmd.Parameters.AddWithValue("@Tipo_sangre", model2.tipo_sangre);
                cmd.Parameters.AddWithValue("@Nombre", model2.nombre);
                cmd.Parameters.AddWithValue("@Apellidos", model2.apellidos);
                cmd.Parameters.AddWithValue("@Anio_nacimiento", model2.anio_nacimiento);
                */
                

                cmd.ExecuteNonQuery();
               
            }
            return RedirectToAction("Index");

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
                            string query = "INSERT INTO Usuario (curp, correo, contrasena,rol) VALUES (@Curp, @Contrasena, @Rol)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Curp", model.curp);
                            cmd.Parameters.AddWithValue("@Contrasena", model.contrasena);
                            cmd.Parameters.AddWithValue("@Rol", model.rol);
                            cmd.ExecuteNonQuery();
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
                    UsuarioViewModel model = new UsuarioViewModel();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM usuario WHERE id_usuario = @id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@id", id);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            model.id_usuario = Convert.ToInt32(reader["id_usuario"]);
                            model.curp = reader["curp"].ToString();
                            model.contrasena = reader["contrasena"].ToString();
                            model.rol = reader["rol"].ToString();
                        }
                        reader.Close();
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
                            string query = "INSERT INTO donante(nombre,apellidos,anio_nacimiento,donacion_realizada) VALUES (@Nombre,@Apellidos, @Anio_Nacimiento, @Donacion_Realizada)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre", model.nombre);
                            cmd.Parameters.AddWithValue("@Apellidos", model.apellidos);
                            cmd.Parameters.AddWithValue("@Anio_Nacimiento", model.anio_nacimiento);
                            cmd.Parameters.AddWithValue("@Donacion_Realizada", model.donacion_realizada);
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
        public IActionResult EditarDonantes(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    DonanteViewModel model = new DonanteViewModel();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM donante WHERE id_donante = @id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@id", id);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            model.id_donante = Convert.ToInt32(reader["id_usuario"]);
                            model.tipo_sangre = reader["tipo_sangre"].ToString();
                            model.nombre = reader["nombre"].ToString();
                            model.apellidos = reader["apellidos"].ToString();
                            model.anio_nacimiento = reader["anio_nacimiento"].ToString();
                            model.donacion_realizada = Convert.ToInt32(reader["donacion_realizada"]);

                        }
                        reader.Close();
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
                            string query = "UPDATE donante SET nombre = @Nombre,  apellidos = @Apellidos, tipo_sangre = @Tipo_Sangre,anio_nacimiento = @Anio_nacimiento,donacion_ralizada = @Donacion_realizada WHERE id_donante = @id";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre", model.nombre);
                            cmd.Parameters.AddWithValue("@Apellidos", model.apellidos);
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.tipo_sangre);
                            cmd.Parameters.AddWithValue("@Anio_nacimiento", model.anio_nacimiento);
                            cmd.Parameters.AddWithValue("@Donacion_realizada", model.donacion_realizada);
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
                            string query = "INSERT INTO almacen(tipo_sangre,fecha_expiracion,cantidad) VALUES (@Tipo_sangre,@Fecha_expiracion, @Cantidad)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Tipo_sangre", model.almacen_tipo_sangre);
                            cmd.Parameters.AddWithValue("@Fecha_expiracion", model.fecha_expiracion);
                            cmd.Parameters.AddWithValue("@Cantidad", model.cantidad);
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
                    AlmacenViewModel model = new AlmacenViewModel();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM almacen WHERE id_almacen = @id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@id", id);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            model.id_almacen = Convert.ToInt32(reader["id_almacen"]);
                            model.almacen_tipo_sangre = reader["tipo_sangre"].ToString();
                            model.fecha_expiracion = reader["fecha_expiracion"].ToString();
                            model.cantidad = Convert.ToInt32(reader["cantidad"]);

                        }
                        reader.Close();
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

        [HttpPost]
        public IActionResult EditarAlmacen(AlmacenViewModel model)
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
                            string query = "INSERT INTO hospitales(nombre_hospital,direccion_hospital) VALUES (@Nombre_hospita,direccion_hospital)";
                            MySqlCommand cmd = new MySqlCommand(query, cnx);
                            cmd.Parameters.AddWithValue("@Nombre_hospital", model.nombre_hospital);
                            cmd.Parameters.AddWithValue("@Direccion_hospital", model.direccion_hospital);
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
        public IActionResult EditarHospitales(int id)
        {
            if (Request.Cookies["UsuarioCookie"] != null)
            {
                string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
                if (rol == "admin")
                {
                    HospitalesViewModel model = new HospitalesViewModel();
                    using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                    {
                        cnx.Open();
                        string query = "SELECT * FROM hospitales WHERE id_hospitales = @id";
                        MySqlCommand cmd = new MySqlCommand(query, cnx);
                        cmd.Parameters.AddWithValue("@id", id);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            model.id_hospital = Convert.ToInt32(reader["id_hospitales"]);
                            model.nombre_hospital = reader["nombre_hospital"].ToString();
                            model.direccion_hospital = reader["direccion_hospital"].ToString();


                        }
                        reader.Close();
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
