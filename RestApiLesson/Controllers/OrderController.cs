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
    public class OrderController : ControllerBase
    {
        private readonly PostamatsContext _db;
        public OrderController(PostamatsContext db)
        {
            _db = db;

            if (!db.Orders.Any())
            {
                var order = new Order
                {
                    Number = 1,
                    Price = 1850,
                    ReceiverFullName = "Петров Иван",
                    TelephoneNumber = "+7-123-456-78-90",
                    Products = new[] { "Телефон", "Зарядка" }
                };

                db.Orders.Add(order);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _db.Orders
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(or => or.Id.Equals(id));
            if (order is null)
                return NotFound();

            return new ObjectResult(order);
        }


        [HttpGet("new/{postamatId}")]
        public async Task<ActionResult<Order>> Create(int postamatId)
        {
            var postamat = await _db.Postamats.FindAsync(postamatId);
            if (postamat is null)
                return NotFound();

            if(!postamat.IsWorking)
                return Forbid();

            var order = new Order(postamat.Number);

            _db.Orders.Add(order);
            _db.SaveChanges();

            return Ok(order);
        }

        /// <summary>
        /// Создать заказ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order is null)
                return BadRequest();

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        /// <summary>
        /// Обновить информацию о заказе
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            if (order is null)
                return BadRequest();

            if (!_db.Orders.Any(p => int.Equals(p.Id, order.Id)))
                return NotFound();

            _db.Update(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        /// <summary>
        /// Закрыть заказ (статус заказа изменится на 'Закрыт')
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/cancel")]
        public async Task<ActionResult<Order>> Cancel(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order is null)
                return NotFound();

            //order.Status = 6;
            _db.Update(order);
            _db.SaveChanges();
            return Ok(order);
        }
    }
}
