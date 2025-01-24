using APIRest_SQL_Exo1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest_SQL_Exo1.Data
{
    public class BiblioContext : DbContext
    {
        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options) { }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Editeur> Editeurs { get; set; }
    }
}
