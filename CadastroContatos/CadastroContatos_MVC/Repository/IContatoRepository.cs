using CadastroContatos_MVC.Models;
using System.Collections.Generic;

namespace CadastroContatos_MVC.Repository
{
    public interface IContatoRepository
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
    }
}
