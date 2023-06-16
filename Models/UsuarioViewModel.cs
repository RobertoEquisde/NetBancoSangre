using System.ComponentModel.DataAnnotations;
namespace Usuario.Models
{
    public class UsuarioViewModel
    {
        [Key]
        public int id_usuario{get;set;}
        [Required]
        public String? curp{get;set;}
        public String? contrasena{get;set;}
        public String? rol{get;set;}
    }
}