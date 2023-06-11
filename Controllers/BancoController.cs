using Microsoft.AspNetCore.Mvc;
using banco_sangre.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using  System.ComponentModel.DataAnnotations;

namespace banco_sangre.Controllers;

public class BancoController: Controller{
    private readonly IConfiguration _configuration;
    public BancoController(IConfiguration configuration)
    {
       this._configuration  = configuration;
    }
    public IActionResult Index()
    {
        DataTable tbl = new DataTable();
        using(MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DevConnection"))){
            conn.Open();
            MySqlDataAdapter adaptador = new MySqlDataAdapter("SELECT * FROM donante",conn);
            adaptador.Fill(tbl);
        }
        return View(tbl);
    }
    public IActionResult Anadir()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Anadir(DonanteViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection cnx = new MySqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                cnx.Open();
                string query = "INSERT INTO donante(nombre,  edad, genero, tipo_sangre,  telefono,direccion)" +
                            "VALUES (@nombre, @apellido, @edad, @genero, @tipoSangre, @direccion, @telefono , @correoElectronico, @ultimaDonacion, @restricciones)";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@nombre", model.nombre);
            
                cmd.Parameters.AddWithValue("@edad", model.edad);
                cmd.Parameters.AddWithValue("@genero", model.genero);
                cmd.Parameters.AddWithValue("@tipo_sangre", model.tipo_sangre);
                cmd.Parameters.AddWithValue("@direccion", model.direccion);
                cmd.Parameters.AddWithValue("@numero_contacto", model.numero_contacto);
                cmd.ExecuteNonQuery();
                cnx.Close();
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }
    public IActionResult Editar(int id)
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
    public IActionResult Editar(DonanteViewModel model)  
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
    public IActionResult Eliminar(int id)
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
  
}
