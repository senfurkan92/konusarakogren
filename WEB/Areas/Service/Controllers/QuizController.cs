using BLL.Data.Service;
using DOMAIN.Entities;
using DTO.QuizDtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Areas.Service.Controllers
{
    [Route("Service/[controller]/[action]")]
    [ApiController]
    [EnableCors("FullAllow")]
    public class QuizController : ControllerBase
    {
        private readonly IService_Quiz quizManager;
        private readonly JsonSerializerSettings serializerSettings;

        public QuizController(IService_Quiz quizManager)
        {
            this.quizManager = quizManager;
            this.serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        /// <summary>
        /// "Home/Index" yolundan gelen yeni quiz kayit isteginin yapilmasi
        /// buraya sorgu vue.js icinde kullanilan axios ile yapilmistir
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] object data)
        {
            var dto = JsonConvert.DeserializeObject<QuizInsertDto>(data.ToString());
            var questions = new List<Question>() {
                new Question () {
                    Content = dto.Question.Q1.Content,
                    Answers = new List<Answer>() { 
                        new Answer() { 
                            Content = dto.Question.Q1.A,
                            IsCorrect = dto.Question.Q1.Correct == "a"
                        },
                        new Answer() {
                            Content = dto.Question.Q1.B,
                            IsCorrect = dto.Question.Q1.Correct == "b"
                        },
                        new Answer() {
                            Content = dto.Question.Q1.C,
                            IsCorrect = dto.Question.Q1.Correct == "c"
                        },
                        new Answer() {
                            Content = dto.Question.Q1.D,
                            IsCorrect = dto.Question.Q1.Correct == "d"
                        },
                    }
                },
                new Question () {
                    Content = dto.Question.Q2.Content,
                    Answers = new List<Answer>() {
                        new Answer() {
                            Content = dto.Question.Q2.A,
                            IsCorrect = dto.Question.Q2.Correct == "a"
                        },
                        new Answer() {
                            Content = dto.Question.Q2.B,
                            IsCorrect = dto.Question.Q2.Correct == "b"
                        },
                        new Answer() {
                            Content = dto.Question.Q2.C,
                            IsCorrect = dto.Question.Q2.Correct == "c"
                        },
                        new Answer() {
                            Content = dto.Question.Q2.D,
                            IsCorrect = dto.Question.Q2.Correct == "d"
                        },
                    }
                },
                new Question () {
                    Content = dto.Question.Q3.Content,
                    Answers = new List<Answer>() {
                        new Answer() {
                            Content = dto.Question.Q3.A,
                            IsCorrect = dto.Question.Q3.Correct == "a"
                        },
                        new Answer() {
                            Content = dto.Question.Q3.B,
                            IsCorrect = dto.Question.Q3.Correct == "b"
                        },
                        new Answer() {
                            Content = dto.Question.Q3.C,
                            IsCorrect = dto.Question.Q3.Correct == "c"
                        },
                        new Answer() {
                            Content = dto.Question.Q3.D,
                            IsCorrect = dto.Question.Q3.Correct == "d"
                        },
                    }
                },
                new Question () {
                    Content = dto.Question.Q4.Content,
                    Answers = new List<Answer>() {
                        new Answer() {
                            Content = dto.Question.Q4.A,
                            IsCorrect = dto.Question.Q4.Correct == "a"
                        },
                        new Answer() {
                            Content = dto.Question.Q4.B,
                            IsCorrect = dto.Question.Q4.Correct == "b"
                        },
                        new Answer() {
                            Content = dto.Question.Q4.C,
                            IsCorrect = dto.Question.Q4.Correct == "c"
                        },
                        new Answer() {
                            Content = dto.Question.Q4.D,
                            IsCorrect = dto.Question.Q4.Correct == "d"
                        },
                    }
                },
            };          
    
            var quiz = new Quiz()
            {
                ArticleTitle = dto.ArticleTitle,
                ArticleContent = dto.ArticleContent,
                Questions = questions,
            };

            var result = await Task.Run(() => quizManager.Insert(quiz));

            return Ok(new {state = result.Success});
        }

        /// <summary>
        /// "/Manager/Index" ve "/Test/Index" yollarında gelen talep ile mevcut quizlerin dondurulmesi
        /// buraya sorgu vue.js icinde kullanilan axios ile yapilmistir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = await Task.Run(() => quizManager.Gets(x=> x.IsActive && !x.IsDeleted, typeof(Quiz).GetProperty("Id"),true,0,-1));
            return Ok(JsonConvert.SerializeObject(query, serializerSettings));
        }

        /// <summary>
        /// "/Test/TakeQuiz" yolu icin ilgili quizin getirilmesi
        /// buraya sorgu vue.js icinde kullanilan axios ile yapilmistir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = await Task.Run(() => quizManager.Get(x => x.Id == id, "Questions.Answers"));
            return Ok(JsonConvert.SerializeObject(query, serializerSettings));
        }

        /// <summary>
        /// "/Manager/Index" yolundan gelen talep ile quizin silinmesi
        /// buraya sorgu vue.js icinde kullanilan axios ile yapilmistir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var quizFind = await Task.Run(() => quizManager.Get(x => x.Id == id));
            var query = await Task.Run(() => quizManager.Delete(quizFind.Data));
            return Ok(JsonConvert.SerializeObject(query, serializerSettings));
        }

        /// <summary>
        /// "/Test/TakeQuiz" yolundan gelen talep ile quiz cevaplarinin donulmesi
        /// buraya sorgu vue.js icinde kullanilan axios ile yapilmistir
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Check([FromBody] object data)
        {
            var dataJson = (JObject)JsonConvert.DeserializeObject(data.ToString());
            var questions = (await Task.Run(() => quizManager.Get(x => x.Id == Convert.ToInt32(dataJson["quizId"]),"Questions.Answers"))).Data.Questions;
            var list = new List<string>();
            questions.ToList().ForEach(x => {
                var answers = x.Answers.ToList();
                for (int i = 0; i < answers.Count; i++)
                {
                    if (answers[i].IsCorrect)
                    {
                        switch (i) {
                            case 0: list.Add("a"); break;
                            case 1: list.Add("b"); break;
                            case 2: list.Add("c"); break;
                            case 3: list.Add("d"); break;
                        }
                        continue;
                    }
                }
            });
            return Ok(list);
        }
    }
}
