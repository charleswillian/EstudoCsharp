using CadastroContatos_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos_MVC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options): base(options)
        {

        }

        public DbSet<ContatoModel> Contatos { get; set; }
    }
}
