using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Services;
using BonifQ.Tests.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BonifQ.Tests
{
    public class PhotoServiceTests
    {
        private readonly IPhotoService _photoService;
        private readonly Mock<IPhotosRepository> _photosRepository;  
        private readonly Mock<IImageRequests> _httpRequest;  

        public PhotoServiceTests()
        {
            _photosRepository = new Mock<IPhotosRepository>();
            _httpRequest = new Mock<IImageRequests>();
            _photoService = new PhotoService(_photosRepository.Object, _httpRequest.Object);
        }

        [Fact]
        public async Task GetPhotoById_WhenPhotoIsUpdated_Success()
        {
            Photo repositoryNock = PhotosRepositoryMock.GetUpdatePhotoObjectMock(); // criando objeto de mock

            _photosRepository.Setup(x => x.GetPhotoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repositoryNock); // configurando o repositório para retornar o nosso moc
            Photo response = await _photoService.GetPhotoById(repositoryNock.Id);   

            Assert.Equal(repositoryNock.Id, response.Id);           
            Assert.NotNull(response);           
        }

        [Fact]
        public async Task GetPhotoById_WhenPhotoIsNotUpdated_Success()
        {
            Photo repositoryNock = PhotosRepositoryMock.GetNotUpdatePhotoObjectMock(); // criando objeto de mock

            _photosRepository.Setup(x => x.GetPhotoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repositoryNock); // configurando o repositório para retornar o nosso mock

            PhotoResponse httpRequestMock = PhotosRepositoryMock.GetPhotoResponseObjectMock();

            _httpRequest.Setup(x => x.GetPhotoById(It.IsAny<int>()))
                .ReturnsAsync(httpRequestMock); // configurando o repositório para retornar o nosso mock

            Photo response = await _photoService.GetPhotoById(repositoryNock.Id);

            Assert.Equal(httpRequestMock.Id, response.Id);
            Assert.Equal(httpRequestMock.Title, response.Title);
            Assert.Equal(httpRequestMock.ThumbnailUrl, response.ThumbnailUrl);
            Assert.Equal(httpRequestMock.AlbumId, response.AlbumId);
            Assert.Equal(httpRequestMock.Url, response.Url);

            _photosRepository.Verify(x => x.CommitChangesAsync(), Times.Once);
        }
    }
}