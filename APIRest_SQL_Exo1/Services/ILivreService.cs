using APIRest_SQL_Exo1.Data;

namespace APIRest_SQL_Exo1.Services
{
    public interface ILivreService
    {
        Task<IEnumerable<LivreBO>> GetLivresAsync();
        Task<LivreBO> GetLivreAsync(int id);
        Task<IEnumerable<LivreBO>> GetLivresAsync(string titre, string auteur, int? annee);
        Task<LivreBO> AddLivreAsync(LivreBO livre);
        Task<bool> UpdateLivreAsync(int id, LivreBO livre);
        Task<bool> DeleteLivreAsync(int id);
    }
}
