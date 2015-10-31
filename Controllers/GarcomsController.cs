using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CorsClient.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Text;

namespace CorsClient.Controllers
{
    public class GarcomsController : Controller
    {
        private static string APIUrl = WebConfigurationManager.AppSettings["APIUrl"];
        private static Uri APIUri = new Uri(APIUrl);

        // GET: Garcoms
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("garcom");
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var garcons = JsonConvert.DeserializeObject<IEnumerable<Garcom>>(tmpData);
                    return View(garcons);
                }

            }

            return HttpNotFound();
        }

        // GET: Garcoms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("garcom/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var garcom = JsonConvert.DeserializeObject<Garcom>(tmpData);
                    return View(garcom);
                }

            }

            return HttpNotFound();
        }

        // GET: Garcoms/Create
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("restaurante");
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var restaurantes = JsonConvert.DeserializeObject<IEnumerable<Restaurante>>(tmpData);
                    ViewBag.RestauranteId = new SelectList(restaurantes, "Id", "Nome");
                    return View();
                }

            }

            return HttpNotFound();
        }

        // POST: Garcoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Idade,RestauranteId")] Garcom garcom)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    var tmpData = JsonConvert.SerializeObject(garcom);
                    HttpResponseMessage response = await client.PostAsync("garcom", new StringContent(tmpData, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

                return HttpNotFound();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("restaurante");
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var restaurantes = JsonConvert.DeserializeObject<IEnumerable<Restaurante>>(tmpData);
                    ViewBag.RestauranteId = new SelectList(restaurantes, "Id", "Nome", garcom.RestauranteId);
                    return View(garcom);
                }

            }

            return HttpNotFound();
            
        }

        // GET: Garcoms/Edit/5
        /*public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garcom garcom = db.Garcoms.Find(id);
            if (garcom == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "Id", "Nome", garcom.RestauranteId);
            return View(garcom);
        }

        // POST: Garcoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Idade,RestauranteId")] Garcom garcom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(garcom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestauranteId = new SelectList(db.Restaurantes, "Id", "Nome", garcom.RestauranteId);
            return View(garcom);
        }

        // GET: Garcoms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garcom garcom = db.Garcoms.Find(id);
            if (garcom == null)
            {
                return HttpNotFound();
            }
            return View(garcom);
        }

        // POST: Garcoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Garcom garcom = db.Garcoms.Find(id);
            db.Garcoms.Remove(garcom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
