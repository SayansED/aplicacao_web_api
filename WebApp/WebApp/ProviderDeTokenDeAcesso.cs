using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class ProviderDeTokenDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = BaseUsuarios
                .Usuarios()
                .FirstOrDefault(x => x.Nome == context.UserName
                    && x.Senha == context.Password);

            if(usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou senha incorreta");
                return;
            }      
            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);

            foreach (var funcao in usuario.Funcoes)
            {
                identidadeUsuario.AddClaim(new Claim(ClaimTypes.Role, funcao));
            }

            context.Validated(identidadeUsuario);          
        }
    }
}