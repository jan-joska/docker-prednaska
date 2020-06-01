using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FunctionsController : ControllerBase
    {
        private static string ReverseString(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        [HttpGet]
        public void Reverse([FromQuery] string input)
        {
            Response.WriteAsync(ReverseString(input), Encoding.UTF8);
        }

        [HttpGet]
        public JsonResult GetVersion()
        {
            return new JsonResult( new {version = Environment.GetEnvironmentVariable("DEMO_API_VERSION") ?? "Unknown"});
        }

    }
}