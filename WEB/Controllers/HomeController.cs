using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OpenQA.Selenium.Interactions;
using Selenium;
using System.Web;

namespace WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// yeni quiz olusturulmasi
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.wired.com/");
            var list = new List<ArticleOption>();

            for (int i = 0; i < 5; i++)
            {
                // HtmlAgilityPack scrap title and url
                var title = doc.DocumentNode.SelectSingleNode($"//*[@id='app-root']/div/div[3]/div/div/div[2]/div[3]/div[1]/div[1]/div/ul/li[{i+1}]/a/div[2]/h5").InnerText;
                var url = "https://www.wired.com/" + doc.DocumentNode.SelectSingleNode($"//*[@id='app-root']/div/div[3]/div/div/div[2]/div[3]/div[1]/div[1]/div/ul/li[{i+1}]/a").GetAttributeValue("href", "");
             
                list.Add(new ArticleOption()
                {
                    Title = HttpUtility.HtmlDecode(title),
                    Url = url
                });
            }

            return View(list);
        }
    }
}
