using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Core.DataAccess;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Abstract
{
    public interface IEvidenceDal : IEntityRepository<Evidence>
    {
        Task AddEvidencesAsync(ICollection<Evidence> evidences);
    }
}
