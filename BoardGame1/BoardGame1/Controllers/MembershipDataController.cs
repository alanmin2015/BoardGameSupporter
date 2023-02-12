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
using Membership = BoardGame1.Models.Membership;

namespace BoardGame1.Controllers
{
    public class MembershipDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        [ResponseType(typeof(MembershipDto))]
        // GET:  api/MembershipData/ListMemberships
        public IHttpActionResult ListMemberships()
        {
            List<Membership> Memberships = db.Memberships.ToList();
            List<MembershipDto> MembershipDtos = new List<MembershipDto>();

            Memberships.ForEach(m => MembershipDtos.Add(new MembershipDto()
            {
                MembershipId = m.MembershipId,
                FirstName = m.FirstName,
                LastName = m.LastName
            }));

            return Ok(MembershipDtos);
        }


        [HttpGet]
        [ResponseType(typeof(MembershipDto))]
        // GET:  api/MembershipData/ListMembershipsForGame/1
        public IHttpActionResult ListMembershipsForGame(int id)
        {
            List<Membership> Memberships = db.Memberships.Where(
                m=>m.Games.Any(
                    a=>a.GameId== id)
                ).ToList();
            List<MembershipDto> MembershipDtos = new List<MembershipDto>();

            Memberships.ForEach(m => MembershipDtos.Add(new MembershipDto()
            {
                MembershipId = m.MembershipId,
                FirstName = m.FirstName,
                LastName = m.LastName
            }));

            return Ok(MembershipDtos);
        }

        [HttpGet]
        [ResponseType(typeof(MembershipDto))]
        // GET:  api/MembershipData/ListMembershipsNoGame/1
        public IHttpActionResult ListMembershipsNoGame(int id)
        {
            List<Membership> Memberships = db.Memberships.Where(
                m => !m.Games.Any(
                    a => a.GameId == id)
                ).ToList();
            List<MembershipDto> MembershipDtos = new List<MembershipDto>();

            Memberships.ForEach(m => MembershipDtos.Add(new MembershipDto()
            {
                MembershipId = m.MembershipId,
                FirstName = m.FirstName,
                LastName = m.LastName
            }));

            return Ok(MembershipDtos);
        }
        // GET: api/MembershipData/FindMembership/1
        [ResponseType(typeof(MembershipDto))]
        [HttpGet]
        public IHttpActionResult FindMembership(int id)
        {
            Membership membership = db.Memberships.Find(id);
            MembershipDto MembershipDto = new MembershipDto()
            {
                MembershipId = membership.MembershipId,
                FirstName = membership.FirstName,
                LastName = membership.LastName

            };
            if (membership == null)
            {
                return NotFound();
            }

            return Ok(MembershipDto);
        }

        // POST: api/MembershipData/UpdateMembership/1
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMembership(int id, Membership membership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membership.MembershipId)
            {
                return BadRequest();
            }

            db.Entry(membership).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(id))
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

        // POST: api/MembershipData/AddMembership
        [ResponseType(typeof(Membership))]
        [HttpPost]
        public IHttpActionResult AddMembership(Membership membership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Memberships.Add(membership);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = membership.MembershipId }, membership);
        }

        // DELETE: api/MembershipData/DeleteMembership/5
        [ResponseType(typeof(Membership))]
        [HttpPost]
        public IHttpActionResult DeleteMembership(int id)
        {
            Membership membership = db.Memberships.Find(id);
            if (membership == null)
            {
                return NotFound();
            }

            db.Memberships.Remove(membership);
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

        private bool MembershipExists(int id)
        {
            return db.Memberships.Count(e => e.MembershipId == id) > 0;
        }
    }
}