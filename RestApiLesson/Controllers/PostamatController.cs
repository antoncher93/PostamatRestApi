using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiLesson.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiLesson.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostamatController : ControllerBase
    {
        private readonly ILogger<PostamatController> _logger;
        private readonly PostamatsContext _db;
        public PostamatController(ILogger<PostamatController> logger, PostamatsContext db)
        {
            _logger = logger;
            _db = db;

            // добавляем дефолтный постамат
            if(!db.Postamats.Any())
            {
                db.Postamats.Add(new Postamat()
                {
                    Number = "1234-5678",
                    Adress = "My Adress",
                    IsWorking = true
                });
                db.SaveChanges();
            }

           
        }

        /// <summary>
        /// Получение списка рабочих постаматов, отсортированных по номеру в алфавитном порядке
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Postamat>>> Get()
        {
            return await _db.Postamats
                .Where(p => p.IsWorking)
                .OrderBy(p => p.Number)
                .ToListAsync();
        }

        /// <summary>
        /// Получение информации о постамате
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{number}")]
        public async Task<ActionResult<Postamat>> Get(string number)
        {
            var postamat = await _db.Postamats.FirstOrDefaultAsync(p => string.Equals(p.Number, number));
            if (postamat is null)
                return NotFound();

            return new ObjectResult(postamat);
        }

        
    }
}
