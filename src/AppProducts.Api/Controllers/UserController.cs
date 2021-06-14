using AppProducts.Api.Data;
using AppProducts.Api.Models;
using AppProducts.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProducts.Api.Controllers
{
    [Route("Users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(int id, [FromServices] DataContext context, [FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != user.Id) return NotFound(new { message = "Usuário não encontrado" });

            try
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }

        }

        [HttpPost]
        [Route("add-new")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                user.Role = "employee";

                context.Users.Add(user);
                await context.SaveChangesAsync();

                user.Password = "";

                return user;
            }
            catch (Exception)
            {
                return BadRequest( new { message = "Não foi possível criar o usuário."});
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User user)
        {
            var loggedUser = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == user.Username && x.Password == user.Password)
                .FirstOrDefaultAsync();

            if (loggedUser == null) return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(loggedUser);

            loggedUser.Password = "";

            return new
            {
                user = loggedUser,
                token = token
            };

        }

    }
}
