using dominio.Interfaces;
using Entidades.Entidades;
using infraestrutura.Configuracoes;
using infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
    {

        private readonly DbContextOptions<Contexto> _optionsbuilder;

        public RepositorioUsuario()
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }


        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, int celular)
        {
            using (var data = new Contexto(_optionsbuilder))
            {
                try
                {
                    using (var Data = new Contexto(_optionsbuilder))
                    {
                        await Data.ApplicationUsers.AddAsync(
                            new ApplicationUser
                            {
                                Email = email,
                                PasswordHash = senha,
                                Idade = idade,
                                Celular = celular

                            });
                        await data.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var Data = new Contexto(_optionsbuilder))
                {
                    return await Data.ApplicationUsers.
                         Where(u =>u.Email.Equals(email) && u.PasswordHash.Equals(senha))
                         .AsNoTracking()
                         .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            try
            {
                using (var Data = new Contexto(_optionsbuilder))
                {
                    var usuario = await Data.ApplicationUsers.
                         Where(u => u.Email.Equals(email))
                         .AsNoTracking()
                         .FirstOrDefaultAsync();

                    return usuario.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
