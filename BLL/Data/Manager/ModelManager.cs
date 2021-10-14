using BLL.Data.Service;
using DAL.Data.Abstract;
using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Manager
{
    public class Manager_Quiz : Manager_Generic<Quiz, IDal_Quiz>, IService_Quiz
    {
        public Manager_Quiz(IDal_Quiz dal) : base (dal)
        {

        }
    }

    public class Manager_Question : Manager_Generic<Question, IDal_Question>, IService_Question
    {
        public Manager_Question(IDal_Question dal) : base(dal)
        {

        }
    }

    public class Manager_Answer : Manager_Generic<Answer, IDal_Answer>, IService_Answer
    {
        public Manager_Answer(IDal_Answer dal) : base(dal)
        {

        }
    }
}
