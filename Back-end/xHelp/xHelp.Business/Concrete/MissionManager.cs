using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Concrete
{
    public class MissionManager : IMissionService
    {
        private readonly IMissionDal _missionDal;

        public MissionManager(IMissionDal missionDal)
        {
            _missionDal = missionDal;
        }

        public async Task<Mission> AddMissionAsync(Mission mission)
        {
            return await _missionDal.AddAsync(mission);
        }

        public async Task DeleteMissionAsync(int id)
        {
            await _missionDal.DeleteAsync(new Mission { Id = id });
        }

        public async Task<ICollection<Mission>> GetAllAsync()
        {
            return await _missionDal.GetListAsync();
        }

        public async Task<Mission> GetMissionByIdAsync(int id)
        {
            return await _missionDal.GetAsync(m => m.Id == id);
        }

        public async Task UpdateMissionAsync(Mission mission)
        {
            await _missionDal.UpdateAsync(mission);
        }
    }
}
