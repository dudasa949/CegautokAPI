using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KikuldottJarmuController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using var context = new FlottaContext();
            return Ok(context.Kikuldottjarmus.ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            using var context = new FlottaContext();
            var item = context.Kikuldottjarmus.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen kiküldött jármű!");
            return Ok(item);
        }

        [HttpPost("Post")]
        public IActionResult Post(Kikuldottjarmu kikuldottjarmu)
        {
            using var context = new FlottaContext();
            try
            {
                context.Kikuldottjarmus.Add(kikuldottjarmu);
                context.SaveChanges();
                return Ok("Sikeres rögzítés!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a rögzítés közben: {ex.Message}");
            }
        }

        [HttpPut("Put")]
        public IActionResult Put(Kikuldottjarmu kikuldottjarmu)
        {
            using var context = new FlottaContext();
            var existing = context.Kikuldottjarmus.Find(kikuldottjarmu.Id);
            if (existing == null)
                return NotFound("Nincs ilyen kiküldött jármű!");
            try
            {
                context.Entry(existing).CurrentValues.SetValues(kikuldottjarmu);
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
            var item = context.Kikuldottjarmus.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen kiküldött jármű!");
            try
            {
                context.Kikuldottjarmus.Remove(item);
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