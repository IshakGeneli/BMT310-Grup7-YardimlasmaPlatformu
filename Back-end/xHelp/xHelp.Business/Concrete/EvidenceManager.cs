using AutoMapper;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
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
    public class EvidenceManager : IEvidenceService
    {
        private readonly IEvidenceDal _evidenceDal;
        private readonly IMapper _mapper;
        private ICloudinaryOperations _cloudinaryOperations;

        public EvidenceManager(IEvidenceDal evidenceDal, IMapper mapper, ICloudinaryOperations cloudinaryOperations)
        {
            _evidenceDal = evidenceDal;
            _mapper = mapper;
            _cloudinaryOperations = cloudinaryOperations;
        }

        public async Task<IDataResult<Evidence>> AddEvidenceAsync(CreateEvidenceDTO createEvidenceDTO)
        {
            var uploadResult = await _cloudinaryOperations.UploadImageAsync(createEvidenceDTO.ImageFile);

            var evidence = _mapper.Map<Evidence>(createEvidenceDTO);
            await AddEvidenceWithImageAsync(evidence, uploadResult);

            return new SuccessfulDataResult<Evidence>(evidence, HttpStatusCode.OK);
        }

        public async Task AddEvidencesAsync(ICollection<Evidence> evidences)
        {
            await _evidenceDal.AddEvidencesAsync(evidences);
        }

        public async Task<IResult> DeleteEvidenceAsync(int id)
        {
            await _evidenceDal.DeleteAsync(new Evidence { Id = id });

            return new SuccessfulResult(HttpStatusCode.OK);
        }

        public async Task<ICollection<Evidence>> GetAllAsync()
        {
            return await _evidenceDal.GetListAsync();
        }

        public async Task<IDataResult<ICollection<Evidence>>> GetAllByMissionIdAsync(int missionId)
        {
            var evidences = await _evidenceDal.GetListAsync(e => e.MissionId == missionId);

            return new SuccessfulDataResult<ICollection<Evidence>>(evidences, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Evidence>> GetEvidenceByIdAsync(int id)
        {
            var evidence = await _evidenceDal.GetAsync(e => e.Id == id);

            return new SuccessfulDataResult<Evidence>(evidence, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Evidence>> UpdateEvidenceAsync(UpdateEvidenceDTO updateEvidenceDTO)
        {
            var evidence = _mapper.Map<Evidence>(updateEvidenceDTO);
            var newEvidence = await _evidenceDal.UpdateAsync(evidence);

            return new SuccessfulDataResult<Evidence>(evidence, HttpStatusCode.OK);
        }

        public async Task UpdateEvidencesAsync(ICollection<Evidence> evidences)
        {
            await _evidenceDal.UpdateEvidencesAsync(evidences);
        }

        private async Task AddEvidenceWithImageAsync(Evidence evidence, ImageUploadResult ımageUploadResult)
        {
            var evidenceImage = new EvidenceImage
            {
                Image = new Image
                {
                    Url = ımageUploadResult.Url.ToString()
                },
                Evidence = evidence
            };
            await _evidenceDal.AddEvidenceWithImageAsync(evidenceImage);
        }
    }
}
