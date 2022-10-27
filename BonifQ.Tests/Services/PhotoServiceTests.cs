using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Services;
using BonifiQ.Domain.Settings.ErrorHandler.ErrorStatusCodes;
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
            Photo repositoryMock = PhotosRepositoryMock.GetUpdatePhotoObjectMock(); 

            _photosRepository.Setup(x => x.GetPhotoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repositoryMock); 
            PhotoResponse response = await _photoService.GetPhotoById(repositoryMock.Id);

            Assert.Equal(repositoryMock.Id, response.Id);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPhotoById_WhenPhotoIsNotUpdated_Success()
        {
            Photo repositoryMock = PhotosRepositoryMock.GetNotUpdatePhotoObjectMock(); 

            _photosRepository.Setup(x => x.GetPhotoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repositoryMock); 

            PhotoApiResponse httpRequestMock = PhotosRepositoryMock.GetPhotoResponseObjectMock();

            _httpRequest.Setup(x => x.GetPhotoById(It.IsAny<int>()))
                .ReturnsAsync(httpRequestMock); 

            PhotoResponse response = await _photoService.GetPhotoById(repositoryMock.Id);

            Assert.Equal(httpRequestMock.Id, response.Id);
            Assert.Equal(httpRequestMock.Title, response.Title);
            Assert.Equal(httpRequestMock.ThumbnailUrl, response.ThumbnailUrl);
            Assert.Equal(httpRequestMock.AlbumId, response.AlbumId);
            Assert.Equal(httpRequestMock.Url, response.Url);

            _photosRepository.Verify(x => x.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPhotoById_WhenPhotoNotFound_Error()
        {
            Task act() => _photoService.GetPhotoById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task GetPhotoById_WhenPhotoNotFoundAndApiReturnsOk_Success()
        {
            PhotoApiResponse httpRequestMock = PhotosRepositoryMock.GetPhotoResponseObjectMock();

            _httpRequest.Setup(x => x.GetPhotoById(It.IsAny<int>()))
                .ReturnsAsync(httpRequestMock);

            PhotoResponse response = await _photoService.GetPhotoById(It.IsAny<int>());

            Assert.Equal(httpRequestMock.Id, response.Id);
            Assert.Equal(httpRequestMock.Title, response.Title);
            Assert.Equal(httpRequestMock.ThumbnailUrl, response.ThumbnailUrl);
            Assert.Equal(httpRequestMock.AlbumId, response.AlbumId);
            Assert.Equal(httpRequestMock.Url, response.Url);

            _photosRepository.Verify(x => x.InsertPhotoAsync(It.IsAny<Photo>()), Times.Once);
            _photosRepository.Verify(x => x.CommitChangesAsync(), Times.Once);
        }
    }
}