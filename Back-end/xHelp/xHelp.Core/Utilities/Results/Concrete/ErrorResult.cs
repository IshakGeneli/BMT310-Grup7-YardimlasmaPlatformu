using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using xHelp.Core.Utilities.Results.Abstract;

namespace xHelp.Core.Utilities.Results.Concrete
{
    public class ErrorResult : IResult
    {
        public int HttpStatusCode { get; set; }

        public ErrorResult(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = Convert.ToInt32(httpStatusCode);
        }
    }
}
