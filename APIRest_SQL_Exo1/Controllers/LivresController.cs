using APIRest_SQL_Exo1.Data;
using APIRest_SQL_Exo1.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIRest_SQL_Exo1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivresController : ControllerBase
    {
        private readonly ILivreService _livreService;

        public LivresController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivreBO>>> GetLivres()
        {
            var livres = await _livreService.GetLivresAsync();
            return Ok(livres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivreBO>> GetLivre(int id)
        {
            var livre = await _livreService.GetLivreAsync(id);
            if (livre == null) return NotFound();

            return Ok(livre);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LivreBO>>> SearchBooks([FromQuery] string titre, [FromQuery] string auteur, [FromQuery] int annee)
        {
            var books = await _livreService.GetLivresAsync(titre, auteur, annee);
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<LivreBO>> PostBook(LivreBO livre)
        {
            var createdLivre = await _livreService.AddLivreAsync(livre);
            return CreatedAtAction(nameof(GetLivre), new { id = createdLivre.Id }, createdLivre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, LivreBO livre)
        {
            var success = await _livreService.UpdateLivreAsync(id, livre);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _livreService.DeleteLivreAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
