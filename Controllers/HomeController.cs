using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class HomeController : Controller
    {
        private static string APIUrl = WebConfigurationManager.AppSettings["APIUrl"];
        private static Uri APIUri = new Uri(APIUrl);

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Relatorio()
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

                    ViewBag.GarcomId = new SelectList(garcons, "Id", "Nome");
                    ViewBag.APIUrl = WebConfigurationManager.AppSettings["APIUrl"];
                    return View();
                }

            }

            return HttpNotFound();
        }

        /*public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
    }
}