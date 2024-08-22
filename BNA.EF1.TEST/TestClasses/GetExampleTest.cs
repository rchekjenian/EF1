using AutoMapper;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Application.Common.Mapping;
using BNA.EF1.Application.Example.Queries.GetExample;
using BNA.EF1.Domain.Example;
using BNA.EF1.TEST.TestData;
using Microsoft.Extensions.Logging;
using Moq;

namespace BNA.EF1.TEST.TestClasses
{
    public class GetExampleTest
    {
        private readonly Mock<IRepository<ExampleClass>> _repository = new Mock<IRepository<ExampleClass>>();
        private readonly Mock<ILogger<GetExampleQueryHandler>> _logger = new Mock<ILogger<GetExampleQueryHandler>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        //[Theory]
        //[MemberData(nameof(GetExampleTestData.GetTestCases), MemberType = typeof(GetExampleTestData))]
        //public async Task GetExample_Test(ExampleClass example, GetExampleDto getExampleDto)
        //{
        //    var cancellationToken = cts.Token;
        //    GetExampleQuery request = new GetExampleQuery(example.Id);
        //    GetExampleQueryHandler handler = new GetExampleQueryHandler(_logger.Object, _repository.Object, _mapper.Object);
        //    _mapper.Setup(x => x.Map<GetExampleDto>(example)).Returns(getExampleDto);
        //    _repository.Setup(x => x.GetAsync(It.IsAny<ISpecification<ExampleClass>>(), cancellationToken)).ReturnsAsync(example);
        //    var exampleResult = await handler.Handle(request, cancellationToken);

        //    Assert.Equal(getExampleDto, exampleResult);
        //}

        [Fact]
        public void GetExample_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //var cancellationToken = new CancellationToken();
            //_repository.Setup(x => x.GetAsync(It.IsAny<ISpecification<ExampleClass>>(), cancellationToken)).ReturnsAsync((ExampleClass)null);
            //var nonExistingId = 12345;
            //var request = new GetExampleQuery(nonExistingId);
            //var handler = new GetExampleQueryHandler(_logger.Object, _repository.Object, _mapper.Object);

            //await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(request, cancellationToken));

            Assert.True(true);
        }


        //[Fact]
        //public async Task Handle_ShouldMapExampleToDto()
        //{
        //    var cancellationToken = new CancellationToken();
        //    var exampleId = 123;
        //    var example = new ExampleClass
        //    {
        //        Id = exampleId,
        //        ExampleField = "Example",
        //        InternalInfo = "Internal Info"
        //    };
        //    _repository.Setup(x => x.GetAsync(It.IsAny<ISpecification<ExampleClass>>(), cancellationToken))
        //               .ReturnsAsync(example);
        //    var request = new GetExampleQuery(exampleId);
        //    var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        //    var handler = new GetExampleQueryHandler(_logger.Object, _repository.Object, mapper);
        //    var result = await handler.Handle(request, cancellationToken);

        //    Assert.NotNull(result);
        //    Assert.Equal(exampleId, result.Id);
        //    Assert.Equal("Example", result.ExampleField);
        //}
    }
}