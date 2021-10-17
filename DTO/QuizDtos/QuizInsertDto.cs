using DTO.QuestionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuizDtos
{
    public class QuizInsertDto
    {
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }

        public QuestionsInsertDto Question { get; set; }
    }
}
