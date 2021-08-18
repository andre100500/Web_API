using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Attributes;
using Test.Models;
using Test.Utils;


namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersControoller : ControllerBase
    {
        UserDBContext db;
        public UsersControoller(UserDBContext context)
        {
            db = context;
            if (!db.users.Any())
            {
                db.users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Email = "asd@gmail.com",
                    Name = "Das",
                    Surname = "petrovich"
                });
            }
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<User>>> GetListUsers()
        {
            return await db.users.ToListAsync();
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<User>>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<User>>> GetID(Guid id)
        {
            User user = await db.users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
    }
}
