using System.ComponentModel.DataAnnotations;

namespace APIRest_SQL_Exo1.Models
{
    public class Editeur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nom { get; set; }

        public ICollection<Livre>? Livres { get; set; }
    }
}
