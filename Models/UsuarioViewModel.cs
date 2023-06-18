using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Usuario.Models
{
    public class UsuarioViewModel
    {
        [Key]
        public int id_usuario { get; set; }
        [Required]
        public String? curp { get; set; }
        public String? contrasena { get; set; }
        public String? rol { get; set; }
    }
    public class DonanteViewModel
    {
        [Key]
        public int id_donante { get; set; }
        [Required]
        public String? tipo_sangre { get; set; }
        public String? nombre { get; set; }
        public String? apellidos { get; set; }
        public String? anio_nacimiento { get; set; }

        public int donacion_realizada { get; set; }
        [ForeignKey("id_usuario")]
        public int id_usuario { get; set; }

    }
    public class AlmacenViewModel
    {
        [Key]
        public int id_almacen { get; set; }
        [Required]
        public String? almacen_tipo_sangre { get; set; }
        public String? fecha_expiracion { get; set; }
        public int? cantidad { get; set; }
        public int donacion_realizada { get; set; }
        [ForeignKey("id_donante")]
        public int id_donante { get; set; }

    }
    public class CitaViewModel
    {
        [Key]
        public int id_cita { get; set; }
        [Required]
        public String? fecha_cita { get; set; }
        public String? lugar_cita { get; set; }
        [ForeignKey("id_donante")]
        public int id_donante { get; set; }

    }
    public class HospitalesViewModel
    {
        [Key]
        public int id_hospital { get; set; }
        [Required]
        public String? nombre_hospital { get; set; }
        public String? direccion_hospital { get; set; }
    }
    public class DonacionViewModel
    {
        [Key]
        public int id_donacion { get; set; }
        [Required]
        public String? fecha_envio { get; set; }
        [ForeignKey("id_donante")]
        public int id_donante { get; set; }
        [ForeignKey("id_almacen")]
        public int id_almacen { get; set; }
        [ForeignKey("id_hospital")]
        public int id_hospital { get; set; }


    }
}