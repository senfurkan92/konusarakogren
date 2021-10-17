using System.Collections.Generic;

namespace CORE.Models
{
    public class ResultModel
    {
        public bool Success { get; set; }

        public string Description { get; set; }

        public List<string> Errors { get; set; }

        public ResultModel()
        {

        }

        public ResultModel(bool success, string description, List<string> errors = null)
        {
            Success = success;
            Description = description;
            Errors = errors;
        }
    }

    public class ResultModel<TData> : ResultModel
    {
        public TData Data { get; set; }

        public ResultModel()
        {

        }

        public ResultModel(TData data, bool success, string description, List<string> errors = null)
            : base(success,description,errors)
        {
            Data = data;
        }
    }
}
