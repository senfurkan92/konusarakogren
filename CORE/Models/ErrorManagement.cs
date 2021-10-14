using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public static class ErrorManagement
    {
        public static Exception GetInnestExp(this Exception ex)
        {
            if (ex.InnerException == null) return ex;
            else return ex.InnerException.GetInnestExp();
        }
    }
}
