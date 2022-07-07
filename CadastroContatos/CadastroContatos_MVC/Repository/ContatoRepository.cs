using CadastroContatos_MVC.Data;
using CadastroContatos_MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace CadastroContatos_MVC.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
            //Gravar no banco
        }

        
    }
}
