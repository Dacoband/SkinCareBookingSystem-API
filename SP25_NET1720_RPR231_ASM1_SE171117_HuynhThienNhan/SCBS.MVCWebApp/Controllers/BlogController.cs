using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SCBS.Repositories.Models;

namespace SCBS.MVCWebApp.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private string APIEndPoint = "https://localhost:7294/api/";
        public BlogController() { }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion
                using (var response = await httpClient.GetAsync(APIEndPoint + "Blog"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Blog>>(content);    
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return View(new List<Blog>());
        }
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "Blog/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Blog>(content);
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return View(new Blog());
        }
        public async Task<IActionResult> Create()
        {
            ViewData["BlogImageId"] = new SelectList(await GetBlogImage(), "Id", "Email");
            return View();
        }
        private async Task<List<BlogImage>> GetBlogImage()
        {
            var blogImages = new List<BlogImage>();
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                using (var response = await httpClient.GetAsync(APIEndPoint + "BlogImage"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        blogImages = JsonConvert.DeserializeObject<List<BlogImage>>(content);
                    }
                }
            }
            return blogImages;
        }
        public async Task<IActionResult> BlogList()
        {
            return View();
        }
    }
}
