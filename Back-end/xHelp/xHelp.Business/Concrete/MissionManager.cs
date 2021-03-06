using AutoMapper;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Utilities.Abstract;
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
        private ICloudinaryOperations _cloudinaryOperations;

        public MissionManager(
            IMissionDal missionDal, 
            IMapper mapper, 
            IEvidenceService evidenceService, 
            ICloudinaryOperations cloudinaryOperations)
        {
            _missionDal = missionDal;
            _mapper = mapper;
            _evidenceService = evidenceService;
            _cloudinaryOperations = cloudinaryOperations;
        }

        public async Task<IDataResult<Mission>> AddMissionAsync(CreateMissionDTO createMissionDTO)
        {
            var uploadResult = await _cloudinaryOperations.UploadImageAsync(createMissionDTO.ImageFile);

            var mission = _mapper.Map<Mission>(createMissionDTO);
            await AddMissionWithImageAsync(mission, uploadResult);
            
            return new SuccessfulDataResult<Mission>(mission,HttpStatusCode.Created);
        }

        public async Task CreateEvidenceOnMission(CreateEvidenceDTO createEvidenceDTO)
        {
            await _evidenceService.AddEvidenceAsync(createEvidenceDTO);
        }

        public async Task<IResult> DeleteMissionAsync(int id)
        {
            await _missionDal.DeleteAsync(new Mission { Id = id });

            return new SuccessfulResult(HttpStatusCode.OK);
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

        public async Task<IDataResult<Mission>> UpdateMissionAsync(UpdateMissionDTO updateMissionDTO)
        {
            var mission = await _missionDal.GetMissionWithImagesAsync(m => m.PublicId == updateMissionDTO.PublicId);
            var image = mission.MissionImages.FirstOrDefault(mI => mI.MissionId == updateMissionDTO.Id).Image;

            var uploadResult = await _cloudinaryOperations.UpdateImageAsync(updateMissionDTO.ImageFile, mission.PublicId);

            mission.PublicId = uploadResult.PublicId;
            mission.Latitude = updateMissionDTO.Latitude;
            mission.Longitude = updateMissionDTO.Longitude;
            mission.Title = updateMissionDTO.Title;
            mission.Content = updateMissionDTO.Content;
            mission.CreatedDate = updateMissionDTO.CreatedDate;
            mission.Difficulty = updateMissionDTO.Difficulty;
            mission.OwnerUserId = updateMissionDTO.OwnerUserId;
            image.Url = uploadResult.Url.ToString();

            await _missionDal.UpdateMissionWithImageAsync(mission, image);

            return new SuccessfulDataResult<Mission>(mission, HttpStatusCode.Created);
        }

        public async Task<IDataResult<Mission>> UpdateMissionWithEvidencesAsync(UpdateMissionWithEvidencesDTO updateMissionDTO)
        {
            var mission = _mapper.Map<Mission>(updateMissionDTO);
            var evidences = _mapper.Map<ICollection<Evidence>>(updateMissionDTO.UpdateEvidenceDTOs);

            var addedNewMission = await _missionDal.UpdateAsync(mission);
            await _evidenceService.UpdateEvidencesAsync(evidences);

            addedNewMission.Evidences = evidences;

            return new SuccessfulDataResult<Mission>(addedNewMission, HttpStatusCode.Created);
        }

        public async Task<IDataResult<Mission>> GetMissionByIdWithEvidencesAsync(int id)
        {
            var mission = await _missionDal.GetWithEvidencesAsync(m => m.Id == id);
            return new SuccessfulDataResult<Mission>(mission, HttpStatusCode.OK);
        }

        private async Task AddMissionWithImageAsync(Mission mission, ImageUploadResult imageUploadResult)
        {
            mission.PublicId = imageUploadResult.PublicId;
            var missionImage = new MissionImage
            {
                Image = new Image
                {
                    Url = imageUploadResult.Url.ToString()
                },
                Mission = mission
            };
            await _missionDal.AddMissionWithImageAsync(missionImage);
        }
    }
}
