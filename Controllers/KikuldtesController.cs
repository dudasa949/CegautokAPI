using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KikuldtesController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using var context = new FlottaContext();
            return Ok(context.Kikuldtes.ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            using var context = new FlottaContext();
            var item = context.Kikuldtes.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen kiküldetés!");
            return Ok(item);
        }

        [HttpPost("Post")]
        public IActionResult Post(Kikuldte kikuldtes)
        {
            using var context = new FlottaContext();
            try
            {
                context.Kikuldtes.Add(kikuldtes);
                context.SaveChanges();
                return Ok("Sikeres rögzítés!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a rögzítés közben: {ex.Message}");
            }
        }

        [HttpPut("Put")]
        public IActionResult Put(Kikuldte kikuldtes)
        {
            using var context = new FlottaContext();
            var existing = context.Kikuldtes.Find(kikuldtes.Id);
            if (existing == null)
                return NotFound("Nincs ilyen kiküldetés!");
            try
            {
                context.Entry(existing).CurrentValues.SetValues(kikuldtes);
                context.SaveChanges();
                return Ok("Sikeres módosítás!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a módosítás közben: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new FlottaContext();
            var item = context.Kikuldtes.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen kiküldetés!");
            try
            {
                context.Kikuldtes.Remove(item);
                context.SaveChanges();
                return Ok("Sikeres törlés!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a törlés közben: {ex.Message}");
            }
        }
    }
}