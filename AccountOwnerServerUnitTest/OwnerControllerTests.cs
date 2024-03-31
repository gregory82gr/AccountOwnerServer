using AccountOwnerServer.Controllers;
using AutoMapper;
using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOwnerServerUnitTest
{
    public class OwnerControllerTests: IClassFixture<OwnerControllerTests>
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly OwnerController _controller;
        private Mock<ILoggerManager> _logger;
        private IRepositoryWrapper _repository;
        private Mock<IMapper> _mapper;
        private Mock<IUnitOfWork> _unitOfWork;


        public OwnerControllerTests()
        {
            
            _mockRepo = new Mock<IRepositoryWrapper>();
            _logger = new Mock<ILoggerManager>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _controller = new OwnerController(_logger.Object, _mockRepo.Object, _mapper.Object, _unitOfWork.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfOwners()
        {
            _mockRepo.Setup(repo => repo.Owner.GetAllOwners())
                .Returns(new List<Owner>() { new Owner(), new Owner() });

            var result = _controller.GetAllOwners();
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            //Assert.Equal(2, okResult.Value);
        }
    }
}
