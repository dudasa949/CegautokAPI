using CegautokAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CegautokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using var context = new FlottaContext();
            return Ok(context.Gepjarmus.ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            using var context = new FlottaContext();
            var item = context.Gepjarmus.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen gépjármű!");
            return Ok(item);
        }

        [HttpPost("Post")]
        public IActionResult Post(Gepjarmu gepjarmu)
        {
            using var context = new FlottaContext();
            try
            {
                context.Gepjarmus.Add(gepjarmu);
                context.SaveChanges();
                return Ok("Sikeres rögzítés!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba a rögzítés közben: {ex.Message}");
            }
        }

        [HttpPut("Put")]
        public IActionResult Put(Gepjarmu gepjarmu)
        {
            using var context = new FlottaContext();
            var existing = context.Gepjarmus.Find(gepjarmu.Id);
            if (existing == null)
                return NotFound("Nincs ilyen gépjármű!");
            try
            {
                context.Entry(existing).CurrentValues.SetValues(gepjarmu);
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
            var item = context.Gepjarmus.Find(id);
            if (item == null)
                return NotFound("Nincs ilyen gépjármű!");
            try
            {
                context.Gepjarmus.Remove(item);
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