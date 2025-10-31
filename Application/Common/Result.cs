using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Error { get; set; }

        public static Result Ok() => new Result { Success = true };
        public static Result Fail(string message) => new Result { Success = false, Error = message };
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Ok(T data) => new Result<T> { Success = true, Data = data };
        public new static Result<T> Fail(string message) => new Result<T> { Success = false, Error = message };
    }    
}
