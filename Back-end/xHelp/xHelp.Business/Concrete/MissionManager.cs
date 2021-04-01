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
        private readonly IEvidenceService _evidenceService;
        private readonly IMapper _mapper;

        public MissionManager(IMissionDal missionDal, IMapper mapper, IEvidenceService evidenceService)
        {
            _missionDal = missionDal;
            _mapper = mapper;
            _evidenceService = evidenceService;
        }

        public async Task<IDataResult<Mission>> AddMissionAsync(CreateMissionDTO createMissionDTO)
        {
            var mission = _mapper.Map<Mission>(createMissionDTO);
            await _missionDal.AddAsync(mission);
            return new SuccessfulDataResult<Mission>(mission,HttpStatusCode.Created);
        }

        public async Task CreateEvidencesOnMission(CreateEvidenceListDTO createEvidenceListDTO)
        {
            var evidences = _mapper.Map<List<Evidence>>(createEvidenceListDTO.CreateEvidencesDTO);

            foreach (var evidence in evidences)
            {
                evidence.MissionId = createEvidenceListDTO.MissionId;
            }

            await _evidenceService.AddEvidencesAsync(evidences);
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

        public async Task<IDataResult<ICollection<Mission>>> GetAllWithEvidencesAsync()
        {
            var missions = await _missionDal.GetListWithEvidencesAsync();

            return new SuccessfulDataResult<ICollection<Mission>>(missions, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Mission>> GetMissionByIdAsync(int id)
        {
            var mission = await _missionDal.GetAsync(m => m.Id == id);
            return new SuccessfulDataResult<Mission>(mission,HttpStatusCode.OK);
        }

        public async Task<IDataResult<Mission>> GetWithEvidencesAsync(int id)
        {
            var mission = await _missionDal.GetWithEvidencesAsync(m => m.Id == id);
            return new SuccessfulDataResult<Mission>(mission, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Mission>> UpdateMissionAsync(UpdateMissionDTO updateMissionDTO)
        {
            var mission = _mapper.Map<Mission>(updateMissionDTO);
            var evidences = _mapper.Map<ICollection<Evidence>>(updateMissionDTO.UpdateEvidenceDTOs);

            var addedNewMission = await _missionDal.UpdateAsync(mission);
            await _evidenceService.UpdateEvidencesAsync(evidences);

            return new SuccessfulDataResult<Mission>(addedNewMission, HttpStatusCode.Created);
        }
    }
}
