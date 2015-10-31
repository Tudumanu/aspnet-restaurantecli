using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using CorsClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CorsClient.Controllers
{
    public class RestaurantesController : Controller
    {
        private static string APIUrl = WebConfigurationManager.AppSettings["APIUrl"];
        private static Uri APIUri = new Uri(APIUrl);

        // GET: Restaurantes
        public async Task<ActionResult> Index()
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
                    return View(restaurantes);
                }

            }

            return HttpNotFound();
        }

        // GET: Restaurantes/Details/5
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
                HttpResponseMessage response = await client.GetAsync("restaurante/"+id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var restaurante = JsonConvert.DeserializeObject<Restaurante>(tmpData);
                    return View(restaurante);
                }

            }

            return HttpNotFound();
        }

        // GET: Restaurantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Endereco,Cidade,Estado")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    var tmpData = JsonConvert.SerializeObject(restaurante);
                    HttpResponseMessage response = await client.PostAsync("restaurante", new StringContent(tmpData, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

                return HttpNotFound();
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
                HttpResponseMessage response = await client.GetAsync("restaurante/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var restaurante = JsonConvert.DeserializeObject<Restaurante>(tmpData);
                    return View(restaurante);
                }

            }

            return HttpNotFound();
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Endereco,Cidade,Estado")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP PUT
                    var tmpData = JsonConvert.SerializeObject(restaurante);
                    HttpResponseMessage response = await client.PutAsync("restaurante/"+restaurante.Id.ToString(), new StringContent(tmpData, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

                return RedirectToAction("Index");
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
                HttpResponseMessage response = await client.GetAsync("restaurante/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var restaurante = JsonConvert.DeserializeObject<Restaurante>(tmpData);
                    return View(restaurante);
                }

            }

            return HttpNotFound();
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("restaurante/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();

                    // HTTP DELETE
                    response = await client.DeleteAsync("restaurante/" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }                  
                }
            }

            return HttpNotFound();
        }
    }
}
