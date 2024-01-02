using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Services;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services
{
    public class AuthorCreateServiceTests
    {
        private IMapper Mapper { get; }
        private AuthorCreateValidator Validator { get; }

        public AuthorCreateServiceTests()
        {
            Mapper = new MapperConfiguration(cfg =>
            cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();
            Validator = new AuthorCreateValidator();
        }

        [Fact]
        public async Task Author_Created()
        {
            //Arrange
            var authorCreate = new AuthorCreate("Test", "Test");
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var authorCreateSerice = new AuthorCreateService(authorRepositoryMock.Object,
                Mapper, Validator);

            //Act
            await authorCreateSerice.CreateAuthorAsync(authorCreate);

            //Assert
            authorRepositoryMock.Verify(authorRepositoryMock =>
            authorRepositoryMock.AddAuthorAsync(It.IsAny<Author>()), Times.Once);
        }
    }
}
