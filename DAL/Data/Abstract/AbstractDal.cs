using CORE.Data.Abstract;
using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Abstract
{
    public interface IDal_Quiz : IRepository<Quiz>
    { 
    
    }

    public interface IDal_Question : IRepository<Question>
    {

    }

    public interface IDal_Answer : IRepository<Answer>
    {

    }
}
