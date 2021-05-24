using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.Business.Concrete;
using xHelp.Business.Utilities;
using xHelp.Business.Utilities.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Tests
{
    [TestClass]
    public class EvidenceManagerTests
    {
        private Mock<IEvidenceDal> _mockEvidenceDal;
        private IMapper _mapper;
        private Mock<ICloudinaryOperations> _mockCloudinaryOperations;

        List<Evidence> _evidences;

        [TestInitialize]
        public void Start()
        {
            _mockEvidenceDal = new Mock<IEvidenceDal>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _mockCloudinaryOperations = new Mock<ICloudinaryOperations>();

            _evidences = new List<Evidence>
            {
                new Evidence {Id=1,Argument="argument1",MissionId=1,PublicId="ads"},
                new Evidence {Id=2,Argument="argument2",MissionId=2,PublicId="ads"},
                new Evidence {Id=3,Argument="argument3",MissionId=1,PublicId="ads"},
                new Evidence {Id=4,Argument="argument4",MissionId=3,PublicId="ads"},
                new Evidence {Id=5,Argument="argument5",MissionId=1,PublicId="ads"},
            };

            _mockEvidenceDal.Setup(e => e.GetListAsync(It.IsAny<Expression<Func<Evidence, bool>>>())).Returns<Expression<Func<Evidence, bool>>>((P)=>Task.FromResult(_evidences.Where(P.Compile()).ToList()));
            _mockEvidenceDal.Setup(e => e.GetEvidenceWithImageAsync(It.IsAny<Expression<Func<Evidence, bool>>>())).Returns<Expression<Func<Evidence, bool>>>((P) => Task.FromResult(_evidences.SingleOrDefault(P.Compile())));
        }

        [TestMethod]
        public async Task GetAllByMissionIdAsync()
        {
            // Arrange
            IEvidenceService _evidenceService = new EvidenceManager(_mockEvidenceDal.Object,_mapper,_mockCloudinaryOperations.Object);
            // Act
            ICollection<Evidence> evidences = (await _evidenceService.GetAllByMissionIdAsync(1)).Data;
            // Assert
            Assert.AreEqual(3, evidences.Count);
        }

        [TestMethod]
        public async Task GetEvidenceByIdAsync()
        {
            // Arrange
            IEvidenceService _evidenceService = new EvidenceManager(_mockEvidenceDal.Object, _mapper, _mockCloudinaryOperations.Object);
            // Act
            Evidence evidence = (await _evidenceService.GetEvidenceByIdAsync(1)).Data;
            // Assert
            Assert.IsTrue(_evidences.Contains(evidence));
        }
    }
}
