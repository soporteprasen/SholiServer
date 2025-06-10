using CL_EN;
using CL_LN;
using CL_AD;
using Microsoft.AspNetCore.Mvc;
using SholiServer.ConexionAplicaciones;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace SholiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CTusuarioController : ControllerBase
    {
        private readonly CL_AD.TokenService _tokenService;

        public CTusuarioController(CL_AD.TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("BuscarUsuario")]
        public async Task<IActionResult> guardarBuscarUsuario([FromBody] ENusuarios DatosEnviados)
        {
            ENusuarios ent = new ENusuarios();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    ent = new LNusuarios().guardarBuscarUsuario(DatosEnviados);

                    if (ent.ValorConsulta == "1")
                    {

                        var jwt = _tokenService.GenerateToken(ent, 15);

                        Response.Cookies.Append("jwt", jwt, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false,
                            SameSite = SameSiteMode.Lax,
                            Expires = DateTime.UtcNow.AddYears(15)
                        });

                        ent.MensajeConsulta = "Login exitoso";
                    }
                    else
                    {
                        ent.MensajeConsulta = "Usuario o contraseña incorrectos";
                        ent.ValorConsulta = "-1";
                    }
                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        [Authorize]
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            var idUsuario = User.FindFirst("id_usuario")?.Value;
            var usuarioNombre = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return Ok(new
            {
                idUsuario,
                usuarioNombre
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Sesión cerrada correctamente." });
        }

    }


}
