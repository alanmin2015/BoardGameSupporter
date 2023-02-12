using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Script.Serialization;
using BoardGame1.Models;
using BoardGame1.Models.ViewModels;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace BoardGame1.Controllers
{
    public class GameController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static GameController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        // GET: Game/List
        public ActionResult List()
        {

            string url = "gamedata/ListGames";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<GameDto> games = response.Content.ReadAsAsync<IEnumerable<GameDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            DetailsGame ViewModel = new DetailsGame();

           

            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;

            ViewModel.SelectedGame = SelectedGame;


            url = "membershipdata/listmembershipsforgame/" + id;
            response=client.GetAsync(url).Result;
            IEnumerable<MembershipDto> RelatedMemberships= response.Content.ReadAsAsync<IEnumerable<MembershipDto>>().Result;

            ViewModel.RelatedMemberships = RelatedMemberships;


            url = "membershipdata/ListMembershipsNoGame/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<MembershipDto> AvailableMemberships = response.Content.ReadAsAsync<IEnumerable<MembershipDto>>().Result;
            ViewModel.AvailableMemberships = AvailableMemberships;



            return View(ViewModel);
        }

        // GET: Game/New
        public ActionResult New()
        {
            string url= "RentalData/ListRentals";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<RentalDto> RentalOptions=response.Content.ReadAsAsync<IEnumerable<RentalDto>>().Result;
            return View(RentalOptions);
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            string url = "gamedata/addgame";
            string jsonpayload = jss.Serialize(game);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateGame ViewModel = new UpdateGame();

            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;

            ViewModel.SelectedGame = SelectedGame;

            url = "RentalData/ListRentals/";
            response = client.GetAsync(url).Result;
            IEnumerable<RentalDto> RentalOptions = response.Content.ReadAsAsync<IEnumerable<RentalDto>>().Result;

            ViewModel.RentalOptions = RentalOptions;
            return View(ViewModel);
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Game game)
        {
            string url = "gamedata/updategame/" + id;
            Debug.WriteLine(url);
            string jsonpayload = jss.Serialize(game);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Game/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GameDto selectedgame = response.Content.ReadAsAsync<GameDto>().Result;
            return View(selectedgame);
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "gamedata/deletegame/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
