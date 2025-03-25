using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WCP.Repositories.DBContext;
using WCP.Repositories.Models;

namespace WCP.WebClient.Controllers
{
    public class WatercolorsPaintingController : Controller
    {
        private string APIEndPoint = "https://localhost:7276/api/";
        private string OdataEndPoint = "https://localhost:7276/odata/";

        // GET: WatercolorsPainting
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "WatercolorsPainting"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<WatercolorsPainting>>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return View(new List<WatercolorsPainting>());
        }


        public async Task<IActionResult> Search(string? paintingAuthor, int? publishYear)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                var filter = $"$filter=contains(PaintingAuthor, '{paintingAuthor}') and contains(PublishYear, '{publishYear}')";
                //var groupBy = "$apply=groupby((CosmeticName, CosmeticSize)";
                var odataQuery = $"{OdataEndPoint}ComesticInformation?{filter}";

                using (var response = await httpClient.GetAsync(odataQuery))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<ODataResponse<WatercolorsPainting>>(content);

                        if (result != null)
                        {
                            return View("Index", result.Value);
                        }
                    }
                }
            }
            return View("Index", new List<WatercolorsPainting>());
        }

        public class ODataResponse<T>
        {
            [JsonProperty("value")]
            public List<T> Value { get; set; }
        }

        // GET: WatercolorsPainting/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "WatercolorsPainting/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WatercolorsPainting>(content);
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }

        private async Task<List<Style>> GetStyles()
        {
            var Styles = new List<Style>();
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "WatercolorsPainting/styles"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Styles = JsonConvert.DeserializeObject<List<Style>>(content);
                    }
                }
            }
            return Styles;
        }

        // GET: WatercolorsPainting/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StyleId"] = new SelectList(await GetStyles(), "StyleId", "StyleName");
            return View();
        }

        // POST: WatercolorsPainting/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaintingId,PaintingName,PaintingDescription,PaintingAuthor,Price,PublishYear,CreatedDate,StyleId")] WatercolorsPainting watercolorsPainting)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request

                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    #endregion

                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "WatercolorsPainting", watercolorsPainting))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);
                            if (result > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            }
            ViewData["StyleId"] = new SelectList(await GetStyles(), "StyleId", "StyleName", watercolorsPainting.StyleId);
            return View(watercolorsPainting);
        }

        // GET: WatercolorsPainting/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Categorys = await GetStyles();
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "WatercolorsPainting/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WatercolorsPainting>(content);
                        if (result != null)
                        {
                            ViewData["StyleId"] = new SelectList(Categorys, "StyleId", "StyleName", result.StyleId);
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: WatercolorsPainting/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PaintingId,PaintingName,PaintingDescription,PaintingAuthor,Price,PublishYear,CreatedDate,StyleId")] WatercolorsPainting watercolorsPainting)
        {
          

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request

                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    #endregion

                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "WatercolorsPainting/" + id, watercolorsPainting))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);
                            if (result > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            }
            ViewData["StyleId"] = new SelectList(await GetStyles(), "StyleId", "StyleId", watercolorsPainting.StyleId);
            return View(watercolorsPainting);
        }

        // GET: WatercolorsPainting/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "WatercolorsPainting/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WatercolorsPainting>(content);
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: WatercolorsPainting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.DeleteAsync(APIEndPoint + "WatercolorsPainting/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
    
}
