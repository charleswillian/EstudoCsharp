using aplicacao.Interfaces;
using dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Aplicacoes
{
    public class AplicacaoUsuario : IAplicacaoUsuario
    {
        IUsuario _IUsuario;
        public AplicacaoUsuario(IUsuario IUsuario)
        {
            _IUsuario = IUsuario;
        }
        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, int celular)
        {
            return await _IUsuario.AdicionarUsuario(email, senha, idade, celular);
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            return await _IUsuario.ExisteUsuario(email, senha);
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            return await _IUsuario.RetornaIdUsuario(email);
        }
    }
}
