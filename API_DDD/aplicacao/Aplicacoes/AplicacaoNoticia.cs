using aplicacao.Interfaces;
using dominio.Interfaces;
using dominio.Interfaces.InterfaceServicos;
using dominio.Servicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Aplicacoes
{
    public class AplicacaoNoticia : IAplicacaoNoticia
    {
        INoticia _INoticia;
        IServicoNoticia _ServicoNoticia;

        public AplicacaoNoticia(INoticia INoticia, IServicoNoticia IServicoNoticia)
        {
            _INoticia = INoticia;
            _ServicoNoticia = IServicoNoticia;
        }
        public async Task AdicionaNoticia(Noticia noticia)
        {
            await _ServicoNoticia.AdicionaNoticia(noticia);
        }

        public async Task AtualizaNoticia(Noticia noticia)
        {
            await _ServicoNoticia.AtualizaNoticia(noticia);
        }

        public async Task<List<Noticia>> ListarNoticiasAtivas()
        {
           return await _ServicoNoticia.ListarNoticiasAtivas();
        }


        public async Task Adicionar(Noticia objeto)
        {
            await _INoticia.Adicionar(objeto);
        }

       
        public async Task Atualizar(Noticia objeto)
        {
            await _INoticia.Atualizar(objeto);
        }

        public async  Task<Noticia> BuscarPorId(int Id)
        {
            return await _INoticia.BuscarPorId(Id);
        }

        public async Task Excluir(Noticia objeto)
        {
            await _INoticia.Excluir(objeto);
        }

        public async Task<List<Noticia>> Listar()
        {
            return await _INoticia.Listar();
        }

       
    }
}
