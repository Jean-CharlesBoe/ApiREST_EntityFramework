using APIRest_SQL_Exo1.Data;
using APIRest_SQL_Exo1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace APIRest_SQL_Exo1.Services
{
    public class LivreService: ILivreService
    {
        private readonly BiblioContext _context;

        public LivreService(BiblioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LivreBO>> GetLivresAsync()
        {
            return await _context.Livres
                .Include(b => b.Editeur)
                .Select(b => new LivreBO
                {
                    Id = b.id,
                    Titre = b.Titre,
                    Auteur = b.Auteur,
                    AnneePublication = b.AnneePublication,
                    NomEditeur = b.Editeur.Nom
                }).ToListAsync();
        }

        public async Task<LivreBO> GetLivreAsync(int id)
        {
            var book = await _context.Livres
                .Include(b => b.Editeur)
                .FirstOrDefaultAsync(b => b.id == id);

            if (book == null) return null;

            return new LivreBO
            {
                Id = book.id,
                Titre = book.Titre,
                Auteur = book.Auteur,
                AnneePublication = book.AnneePublication,
                NomEditeur = book.Editeur.Nom
            };
        }

        public async Task<IEnumerable<LivreBO>> GetLivresAsync(string titre, string auteur, int? annee)
        {
            var query = _context.Livres.Include(b => b.Editeur).AsQueryable();

            if (!string.IsNullOrEmpty(titre))
            {
                query = query.Where(b => b.Titre.Contains(titre));
            }

            if (!string.IsNullOrEmpty(auteur))
            {
                query = query.Where(b => b.Auteur.Contains(auteur));
            }

            if (annee.HasValue)
            {
                query = query.Where(b => b.AnneePublication == annee.Value);
            }

            return await query
                .Select(b => new LivreBO
                {
                    Id = b.id,
                    Titre = b.Titre,
                    Auteur = b.Auteur,
                    AnneePublication = b.AnneePublication,
                    NomEditeur = b.Editeur.Nom
                }).ToListAsync();
        }

        public async Task<LivreBO> AddLivreAsync(LivreBO livre)
        {
            var editeur = await _context.Editeurs.FirstOrDefaultAsync(p => p.Nom == livre.NomEditeur);
            if (editeur == null)
            {
                editeur = new Editeur { Nom = livre.NomEditeur };
                _context.Editeurs.Add(editeur);
                await _context.SaveChangesAsync();
            }

            var entity = new Livre
            {
                Titre = livre.Titre,
                Auteur = livre.Auteur,
                AnneePublication = livre.AnneePublication,
                EditeurId = editeur.Id
            };

            _context.Livres.Add(entity);
            await _context.SaveChangesAsync();

            livre.Id = entity.id;
            return livre;
        }

        public async Task<bool> UpdateLivreAsync(int id, LivreBO livreBO)
        {
            var entity = await _context.Livres.Include(b => b.Editeur).FirstOrDefaultAsync(b => b.id == id);
            if (entity == null) return false;

            var editeur = await _context.Editeurs.FirstOrDefaultAsync(p => p.Nom == livreBO.NomEditeur);
            if (editeur == null)
            {
                editeur = new Editeur { Nom = livreBO.NomEditeur };
                _context.Editeurs.Add(editeur);
                await _context.SaveChangesAsync();
            }

            entity.Titre = livreBO.Titre;
            entity.Auteur = livreBO.Auteur;
            entity.AnneePublication = livreBO.AnneePublication;
            entity.EditeurId = editeur.Id;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLivreAsync(int id)
        {
            var book = await _context.Livres.FindAsync(id);
            if (book == null) return false;

            _context.Livres.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
