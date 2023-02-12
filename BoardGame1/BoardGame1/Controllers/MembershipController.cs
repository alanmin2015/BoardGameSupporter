using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using BoardGame1.Models;
using BoardGame1.Models.ViewModels;
using System.Web.Script.Serialization;
using BoardGame1.Migrations;
using Membership = BoardGame1.Models.Membership;
using System.Web.UI.WebControls;

namespace BoardGame1.Controllers
{
    public class MembershipController : Controller

    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static MembershipController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        // GET: Membership
        public ActionResult List()
        {
            string url = "MembershipData/ListMemberships";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<MembershipDto> memberships = response.Content.ReadAsAsync<IEnumerable<MembershipDto>>().Result;



            return View(memberships);
        }

        // GET: Membership/Details/5
        public ActionResult Details(int id)
        {
            DetailsMembership ViewModel = new DetailsMembership();

            string url = "MembershipData/findmembership/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            MembershipDto SelectedMembership = response.Content.ReadAsAsync<MembershipDto>().Result;


            ViewModel.SelectedMembership = SelectedMembership;


            url = "gameData/listgamesformembership/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<GameDto> KeptGames = response.Content.ReadAsAsync<IEnumerable<GameDto>>().Result;

            ViewModel.KeptGames = KeptGames;

            return View(ViewModel);
        }

        // GET: Membership/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Membership/Create
        [HttpPost]
        public ActionResult Create(Membership membership)
        {
            string url = "MembershipData/addmembership";
            string jsonpayload = jss.Serialize(membership);
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

        // GET: Membership/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "MembershipData/findmembership/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            MembershipDto SelectedMembership = response.Content.ReadAsAsync<MembershipDto>().Result;

            return View(SelectedMembership);
        }

        // POST: Membership/Update/5
        [HttpPost]
        public ActionResult Update(int id, Membership membership)
        {
          
            string url = "MembershipData/updatemembership/" + id;
       
            string jsonpayload = jss.Serialize(membership);
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

        // GET: Membership/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "MembershipData/findmembership/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MembershipDto SelectedMembership = response.Content.ReadAsAsync<MembershipDto>().Result;
            return View(SelectedMembership);
        }

        // POST: Membership/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "MembershipData/deletemembership/" + id;
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
