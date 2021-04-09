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
            var evidence = await _evidenceDal.GetEvidenceWithImageAsync(e => e.Id == id);

            return new SuccessfulDataResult<Evidence>(evidence, HttpStatusCode.OK);
        }

        public async Task<IDataResult<Evidence>> UpdateEvidenceAsync(UpdateEvidenceDTO updateEvidenceDTO)
        {
            var evidence = await _evidenceDal.GetEvidenceWithImageAsync(e => e.PublicId == updateEvidenceDTO.PublicId);
            var image = evidence.EvidenceImages.FirstOrDefault(eI => eI.EvidenceId == updateEvidenceDTO.Id).Image;

            var uploadResult = await _cloudinaryOperations.UpdateImageAsync(updateEvidenceDTO.ImageFile, evidence.PublicId);
            
            evidence.PublicId = uploadResult.PublicId;
            image.Url = uploadResult.Url.ToString();
            evidence.Argument = updateEvidenceDTO.Argument;

            await _evidenceDal.UpdateEvidenceWithImageAsync(evidence, image);

            return new SuccessfulDataResult<Evidence>(evidence, HttpStatusCode.OK);
        }

        public async Task UpdateEvidencesAsync(ICollection<Evidence> evidences)
        {
            await _evidenceDal.UpdateEvidencesAsync(evidences);
        }

        private async Task AddEvidenceWithImageAsync(Evidence evidence, ImageUploadResult imageUploadResult)
        {
            evidence.PublicId = imageUploadResult.PublicId;
            var evidenceImage = new EvidenceImage
            {
                Image = new Image
                {
                    Url = imageUploadResult.Url.ToString()
                },
                Evidence = evidence
            };
            await _evidenceDal.AddEvidenceWithImageAsync(evidenceImage);
        }
    }
}
