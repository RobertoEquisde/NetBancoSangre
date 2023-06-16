using Microsoft.AspNetCore.Mvc;
using banco_sangre.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using  System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
namespace banco_sangre.Controllers;

//[Authorize]
public class BancoController: Controller{
    private readonly IConfiguration _configuration;
    public BancoController(IConfiguration configuration)
    {
       this._configuration  = configuration;
    }
    public IActionResult Index()
    {
      return View();
    }
       public IActionResult Almacenamiento(){
           DataTable tbl = new DataTable();
        using(MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection"))){
            conn.Open();
            MySqlDataAdapter adaptador = new MySqlDataAdapter("SELECT * FROM Almacenamiento",conn);
            adaptador.Fill(tbl);
        }
        return View(tbl);
    }
     public IActionResult AnadirAlmacenamiento()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AnadirAlmacenamiento(AlmacenamientoViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                conn.Open();
                string query = "INSERT INTO Almacenamiento(tipo_sangre,cantidad,fecha_expiracion)" +
                            "VALUES (@tipo_sangre, @cantidad, @fecha_expiracion)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tipo_sangre", model.tipo_sangre);
                 cmd.Parameters.AddWithValue("@cantidad", model.cantidad);
                cmd.Parameters.AddWithValue("@fecha_expiracion", model.fecha_expiracion);
               
        

               
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }
    public IActionResult EditarAlmacenamiento(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            conn.Open();
            string query = "SELECT * FROM Almacenamiento WHERE almacenamiento_id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    AlmacenamientoViewModel model = new AlmacenamientoViewModel
                    {
                        almacenamiento_id = Convert.ToInt32(reader["almacenamiento_id"]),
                        tipo_sangre = Convert.ToString(reader["tipo_sangre"]),
                        cantidad = Convert.ToInt32(reader["cantidad"]),
                        fecha_expiracion = Convert.ToString(reader["fecha_expiracion"])
                      
                    };

                    return View(model);
                }
            }
        }
        return RedirectToAction("Index");
    }
     [HttpPost]
    public IActionResult EditarAlmacenamiento(AlmacenamientoViewModel model)  
    {
        
        if(ModelState.IsValid)
        {
            using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                    cnx.Open();
                    string query = "UPDATE almacenamiento SET tipo_sangre = @Tipo_sangre , cantidad = @Cantidad ,fecha_expiracion = @Fecha_expiracion  WHERE almacenamiento_id = @Almacenamiento_id";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);

                        cmd.Parameters.AddWithValue("@Tipo_Sangre", model.tipo_sangre);
                        cmd.Parameters.AddWithValue("@Cantidad", model.cantidad);
                        cmd.Parameters.AddWithValue("@Fecha_expiracion", model.fecha_expiracion);
                        cmd.Parameters.AddWithValue("@Almacenamiento_id", model.almacenamiento_id);
                        cmd.ExecuteNonQuery();
                    
            }
                      
            return RedirectToAction("Index");
          
        }
        
         return View(model);
    }
    public IActionResult EliminarAlmacenamiento(int id)
    {
        using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            cnx.Open();
            string query = "DELETE FROM almacenamiento WHERE almacenamiento_id = @almacenamiento_id";
            MySqlCommand cmd = new MySqlCommand(query, cnx);
            cmd.Parameters.AddWithValue("@almacenamiento_id", id);
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

        return RedirectToAction("Index");
    }
     public IActionResult Donacion(){
           DataTable tbl = new DataTable();
        using(MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection"))){
            conn.Open();
            MySqlDataAdapter adaptador = new MySqlDataAdapter("SELECT * FROM donacion",conn);
            adaptador.Fill(tbl);
        }
        return View(tbl);
    }
    public IActionResult AnadirDonacion()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AnadirDonacion(DonacionViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                conn.Open();
                string query = "INSERT INTO donacion(tipo_sangre,fecha, hora,cantidad,ubicacion)" +
                            "VALUES (@tipo_sangre, @fecha, @hora, @cantidad,@ubicacion )";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tipo_sangre", model.tipo_sangre);
            
                cmd.Parameters.AddWithValue("@fecha", model.fecha);
                cmd.Parameters.AddWithValue("@hora", model.hora);
                cmd.Parameters.AddWithValue("@cantidad", model.cantidad);
                 cmd.Parameters.AddWithValue("@ubicacion", model.ubicacion);

               
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }
    public IActionResult EditarDonancion(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            conn.Open();
            string query = "SELECT * FROM donacion WHERE donacion_id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    DonacionViewModel model = new DonacionViewModel
                    {
                        donante_id = Convert.ToInt32(reader["donacion_id"]),
                        tipo_sangre = Convert.ToString(reader["tipo_sangre"]),
                        fecha = Convert.ToString(reader["fecha"]),
                        hora = Convert.ToString(reader["hora"]),
                        cantidad = Convert.ToInt32(reader["cantidad"]),
                        ubicacion = Convert.ToString(reader["ubicacion"])
                    };

                    return View(model);
                }
            }
        }
        return RedirectToAction("Index");
    }
     [HttpPost]
    public IActionResult EditarDonacion(DonacionViewModel model)  
    {
        
        if(ModelState.IsValid)
        {
            using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                    cnx.Open();
                    string query = "UPDATE donacion SET tipo_sangre = @Tipo_sangre ,  fecha = @Fecha , hora = @Hora ,cantidad = @Cantidad , ubicacion = @Ubicacion  WHERE donacion_id = @Donacion_id";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);

                        cmd.Parameters.AddWithValue("@Tipo_Sangre", model.tipo_sangre);
                        cmd.Parameters.AddWithValue("@Fecha", model.fecha);
                        cmd.Parameters.AddWithValue("@Hora", model.hora);
                        cmd.Parameters.AddWithValue("@Cantidad", model.tipo_sangre);
                        cmd.Parameters.AddWithValue("@Ubicacion", model.ubicacion);
                        cmd.Parameters.AddWithValue("@Donacion_id", model.donacion_id);
                        cmd.ExecuteNonQuery();
                    
            }
                      
            return RedirectToAction("Index");
          
        }
        
         return View(model);
    }
    public IActionResult EliminarDonacion(int id)
    {
        using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            cnx.Open();
            string query = "DELETE FROM donacion WHERE donacion_id = @donacion_id";
            MySqlCommand cmd = new MySqlCommand(query, cnx);
            cmd.Parameters.AddWithValue("@donacion_id", id);
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

        return RedirectToAction("Index");
    }
    public IActionResult Donantes(){
           DataTable tbl = new DataTable();
        using(MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection"))){
            conn.Open();
            MySqlDataAdapter adaptador = new MySqlDataAdapter("SELECT * FROM donante",conn);
            adaptador.Fill(tbl);
        }
        return View(tbl);
    }
    public IActionResult AnadirDonantes()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AnadirDonantes(DonanteViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                conn.Open();
                string query = "INSERT INTO donante(nombre,edad, genero,tipo_sangre,numero_contacto,direccion)" +
                            "VALUES (@Nombre, @Edad, @Genero, @Tipo_sangre,@Numero_contacto , @Direccion )";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", model.nombre);
            
                cmd.Parameters.AddWithValue("@Edad", model.edad);
                cmd.Parameters.AddWithValue("@Genero", model.genero);
                cmd.Parameters.AddWithValue("@Tipo_sangre", model.tipo_sangre);
                 cmd.Parameters.AddWithValue("@Numero_contacto", model.numero_contacto);
                cmd.Parameters.AddWithValue("@Direccion", model.direccion);
               
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }
    public IActionResult EditarDonantes(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            conn.Open();
            string query = "SELECT * FROM donante WHERE donante_id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    DonanteViewModel model = new DonanteViewModel
                    {
                        donante_id = Convert.ToInt32(reader["donante_id"]),
                        nombre = Convert.ToString(reader["nombre"]),
                        edad = Convert.ToInt32(reader["edad"]),
                        genero = Convert.ToString(reader["genero"]),
                        tipo_sangre = Convert.ToString(reader["tipo_sangre"]),
                        numero_contacto = Convert.ToString(reader["numero_contacto"]),
                        direccion = Convert.ToString(reader["direccion"])
                    };

                    return View(model);
                }
            }
        }
        return RedirectToAction("Index");
    }
     [HttpPost]
    public IActionResult EditarDonantes(DonanteViewModel model)  
    {
        
        if(ModelState.IsValid)
        {
            using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                    cnx.Open();
                    string query = "UPDATE donante SET nombre = @Nombre ,  edad = @Edad , genero = @Genero ,tipo_sangre = @Tipo_sangre , direccion = @Direccion , numero_contacto = @numero_contacto WHERE donante_id = @Donante_id";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);

                        cmd.Parameters.AddWithValue("@Nombre", model.nombre);
                        cmd.Parameters.AddWithValue("@Edad", model.edad);
                        cmd.Parameters.AddWithValue("@Genero", model.genero);
                        cmd.Parameters.AddWithValue("@Tipo_sangre", model.tipo_sangre);
                        cmd.Parameters.AddWithValue("@Direccion", model.direccion);
                        cmd.Parameters.AddWithValue("@numero_contacto", model.numero_contacto);
                        cmd.Parameters.AddWithValue("@Donante_id", model.donante_id);
                        cmd.ExecuteNonQuery();
                    
            }
                      
            return RedirectToAction("Index");
          
        }
        
         return View(model);
    }
    public IActionResult EliminarDonantes(int id)
    {
        using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
        {
            cnx.Open();
            string query = "DELETE FROM donante WHERE donante_id = @donante_id";
            MySqlCommand cmd = new MySqlCommand(query, cnx);
            cmd.Parameters.AddWithValue("@donante_id", id);
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

        return RedirectToAction("Index");
    }
    private string ObtenerRol(int id)
        {
            string rol="";
            // Aquí puedes realizar la lógica de validación de las credenciales
            // Conectarte a la base de datos y comparar el correo y la contraseña con los registros de usuarios

            // Ejemplo básico de validación (solo como referencia, debes implementar tu propia lógica):
            using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                cnx.Open();
                string query = "SELECT rol FROM usuario WHERE usuario_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", id);
               object result = cmd.ExecuteScalar();

            // Verificar si se obtuvo un resultado y realizar las acciones necesarias
            if (result != null)
            {
                rol = result.ToString();
                // Hacer algo con el rol obtenido
            }
                return rol ;
            }
        }
  
}
