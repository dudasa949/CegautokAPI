using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Users")]
        public IActionResult GetAllUsers()
        {
            using (var context = new CegautokAPI.Models.FlottaContext())
            {
                try
                {
                    List<User> users = [.. context.Users];

                    return Ok(users);

                }
                catch (Exception ex)
                {
                    return BadRequest(new User()
                    {
                        Id = -1,
                        Name = $"Hiba történt: {ex.Message}",
                        Address = null
                    });
                }

            }
        }

        [HttpGet("UserById")]
        public IActionResult GetUserById(int Id)
        {
            using (var context = new CegautokAPI.Models.FlottaContext())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == Id);
                    if (user is User)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest(new User()
                        {
                            Id = -1,
                            Name = $"Hiba történt: Nincs ilyen azonosítójú felhasználó",
                            Address = null
                        });
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(new User()
                    {
                        Id = -1,
                        Name = $"Hiba történt: {ex.Message}",
                        Address = null
                    });
                }
            }
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser()
        {
            using (var context = new CegautokAPI.Models.FlottaContext())
            {
                try
                {
                    context.Users.Add(new User() { Id = 1, });
                    context.SaveChanges();
                    return Ok("Sikeres hozzáadás");
                }
                catch (Exception ex)
                {
                    return BadRequest(new User()
                    {
                        Id = -1,
                        Name = $"Hiba történt: {ex.Message}",
                        Address = null
                    });
                }
            }
        }

        [HttpPut("ModifyUser")]
        public IActionResult ModifyUser(User user)
        {
            using (var context = new CegautokAPI.Models.FlottaContext())
            {
                try
                {
                    if (context.Users.Contains(user))
                    {
                        context.Update(user);
                        context.SaveChanges();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return BadRequest("Nincsen megadott felhasználó!");
                    }
                }
                catch (Exception ex)
                {
                    {
                        return BadRequest($"Hiba a módosítás közben! {ex.Message}");
                    }
                }
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            using (var context = new CegautokAPI.Models.FlottaContext())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == 1);
                    if (user is User)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return BadRequest("Nincs ilyen azonosítójú felhasználó");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest($"Hiba a törlés közben! {ex.Message}");
                }
            }
        }

    }
}