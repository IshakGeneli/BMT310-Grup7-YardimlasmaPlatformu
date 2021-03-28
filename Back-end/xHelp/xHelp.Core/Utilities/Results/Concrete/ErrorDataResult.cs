using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using xHelp.Core.Utilities.Results.Abstract;

namespace xHelp.Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : IDataResult<T>
    {
        public T Data { get; set; }
        public int HttpStatusCode { get; set; }

        public ErrorDataResult(T data, HttpStatusCode httpStatusCode)
        {
            this.Data = data;
            this.HttpStatusCode = Convert.ToInt32(httpStatusCode);
        }
    }
}
