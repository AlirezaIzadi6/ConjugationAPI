using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToLearnApi.Contexts;

namespace ToLearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : MyController
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        { 
            _context = context;
        }

        [HttpGet("Score")]
        public async Task<ActionResult<int>> GetScore()
        {
            return _context.UserScores.Where(e => e.UserId == CurrentUser(User))
                .Sum(e => e.Score);
        }
    }
}
