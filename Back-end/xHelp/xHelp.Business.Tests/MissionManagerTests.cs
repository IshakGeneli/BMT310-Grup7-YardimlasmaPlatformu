using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Concrete;
using xHelp.Business.Utilities;
using xHelp.Business.Utilities.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Tests
{
    [TestClass]
    public class MissionManagerTest
    {
        private IMapper _mapper;
        private Mock<IMissionDal> _mockMissionDal;
        private Mock<IEvidenceService> _mockEvidenceService;
        private Mock<ICloudinaryOperations> _mockCloudinaryOperations;

        private List<Mission> _missions;
        private Mission _mission = new Mission { Id = 3, Content = "İçerik 3", Difficulty = 2, Title = "Hayvanlara yardım 3" };
        
        private CreateMissionDTO _createMissionDTO = new CreateMissionDTO { Content = "İçerik 6", Difficulty = 2, Title = "Hayvanlara yardım 6" };
        ImageUploadResult uploadResult = new ImageUploadResult 
        { 
            PublicId="asdwqe",
            Url= new System.Uri("http://farm4.static.flickr.com/2232/2232/someimage.jpg")
        };

        [TestInitialize]
        public void Start()
        {
            _mockMissionDal = new Mock<IMissionDal>();
            _mockEvidenceService = new Mock<IEvidenceService>();
            _mockCloudinaryOperations = new Mock<ICloudinaryOperations>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _missions = new List<Mission>
            {
                new Mission {Id=1,Content="İçerik 1",Difficulty=1,Title="Hayvanlara yardım 1"},
                new Mission {Id=2,Content="İçerik 2",Difficulty=0,Title="Hayvanlara yardım 2"},
                new Mission {Id=3,Content="İçerik 3",Difficulty=2,Title="Hayvanlara yardım 3"},
                new Mission {Id=4,Content="İçerik 4",Difficulty=1,Title="Hayvanlara yardım 4"},
                new Mission {Id=5,Content="İçerik 5",Difficulty=0,Title="Hayvanlara yardım 5"}
            };
            _mockMissionDal.Setup(m => m.GetListWithEvidencesAsync(null).Result).Returns(_missions);
            _mockMissionDal.Setup(m => m.GetWithEvidencesAsync(m=>m.Id==3).Result).Returns(_mission);
            _mockCloudinaryOperations.Setup(c => c.UploadImageAsync(null)).Returns(Task.FromResult(uploadResult));
            _mockMissionDal.Setup(m => m.AddMissionWithImageAsync(It.IsAny<MissionImage>())).Callback<MissionImage>(m => _missions.Add(m.Mission));
        }

        [TestMethod]
        public async Task GetAllWithEvidences()
        {
            // Arrange
            IMissionService _missionService = new MissionManager(_mockMissionDal.Object,_mapper,_mockEvidenceService.Object,_mockCloudinaryOperations.Object);
            // Act
            ICollection<Mission> missions = (await _missionService.GetAllWithEvidencesAsync()).Data;
            // Assert
            Assert.AreEqual(5, missions.Count);
        }

        [TestMethod]
        public async Task GetMissionByIdWithEvidences()
        {
            // Arrange
            IMissionService _missionService = new MissionManager(_mockMissionDal.Object, _mapper, _mockEvidenceService.Object, _mockCloudinaryOperations.Object);
            // Act
            var mission = (await _missionService.GetMissionByIdWithEvidencesAsync(3)).Data;
            // Assert
            Assert.IsTrue(_missions[2].Id==mission.Id);
        }
        
        [TestMethod]
        public async Task AddMission()
        {
            Mission _newMission = new Mission { Content = "İçerik 6", Difficulty = 2, Title = "Hayvanlara yardım 6"};

            // Arrange
            MissionManager _missionService = new MissionManager(_mockMissionDal.Object, _mapper, _mockEvidenceService.Object, _mockCloudinaryOperations.Object);
            // Act
            await _missionService.AddMissionAsync(_createMissionDTO);
            // Assert
            Assert.IsNotNull(_missions.Find(m => m.Content == _newMission.Content));
        }
    }
}
