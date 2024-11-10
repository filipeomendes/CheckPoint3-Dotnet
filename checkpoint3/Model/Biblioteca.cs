using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkpoint3.Models
{
    [Table("T_BIBLIOTECA")]
    public class Biblioteca
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NOME", TypeName = "VARCHAR2(100)")]
        public string Nome { get; set; }

        [Required]
        [Column("CIDADE", TypeName = "VARCHAR2(100)")]
        public string Cidade { get; set; }

        [Required]
        [Column("ESTADO", TypeName = "VARCHAR2(2)")]
        public string Estado { get; set; }
    }
}