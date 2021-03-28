using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        int HttpStatusCode { get; set; }
    }
}
