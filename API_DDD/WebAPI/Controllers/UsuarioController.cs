using aplicacao.Aplicacoes;
using aplicacao.Interfaces;
using Entidades.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicacaoUsuario _AplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;



        public UsuarioController(IAplicacaoUsuario IAplicacaoUsuario, SignInManager<ApplicationUser> singInManager, 
            UserManager<ApplicationUser>userManager)
        {
            _AplicacaoUsuario = IAplicacaoUsuario;
            _singInManager = singInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]
        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                    return Unauthorized();

            var resultado = await _AplicacaoUsuario.ExisteUsuario(login.email, login.senha);
            if(resultado)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-123456"))
                    .AddSubject("Estudando")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("UsuarioAPINumero", "1")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();

            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Unauthorized();

            var resultado = await _AplicacaoUsuario.AdicionarUsuario(login.email, login.senha, login.idade, login.celular);

            if(resultado)
            return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao adicionar usuário");

        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Unauthorized();

            var resultado = await _singInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure:false);

            if (resultado.Succeeded)
            { 
                var idUsuario = await _AplicacaoUsuario.RetornaIdUsuario(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-123456"))
                    .AddSubject("Estudando")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();

            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuarioTokenIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Celular = login.celular,
                Tipo = Entidades.Enums.TipoUsuario.Comum
            };

            var result = await _userManager.CreateAsync(user, login.senha);

            if(result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            //Geração de confirmação caso precise

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //retorno email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result2 = await _userManager.ConfirmEmailAsync(user, code);
            var StatusMessage = result2.Succeeded;

            if (result2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar Usuário");

        }

    }
}
