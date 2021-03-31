using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Core.Utilities.Results.Abstract;
using xHelp.Core.Utilities.Results.Concrete;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Concrete
{
    public class MissionManager : IMissionService
    {
        private readonly IMissionDal _missionDal;
        private readonly IMapper _mapper;

        public MissionManager(IMissionDal missionDal, IMapper mapper)
        {
            _missionDal = missionDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<Mission>> AddMissionAsync(CreateMissionDTO createMissionDTO)
        {
            var mission = _mapper.Map<Mission>(createMissionDTO);
            await _missionDal.AddAsync(mission);
            return new SuccessfulDataResult<Mission>(mission,HttpStatusCode.Created);
        }

        public async Task DeleteMissionAsync(int id)
        {
            await _missionDal.DeleteAsync(new Mission { Id = id });
        }

        public async Task<IDataResult<ICollection<Mission>>> GetAllAsync()
        {
            var missions = await _missionDal.GetListAsync();

            return new SuccessfulDataResult<ICollection<Mission>>(missions, HttpStatusCode.OK);
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
