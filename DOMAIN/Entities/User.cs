using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DOMAIN.Entities
{
    public class User : IdentityUser
    {
        // relation 
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
