using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    [Authorize(Roles ="Admin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;

        public ProductController(IHttpClientFactory httpClientFactory, HttpClient client)
        {
            _httpClientFactory = httpClientFactory;
            this.client = client;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")!.Value;
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:5125/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    return View(result);
                }
            }
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")!.Value;
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.DeleteAsync($"http://localhost:5125/api/products/{id}");
            }
            return RedirectToAction("List");
        }

        public async Task <IActionResult> Create()
        {
            var model = new ProductCreateModel();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")!.Value;
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"http://localhost:5125/api/categories");
                if (response.IsSuccessStatusCode) 
                {
                    var jsonData= await response.Content.ReadAsStringAsync();
                 var data=   JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    model.Categories=new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data,"Id", "Definition");
                    return View(model);
                }
              
            }
             
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var data=TempData["Categories"]?.ToString();
            if (data!=null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                model.Categories=new SelectList(categories,"Value","Text");
            }
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")!.Value;
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:5125/api/products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bir hata meydana geldi");
                    }
                }
            }
            return View(model);
        }

    }
}
