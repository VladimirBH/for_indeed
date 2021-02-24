using for_Indeed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace for_Indeed.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        ClientsContext db;
        public ClientsController(ClientsContext context)
        {
            db = context;
            if (!db.Clients.Any())
            {
                db.Clients.Add(new Models.Client { Last_Name = "Gekker", Name = "Tom", Address= "8 Rue du Fort, 92500 Rueil-Malmaison, France" });
                db.Clients.Add(new Models.Client { Last_Name = "Smith", Name = "Alice", Address = "12 Rue du Fort, 92500 Rueil-Malmaison, France" });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return await db.Clients.ToListAsync();
        }

        //Поиск
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            Client client = await db.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return new ObjectResult(client);
        }

        //Создание
        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            // обработка частных случаев валидации
            if (client.Last_Name == "admin")
            {
                ModelState.AddModelError("Last_Name", "Недопустимое имя пользователя - admin");
            }
            if (client.Name == "admin")
            {
                ModelState.AddModelError("Name", "Недопустимое имя пользователя - admin");
            }
            // если есть лшибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // если ошибок нет, сохраняем в базу данных
            db.Clients.Add(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }

        //Редактирование
        [HttpPut]
        public async Task<ActionResult<Client>> Put(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            if (!db.Clients.Any(x => x.Id == client.Id))
            {
                return NotFound();
            }
            db.Update(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }

        //Удаление
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            Client client = db.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }
    }
}
