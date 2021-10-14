using System;

namespace CORE.Data.Models
{
    public interface IBaseModel
    {
        DateTime? DeleteDate { get; set; }
        int Id { get; set; }
        DateTime InsertDate { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
        DateTime UpdateDate { get; set; }
    }
}