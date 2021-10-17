using CORE.Models;
using HtmlAgilityPack;
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
            try
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.Combine(env.WebRootPath, "chrome"));
                service.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");
                var driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
                driver.Navigate().GoToUrl(url.ToString());
                IReadOnlyCollection<IWebElement> elements = null;
                try
                {
                    elements = driver.FindElement(By.TagName("article")).FindElements(By.TagName("p"));
                }
                catch (Exception)
                {
                    try
                    {
                        var container = driver.FindElement(By.TagName("div")).FindElement(By.ClassName("BodyWrapper-ctnerm"));
                        elements = container.FindElements(By.TagName("p"));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                var content = string.Empty;
                foreach (var item in elements)
                {
                    content += $"<p>{item.Text}</p>";
                }
                return Ok(new { state = true, content = content});
            }
            catch (Exception ex)
            {
                return Ok(new { state = false, content = $"<b class='text-danger'>This article is not suitable.<b><br /><small>Advertisement Content</small><br />" });
            }                   
        }
    }
}
