using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Areas
{
    [Route("Service/[controller]/[action]")]
    [ApiController]
    [EnableCors("FullAllow")]
    public class ScrapController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public ScrapController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        public IActionResult GetArticleContent([FromBody] object data)
        {
            var url = ((JObject)JsonConvert.DeserializeObject(data.ToString()))["url"];

            // Selenium for dynamic scrap              
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.Combine(env.WebRootPath, "chrome"));
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            var driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(url.ToString());
            var elements = driver.FindElement(By.TagName("article")).FindElements(By.TagName("p"));
            var content = string.Empty;
            foreach (var item in elements)
            {
                content += $"<p>{item.Text}</p>";
            }
            return Ok(content);
        }
    }
}
