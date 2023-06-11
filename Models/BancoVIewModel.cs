using System.ComponentModel.DataAnnotations;
namespace banco_sangre.Models
{

        public class DonanteViewModel
        {
                [Key]
                public int donante_id{get;set;}
                [Required]
                public String? nombre{get;set;}
                public int? edad{get;set;}
                public String? genero{get;set;}
                public String? tipo_sangre{get;set;}
         
                public String? numero_contacto{get;set;}
                public String? direccion{get;set;}
        }
}