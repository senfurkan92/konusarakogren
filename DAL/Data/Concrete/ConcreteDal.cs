using CORE.Data.Concrete;
using DAL.Context;
using DAL.Data.Abstract;
using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Concrete
{
    public class Dal_Quiz : Repository<Quiz, ProjectDbContext>, IDal_Quiz
    {
        public Dal_Quiz(ProjectDbContext ctx) : base (ctx)
        {

        }
    }

    public class Dal_Question : Repository<Question, ProjectDbContext>, IDal_Question
    {
        public Dal_Question(ProjectDbContext ctx) : base(ctx)
        {

        }
    }

    public class Dal_Answer : Repository<Answer, ProjectDbContext>, IDal_Answer
    {
        public Dal_Answer(ProjectDbContext ctx) : base(ctx)
        {

        }
    }
}
