using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
    public class BaseReturn
    {
        public string Message { get; internal set; }
        public bool IsError { get; internal set; }

        public string? info { get; internal set; }

        public BaseReturn returnSucess(object result)
        {
            return new BaseReturn
            {
                IsError = false,
                Message = JsonSerializer.Serialize(result),
                info = null
            };
        }

        public BaseReturn returnError(Exception result)
        {
            return new BaseReturn
            {
                IsError = true,
                Message = JsonSerializer.Serialize(result.Message),
                info = result.StackTrace
            };
        }

    }
}
