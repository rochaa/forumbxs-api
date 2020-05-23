using System;
using Microsoft.AspNetCore.Mvc;

namespace ForumBXS.WebAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public HomeController() { }

        [HttpGet]
        public string Get()
        {
            return new
            {
                service = "ForumBXS",
                success = true,
                version = "1.0",
                host = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            }
            .ToString();
        }
    }
}
