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
    [ApiKey]
    [ApiController]
    [Route("[controller]")]
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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.users.ToListAsync();
        }
        [HttpPost]
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
        public async Task<ActionResult<IEnumerable<User>>> Get(Guid id)
        {
            User user = await db.users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
    }
}
