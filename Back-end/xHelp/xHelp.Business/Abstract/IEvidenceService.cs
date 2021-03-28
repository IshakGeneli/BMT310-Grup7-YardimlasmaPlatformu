﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Abstract
{
    public interface IEvidenceService
    {
        Task<ICollection<Evidence>> GetAllAsync();
        Task<Evidence> GetEvidenceByIdAsync(int id);
        Task<Evidence> AddEvidenceAsync(Evidence evidence);
        Task UpdateEvidenceAsync(Evidence evidence);
        Task DeleteEvidenceAsync(int id);
    }
}