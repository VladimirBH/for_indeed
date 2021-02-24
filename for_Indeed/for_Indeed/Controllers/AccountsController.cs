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
    public class AccountsController : ControllerBase
    {
        AccountsContext db;
        public AccountsController(AccountsContext context) 
        {
            db = context;
            if (!db.Accounts.Any()) 
            {
                db.Accounts.Add(new Models.Account { Id_Client = 1, Currency = "EUR", Amount = 200});
                db.Accounts.Add(new Models.Account { Id_Client = 1, Currency = "RUB", Amount = 10000 });
                db.Accounts.Add(new Models.Account { Id_Client = 1, Currency = "USD", Amount = 100 });
                db.Accounts.Add(new Models.Account { Id_Client = 2, Currency = "IDR", Amount = 300000 });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get() 
        {
            return await db.Accounts.ToListAsync();
        }
        //Вывод счетов определенного пользователя
        [HttpGet("{id_cli}")]
        public async Task<ActionResult<IEnumerable<Account>>> Get(int id_cli)
        {
            List<Account> acc = await db.Accounts.ToListAsync();
            Account[] accounts = acc.Where(x => x.Id_Client == id_cli).ToArray();
            if (acc.Count == 0)
            {
                return NotFound();
            }
            return accounts.ToList();
        }

        //Создание счета
        [HttpPost]
        public async Task<ActionResult<Account>> Post(Account account) 
        {
            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }

        //Редактирование
        [HttpPut]
        public async Task<ActionResult<Account>> Put(Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }
            if (!db.Accounts.Any(x => x.Id == account.Id))
            {
                return NotFound();
            }
            db.Update(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }

        //Удаление
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> Delete(int id)
        {
            Account account = db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }
    }
}
