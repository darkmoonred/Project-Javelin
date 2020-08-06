using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProjectJavelin.Models;
using System.Threading.Tasks;

namespace Project_Javelin.Controllers
{
    public class GamesController : Controller
    {

        private xikfmwkwContext _context;
        public GamesController(xikfmwkwContext context)
        {
            this._context = context;
        }


        // GET: GamesLatest
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Games> games = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://halo.api.stdlib.com/mcc@0.0.11/games/latest/");
                var responseTask = client.GetAsync("games");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Games>>();
                    readJob.Wait();
                    games = readJob.Result;

                   
                }
                else
                {
                    //return the error code here
                    games = Enumerable.Empty<Games>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Contact administrator for help.");
                }
            }

            return View(games);
        }


        // POST: Customer
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Gamertag model)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://halo.api.stdlib.com/mcc@0.0.11/games/latest/");

            request.UserAgent = "PostmanRuntime/7.25.0";
            request.ContentType = "application/json";

            string reqString = "{\"gamertag\": \"" + model.GamertagName + "\" }";

            byte[] requestData = Encoding.UTF8.GetBytes(reqString);


            request.ContentLength = requestData.Length;
            request.Method = "POST";
            Stream dataStream = request.GetRequestStream();
            //from what index and from what length, 0 = index from beggining
            dataStream.Write(requestData, 0, requestData.Length);
            dataStream.Close();

            //get the response
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            GamesContainer games;

            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                games = JsonConvert.DeserializeObject<GamesContainer>(responseFromServer);
            };
            response.Close();

            //Add new stat entry to postgresql database
            var newStatsEntry = new Stats
            {
                Gamertag = games.gamertag,
                GameVariant = games.games.FirstOrDefault().gameVariant,
                MapId = games.games.FirstOrDefault().mapId,
                Won = games.games.FirstOrDefault().won,
                Score = games.games.FirstOrDefault().score,
                Kills = games.games.FirstOrDefault().kills,
                Deaths = games.games.FirstOrDefault().deaths,
                Assists = games.games.FirstOrDefault().assists,
                Headshots = games.games.FirstOrDefault().headshots,
                Medals = games.games.FirstOrDefault().medals,
                KillDeathRatio = games.games.FirstOrDefault().killDeathRatio,
                HeadShotRate = games.games.FirstOrDefault().headshotRate,
                PlayedAt = games.games.FirstOrDefault().playedAt

            };

            _context.Stats.Add(newStatsEntry);
            await _context.SaveChangesAsync();

            return View("Index", games.games);
        }
    }
}