using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Core.Utilities.Results.Abstract
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; set; }
    }
}
