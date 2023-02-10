using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BoardGame1.Migrations;
using BoardGame1.Models;
using Rental = BoardGame1.Models.Rental;

namespace BoardGame1.Controllers
{
    public class RentalDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(RentalDto))]
        // GET:  api/RentalData/ListRentals
        public IHttpActionResult ListRentals()
        {
            List<Rental> Rentals = db.Rentals.ToList();
            List<RentalDto> RentalDtos = new List<RentalDto>();

            Rentals.ForEach(r => RentalDtos.Add(new RentalDto()
            {
                RentalId = r.RentalId,
                RentDate = r.RentDate
            }));

            return Ok(RentalDtos);
        }

        // GET: api/RentalData/FindRental/1
        [ResponseType(typeof(RentalDto))]
        [HttpGet]
        public IHttpActionResult FindRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            RentalDto RentalDto = new RentalDto()
            {
                RentalId = rental.RentalId,
                RentDate = rental.RentDate,
       
            };
            if (rental == null)
            {
                return NotFound();
            }

            return Ok(RentalDto);
        }

        // POST: api/RentalData/UpdateRental/1
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.RentalId)
            {
                return BadRequest();
            }

            db.Entry(rental).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RentalData/AddRental
        [ResponseType(typeof(Rental))]
        [HttpPost]
        public IHttpActionResult AddRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rentals.Add(rental);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rental.RentalId }, rental);
        }

        // DELETE: api/RentalData/DeleteRental/1
        [ResponseType(typeof(Rental))]
        [HttpPost]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rentals.Count(e => e.RentalId == id) > 0;
        }
    }
}