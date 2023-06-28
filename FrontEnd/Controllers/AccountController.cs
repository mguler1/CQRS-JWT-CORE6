using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }
        public IActionResult SignIn()
        {
            return View(new UserLoginModel());
        }
        [HttpPost]
        public async  Task<IActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var content=new StringContent(JsonSerializer.Serialize(model),Encoding.UTF8,"application/json");
                var response= await client.PostAsync("http://localhost:5125/api/Auth/SignIn",content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenModel=
                    JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData,new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase });
                    if (tokenModel!=null)
                    {
                        JwtSecurityTokenHandler handler = new();
                        var token = handler.ReadJwtToken(tokenModel.Token);
                        var claimsIdentity = new ClaimsIdentity(token.Claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties { ExpiresUtc=tokenModel.ExpireDate,IsPersistent=true};

                     await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);
                        return RedirectToAction("Index","Home");
                    }
               
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı ve Şifre Hatalı");
                }
            }
            return View(model);
        }
    }
}
