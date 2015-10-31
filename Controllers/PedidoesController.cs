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
    public class PedidoesController : Controller
    {
        private static string APIUrl = WebConfigurationManager.AppSettings["APIUrl"];
        private static Uri APIUri = new Uri(APIUrl);

        // GET: Pedidoes
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("pedido");
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var pedidos = JsonConvert.DeserializeObject<IEnumerable<Pedido>>(tmpData);
                    return View(pedidos);
                }

            }

            return HttpNotFound();
        }
        
        // GET: Pedidoes/Details/5
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
                HttpResponseMessage response = await client.GetAsync("pedido/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    var pedido = JsonConvert.DeserializeObject<Pedido>(tmpData);
                    return View(pedido);
                }

            }

            return HttpNotFound();
        }

        //PRIVATE HELPER TO GET RESTAURANTS
        private async Task<IEnumerable<Garcom>> getGarcons()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = APIUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET restaurante
                HttpResponseMessage response = await client.GetAsync("garcom");
                if (response.IsSuccessStatusCode)
                {
                    var tmpData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Garcom>>(tmpData);
                }

            }

            return null;
        }

        // GET: Pedidoes/Create
        public async Task<ActionResult> Create()
        {
            var garcons = await getGarcons();
            if (garcons != null)
            {
                ViewBag.GarcomId = new SelectList(garcons, "Id", "Nome");
                return View();
            }

            return HttpNotFound();
        }

        
        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NumeroMesa,Quantidade,Produto,GarcomId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = APIUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    var tmpData = JsonConvert.SerializeObject(pedido);
                    HttpResponseMessage response = await client.PostAsync("pedido", new StringContent(tmpData, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

                return HttpNotFound();
            }


            var garcons = await getGarcons();
            if (garcons != null)
            {
                ViewBag.GarcomId = new SelectList(garcons, "Id", "Nome", pedido.GarcomId);
                return View(pedido);
            }

            return HttpNotFound();
        }

        /*
        // GET: Pedidoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.GarcomId = new SelectList(db.Garcoms, "Id", "Nome", pedido.GarcomId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NumeroMesa,Quantidade,Produto,GarcomId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GarcomId = new SelectList(db.Garcoms, "Id", "Nome", pedido.GarcomId);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            db.Pedidoes.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
