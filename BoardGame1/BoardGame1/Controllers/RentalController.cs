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
using Rental = BoardGame1.Models.Rental;
using System.Web.UI.WebControls;

namespace BoardGame1.Controllers
{
    public class RentalController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static RentalController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        // GET: Rental
        public ActionResult List()
        {

            string url = "RentalData/ListRentals";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<RentalDto> rentals = response.Content.ReadAsAsync<IEnumerable<RentalDto>>().Result;
        


            return View(rentals);
        }

        // GET: Rental/Details/5
        public ActionResult Details(int id)
        {

  

            DetailsRental ViewModel = new DetailsRental();

            string url = "RentalData/findrental/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RentalDto SelectedRental = response.Content.ReadAsAsync<RentalDto>().Result;
            

            ViewModel.SelectedRental = SelectedRental;

     


            return View(ViewModel);
        }

        // GET: Rental/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Rental/Create
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            string url = "RentalData/addrental";
            string jsonpayload = jss.Serialize(rental);
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

        // GET: Rental/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "RentalData/findrental/" + id;
           
            HttpResponseMessage response = client.GetAsync(url).Result;
            RentalDto selectedRental = response.Content.ReadAsAsync<RentalDto>().Result;
       
            return View(selectedRental);
        }

        // POST: Rental/Update/5
        [HttpPost]
        public ActionResult Update(int id, Rental rental)
        {
            Debug.WriteLine(789);
            string url = "rentaldata/updaterental/" + id;
            Debug.WriteLine(url);
            string jsonpayload = jss.Serialize(rental);
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

        // GET: Rental/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "RentalData/findrental/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RentalDto selectedRental = response.Content.ReadAsAsync<RentalDto>().Result;
            return View(selectedRental);
        }

        // POST: Rental/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "RentalData/deleterental/" + id;
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
