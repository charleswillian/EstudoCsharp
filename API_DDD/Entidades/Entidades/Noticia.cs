using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("TB_NOTICIA")]
    public class Noticia : Notifica
    {
        [Column("NTC_ID")]
        public int Id { get; set; }
        [Column("NTC_TITULO")]
        public int Titulo { get; set; }
        [MaxLength(255)]

        [Column("NTC_INFORMACAO")]
        public int Informacao { get; set; }
        [MaxLength(255)]

        [Column("NTC_ATIVO")]
        public bool Ativo { get; set; }

        public bool ValidarPropriedadeString(int titulo, string v)
        {
            throw new NotImplementedException();
        }

        [Column("NTC_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("NTC_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser AppplicationUser { get; set; }
    }
}
