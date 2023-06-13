using banco_sangre.Models;
namespace   banco_sangre.Data
{
    public class Da_logica
    {
        public List<UsuarioViewModel>ListaUsuario()
        {
            return new List<UsuarioViewModel>{
                new UsuarioViewModel{usuario_id=1,nombre="Juan",correo="juan.perez28@gmail.com",contrasena="123456",rol=new String[]{"admin"}},
                new UsuarioViewModel{usuario_id=2,nombre="Luisa",correo="luisa.gonzalez93@gmail.com",contrasena="password123",rol=new String[]{"usuario"}},
                new UsuarioViewModel{usuario_id=3,nombre="Ana",correo="ana.martinez45@gmail.com",contrasena="securepass",rol=new String[]{"usuario"}},
                new UsuarioViewModel{usuario_id=4,nombre="Pedro",correo="pedro.sanchez76@gmail.com",contrasena="qwerty",rol=new String[]{"usuario"}},
                new UsuarioViewModel{usuario_id=5,nombre="María",correo="maria.lopez82@gmail.com",contrasena="pass1234",rol=new String[]{"usuario"}}
            };
        }
        public UsuarioViewModel ValidarUsuario(string _correo , string _contrasena)
        {
            return ListaUsuario().Where(item=>item.correo==_correo && item.contrasena==_contrasena).FirstOrDefault();
        }
    }
}