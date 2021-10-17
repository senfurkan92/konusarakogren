using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    /// <summary>
    /// Her entity icin ortak olacak propertylerin belirlenmesi
    /// FluentApi yapilarinin kalitim ile kisaltilmasına icin destek 
    /// </summary>
    public abstract class BaseModel : IBaseModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
