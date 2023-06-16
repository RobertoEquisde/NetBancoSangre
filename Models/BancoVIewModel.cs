using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public class DonacionViewModel
        {
                [Key]
                public int donacion_id{get;set;}
                [ForeignKey("donante_id")]
                public int? donante_id{get;set;}
                public String? tipo_sangre{get;set;}
                public String? fecha{get;set;}
                public  String? hora{get;set;}
                public int?  cantidad {get;set;}
                public string? ubicacion {get;set;}
         
            
        }
        public class AlmacenamientoViewModel
        {
                [Key]
                public int almacenamiento_id{get;set;}
                [ForeignKey("donacion_id")]
                public int? donacion_id{get;set;}
                public String? tipo_sangre{get;set;}
                public int?  cantidad {get;set;}
                public string? fecha_expiracion {get;set;}  
        }
}