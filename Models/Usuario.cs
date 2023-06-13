namespace banco_sangre.Models{
    public class Usuario{
        public int usuario_id{get;set;}
        public String? nombre{get;set;}
        public String? correo{get;set;}
        public String? contrasena{get;set;}
        public String[]? rol{get;set;}
    }
}