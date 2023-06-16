
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
        private string ObtenerRol(int id)
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
