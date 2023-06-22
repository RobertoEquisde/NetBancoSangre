
using Microsoft.AspNetCore.Mvc;
using Usuario.Models;
using System.Data;
using RazorPDF;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;

namespace Usuario.Controllers;

//[Authorize]
public class BancoController : Controller
{
    private readonly IConfiguration _conf;
    public BancoController(IConfiguration conf)
    {
        this._conf = conf;
    }
    //==========================================PDF==========================================================
    public IActionResult PDF()
    {
          DataTable tbl1 = new DataTable();
                DataTable tbl2 = new DataTable();

                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                {
                    cnx.Open();

                    // Obtener datos de la primera tabla
                    String query1 = "select * from donante WHERE id_donante = " + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(query1, cnx);
                    adp1.Fill(tbl1);

                    // Obtener datos de la segunda tabla
                    String query2 = "select * from almacen Where id_almacen =" + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(query2, cnx);
                    adp2.Fill(tbl2);

                    cnx.Close();
                }

                // Crear un modelo de vista para pasar las dos tablas a la vista
                var viewModel = new Tuple<DataTable, DataTable>(tbl1, tbl2);

                return View(viewModel);
    }
     
    public ActionResult Export()
    {   
                DataTable tbl1 = new DataTable();
                DataTable tbl2 = new DataTable();

                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                {
                    cnx.Open();

                    // Obtener datos de la primera tabla
                    String query1 = "select * from donante WHERE id_donante = " + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(query1, cnx);
                    adp1.Fill(tbl1);

                    // Obtener datos de la segunda tabla
                    String query2 = "select * from almacen Where id_almacen =" + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(query2, cnx);
                    adp2.Fill(tbl2);

                    cnx.Close();
                }

                // Crear un modelo de vista para pasar las dos tablas a la vista
                var viewModel = new Tuple<DataTable, DataTable>(tbl1, tbl2);
       
        return new ViewAsPdf("PDF", viewModel)
        {
            FileName = Request.Cookies["UsuarioCookie"] +".pdf",
            PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
            
        };
        
    }

    public IActionResult Index()
    {
        if (Request.Cookies["UsuarioCookie"] != null)
        {
            string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
            if (rol == "usuario")
            {


                DataTable tbl1 = new DataTable();
                DataTable tbl2 = new DataTable();

                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                {
                    cnx.Open();

                    // Obtener datos de la primera tabla
                    String query1 = "select * from donante WHERE id_donante = " + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp1 = new MySqlDataAdapter(query1, cnx);
                    adp1.Fill(tbl1);

                    // Obtener datos de la segunda tabla
                    String query2 = "select * from citas Where id_citas =" + Request.Cookies["UsuarioCookie"] + ";";
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(query2, cnx);
                    adp2.Fill(tbl2);

                    cnx.Close();
                }

                // Crear un modelo de vista para pasar las dos tablas a la vista
                var viewModel = new Tuple<DataTable, DataTable>(tbl1, tbl2);

                return View(viewModel);
            }
            else if (rol == "admin")
            {
                return RedirectToAction("Index", "usuario");
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
    //====================================================================================================
    public IActionResult RegistroDonacion()
    {
        return View();
    }
    [HttpPost]
    public IActionResult RegistroDonacion(CitaViewModel model, AlmacenViewModel model2)
    {
        if (Request.Cookies["UsuarioCookie"] != null)
        {
            string rol = ObtenerRol(Convert.ToInt32(Request.Cookies["UsuarioCookie"]));
            if (rol == "usuario")
            {


                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
                {

                    cnx.Open();
                    string query = "INSERT INTO citas (fecha_cita, lugar_cita,id_donante) VALUES (@fecha_cita,@lugar_cita,@Id_donante);INSERT INTO almacen (tipo_sangre, fecha_expiracion, cantidad,id_donante) VALUES (@Tipo_sangre,@Fecha_expiracion, @cantidad,@Id_donante);";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);
                    string id_donante = Convert.ToString(Request.Cookies["UsuarioCookie"]);
                    //tabla citas
                    cmd.Parameters.AddWithValue("@fecha_cita", model.fecha_cita);
                    cmd.Parameters.AddWithValue("@lugar_cita", model.lugar_cita);
                    cmd.Parameters.AddWithValue("@Id_donante", id_donante);
                    //para tabla almacen
                    DateTime fecha_expiracion = Convert.ToDateTime(model2.fecha_expiracion);
                    fecha_expiracion = fecha_expiracion.AddDays(42);
                    string fecha_expiracion2 = Convert.ToString(fecha_expiracion);
                    string tipo_sangre = ObtenerTipoSangre();
                    int randomNumber = 0;
                    string cantidad = Convert.ToString(randomNumber = RandomNumberGenerator.GenerateRandomNumber());
                    cmd.Parameters.AddWithValue("@Tipo_sangre", tipo_sangre);
                    cmd.Parameters.AddWithValue("@Fecha_expiracion", fecha_expiracion2);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();

                }
                return RedirectToAction("Index");
            }
            else if (rol == "admin")
            {
                return RedirectToAction("Index", "usuario");
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


    //====================================================================================================
    private string ObtenerTipoSangre()
    {
        string tipo_sangre = "";
        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection")))
        {
            cnx.Open();
            string query = "SELECT tipo_sangre FROM donante WHERE id_donante = " + Request.Cookies["UsuarioCookie"] + ";";
            MySqlCommand cmd = new MySqlCommand(query, cnx);
            tipo_sangre = Convert.ToString(cmd.ExecuteScalar());
        }
        return tipo_sangre;
    }

    //====================================================================================================
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
