using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Quiz : BaseModel
    {
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }

        // relation
        public string UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
