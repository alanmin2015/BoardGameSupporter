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
using BoardGame1.Models;
using System.Diagnostics;
using BoardGame1.Migrations;
using Game = BoardGame1.Models.Game;
using Membership = BoardGame1.Models.Membership;

namespace BoardGame1.Controllers
{
    public class GameDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GameData/ListGames
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult ListGames()
        {
            List<Game> Games= db.Games.ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(a => GameDtos.Add(new GameDto()
            {
                GameId = a.GameId,
                GameName = a.GameName,
                GamePrice = a.GamePrice,
               RentalId = a.Rentals.RentalId,
                RentDate = a.Rentals.RentDate
            }));

            return Ok(GameDtos);
        }

        // GET: api/GameData/ListGamesForRental/5
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult ListGamesForRental(int id)
        {
            List<Game> Games = db.Games.Where(a=>a.RentalId==id).ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(a => GameDtos.Add(new GameDto()
            {
                GameId = a.GameId,
                GameName = a.GameName,
                GamePrice = a.GamePrice,
                RentalId = a.Rentals.RentalId,
                RentDate = a.Rentals.RentDate
            }));

            return Ok(GameDtos);
        }

        // GET: api/GameData/ListGamesForMembership/1
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult ListGamesForMembership(int id)
        {
            List<Game> Games = db.Games.Where(
                a => a.Memberships.Any(
                    m=>m.MembershipId==id
                    )).ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(a => GameDtos.Add(new GameDto()
            {
                GameId = a.GameId,
                GameName = a.GameName,
                GamePrice = a.GamePrice,
                RentalId = a.Rentals.RentalId,
                RentDate = a.Rentals.RentDate
            }));

            return Ok(GameDtos);
        }

        [HttpPost]
        [Route("api/gamedata/AssociateGameWithMembership/{gameid}/{membershipid}")]

        public IHttpActionResult AssociateGameWithMembership(int gameid, int membershipid)
        {
            Game SelectedGame = db.Games.Include(a=>a.Memberships).Where(a=>a.GameId==gameid).FirstOrDefault();
            Membership SelectedMembership=db.Memberships.Find(membershipid);
 

            SelectedGame.Memberships.Add(SelectedMembership);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/gamedata/UnAssociateGameWithMembership/{gameid}/{membershipid}")]
        public IHttpActionResult UnAssociateGameWithMembership(int gameid, int membershipid)
        {
            Game SelectedGame = db.Games.Include(a => a.Memberships).Where(a => a.GameId == gameid).FirstOrDefault();
            Membership SelectedMembership = db.Memberships.Find(membershipid);





            SelectedGame.Memberships.Remove(SelectedMembership);
            db.SaveChanges();

            return Ok();
        }

        // GET: api/GameData/FindGame/1
        [ResponseType(typeof(GameDto))]
        [HttpGet]
        public IHttpActionResult FindGame(int id)
        {
            Game game = db.Games.Find(id);
            GameDto GameDto = new GameDto()
            {
                GameId = game.GameId,
                GameName = game.GameName,
                GamePrice = game.GamePrice,
                RentalId = game.Rentals.RentalId,
                RentDate = game.Rentals.RentDate
            };
            if (game == null)
            {
                return NotFound();
            }

            return Ok(GameDto);
        }

        // POST: api/GameData/UpdateAnimal/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.GameId)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/GameData/AddGame
        [ResponseType(typeof(Game))]
        [HttpPost]
        public IHttpActionResult AddGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(game);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game.GameId }, game);
        }

        // POST: api/GameData/DeleteAnimal/5
        [ResponseType(typeof(Game))]
        [HttpPost]
        public IHttpActionResult DeleteGame(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
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

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.GameId == id) > 0;
        }
    }
}