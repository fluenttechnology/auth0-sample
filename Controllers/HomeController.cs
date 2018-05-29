using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Reviewers.Controllers
{
    public class HomeController : Controller
    {

        //const string API = "http://localhost:8890/widgets/hello-world?name=YOLO";
        //const string API = "http://localhost:5001/widgets/hello-world?name=YOLO";
        //const string API = "http://localhost:22585/Service1.svc/GetForms";

        const string API = "/api/helloworld";

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                //string idToken = await HttpContext.GetTokenAsync("id_token");
                using(var client = new HttpClient()) {

                    var url = $"{Request.Scheme}://{Request.Host}{API}";
Console.WriteLine( url );
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    try {

                        var fetched = await client.SendAsync(request);
                        if(!fetched.IsSuccessStatusCode) {
                            throw new Exception($"Failed to fetch: {fetched.ReasonPhrase} {fetched.StatusCode}");
                        }
                        var content = await fetched.Content.ReadAsStringAsync();
                        ViewData[ "fromAPI" ] = content;

                    } catch(Exception ex) {

                        ViewData[ "fromAPI" ] = ex.ToString();

                    }

                }

            }

            return View();
        }

    }
}
