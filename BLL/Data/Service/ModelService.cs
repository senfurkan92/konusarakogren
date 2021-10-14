using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Service
{
    public interface IService_Quiz : IService_Generic<Quiz>
    { 
    
    }

    public interface IService_Question : IService_Generic<Question>
    {

    }

    public interface IService_Answer : IService_Generic<Answer>
    {

    }
}
