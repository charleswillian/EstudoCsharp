using CadastroContatos_MVC.Models;
using System.Collections.Generic;

namespace CadastroContatos_MVC.Repository
{
    public interface IContatoRepository
    {
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
    }
}
