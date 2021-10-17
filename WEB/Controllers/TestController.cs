using BLL.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly IService_Quiz quizManager;

        public TestController(IService_Quiz quizManager)
        {
            this.quizManager = quizManager;
        }

        /// <summary>
        /// baslanilacak quiz secimi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// secilen quizin sunulmasi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TakeQuiz(int id)
        {        
            return View(id);
        }
    }
}
