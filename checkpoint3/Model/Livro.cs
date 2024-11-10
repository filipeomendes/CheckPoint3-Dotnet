using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkpoint3.Models
{
    [Table("T_LIVRO")]
    public class Livro
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TITULO", TypeName = "VARCHAR2(100)")]
        public string Titulo { get; set; }

        [Required]
        [Column("AUTOR", TypeName = "VARCHAR2(100)")]
        public string Autor { get; set; }

        [Required]
        [Column("GENERO", TypeName = "VARCHAR2(50)")]
        public string Genero { get; set; }

        [Column("SINOPSE", TypeName = "VARCHAR2(1000)")]
        public string Sinopse { get; set; }

        [Required]
        [Column("ANO_PUBLICACAO")]
        public int AnoPublicacao { get; set; }

        [Column("CLASSIFICACAO", TypeName = "NUMBER(3,1)")]
        public decimal? Classificacao { get; set; }
    }
}
