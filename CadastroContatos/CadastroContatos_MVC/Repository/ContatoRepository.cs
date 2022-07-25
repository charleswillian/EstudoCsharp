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
            this._bancoContext = bancoContext;
        }

        public ContatoModel ListarPorId(int Id)
        {
            return  _bancoContext.Contatos.FirstOrDefault(x => x.id == Id);
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

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.id);
            if (contatoDB == null) throw new System.Exception("Erro na atualização do contato");

            contatoDB.name = contato.name;
            contatoDB.email = contato.email;
            contatoDB.celular = contato.celular;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();
            return contatoDB;
        }
    }
}
