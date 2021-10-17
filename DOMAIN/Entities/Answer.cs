using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Answer : BaseModel
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        // relation
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
