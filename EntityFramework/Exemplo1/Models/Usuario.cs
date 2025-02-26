using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Exemplo1.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]  // Define explicitamente o nome da coluna
        // [Key]   // Define a chave primária
        public int Id { get; set; }

        [Column("name")]    // Define explicitamente o nome da coluna
        [Required]          // Define que o campo é obrigatório
        [StringLength(100)] // Define o tamanho máximo do campo
        public string Name { get; set; } = string.Empty;

        [Column("email")]   // Define explicitamente o nome da coluna
        [Required]          // Define que o campo é obrigatório
        [StringLength(100)] // Define o tamanho máximo do campo
        public string Email { get; set; } = string.Empty;
    }
}