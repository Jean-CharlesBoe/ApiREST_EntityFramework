using System.ComponentModel.DataAnnotations;

namespace APIRest_SQL_Exo1.Models
{
    public class Livre
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Titre { get; set; }

        [Required]
        [StringLength(100)]
        public string? Auteur { get; set; }

        [Range(1500, int.MaxValue, ErrorMessage ="L'année de publication doit être après 1500")]
        public int AnneePublication { get; set; }

        public int EditeurId { get; set; }
        public Editeur? Editeur { get; set; }
    }
}
