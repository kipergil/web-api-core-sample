using System;
using System.Collections.Generic;
using System.Text;

namespace RunPath.WebApi.Domain
{
    public class ApiException : Exception
    {
        public ApiException()
        {

        }

        public int StatusCode { get; set; }
        public object Content { get; set; }
    }
}
